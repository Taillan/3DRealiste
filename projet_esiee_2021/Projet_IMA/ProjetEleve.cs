using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void displayCube(Couleur color,Couleur lampe, V3 offsetx, V3 offsety)
        {
            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Rectangle 3D  + exemple texture
            /// 
            //////////////////////////////////////////////////////////////////////////

            V3 Origine = new V3(50, 20, 30) + offsetx+offsety;
            V3 Cote1 = new V3(30, 00, 00);
            V3 Cote2 = new V3(00, 20, 00);
            Couleur CRect = color * lampe;

            float pas = 0.002f;
            for (float u = 0; u < 1; u += pas)  // echantillonage fnt paramétrique
                for (float v = 0; v < 1; v += pas)
                {
                    //  if (u+v >= 1)
                    //  {
                    V3 P3D = Origine + u * Cote1 + v * Cote2;

                    // projection orthographique => repère écran

                    int x_ecran = (int)(P3D.x);
                    int y_ecran = (int)(P3D.y);
                    //   for (int i = 0; i < 100; i++)  // pour ralentir et voir l'animation - devra être retiré
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, CRect);
                    //}
                }

            // Gestion des textures
            // Texture T1 = new Texture("brick01.jpg");
            // Couleur c = T1.LireCouleur(u, v);

        }
        public static void Go()
        {
            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Sphère en 3D
            /// 
            //////////////////////////////////////////////////////////////////////////

            /*  V3 CentreSphere = new V3(300,200,300);
              float Rayon = 150;
              Couleur CSphere = Couleur.Red;


              float pas = 0.005f;
              for (float u = 0; u < 2 * IMA.PI ; u += pas)  // echantillonage fnt paramétrique
                  for (float v = -IMA.PI / 2 ; v < IMA.PI / 2 ; v += pas)
                  {
                      // calcul des coordoonées dans la scène 3D
                      float x3D = Rayon * IMA.Cosf(v) * IMA.Cosf(u) + CentreSphere.x;
                      float y3D = Rayon * IMA.Cosf(v) * IMA.Sinf(u) + CentreSphere.y;
                      float z3D = Rayon * IMA.Sinf(v) + CentreSphere.z;

                      // projection orthographique => repère écran

                      int x_ecran = (int)(x3D);  
                      int y_ecran = (int)(z3D);


                      //for (int i = 0; i < 100; i++)  // pour ralentir et voir l'animation - devra être retiré
                         BitmapEcran.DrawPixel(x_ecran, y_ecran, CSphere);

                  }*/
            var couleurs = new Dictionary<string, Couleur>()
            {
                { "Blanc", new Couleur(255,255,255) },
                { "Rouge", new Couleur(255,0,0) },
                { "Jaune", new Couleur(255,255,0) },
                { "Vert", new Couleur(0,255,0) },
                { "Cyan", new Couleur(0,255,255) },
                { "Bleu", new Couleur(0,0,255) },
                { "Rose", new Couleur(253,0,255) },
                { "Noir", new Couleur(0,0,0) },

            };

            var lampes = new Dictionary<string, Couleur>()
            {
                { "Blanc", new Couleur(255,255,255) },
                { "Rouge", new Couleur(255,0,0) },
                { "Jaune", new Couleur(255,255,0) },
                { "Vert", new Couleur(0,255,0) },
                { "Cyan", new Couleur(0,255,255) },
                { "Bleu", new Couleur(0,0,255) },
                { "Rose", new Couleur(253,0,255) },
                { "Noir", new Couleur(0,0,0) },

            };

            V3 offsetx = new V3(00, 00, 00);

            V3 offsety = new V3(00, 00, 00);
            foreach (var lampe in lampes)
            {
                foreach (var couleur in couleurs)
                {
                    displayCube(couleur.Value, lampe.Value, offsetx, offsety);
                    offsetx += new V3(50, 00, 00);
                }
                offsetx = new V3(00, 00, 00);
                offsety += new V3(00, 50, 00);
            }

        }
    }
}
