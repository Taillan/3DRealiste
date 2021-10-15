using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Sphère en 3D
            /// 
            //////////////////////////////////////////////////////////////////////////
            
            V3 CentreSphere = new V3(300,200,300);
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

                    /* 
                     for (int i = 0; i < 100; i++) */ // pour ralentir et voir l'animation - devra être retiré
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, CSphere);
                    
                }

            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Rectangle 3D  + exemple texture
            /// 
            //////////////////////////////////////////////////////////////////////////


            V3 Origine = new V3(500, 200, 300);
            V3 Coté1 = new V3(300, 000, 000);
            V3 Coté2 = new V3(000, 200, 000);
            Couleur CRect = Couleur.Blue;

            pas = 0.02f;
            for (float u = 0; u < 1; u += pas)  // echantillonage fnt paramétrique
                for (float v = 0; v < 1; v += pas)
                {
                    V3 P3D = Origine + u * Coté1 + v * Coté2;
  
                    // projection orthographique => repère écran

                    int x_ecran = (int)(P3D.x);
                    int y_ecran = (int)(P3D.y);
                    for (int i = 0; i < 100; i++)  // pour ralentir et voir l'animation - devra être retiré
                        BitmapEcran.DrawPixel(x_ecran, y_ecran, CRect);
                }

            // Gestion des textures
            // Texture T1 = new Texture("brick01.jpg");
            // Couleur c = T1.LireCouleur(u, v);
            
        }
    }
}
