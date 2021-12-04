using System.Drawing;
using System.Drawing.Imaging;

namespace Projet_IMA
{
    class Texture
    {
        private int Hauteur;
        private int Largeur;
        private Couleur [,] C;


        #region "constructeur"

        /// <summary>
        /// Constructeur de la texture
        /// </summary>
        /// <param name="ff">Nom du fichier texture situé dans le sous-dossier textures</param>
        public Texture(string ff)
        {
            string s = System.IO.Path.GetFullPath("..\\..");
            string path = System.IO.Path.Combine(s,"textures",ff);
            Bitmap B = new Bitmap(path); 
            
            Hauteur = B.Height;
            Largeur = B.Width;
            BitmapData data = B.LockBits(new Rectangle(0, 0, B.Width, B.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = data.Stride;
             
            C = new Couleur[Largeur,Hauteur];

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                for (int x = 0; x < Largeur; x++)
                    for (int y = 0; y < Hauteur; y++)
                    {
                        byte RR, VV, BB;
                        BB = ptr[(x * 3) + y * stride];
                        VV = ptr[(x * 3) + y * stride + 1];
                        RR = ptr[(x * 3) + y * stride + 2];
                        C[x, y].From255(RR, VV, BB);
                    }
            }
            B.UnlockBits(data);
            B.Dispose();
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Permet de retourner la couleur de la texture sur les coordonées données
        /// </summary>
        /// <param name="u">Position du vecteur u qui pointe sur le pixel de l'objet</param>
        /// <param name="v">Position du vecteur v qui pointe sur le pixel de l'objet</param>
        /// <returns>Couleur du pixel pointé</returns>
        public Couleur LireCouleur(float u, float v)
        {
            return Interpol(Largeur * u, Hauteur * v);
        }

        /// <summary>
        /// Permet de calculer les grandeurs dHdu et dHdv de la texture pour déterminer le bump du pixel.
        /// </summary>
        /// <param name="u">Position du vecteur u qui pointe sur le pixel de l'objet</param>
        /// <param name="v">Position du vecteur v qui pointe sur le pixel de l'objet</param>
        /// <param name="dhdu">Dérivée de h (la variation de la hauteur sur le bump) en fonction de u</param>
        /// <param name="dhdv">Dérivée de h (la variation de la hauteur sur le bump) en fonction de v</param>
        public void Bump(float u, float v, out float dhdu, out float dhdv)
        {
            float x = u * Hauteur;
            float y = v * Largeur;

            float vv = Interpol(x, y).GreyLevel();
            float vx = Interpol(x + 1, y).GreyLevel();
            float vy = Interpol(x, y + 1).GreyLevel();

            dhdu = vx - vv;
            dhdv = vy - vv;
        }


        #endregion

        #region Fonctions privées

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lu"></param>
        /// <param name="Hv"></param>
        /// <returns></returns>
        private Couleur Interpol(float Lu, float Hv)
        {
            int x = (int)(Lu);  // plus grand entier <=
            int y = (int)(Hv);

          //  float cx = Lu - x; // reste
          //  float cy = Hv - y;

            x = x % Largeur;
            y = y % Hauteur;
            if (x < 0) x += Largeur;
            if (y < 0) y += Hauteur;


            return C[x, y];

        /*    int xpu = (x + 1) % Largeur;
            int ypu = (y + 1) % Hauteur;

            float ccx = cx * cx;
            float ccy = cy * cy;

            return
              C[x, y] * (1 - ccx) * (1 - ccy)
            + C[xpu, y] * ccx * (1 - ccy)
            + C[x, ypu] * (1 - ccx) * ccy
            + C[xpu, ypu] * ccx * ccy;*/
        }
        #endregion
    }

}