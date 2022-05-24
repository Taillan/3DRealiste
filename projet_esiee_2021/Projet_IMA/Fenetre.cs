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
            textBoxRayon.Visible = false;
            labelRayon.Visible = false;
            pictureBox1.Image = BitmapEcran.Init(pictureBox1.Width, pictureBox1.Height,pictureBox1);
            button1.Enabled = false;

        }

        public bool Checked()               { return showCheckBox.Checked;   }
        public void PictureBoxInvalidate()  { pictureBox1.Invalidate(); }
        public void PictureBoxRefresh()     { pictureBox1.Refresh();    }

        private void button1_Click(object sender, EventArgs e)
        {
            //BitmapEcran.RefreshScreen();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //RAYON
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox1 = (ComboBox)sender;

            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            string selectedRayon = (string)comboBox1.SelectedItem;

            int resultIndex = -1;

            // Call the FindStringExact method to find the first 
            // occurrence in the list.
            resultIndex = comboBox1.FindStringExact(selectedRayon,
                resultIndex);


            Global.rayon = resultIndex;
          //  Console.WriteLine(comboBox1.SelectedIndex);
            Console.WriteLine(resultIndex);
        }

        //THREAD
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            int selectedIndex = cmb.SelectedIndex;

            string selectedItem = cmb.Items[cmb.SelectedIndex].ToString();
            Console.WriteLine(selectedItem);
            Global.thread = int.Parse(selectedItem);
            if (cmb.SelectedIndex > -1) button1.Enabled = true;
            else button1.Enabled = false;
        }

        //RENDER MODE
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ComboBox cmb = (ComboBox)sender;
            if (cmb.SelectedIndex > -1) button1.Enabled = true;
            else button1.Enabled = false;
            int selectedIndex = cmb.SelectedIndex;

            string selectedItem = cmb.Items[cmb.SelectedIndex].ToString();
            Console.WriteLine(selectedItem);
            //  Global.RenderMode = selectedItem;
            if (Enum.TryParse<Global.RenderMode>(selectedItem, out Global.RenderMode renderMode))
            {
                Global.render_mode = renderMode;

                Console.WriteLine("conversion OK render mode is " + Global.render_mode);
                if ((Global.render_mode.CompareTo(Global.RenderMode.PATH_TRACING) == -1) && (Global.render_mode.CompareTo(Global.RenderMode.RAY_TRACING) == -1))
                {
                    textBoxRayon.Visible = false;
                    labelRayon.Visible = false;
                }

                else
                {
                    labelRayon.Text = "Rayons";
                    textBoxRayon.Visible = true;
                    labelRayon.Visible = true;
                }
                if (Global.render_mode.CompareTo(Global.RenderMode.VPL) == 0)
                {
                    textBoxRayon.Visible = true;
                    labelRayon.Visible = true;
                    labelRayon.Text = "VPL Level";
                }

                

            }
            else
                Console.WriteLine("not ok");

        }

        //RAYON
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string rayon = textBoxRayon.Text;
            Console.WriteLine(rayon);
            if (textBoxRayon.Text.Length != 0)
                Global.rayon = int.Parse(textBoxRayon.Text);
            else
                Global.rayon = 0;
        }
    }
}
