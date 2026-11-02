using SpeechApp.Services;

namespace SpeechApp;

/// <summary>
/// Ana pencere. Yalnızca arayüz bağlantılarını yönetir; konuşma işlemleri servislere devredilir.
/// </summary>
public partial class MainForm : Form
{
    private const string TurkishLanguageCode = "tr";

    private readonly SpeechToTextService _speechToText;
    private readonly TextToSpeechService _textToSpeech;

    public MainForm()
    {
        _speechToText = new SpeechToTextService();
        _textToSpeech = new TextToSpeechService();

        InitializeComponent();

        _speechToText.TextRecognized += OnTextRecognized;
        _speechToText.RecognitionRejected += OnRecognitionRejected;
        _speechToText.ListeningStopped += OnListeningStopped;
        _textToSpeech.SpeakCompleted += OnSpeakCompleted;

        LoadVoices();
    }

    /// <summary>Kurulu sesleri ComboBox'a doldurur; Türkçe ses varsa onu seçer.</summary>
    private void LoadVoices()
    {
        cmbVoice.Items.Clear();
        foreach (string name in _textToSpeech.InstalledVoiceNames)
        {
            cmbVoice.Items.Add(name);
        }

        if (cmbVoice.Items.Count == 0)
        {
            btnSpeak.Enabled = false;
            lblTtsStatus.Text = "Kurulu ses bulunamadı";
            return;
        }

        string? turkishVoice = _textToSpeech.FindVoiceNameForLanguage(TurkishLanguageCode);
        int index = turkishVoice is null ? -1 : cmbVoice.Items.IndexOf(turkishVoice);
        cmbVoice.SelectedIndex = index >= 0 ? index : 0;
    }

    /// <summary>
    /// Olaylar arka plan iş parçacıklarından gelebilir; arayüz işlemlerini UI iş parçacığına taşır.
    /// </summary>
    private void RunOnUiThread(Action action)
    {
        if (IsDisposed || !IsHandleCreated)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(action);
        }
        else
        {
            action();
        }
    }

    private void OnTextRecognized(object? sender, TextRecognizedEventArgs e)
    {
        RunOnUiThread(() =>
        {
            rtbTranscript.AppendText($"[{DateTime.Now:HH:mm:ss}] {e.Text}{Environment.NewLine}");
            rtbTranscript.ScrollToCaret();
            lblSttStatus.Text = "Metin alındı";
        });
    }

    private void OnRecognitionRejected(object? sender, EventArgs e)
    {
        RunOnUiThread(() => lblSttStatus.Text = "Konuşma anlaşılamadı");
    }

    private void OnListeningStopped(object? sender, Exception? error)
    {
        RunOnUiThread(() =>
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblSttStatus.Text = error is null ? "Hazır" : $"Hata: {error.Message}";
        });
    }

    private void OnSpeakCompleted(object? sender, EventArgs e)
    {
        RunOnUiThread(() => lblTtsStatus.Text = "Hazır");
    }

    private void btnStart_Click(object? sender, EventArgs e)
    {
        try
        {
            _speechToText.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Dinleme başlatılamadı: {ex.Message}", "Hata",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        btnStart.Enabled = false;
        btnStop.Enabled = true;
        lblSttStatus.Text = $"Dinleniyor ({_speechToText.RecognizerCultureName ?? "varsayılan"})";
    }

    private void btnStop_Click(object? sender, EventArgs e)
    {
        btnStop.Enabled = false;
        lblSttStatus.Text = "Durduruluyor...";
        _speechToText.Stop();
    }

    private void btnSpeak_Click(object? sender, EventArgs e)
    {
        string text = txtInput.Text.Trim();
        if (text.Length == 0)
        {
            lblTtsStatus.Text = "Lütfen metin girin";
            return;
        }

        _textToSpeech.SpeakAsync(text);
        lblTtsStatus.Text = "Seslendiriliyor...";
    }

    private void btnSpeakStop_Click(object? sender, EventArgs e)
    {
        _textToSpeech.Stop();
        lblTtsStatus.Text = "Durduruldu";
    }

    private void cmbVoice_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbVoice.SelectedItem is string name)
        {
            _textToSpeech.VoiceName = name;
        }
    }

    private void tbRate_ValueChanged(object? sender, EventArgs e)
    {
        _textToSpeech.Rate = tbRate.Value;
        lblRateValue.Text = tbRate.Value.ToString();
    }

    private void tbVolume_ValueChanged(object? sender, EventArgs e)
    {
        _textToSpeech.Volume = tbVolume.Value;
        lblVolumeValue.Text = tbVolume.Value.ToString();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _speechToText.Dispose();
        _textToSpeech.Dispose();
        base.OnFormClosed(e);
    }
}
