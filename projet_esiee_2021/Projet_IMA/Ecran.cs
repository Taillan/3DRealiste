using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;


namespace Projet_IMA
{
    enum ModeAff { SLOW_MODE, FULL_SPEED};

    class BitmapEcran
    {
        const int refresh_every = 1000; // force l'affiche tous les xx pix
        static int nb_pix = 0;                 // comptage des pixels

        static private Bitmap B;
        static private ModeAff Mode;
        static private int stride;
        static private BitmapData data;
        static private Couleur background;
        static public int s_LargeurEcran { get; set; }
        static public int s_HauteurEcran { get; set; }
        static public V3 s_CameraPosition { get; set; }
        static public List<Lumiere> s_Lumieres { get; set; }
        static public List<Objet3D> s_Objets { get; set; }    

        static public Bitmap Init(int LargeurEcran, int HauteurEcran)
        {
            s_LargeurEcran = LargeurEcran;
            s_HauteurEcran = HauteurEcran;
            B = new Bitmap(LargeurEcran, HauteurEcran);
            s_CameraPosition = new V3(LargeurEcran / 2, -1.5f * LargeurEcran, HauteurEcran / 2);
            return B;
        }

        #region Méthodes privées

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

        #endregion

        #region Méthodes publiques

        static public void RefreshScreen()
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


        public static void DrawPixel(int x, int y, Couleur c)
        {
            int x_ecran = x;
            int y_ecran = s_HauteurEcran - y;

            if ((x_ecran >= 0) && (x_ecran < s_LargeurEcran) && (y_ecran >= 0) && (y_ecran < s_HauteurEcran))
                if (Mode == ModeAff.SLOW_MODE) DrawSlowPixel(x_ecran, y_ecran, c);
                else DrawFastPixel(x_ecran, y_ecran, c);
        }

        static public void Show()
        {
            if (Mode == ModeAff.FULL_SPEED)
                B.UnlockBits(data);

            Program.MyForm.PictureBoxInvalidate();
        }

        static public void setBackground(Couleur c)
        {
            background = c;
        }

        static public void DrawAll()
        {
            for (int x_ecran = 0; x_ecran <= s_LargeurEcran; x_ecran++)
            {
                for (int y_ecran = 0; y_ecran <= s_HauteurEcran; y_ecran++)
                {
                    V3 PosPixScene = new V3(x_ecran, 0, y_ecran);
                    V3 DirRayon = PosPixScene - s_CameraPosition;
                    Couleur C = RayCast(s_CameraPosition, DirRayon, s_Objets);
                    DrawPixel(x_ecran, y_ecran, C);
                }
            }
        }
        #endregion
    }
}
