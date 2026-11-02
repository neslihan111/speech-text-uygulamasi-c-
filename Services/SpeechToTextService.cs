using System.Globalization;
using System.Speech.Recognition;

namespace SpeechApp.Services;

/// <summary>
/// Konuşma tanıma sonucu için olay argümanları.
/// </summary>
public sealed class TextRecognizedEventArgs : EventArgs
{
    public TextRecognizedEventArgs(string text, float confidence)
    {
        Text = text;
        Confidence = confidence;
    }

    /// <summary>Tanınan metin.</summary>
    public string Text { get; }

    /// <summary>Tanıma güveni (0 ile 1 arası).</summary>
    public float Confidence { get; }
}

/// <summary>
/// SpeechRecognitionEngine nesnesini saran servis. Mikrofondan serbest konuşma (dictation) alır.
/// Kullanıcı arayüzüne doğrudan erişmez; olayları kendi iş parçacığında tetikler.
/// </summary>
public sealed class SpeechToTextService : IDisposable
{
    private const string TurkishCultureName = "tr-TR";

    private SpeechRecognitionEngine? _engine;
    private bool _disposed;

    /// <summary>Metin tanındığında tetiklenir.</summary>
    public event EventHandler<TextRecognizedEventArgs>? TextRecognized;

    /// <summary>Konuşma anlaşılamadığında tetiklenir.</summary>
    public event EventHandler? RecognitionRejected;

    /// <summary>Dinleme durduğunda tetiklenir. Hata varsa exception bilgisi taşınır.</summary>
    public event EventHandler<Exception?>? ListeningStopped;

    /// <summary>Dinleme devam ediyor mu?</summary>
    public bool IsListening { get; private set; }

    /// <summary>Kullanılan tanıyıcının kültür adı (örn. tr-TR).</summary>
    public string? RecognizerCultureName { get; private set; }

    /// <summary>
    /// Dinlemeyi başlatır. Tanıyıcı yoksa veya mikrofon kullanılamazsa InvalidOperationException fırlatır.
    /// </summary>
    public void Start()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (IsListening)
        {
            return;
        }

        _engine ??= CreateEngine();
        _engine.RecognizeAsync(RecognizeMode.Multiple);
        IsListening = true;
    }

    /// <summary>Dinlemeyi nazikçe durdurur. Devam eden tanıma tamamlanır.</summary>
    public void Stop()
    {
        if (!IsListening || _engine is null)
        {
            return;
        }

        _engine.RecognizeAsyncStop();
    }

    private SpeechRecognitionEngine CreateEngine()
    {
        RecognizerInfo? info = FindRecognizer(TurkishCultureName)
            ?? FindRecognizer(CultureInfo.CurrentCulture.Name);

        SpeechRecognitionEngine engine = info is null
            ? new SpeechRecognitionEngine()
            : new SpeechRecognitionEngine(info);

        RecognizerCultureName = info?.Culture.Name ?? engine.RecognizerInfo?.Culture.Name;

        // Serbest konuşma gramerini ve varsayılan mikrofonu motor oluşturulurken bir kez tanımla.
        engine.LoadGrammar(new DictationGrammar());
        engine.SetInputToDefaultAudioDevice();

        engine.SpeechRecognized += OnSpeechRecognized;
        engine.SpeechRecognitionRejected += OnSpeechRecognitionRejected;
        engine.RecognizeCompleted += OnRecognizeCompleted;
        return engine;
    }

    private static RecognizerInfo? FindRecognizer(string cultureName)
    {
        return SpeechRecognitionEngine.InstalledRecognizers()
            .FirstOrDefault(r => string.Equals(r.Culture.Name, cultureName, StringComparison.OrdinalIgnoreCase));
    }

    private void OnSpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
    {
        RecognitionResult? result = e.Result;
        if (result is null || string.IsNullOrWhiteSpace(result.Text))
        {
            return;
        }

        TextRecognized?.Invoke(this, new TextRecognizedEventArgs(result.Text, result.Confidence));
    }

    private void OnSpeechRecognitionRejected(object? sender, SpeechRecognitionRejectedEventArgs e)
    {
        RecognitionRejected?.Invoke(this, EventArgs.Empty);
    }

    private void OnRecognizeCompleted(object? sender, RecognizeCompletedEventArgs e)
    {
        IsListening = false;
        ListeningStopped?.Invoke(this, e.Error);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        if (_engine is not null)
        {
            _engine.SpeechRecognized -= OnSpeechRecognized;
            _engine.SpeechRecognitionRejected -= OnSpeechRecognitionRejected;
            _engine.RecognizeCompleted -= OnRecognizeCompleted;

            if (IsListening)
            {
                _engine.RecognizeAsyncCancel();
            }

            _engine.Dispose();
            _engine = null;
        }

        IsListening = false;
    }
}
