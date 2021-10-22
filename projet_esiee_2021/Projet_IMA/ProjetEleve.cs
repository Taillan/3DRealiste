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

            V3 CentreSphere = new V3(50, 20, 30) + offsetx + offsety;
            float Rayon = 30;
            Couleur CSphere = color * lampe;
            Sphere3D SphereA = new Sphere3D(CentreSphere, Rayon, CSphere);

            float pas = 0.05f;

            SphereA.DrawSphere(pas);

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
                offsety += new V3(00, 100, 00);
            }

        }
    }
}
