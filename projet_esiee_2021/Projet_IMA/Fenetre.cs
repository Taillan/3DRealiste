using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Projet_IMA
{
    public partial class Fenetre : Form
    {


        public Fenetre()
        {
            InitializeComponent();
            pictureBox1.Image = BitmapEcran.Init(pictureBox1.Width, pictureBox1.Height,pictureBox1);
        }

        public bool Checked()               { return showCheckBox.Checked;   }
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// arrête les threads si fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BitmapEcran.Form1_FormClosing( sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
