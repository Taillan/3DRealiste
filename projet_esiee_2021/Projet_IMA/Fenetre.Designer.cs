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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(239, 68);
            this.button1.TabIndex = 0;
            this.button1.Text = "Projet Eleves";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // showCheckBox
            // 
            this.showCheckBox.AutoSize = true;
            this.showCheckBox.Location = new System.Drawing.Point(1043, 75);
            this.showCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.showCheckBox.Name = "showCheckBox";
            this.showCheckBox.Size = new System.Drawing.Size(62, 20);
            this.showCheckBox.TabIndex = 4;
            this.showCheckBox.Text = "Show";
            this.showCheckBox.UseVisualStyleBackColor = true;
            this.showCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dark_mode_button
            // 
            this.dark_mode_button.AutoSize = true;
            this.dark_mode_button.Checked = true;
            this.dark_mode_button.Location = new System.Drawing.Point(783, 75);
            this.dark_mode_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dark_mode_button.Name = "dark_mode_button";
            this.dark_mode_button.Size = new System.Drawing.Size(95, 20);
            this.dark_mode_button.TabIndex = 5;
            this.dark_mode_button.TabStop = true;
            this.dark_mode_button.Text = "Dark Mode";
            this.dark_mode_button.UseVisualStyleBackColor = true;
            this.dark_mode_button.CheckedChanged += new System.EventHandler(this.dark_mode_button_CheckedChanged);
            // 
            // white_mode_button
            // 
            this.white_mode_button.AutoSize = true;
            this.white_mode_button.Location = new System.Drawing.Point(897, 75);
            this.white_mode_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.white_mode_button.Name = "white_mode_button";
            this.white_mode_button.Size = new System.Drawing.Size(100, 20);
            this.white_mode_button.TabIndex = 6;
            this.white_mode_button.Text = "White Mode";
            this.white_mode_button.UseVisualStyleBackColor = true;
            this.white_mode_button.CheckedChanged += new System.EventHandler(this.white_mode_button_CheckedChanged);
            // 
            // Fenetre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1323, 842);
            this.Controls.Add(this.white_mode_button);
            this.Controls.Add(this.dark_mode_button);
            this.Controls.Add(this.showCheckBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Fenetre";
            this.Text = "Projet 3D Réaliste - ESIEE Paris (Richard FOUQUOIRE, Sami OURABAH, Mathieu TAILLA" +
    "NDIER, Cathy TRUONG)";
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
    }
}

