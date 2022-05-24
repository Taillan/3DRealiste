using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace Projet_IMA
{
    enum ModeAff { SLOW_MODE, FULL_SPEED};
    enum RenderMode { SIMPLE, PATH_TRACING };

    class BitmapEcran
    {
        /// <summary>
        /// Force l'affiche tous les xx pix
        /// </summary>
        const int refresh_every = 1000; // force l'affiche tous les xx pix
        
        /// <summary>
        /// Comptage des pixels
        /// </summary>
        static int nb_pix = 0;

        /// <summary>
        /// Image bitmap générée par l'affichage de tous les objets
        /// </summary>
        static private Bitmap B;

        /// <summary>
        /// Mode d'affichage (SLOW_MODE pour voir la génération, FULL_SPEED pour que l'image s'affiche directement)
        /// </summary>
        static private ModeAff Mode;
        static private int stride;
        static private BitmapData data;

        /// <summary>
        /// Couleur du fond de la scène
        /// </summary>
        static private Couleur background;

        /// <summary>
        /// Largeur de la fenêtre
        /// </summary>
        static internal int s_LargeurEcran { get; set; }

        /// <summary>
        /// Hauteur de la fenêtre
        /// </summary>
        static internal int s_HauteurEcran { get; set; }

        /// <summary>
        /// Position de la caméra par rapport à la scène
        /// </summary>
        static internal V3 s_CameraPosition { get; set; }
        
        /// <summary>
        /// Liste de toutes les lumières présentes dans la scène
        /// </summary>
        static internal List<Lumiere> s_Lumieres { get; set; }
        
        /// <summary>
        /// Liste de tous les objets présents dans la scène.
        /// </summary>
        static internal List<Objet3D> s_Objets { get; set; }

        #region MultiThrad


        /// <summary>
        /// liste de tous les threads
        /// </summary>
        static internal List<Thread> LThreads { get; set; }

        /// <summary>
        /// largeur de la zone carrée
        /// </summary>
        static internal int LargZonePix { get; set; }

        /// <summary>
        /// liste des zones carré à traiter
        /// </summary>
        static internal ConcurrentBag<Point> JobList { get; set; }

        static internal Graphics canvas;

        static internal PictureBox pictureBox1;

        static internal int LargeurZonePix { get; set; }
        static internal int HauteurZonePix { get; set; }

        #endregion


        #region Constructeurs
        /// <summary>
        /// Créée un Ecran avec une largeur et une hauteur passés en paramètres
        /// </summary>
        /// <param name="LargeurEcran">Largeur de l'Ecran</param>
        /// <param name="HauteurEcran">Hauteur de l'Ecran</param>
        /// <returns>Image bitmap générée</returns>
        static internal Bitmap Init(int LargeurEcran, int HauteurEcran,PictureBox pictureBox)
        {
            pictureBox1 = pictureBox;
            LThreads = new List<Thread>();
            JobList = new ConcurrentBag<Point>();
            canvas = pictureBox.CreateGraphics();
            s_LargeurEcran = LargeurEcran;
            s_HauteurEcran = HauteurEcran;
            B = new Bitmap(LargeurEcran, HauteurEcran);
            s_CameraPosition = new V3(LargeurEcran / 2, -1.5f * LargeurEcran, HauteurEcran / 2);
            return B;
        }
        #endregion

        #region Méthodes privées

        /// <summary>
        /// Dessine directement un pixel sans refresh_rate
        /// </summary>
        /// <param name="x">Coordonnées en abscisse de l'écran</param>
        /// <param name="y">Coordonnées en ordonnées de l'écran</param>
        /// <param name="c">Couleur du pixel</param>
        static private void DrawFastPixel(int x, int y, Couleur c)
        {
            unsafe
            {
                byte RR, VV, BB;
                c.check();
                c.To255(out RR, out VV, out BB);

                byte* ptr = (byte*)data.Scan0;
                ptr[(x * 3) + y * stride] = BB;
                ptr[(x * 3) + y * stride + 1] = VV;
                ptr[(x * 3) + y * stride + 2] = RR;
            }
        }

        /// <summary>
        /// Dessine un pixel l'un après l'autre avec un refresh_rate pour voir la "génération" de la scène
        /// </summary>
        /// <param name="x">Coordonnées en abscisse de l'écran</param>
        /// <param name="y">Coordonnées en ordonnées de l'écran</param>
        /// <param name="c">Couleur du pixel</param>
        static private void DrawSlowPixel(int x, int y, Couleur c,Bitmap Bp, Point CoordZone)
        {
            Color cc = c.Convertion();
            Bp.SetPixel(x , y , cc);
            //Bp.SetPixel(x - CoordZone.X, y - CoordZone.Y, cc);


           // Program.MyForm.PictureBoxInvalidate();
            /*nb_pix++;
            if (nb_pix > refresh_every)  // force l'affichage à l'écran tous les 1000pix
            {
                nb_pix = 0;
            }*/
        }

        /// <summary>
        /// Retourne la couleur associée au pixel pointé par le rayon passé en paramètre
        /// Utilise la méthode du ray casting pour n'afficher que les pixels visibles par la caméra
        /// </summary>
        /// <param name="PositionCamera">Position de la caméra</param>
        /// <param name="DirectionRayon">Direction du rayon utilisé pour le raycasting</param>
        /// <param name="objets">Liste des objets de la scène</param>
        /// <returns>Couleur associée au pixel pointé par le rayon</returns>
        static private Couleur RayCast( V3 PositionCamera, V3 DirectionRayon, List<Objet3D> objets, RenderMode RM)
        {
            
            List<Objet3D> copy = new List<Objet3D>(objets);

            float DistanceIntersectionMax = float.MaxValue;
            Couleur finalColor = background;
            foreach (Objet3D objet in copy)
            {
                if (objet.IntersectionRayon(PositionCamera, DirectionRayon, out float DistanceIntersection, out V3 PixelPosition, out float u, out float v))
                {
                    if (DistanceIntersection > 0 && DistanceIntersection < DistanceIntersectionMax)
                    {
                        DistanceIntersectionMax = DistanceIntersection;
                        finalColor = objet.getCouleur(PixelPosition, u, v, RM);
                    }
                }
            }
            return finalColor;
        }

        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Permet de raffraichir l'écran en y dessinant tous les pixels qui y ont été tracés
        /// </summary>
        static internal void RefreshScreen()
        {
            Couleur c = background;
            if (Program.MyForm.Checked())
            {
                Mode = ModeAff.SLOW_MODE;
                Graphics g = Graphics.FromImage(B);
                Color cc = c.Convertion();
                g.Clear(cc);
            }
            else
            {
                Mode = ModeAff.FULL_SPEED;
                data = B.LockBits(new Rectangle(0, 0, B.Width, B.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                stride = data.Stride;
                for (int x = 0; x < s_LargeurEcran; x++)
                    for (int y = 0; y < s_HauteurEcran; y++)
                        DrawFastPixel(x, y, c);
            }
        }

        /// <summary>
        /// Affiche l'entièreté de la scène
        /// </summary>
        static internal void Show()
        {
            if (Mode == ModeAff.FULL_SPEED)
                B.UnlockBits(data);

            Program.MyForm.PictureBoxInvalidate();
        }
        /// <summary>
        /// Permet de set la couleur de l'arrière-plan de l'écran en fonction de la couleur passée en paramètre
        /// </summary>
        /// <param name="c">Couleur de l'arrière-plan</param>
        static internal void setBackground(Couleur c)
        {
            background = c;
        }

        /// <summary>
        /// Permet de dessiner un pixel aux coordonnées x, y de l'écran avec la couleur passée en paramètre
        /// </summary>
        /// <param name="x">Coordonnées en abscisse de l'Ecran</param>
        /// <param name="y">Coordonnées en ordonnées de l'Ecran</param>
        /// <param name="c">Couleur du pixel qu'on veut dessiner</param>
        internal static void DrawPixel(int x, int y, Couleur c,Bitmap B, Point CoordZone)
        {
            int x_ecran = x;
            int y_ecran = y;
            

            if ((x_ecran >= 0) && (x_ecran < s_LargeurEcran) && (y_ecran >= 0) && (y_ecran < s_HauteurEcran))
                if (Mode == ModeAff.SLOW_MODE) DrawSlowPixel(x_ecran, y_ecran, c,B,  CoordZone);
                else DrawFastPixel(x_ecran, y_ecran, c);
        }

        /// <summary>
        /// Parcourt tous les pixels de l'Ecran et applique la méthode du RayCasting pour afficher tous les objets
        /// présents dans la scène
        /// </summary>
        static internal void DrawAll()
        {
            int LargAff = s_LargeurEcran;
            int HautAff = s_HauteurEcran;

            //Initialise les composant pour le multithread
            LargeurZonePix = s_LargeurEcran / 10;
            HauteurZonePix = s_HauteurEcran / 10;

            // crée la liste des zones à afficher
            for (int x = 0; x < LargAff; x += LargeurZonePix)
                for (int y = 0; y < HautAff; y += HauteurZonePix)
                    JobList.Add(new Point(x, y));

            // crée et lance le pool de threads
            for (int i = 0; i < 6 ; i++)  // 4: nb de threads
            {
                int idThread = i; // capture correctement la valeur de i pour le délégué ci-dessous
                Thread T = new Thread(delegate () { FntThread(idThread); });
                LThreads.Add(T);
                T.Start();        // demarre le thread enfant
            }

        }


        /// <summary>
        /// fonction appelée dans le thread principal suite à l'envoi d'un évènement
        /// par un thread enfant grâce à la méthode invoke
        /// </summary>
        /// <param name="P"></param>
        /// <param name="B"></param>
        private static void DrawInMainThread(Point P, Bitmap B)
        {
            canvas.DrawImage(B, P);
        }

        /// <summary>
        /// methode déclenchée par chaque thread
        /// le code ci-dessous s'exécute dans les threads enfants
        /// </summary>
        /// <param name="idThread"></param>
        private static void FntThread(int idThread)
        {
            Random aleatoire = new Random();
            Point CoordZone;

            // capture une zone dans la liste des zones à traiter
            while (JobList.TryTake(out CoordZone))
            {
                Bitmap Bp = new Bitmap(LargeurZonePix, HauteurZonePix);

                Console.WriteLine("Debut thread         " + idThread + " time:" + DateTime.Now);
                for (int x_ecran =0; x_ecran < LargeurZonePix; x_ecran++)
                {
                    for (int y_ecran =0; y_ecran < HauteurZonePix; y_ecran++)
                    {
                        V3 PosPixScene = new V3(CoordZone.X + x_ecran, 0, s_HauteurEcran  - (CoordZone.Y + y_ecran));
                        V3 DirRayon = PosPixScene - s_CameraPosition;
                        Couleur C = RayCast(s_CameraPosition, DirRayon, s_Objets, RenderMode.PATH_TRACING);
                        DrawPixel(x_ecran, y_ecran, C,Bp,CoordZone);
                    }
                }

                Console.WriteLine("RayCast thread fin   " + idThread + "    time:   " + DateTime.Now);
                var d = new SafeCallDelegate(DrawInMainThread);
                Console.WriteLine("Fin thread           " + idThread + "    time:   " + DateTime.Now);
                pictureBox1.Invoke(d, new object[] { CoordZone, Bp });
                Console.WriteLine("Invoke thread        " + idThread + "    time:   " + DateTime.Now);
            }
        }

        delegate void SafeCallDelegate(Point P, Bitmap B);



        // arrête les threads si fermeture de la fenêtre
        public static void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Thread T in LThreads)
                T.Abort();
        }
        #endregion
    }
}
