using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void displayCube()
        {
            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Rectangle 3D  + exemple texture
            /// 
            ////////////////////////////////////////////////////////////////////////// 
            ///
            /*
            Lumiere lumiere = new Lumiere(new V3(1, 1, -1), new Couleur(255, 255, 255));
            V3 CentreSphere = new V3(50, 20, 30);
            float Rayon = 30;
            //Sphere3D SphereA = new Sphere3D(CentreSphere, Rayon, CSphere);

            float pas = 0.05f;

            SphereA.DrawSphere(pas,lumiere);

            // Gestion des textures
            // Texture T1 = new Texture("brick01.jpg");
            // Couleur c = T1.LireCouleur(u, v);
            */
        }
        public static void Go()
        {
            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Sphère en 3D
            /// 
            //////////////////////////////////////////////////////////////////////////
            Lumiere lumiere = new Lumiere(new V3(1, 1, -1), new Couleur(255, 255, 255));
            Sphere3D SphereA = new Sphere3D(new V3(50, 20, 30), 30, new Couleur(255, 0, 0));
            SphereA.DrawSphere(0.05f, lumiere);
            


        }
    }
}
