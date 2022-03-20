using System;
using System.Windows.Forms;

namespace Projet_IMA
{
    static class Program
    {
        /// <summary>
        /// Fenêtre de l'application
        /// </summary>
        static public Fenetre MyForm;
        /// <summary>
        /// Générateur de nombres pseudo-aléatoires
        /// </summary>
        public static Random s_Random = new Random();

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyForm = new Fenetre();
            Application.Run(MyForm);
        }
    }
}
