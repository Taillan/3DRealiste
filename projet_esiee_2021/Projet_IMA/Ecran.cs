using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Projet_IMA
{
    enum ModeAff { SLOW_MODE, FULL_SPEED};
    enum RenderMode { SIMPLE, PATH_TRACING, VPL };

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

        #region Constructeurs
        /// <summary>
        /// Créée un Ecran avec une largeur et une hauteur passés en paramètres
        /// </summary>
        /// <param name="LargeurEcran">Largeur de l'Ecran</param>
        /// <param name="HauteurEcran">Hauteur de l'Ecran</param>
        /// <returns>Image bitmap générée</returns>
        static internal Bitmap Init(int LargeurEcran, int HauteurEcran)
        {
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
        static private void DrawSlowPixel(int x, int y, Couleur c)
        {
            Color cc = c.Convertion();
            B.SetPixel(x, y, cc);

            Program.MyForm.PictureBoxInvalidate();
            nb_pix++;
            if (nb_pix > refresh_every)  // force l'affichage à l'écran tous les 1000pix
            {
                Program.MyForm.PictureBoxRefresh();
                nb_pix = 0;
            }
        }

        static private void SetVirtualPointLights(int VPL_LEVEL, List<Objet3D> objets)
        {
            List <Lumiere> MainLumieres = new List<Lumiere>();
            foreach(Lumiere lumiere in s_Lumieres)
            {
                MainLumieres.Add(lumiere);
            }
            foreach(Lumiere lumiere in MainLumieres)
            {
                V3 PositionLumiere = lumiere.m_Position;
                for (int i = 0; i < VPL_LEVEL; i++)
                {
                    V3 DirectionLumiere = V3.getRandomVectorInHemisphere(lumiere.m_NormalizedDirection);
                    Lumiere newLumiere = new Lumiere(DirectionLumiere,lumiere.m_Couleur,PositionLumiere);
                    float DistanceIntersectionMax = float.MaxValue;
                    foreach (Objet3D objet in objets)
                    {
                        if (objet.IntersectionRayon(PositionLumiere, DirectionLumiere, out float DistanceIntersection, out V3 PixelPosition, out float u, out float v))
                        {
                            if (DistanceIntersection > 0 && DistanceIntersection < DistanceIntersectionMax)
                            {
                                DistanceIntersectionMax = DistanceIntersection;
                                newLumiere = new Lumiere(V3.getRandomVectorInHemisphere(objet.getBumpedNormal(PixelPosition,u,v)), objet.getCouleurPixel(u, v)*.2f, PixelPosition);
                            }
                        }
                    }
                    s_Lumieres.Add(newLumiere);
                }
            }
        }

        /// <summary>
        /// Retourne la couleur associée au pixel pointé par le rayon passé en paramètre
        /// Utilise la méthode du ray casting pour n'afficher que les pixels visibles par la caméra
        /// </summary>
        /// <param name="PositionCamera">Position de la caméra</param>
        /// <param name="DirectionRayon">Direction du rayon utilisé pour le raycasting</param>
        /// <param name="objets">Liste des objets de la scène</param>
        /// <returns>Couleur associée au pixel pointé par le rayon</returns>
        static private Couleur RayCast(V3 PositionCamera, V3 DirectionRayon, List<Objet3D> objets, RenderMode RM, int PathTracerLevel, int VPL_LEVEL)
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
                        finalColor = objet.getCouleur(PixelPosition, u, v, RM, PathTracerLevel);
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
        internal static void DrawPixel(int x, int y, Couleur c)
        {
            int x_ecran = x;
            int y_ecran = s_HauteurEcran - y;

            if ((x_ecran >= 0) && (x_ecran < s_LargeurEcran) && (y_ecran >= 0) && (y_ecran < s_HauteurEcran))
                if (Mode == ModeAff.SLOW_MODE) DrawSlowPixel(x_ecran, y_ecran, c);
                else DrawFastPixel(x_ecran, y_ecran, c);
        }

        /// <summary>
        /// Parcourt tous les pixels de l'Ecran et applique la méthode du RayCasting pour afficher tous les objets
        /// présents dans la scène
        /// </summary>
        /// <param name="RM">Mode d'affichage voulu (Simple, VPL, PathTracer, RayTracer?)</param>
        /// <param name="PathTracerLevel">Échantillonage du PathTracer</param>
        /// <param name="VPL_LEVEL">Nombre de lumières de niveau 2 à créer en mode d'affichage VPL</param>
        static internal void DrawAll(RenderMode RM, int PathTracerLevel = 1, int VPL_LEVEL = 1)
        {
            if (RM == RenderMode.VPL)
            {
                RM = RenderMode.SIMPLE;
                SetVirtualPointLights(VPL_LEVEL, s_Objets);
            }
            for (int x_ecran = 0; x_ecran <= s_LargeurEcran; x_ecran++)
            {
                for (int y_ecran = 0; y_ecran <= s_HauteurEcran; y_ecran++)
                {
                    V3 PosPixScene = new V3(x_ecran, 0, y_ecran);
                    V3 DirRayon = PosPixScene - s_CameraPosition;
                    Couleur C = RayCast(s_CameraPosition, DirRayon, s_Objets, RM, PathTracerLevel, VPL_LEVEL);
                    DrawPixel(x_ecran, y_ecran, C);
                }
            }
        }
        #endregion
    }
}
