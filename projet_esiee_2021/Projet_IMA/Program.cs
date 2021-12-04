using System;
using System.Windows.Forms;

namespace Projet_IMA
{
    static class Program
    {
        static public Fenetre MyForm;

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
