using System;
using System.Threading;
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
        public static int seed = Environment.TickCount;
        public static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));


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
