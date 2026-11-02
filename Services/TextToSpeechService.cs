using System.Speech.Synthesis;

namespace SpeechApp.Services;

/// <summary>
/// SpeechSynthesizer nesnesini saran servis. Kullanıcı arayüzüne doğrudan erişmez.
/// </summary>
public sealed class TextToSpeechService : IDisposable
{
    public const int MinRate = -10;
    public const int MaxRate = 10;
    public const int MinVolume = 0;
    public const int MaxVolume = 100;

    private readonly SpeechSynthesizer _synthesizer = new();
    private bool _disposed;

    /// <summary>Bir konuşma tamamlandığında veya iptal edildiğinde tetiklenir.</summary>
    public event EventHandler? SpeakCompleted;

    /// <summary>Kurulu ve etkin seslerin adları.</summary>
    public IReadOnlyList<string> InstalledVoiceNames =>
        _synthesizer.GetInstalledVoices()
            .Where(v => v.Enabled)
            .Select(v => v.VoiceInfo.Name)
            .ToList();

    public TextToSpeechService()
    {
        _synthesizer.SpeakCompleted += OnSynthesizerSpeakCompleted;
    }

    /// <summary>Seçili ses. Kurulu olmayan bir ad verilirse ArgumentException fırlatılır.</summary>
    public string? VoiceName
    {
        get => _synthesizer.Voice?.Name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            if (!InstalledVoiceNames.Contains(value))
            {
                throw new ArgumentException($"Ses bulunamadı: {value}", nameof(value));
            }

            _synthesizer.SelectVoice(value);
        }
    }

    /// <summary>Konuşma hızı (-10 ile 10 arası).</summary>
    public int Rate
    {
        get => _synthesizer.Rate;
        set => _synthesizer.Rate = Math.Clamp(value, MinRate, MaxRate);
    }

    /// <summary>Ses düzeyi (0 ile 100 arası).</summary>
    public int Volume
    {
        get => _synthesizer.Volume;
        set => _synthesizer.Volume = Math.Clamp(value, MinVolume, MaxVolume);
    }

    /// <summary>Verilen kültüre ait ilk sesin adını döndürür. Bulunamazsa null döner.</summary>
    public string? FindVoiceNameForLanguage(string twoLetterLanguageCode)
    {
        return _synthesizer.GetInstalledVoices()
            .Select(v => v.VoiceInfo)
            .FirstOrDefault(v => v.Culture.TwoLetterISOLanguageName
                .Equals(twoLetterLanguageCode, StringComparison.OrdinalIgnoreCase))
            ?.Name;
    }

    /// <summary>Metni eşzamansız olarak seslendirir.</summary>
    public void SpeakAsync(string text)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        _synthesizer.SpeakAsync(text);
    }

    /// <summary>Devam eden ve sıradaki tüm konuşmaları durdurur.</summary>
    public void Stop()
    {
        if (!_disposed)
        {
            _synthesizer.SpeakAsyncCancelAll();
        }
    }

    private void OnSynthesizerSpeakCompleted(object? sender, SpeakCompletedEventArgs e)
    {
        SpeakCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _synthesizer.SpeakCompleted -= OnSynthesizerSpeakCompleted;
        _synthesizer.SpeakAsyncCancelAll();
        _synthesizer.Dispose();
    }
}
