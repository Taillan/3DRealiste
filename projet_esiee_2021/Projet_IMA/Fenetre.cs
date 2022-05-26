using System;
using System.Windows.Forms;

namespace Projet_IMA
{
    public partial class Fenetre : Form
    {
        public Fenetre()
        {
            InitializeComponent();
            optionsTextBox.Visible = true;
            labelRayon.Visible = true;
            pictureBox1.Image = BitmapEcran.Init(pictureBox1.Width, pictureBox1.Height,pictureBox1);
            renderButton.Enabled = false;

        }
        public void PictureBoxInvalidate()  { pictureBox1.Invalidate(); }

        private void renderButton_Click(object sender, EventArgs e)
        {
            ProjetEleve.Go();
            BitmapEcran.Show();          
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //RENDER MODE ComboBox (PATH_TRACING, RAY_TRACING, VPL, SIMPLE)
        private void renderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ComboBox cmb = (ComboBox)sender;
            if (cmb.SelectedIndex > -1) renderButton.Enabled = true;
            else renderButton.Enabled = false;
            int selectedIndex = cmb.SelectedIndex;

            string selectedItem = cmb.Items[cmb.SelectedIndex].ToString();
            Console.WriteLine(selectedItem);

            if (Enum.TryParse<Global.RenderMode>(selectedItem, out Global.RenderMode renderMode))
            {
                Global.render_mode = renderMode;

                Console.WriteLine("conversion OK render mode is " + Global.render_mode);
                if ((Global.render_mode.CompareTo(Global.RenderMode.PATH_TRACING) == -1) && (Global.render_mode.CompareTo(Global.RenderMode.RAY_TRACING) == -1))
                {
                    optionsTextBox.Visible = false;
                    labelRayon.Visible = false;
                }

                else
                {
                    labelRayon.Text = "Rayons";
                    optionsTextBox.Visible = true;
                    labelRayon.Visible = true;
                }
                if (Global.render_mode.CompareTo(Global.RenderMode.VPL) == 0)
                {
                    optionsTextBox.Visible = true;
                    labelRayon.Visible = true;
                    labelRayon.Text = "VPL Level";
                }
            }
            else
                Console.WriteLine("not ok");
        }

        //THREAD
        private void threadComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;

            string selectedItem = cmb.Items[cmb.SelectedIndex].ToString();
            Console.WriteLine(selectedItem);
            Global.NbThreads = int.Parse(selectedItem);
        }

        //Options TextBox (Nb RAYONS / VPL LEVEL)
        private void optionsTextBox_TextChanged(object sender, EventArgs e)
        {
            string rayon = optionsTextBox.Text;
            Console.WriteLine(rayon);
            if (optionsTextBox.Text.Length != 0)
            {
                Global.OptionsValue = int.Parse(optionsTextBox.Text);
                renderButton.Enabled = true;
            }
            else
            {
                Global.OptionsValue = 0;
                renderButton.Enabled = false;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
