namespace lab02
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblGrup = new Label();
            nudGrup = new NumericUpDown();
            lblIter = new Label();
            nudIter = new NumericUpDown();
            lblMiara = new Label();
            cmbMiara = new ComboBox();
            lblMink = new Label();
            nudMink = new NumericUpDown();
            btnStart = new Button();
            btnWybierz = new Button();
            lblPlik = new Label();
            pictureBox1 = new PictureBox();
            txtInfo = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nudGrup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMink).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            //
            // lblGrup
            //
            lblGrup.AutoSize = true;
            lblGrup.Location = new Point(12, 15);
            lblGrup.Name = "lblGrup";
            lblGrup.Size = new Size(74, 15);
            lblGrup.Text = "Liczba grup:";
            //
            // nudGrup
            //
            nudGrup.Location = new Point(92, 12);
            nudGrup.Minimum = 1;
            nudGrup.Maximum = 20;
            nudGrup.Value = 4;
            nudGrup.Name = "nudGrup";
            nudGrup.Size = new Size(55, 23);
            //
            // lblIter
            //
            lblIter.AutoSize = true;
            lblIter.Location = new Point(165, 15);
            lblIter.Name = "lblIter";
            lblIter.Size = new Size(86, 15);
            lblIter.Text = "Liczba iteracji:";
            //
            // nudIter
            //
            nudIter.Location = new Point(255, 12);
            nudIter.Minimum = 1;
            nudIter.Maximum = 10000;
            nudIter.Value = 100;
            nudIter.Name = "nudIter";
            nudIter.Size = new Size(70, 23);
            //
            // lblMiara
            //
            lblMiara.AutoSize = true;
            lblMiara.Location = new Point(345, 15);
            lblMiara.Name = "lblMiara";
            lblMiara.Size = new Size(99, 15);
            lblMiara.Text = "Miara odległości:";
            //
            // cmbMiara
            //
            cmbMiara.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMiara.Location = new Point(450, 12);
            cmbMiara.Name = "cmbMiara";
            cmbMiara.Size = new Size(160, 23);
            cmbMiara.SelectedIndexChanged += cmbMiara_SelectedIndexChanged;
            //
            // lblMink
            //
            lblMink.AutoSize = true;
            lblMink.Location = new Point(625, 15);
            lblMink.Name = "lblMink";
            lblMink.Size = new Size(36, 15);
            lblMink.Text = "mink:";
            //
            // nudMink
            //
            nudMink.DecimalPlaces = 1;
            nudMink.Increment = 0.5M;
            nudMink.Location = new Point(665, 12);
            nudMink.Minimum = 0.1M;
            nudMink.Maximum = 20;
            nudMink.Value = 3;
            nudMink.Name = "nudMink";
            nudMink.Size = new Size(55, 23);
            //
            // btnStart
            //
            btnStart.Location = new Point(740, 10);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(120, 27);
            btnStart.Text = "Uruchom";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            //
            // btnWybierz
            //
            btnWybierz.Location = new Point(12, 45);
            btnWybierz.Name = "btnWybierz";
            btnWybierz.Size = new Size(120, 27);
            btnWybierz.Text = "Wybierz CSV...";
            btnWybierz.UseVisualStyleBackColor = true;
            btnWybierz.Click += btnWybierz_Click;
            //
            // lblPlik
            //
            lblPlik.AutoSize = true;
            lblPlik.ForeColor = Color.DimGray;
            lblPlik.Location = new Point(145, 50);
            lblPlik.Name = "lblPlik";
            lblPlik.Size = new Size(80, 15);
            lblPlik.Text = "brak pliku csv";
            //
            // pictureBox1
            //
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.White;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(12, 85);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(620, 445);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            //
            // txtInfo
            //
            txtInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            txtInfo.Font = new Font("Consolas", 9F);
            txtInfo.Location = new Point(645, 85);
            txtInfo.Multiline = true;
            txtInfo.Name = "txtInfo";
            txtInfo.ReadOnly = true;
            txtInfo.ScrollBars = ScrollBars.Vertical;
            txtInfo.Size = new Size(325, 445);
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 543);
            Controls.Add(txtInfo);
            Controls.Add(pictureBox1);
            Controls.Add(lblPlik);
            Controls.Add(btnWybierz);
            Controls.Add(btnStart);
            Controls.Add(nudMink);
            Controls.Add(lblMink);
            Controls.Add(cmbMiara);
            Controls.Add(lblMiara);
            Controls.Add(nudIter);
            Controls.Add(lblIter);
            Controls.Add(nudGrup);
            Controls.Add(lblGrup);
            MinimumSize = new Size(800, 450);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lab SSI - k-srednich";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)nudGrup).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIter).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMink).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGrup;
        private NumericUpDown nudGrup;
        private Label lblIter;
        private NumericUpDown nudIter;
        private Label lblMiara;
        private ComboBox cmbMiara;
        private Label lblMink;
        private NumericUpDown nudMink;
        private Button btnStart;
        private Button btnWybierz;
        private Label lblPlik;
        private PictureBox pictureBox1;
        private TextBox txtInfo;
    }
}
