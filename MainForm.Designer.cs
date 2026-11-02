#nullable disable

namespace SpeechApp;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.GroupBox grpStt;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Label lblSttStatus;
    private System.Windows.Forms.RichTextBox rtbTranscript;

    private System.Windows.Forms.GroupBox grpTts;
    private System.Windows.Forms.TextBox txtInput;
    private System.Windows.Forms.Label lblVoice;
    private System.Windows.Forms.ComboBox cmbVoice;
    private System.Windows.Forms.Label lblRate;
    private System.Windows.Forms.TrackBar tbRate;
    private System.Windows.Forms.Label lblRateValue;
    private System.Windows.Forms.Label lblVolume;
    private System.Windows.Forms.TrackBar tbVolume;
    private System.Windows.Forms.Label lblVolumeValue;
    private System.Windows.Forms.Button btnSpeak;
    private System.Windows.Forms.Button btnSpeakStop;
    private System.Windows.Forms.Label lblTtsStatus;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        grpStt = new System.Windows.Forms.GroupBox();
        btnStart = new System.Windows.Forms.Button();
        btnStop = new System.Windows.Forms.Button();
        lblSttStatus = new System.Windows.Forms.Label();
        rtbTranscript = new System.Windows.Forms.RichTextBox();
        grpTts = new System.Windows.Forms.GroupBox();
        txtInput = new System.Windows.Forms.TextBox();
        lblVoice = new System.Windows.Forms.Label();
        cmbVoice = new System.Windows.Forms.ComboBox();
        lblRate = new System.Windows.Forms.Label();
        tbRate = new System.Windows.Forms.TrackBar();
        lblRateValue = new System.Windows.Forms.Label();
        lblVolume = new System.Windows.Forms.Label();
        tbVolume = new System.Windows.Forms.TrackBar();
        lblVolumeValue = new System.Windows.Forms.Label();
        btnSpeak = new System.Windows.Forms.Button();
        btnSpeakStop = new System.Windows.Forms.Button();
        lblTtsStatus = new System.Windows.Forms.Label();
        grpStt.SuspendLayout();
        grpTts.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)tbRate).BeginInit();
        ((System.ComponentModel.ISupportInitialize)tbVolume).BeginInit();
        SuspendLayout();
        //
        // grpStt
        //
        grpStt.Controls.Add(btnStart);
        grpStt.Controls.Add(btnStop);
        grpStt.Controls.Add(lblSttStatus);
        grpStt.Controls.Add(rtbTranscript);
        grpStt.Location = new System.Drawing.Point(12, 12);
        grpStt.Name = "grpStt";
        grpStt.Size = new System.Drawing.Size(860, 300);
        grpStt.TabIndex = 0;
        grpStt.TabStop = false;
        grpStt.Text = "Konuşmayı Metne Çevirme";
        //
        // btnStart
        //
        btnStart.Location = new System.Drawing.Point(16, 28);
        btnStart.Name = "btnStart";
        btnStart.Size = new System.Drawing.Size(160, 36);
        btnStart.TabIndex = 0;
        btnStart.Text = "Dinlemeyi Başlat";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        //
        // btnStop
        //
        btnStop.Enabled = false;
        btnStop.Location = new System.Drawing.Point(184, 28);
        btnStop.Name = "btnStop";
        btnStop.Size = new System.Drawing.Size(160, 36);
        btnStop.TabIndex = 1;
        btnStop.Text = "Dinlemeyi Durdur";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += btnStop_Click;
        //
        // lblSttStatus
        //
        lblSttStatus.AutoSize = true;
        lblSttStatus.Location = new System.Drawing.Point(360, 38);
        lblSttStatus.Name = "lblSttStatus";
        lblSttStatus.Size = new System.Drawing.Size(40, 15);
        lblSttStatus.TabIndex = 2;
        lblSttStatus.Text = "Hazır";
        //
        // rtbTranscript
        //
        rtbTranscript.Location = new System.Drawing.Point(16, 76);
        rtbTranscript.Name = "rtbTranscript";
        rtbTranscript.ReadOnly = true;
        rtbTranscript.Size = new System.Drawing.Size(828, 208);
        rtbTranscript.TabIndex = 3;
        rtbTranscript.Text = "";
        //
        // grpTts
        //
        grpTts.Controls.Add(txtInput);
        grpTts.Controls.Add(lblVoice);
        grpTts.Controls.Add(cmbVoice);
        grpTts.Controls.Add(lblRate);
        grpTts.Controls.Add(tbRate);
        grpTts.Controls.Add(lblRateValue);
        grpTts.Controls.Add(lblVolume);
        grpTts.Controls.Add(tbVolume);
        grpTts.Controls.Add(lblVolumeValue);
        grpTts.Controls.Add(btnSpeak);
        grpTts.Controls.Add(btnSpeakStop);
        grpTts.Controls.Add(lblTtsStatus);
        grpTts.Location = new System.Drawing.Point(12, 324);
        grpTts.Name = "grpTts";
        grpTts.Size = new System.Drawing.Size(860, 300);
        grpTts.TabIndex = 1;
        grpTts.TabStop = false;
        grpTts.Text = "Metni Seslendirme";
        //
        // txtInput
        //
        txtInput.Location = new System.Drawing.Point(16, 28);
        txtInput.Multiline = true;
        txtInput.Name = "txtInput";
        txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        txtInput.Size = new System.Drawing.Size(828, 110);
        txtInput.TabIndex = 0;
        //
        // lblVoice
        //
        lblVoice.AutoSize = true;
        lblVoice.Location = new System.Drawing.Point(16, 162);
        lblVoice.Name = "lblVoice";
        lblVoice.Size = new System.Drawing.Size(28, 15);
        lblVoice.TabIndex = 1;
        lblVoice.Text = "Ses:";
        //
        // cmbVoice
        //
        cmbVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbVoice.Location = new System.Drawing.Point(60, 158);
        cmbVoice.Name = "cmbVoice";
        cmbVoice.Size = new System.Drawing.Size(300, 23);
        cmbVoice.TabIndex = 2;
        cmbVoice.SelectedIndexChanged += cmbVoice_SelectedIndexChanged;
        //
        // lblRate
        //
        lblRate.AutoSize = true;
        lblRate.Location = new System.Drawing.Point(380, 162);
        lblRate.Name = "lblRate";
        lblRate.Size = new System.Drawing.Size(34, 15);
        lblRate.TabIndex = 3;
        lblRate.Text = "Hız:";
        //
        // tbRate
        //
        tbRate.LargeChange = 2;
        tbRate.Location = new System.Drawing.Point(420, 152);
        tbRate.Maximum = 10;
        tbRate.Minimum = -10;
        tbRate.Name = "tbRate";
        tbRate.Size = new System.Drawing.Size(220, 45);
        tbRate.TabIndex = 4;
        tbRate.TickFrequency = 2;
        tbRate.ValueChanged += tbRate_ValueChanged;
        //
        // lblRateValue
        //
        lblRateValue.AutoSize = true;
        lblRateValue.Location = new System.Drawing.Point(650, 162);
        lblRateValue.Name = "lblRateValue";
        lblRateValue.Size = new System.Drawing.Size(14, 15);
        lblRateValue.TabIndex = 5;
        lblRateValue.Text = "0";
        //
        // lblVolume
        //
        lblVolume.AutoSize = true;
        lblVolume.Location = new System.Drawing.Point(16, 210);
        lblVolume.Name = "lblVolume";
        lblVolume.Size = new System.Drawing.Size(74, 15);
        lblVolume.TabIndex = 6;
        lblVolume.Text = "Ses Düzeyi:";
        //
        // tbVolume
        //
        tbVolume.LargeChange = 10;
        tbVolume.Location = new System.Drawing.Point(100, 202);
        tbVolume.Maximum = 100;
        tbVolume.Minimum = 0;
        tbVolume.Name = "tbVolume";
        tbVolume.Size = new System.Drawing.Size(260, 45);
        tbVolume.TabIndex = 7;
        tbVolume.TickFrequency = 10;
        tbVolume.Value = 100;
        tbVolume.ValueChanged += tbVolume_ValueChanged;
        //
        // lblVolumeValue
        //
        lblVolumeValue.AutoSize = true;
        lblVolumeValue.Location = new System.Drawing.Point(370, 210);
        lblVolumeValue.Name = "lblVolumeValue";
        lblVolumeValue.Size = new System.Drawing.Size(28, 15);
        lblVolumeValue.TabIndex = 8;
        lblVolumeValue.Text = "100";
        //
        // btnSpeak
        //
        btnSpeak.Location = new System.Drawing.Point(16, 250);
        btnSpeak.Name = "btnSpeak";
        btnSpeak.Size = new System.Drawing.Size(140, 36);
        btnSpeak.TabIndex = 9;
        btnSpeak.Text = "Seslendir";
        btnSpeak.UseVisualStyleBackColor = true;
        btnSpeak.Click += btnSpeak_Click;
        //
        // btnSpeakStop
        //
        btnSpeakStop.Location = new System.Drawing.Point(164, 250);
        btnSpeakStop.Name = "btnSpeakStop";
        btnSpeakStop.Size = new System.Drawing.Size(140, 36);
        btnSpeakStop.TabIndex = 10;
        btnSpeakStop.Text = "Durdur";
        btnSpeakStop.UseVisualStyleBackColor = true;
        btnSpeakStop.Click += btnSpeakStop_Click;
        //
        // lblTtsStatus
        //
        lblTtsStatus.AutoSize = true;
        lblTtsStatus.Location = new System.Drawing.Point(320, 262);
        lblTtsStatus.Name = "lblTtsStatus";
        lblTtsStatus.Size = new System.Drawing.Size(40, 15);
        lblTtsStatus.TabIndex = 11;
        lblTtsStatus.Text = "Hazır";
        //
        // MainForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(884, 641);
        Controls.Add(grpStt);
        Controls.Add(grpTts);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Konuşma Tanıma ve Seslendirme";
        grpStt.ResumeLayout(false);
        grpStt.PerformLayout();
        grpTts.ResumeLayout(false);
        grpTts.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)tbRate).EndInit();
        ((System.ComponentModel.ISupportInitialize)tbVolume).EndInit();
        ResumeLayout(false);
    }
}
