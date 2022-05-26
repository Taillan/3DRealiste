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

    class BitmapEcran
    {
        #region Attributs
        /// <summary>
        /// Image bitmap générée par l'affichage de tous les objets
        /// </summary>
        static private Bitmap B;

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

        #endregion

        #region Attributs MultiThrad


        /// <summary>
        /// liste de tous les threads
        /// </summary>
        static internal List<Thread> LThreads { get; set; }

        /// <summary>
        /// Liste des zones carré à traiter
        /// </summary>
        static internal ConcurrentBag<Point> JobList { get; set; }

        /// <summary>
        /// Zone de dessin
        /// </summary>
        static internal Graphics canvas;

        /// <summary>
        /// Image finale sur la fenêtre de l'application
        /// </summary>
        static internal PictureBox pictureBox1;

        /// <summary>
        /// Largeur de la zone de travail d'un thread
        /// </summary>
        static internal int LargeurZonePix { get; set; }

        /// <summary>
        /// Hauteur de la zone de travail d'un thread
        /// </summary>
        static internal int HauteurZonePix { get; set; }

        #endregion


        #region Constructeurs
        /// <summary>
        /// Créée un Ecran avec une largeur et une hauteur passés en paramètres
        /// </summary>
        /// <param name="LargeurEcran">Largeur de l'Ecran</param>
        /// <param name="HauteurEcran">Hauteur de l'Ecran</param>
        /// <param name="pictureBox">Zone de l'application où l'image sera créée</param>
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
        /// Retourne la couleur associée au pixel pointé par le rayon passé en paramètre
        /// Utilise la méthode du ray casting pour n'afficher que les pixels visibles par la caméra
        /// </summary>
        /// <param name="PositionCamera">Position de la caméra</param>
        /// <param name="DirectionRayon">Direction du rayon utilisé pour le raycasting</param>
        /// <param name="objets">Liste des objets de la scène</param>
        /// <returns>Couleur associée au pixel pointé par le rayon</returns>
        static private Couleur RayCast(V3 PositionCamera, V3 DirectionRayon, List<Objet3D> objets)
        {
            float DistanceIntersectionMax = float.MaxValue;
            Couleur finalColor = background;
            foreach (Objet3D objet in objets)
            {
                if (objet.IntersectionRayon(PositionCamera, DirectionRayon, out float DistanceIntersection, out V3 PixelPosition, out float u, out float v))
                {
                    if (DistanceIntersection > 0 && DistanceIntersection < DistanceIntersectionMax)
                    {
                        DistanceIntersectionMax = DistanceIntersection;
                        finalColor = objet.getCouleur(PixelPosition, u, v);
                    }
                }
            }
            return finalColor;
        }

        /// <summary>
        /// Permet de dessiner un pixel aux coordonnées x, y de l'écran avec la couleur passée en paramètre
        /// </summary>
        /// <param name="x">Coordonnées en abscisse de l'Ecran</param>
        /// <param name="y">Coordonnées en ordonnées de l'Ecran</param>
        /// <param name="c">Couleur du pixel qu'on veut dessiner</param>
        private static void DrawPixel(int x, int y, Couleur c,Bitmap B, Point CoordZone)
        {
            if ((x >= 0) && (x < s_LargeurEcran) && (y >= 0) && (y < s_HauteurEcran))
            {
                Color cc = c.Convertion();
                B.SetPixel(x, y, cc);
            }
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
            for (int i = 0; i <= Global.NbThreads ; i++)
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
        /// Méthode déclenchée par chaque thread
        /// le code ci-dessous s'exécute dans les threads enfants
        /// </summary>
        /// <param name="idThread">Id du thread</param>
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
                        Couleur C = RayCast(s_CameraPosition, DirRayon, s_Objets);
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

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Affiche l'entièreté de la scène
        /// </summary>
        static internal void Show()
        {
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
        /// Arrête tous les threads si la fenêtre de l'application est fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Event fermeture</param>
        public static void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Thread T in LThreads)
                T.Abort();
        }
        #endregion
    }
}
