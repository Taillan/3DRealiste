using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Projet_IMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = BitmapEcran.Init(pictureBox1.Width, pictureBox1.Height);
        }

        public bool Checked()               { return checkBox1.Checked;   }
        public void PictureBoxInvalidate()  { pictureBox1.Invalidate(); }
        public void PictureBoxRefresh()     { pictureBox1.Refresh();    }

        private void button1_Click(object sender, EventArgs e)
        {
            BitmapEcran.RefreshScreen();
            ProjetEleve.Go();
            BitmapEcran.Show();          
        }

        private void dark_mode_button_CheckedChanged(object sender, EventArgs e)
        {
            BitmapEcran.setBackground(new Couleur(0, 0, 0));
        }

        private void white_mode_button_CheckedChanged(object sender, EventArgs e)
        {
            BitmapEcran.setBackground(new Couleur(255, 255, 255));
        }
    }
}
