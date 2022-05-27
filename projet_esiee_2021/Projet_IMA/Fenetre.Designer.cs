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
            this.renderButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelRayon = new System.Windows.Forms.Label();
            this.threadComboBox = new System.Windows.Forms.ComboBox();
            this.labelThread = new System.Windows.Forms.Label();
            this.renderComboBox = new System.Windows.Forms.ComboBox();
            this.labelRender = new System.Windows.Forms.Label();
            this.optionsTextBox = new System.Windows.Forms.TextBox();
            Fenetre.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // renderButton
            // 
            this.renderButton.Location = new System.Drawing.Point(31, 28);
            this.renderButton.Margin = new System.Windows.Forms.Padding(4);
            this.renderButton.Name = "renderButton";
            this.renderButton.Size = new System.Drawing.Size(239, 68);
            this.renderButton.TabIndex = 0;
            this.renderButton.Text = "Lancer le rendu";
            this.renderButton.UseVisualStyleBackColor = true;
            this.renderButton.Click += new System.EventHandler(this.renderButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(31, 127);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1275, 700);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // labelRayon
            // 
            this.labelRayon.AutoSize = true;
            this.labelRayon.Location = new System.Drawing.Point(580, 47);
            this.labelRayon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRayon.Name = "labelRayon";
            this.labelRayon.Size = new System.Drawing.Size(54, 16);
            this.labelRayon.TabIndex = 8;
            this.labelRayon.Text = "Rayons";
            this.labelRayon.Click += new System.EventHandler(this.label1_Click);
            // 
            // threadComboBox
            // 
            this.threadComboBox.FormattingEnabled = true;
            this.threadComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11"});
            this.threadComboBox.Location = new System.Drawing.Point(463, 70);
            this.threadComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.threadComboBox.Name = "threadComboBox";
            this.threadComboBox.Size = new System.Drawing.Size(67, 24);
            this.threadComboBox.TabIndex = 9;
            this.threadComboBox.Text = "1";
            this.threadComboBox.SelectedIndexChanged += new System.EventHandler(this.threadComboBox_SelectedIndexChanged);
            // 
            // labelThread
            // 
            this.labelThread.AutoSize = true;
            this.labelThread.Location = new System.Drawing.Point(459, 47);
            this.labelThread.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelThread.Name = "labelThread";
            this.labelThread.Size = new System.Drawing.Size(58, 16);
            this.labelThread.TabIndex = 10;
            this.labelThread.Text = "Threads";
            // 
            // renderComboBox
            // 
            this.renderComboBox.FormattingEnabled = true;
            this.renderComboBox.Items.AddRange(new object[] {
            "SIMPLE",
            "PATH_TRACING",
            "VPL",
            "RAY_TRACING"});
            this.renderComboBox.Location = new System.Drawing.Point(280, 69);
            this.renderComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.renderComboBox.Name = "renderComboBox";
            this.renderComboBox.Size = new System.Drawing.Size(152, 24);
            this.renderComboBox.TabIndex = 11;
            this.renderComboBox.Text = "PATH_TRACING";
            this.renderComboBox.SelectedIndexChanged += new System.EventHandler(this.renderComboBox_SelectedIndexChanged);
            // 
            // labelRender
            // 
            this.labelRender.AutoSize = true;
            this.labelRender.Location = new System.Drawing.Point(277, 47);
            this.labelRender.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRender.Name = "labelRender";
            this.labelRender.Size = new System.Drawing.Size(90, 16);
            this.labelRender.TabIndex = 12;
            this.labelRender.Text = "Render Mode";
            this.labelRender.Click += new System.EventHandler(this.label3_Click);
            // 
            // optionsTextBox
            // 
            this.optionsTextBox.Location = new System.Drawing.Point(584, 71);
            this.optionsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.optionsTextBox.Name = "optionsTextBox";
            this.optionsTextBox.Size = new System.Drawing.Size(56, 22);
            this.optionsTextBox.TabIndex = 13;
            this.optionsTextBox.TextChanged += new System.EventHandler(this.optionsTextBox_TextChanged);
            // 
            // progressBar
            // 
            Fenetre.progressBar.Location = new System.Drawing.Point(743, 72);
            Fenetre.progressBar.Name = "progressBar";
            Fenetre.progressBar.Size = new System.Drawing.Size(563, 23);
            Fenetre.progressBar.TabIndex = 14;
            Fenetre.progressBar.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // Fenetre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1323, 842);
            this.Controls.Add(Fenetre.progressBar);
            this.Controls.Add(this.optionsTextBox);
            this.Controls.Add(this.labelRender);
            this.Controls.Add(this.renderComboBox);
            this.Controls.Add(this.labelThread);
            this.Controls.Add(this.threadComboBox);
            this.Controls.Add(this.labelRayon);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.renderButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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

        private System.Windows.Forms.Button renderButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelRayon;
        private System.Windows.Forms.ComboBox threadComboBox;
        private System.Windows.Forms.Label labelThread;
        private System.Windows.Forms.ComboBox renderComboBox;
        private System.Windows.Forms.Label labelRender;
        private System.Windows.Forms.TextBox optionsTextBox;
        public static System.Windows.Forms.ProgressBar progressBar;
    }
}

