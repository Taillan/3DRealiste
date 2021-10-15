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
            V3 CentreSphere = new V3(300, 200, 300);
            float Rayon = 150;
            Couleur CSphere = Couleur.Red;
            float pas = 0.005f;
            Sphere3D SphereA = new Sphere3D(CentreSphere, Rayon, CSphere);

            SphereA.DrawSphere(pas);
            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Rectangle 3D  + exemple texture
            /// 
            //////////////////////////////////////////////////////////////////////////


            V3 Origine = new V3(500, 200, 300);
            V3 Cote1 = new V3(300, 000, 000);
            V3 Cote2 = new V3(000, 200, 000);
            Couleur CRect = Couleur.Blue;
            Rectangle3D Rect = new Rectangle3D(Origine, Cote1, Cote2,CRect);
            pas = 0.02f;

            Rect.DrawRectangle3D(0.02f);
            // Gestion des textures
            // Texture T1 = new Texture("brick01.jpg");
            // Couleur c = T1.LireCouleur(u, v);
            
        }
    }
}
