namespace Projet_IMA
{
    partial class Fenetre
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fenetre));
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.showCheckBox = new System.Windows.Forms.CheckBox();
            this.dark_mode_button = new System.Windows.Forms.RadioButton();
            this.white_mode_button = new System.Windows.Forms.RadioButton();
            this.labelRayon = new System.Windows.Forms.Label();
            this.comboBoxThread = new System.Windows.Forms.ComboBox();
            this.labelThread = new System.Windows.Forms.Label();
            this.comboBoxRender = new System.Windows.Forms.ComboBox();
            this.labelRender = new System.Windows.Forms.Label();
            this.textBoxRayon = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Lancer le rendu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(23, 103);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(957, 569);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // showCheckBox
            // 
            this.showCheckBox.AutoSize = true;
            this.showCheckBox.Location = new System.Drawing.Point(782, 61);
            this.showCheckBox.Name = "showCheckBox";
            this.showCheckBox.Size = new System.Drawing.Size(53, 17);
            this.showCheckBox.TabIndex = 4;
            this.showCheckBox.Text = "Show";
            this.showCheckBox.UseVisualStyleBackColor = true;
            this.showCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dark_mode_button
            // 
            this.dark_mode_button.AutoSize = true;
            this.dark_mode_button.Checked = true;
            this.dark_mode_button.Location = new System.Drawing.Point(587, 61);
            this.dark_mode_button.Margin = new System.Windows.Forms.Padding(2);
            this.dark_mode_button.Name = "dark_mode_button";
            this.dark_mode_button.Size = new System.Drawing.Size(78, 17);
            this.dark_mode_button.TabIndex = 5;
            this.dark_mode_button.TabStop = true;
            this.dark_mode_button.Text = "Dark Mode";
            this.dark_mode_button.UseVisualStyleBackColor = true;
            this.dark_mode_button.CheckedChanged += new System.EventHandler(this.dark_mode_button_CheckedChanged);
            // 
            // white_mode_button
            // 
            this.white_mode_button.AutoSize = true;
            this.white_mode_button.Location = new System.Drawing.Point(673, 61);
            this.white_mode_button.Margin = new System.Windows.Forms.Padding(2);
            this.white_mode_button.Name = "white_mode_button";
            this.white_mode_button.Size = new System.Drawing.Size(83, 17);
            this.white_mode_button.TabIndex = 6;
            this.white_mode_button.Text = "White Mode";
            this.white_mode_button.UseVisualStyleBackColor = true;
            this.white_mode_button.CheckedChanged += new System.EventHandler(this.white_mode_button_CheckedChanged);
            // 
            // labelRayon
            // 
            this.labelRayon.AutoSize = true;
            this.labelRayon.Location = new System.Drawing.Point(435, 38);
            this.labelRayon.Name = "labelRayon";
            this.labelRayon.Size = new System.Drawing.Size(43, 13);
            this.labelRayon.TabIndex = 8;
            this.labelRayon.Text = "Rayons";
            this.labelRayon.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBoxThread
            // 
            this.comboBoxThread.FormattingEnabled = true;
            this.comboBoxThread.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBoxThread.Location = new System.Drawing.Point(347, 57);
            this.comboBoxThread.Name = "comboBoxThread";
            this.comboBoxThread.Size = new System.Drawing.Size(51, 21);
            this.comboBoxThread.TabIndex = 9;
            this.comboBoxThread.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // labelThread
            // 
            this.labelThread.AutoSize = true;
            this.labelThread.Location = new System.Drawing.Point(344, 38);
            this.labelThread.Name = "labelThread";
            this.labelThread.Size = new System.Drawing.Size(46, 13);
            this.labelThread.TabIndex = 10;
            this.labelThread.Text = "Threads";
            // 
            // comboBoxRender
            // 
            this.comboBoxRender.FormattingEnabled = true;
            this.comboBoxRender.Items.AddRange(new object[] {
            "SIMPLE",
            "PATH_TRACING",
            "VPL",
            "RAY_TRACING"});
            this.comboBoxRender.Location = new System.Drawing.Point(210, 56);
            this.comboBoxRender.Name = "comboBoxRender";
            this.comboBoxRender.Size = new System.Drawing.Size(102, 21);
            this.comboBoxRender.TabIndex = 11;
            this.comboBoxRender.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // labelRender
            // 
            this.labelRender.AutoSize = true;
            this.labelRender.Location = new System.Drawing.Point(208, 38);
            this.labelRender.Name = "labelRender";
            this.labelRender.Size = new System.Drawing.Size(72, 13);
            this.labelRender.TabIndex = 12;
            this.labelRender.Text = "Render Mode";
            this.labelRender.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBoxRayon
            // 
            this.textBoxRayon.Location = new System.Drawing.Point(438, 58);
            this.textBoxRayon.Name = "textBoxRayon";
            this.textBoxRayon.Size = new System.Drawing.Size(43, 20);
            this.textBoxRayon.TabIndex = 13;
            this.textBoxRayon.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Fenetre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(992, 684);
            this.Controls.Add(this.textBoxRayon);
            this.Controls.Add(this.labelRender);
            this.Controls.Add(this.comboBoxRender);
            this.Controls.Add(this.labelThread);
            this.Controls.Add(this.comboBoxThread);
            this.Controls.Add(this.labelRayon);
            this.Controls.Add(this.white_mode_button);
            this.Controls.Add(this.dark_mode_button);
            this.Controls.Add(this.showCheckBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fenetre";
            this.Text = "Projet 3D Réaliste - ESIEE Paris (Richard FOUQUOIRE, Sami OURABAH, Mathieu TAILLA" +
    "NDIER, Cathy TRUONG)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox showCheckBox;
        private System.Windows.Forms.RadioButton dark_mode_button;
        private System.Windows.Forms.RadioButton white_mode_button;
        private System.Windows.Forms.Label labelRayon;
        private System.Windows.Forms.ComboBox comboBoxThread;
        private System.Windows.Forms.Label labelThread;
        private System.Windows.Forms.ComboBox comboBoxRender;
        private System.Windows.Forms.Label labelRender;
        private System.Windows.Forms.TextBox textBoxRayon;
    }
}

