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
            Lumiere lumiere = new Lumiere(new V3(1, -1, 1), new Couleur(155, 155, 155));
            Sphere3D SphereA = new Sphere3D(new V3(200, 200, 300), 150, Couleur.m_Red, lumiere);
            SphereA.Draw();


            Lumiere lumiereb = new Lumiere(new V3(1, 0, 1), new Couleur(155, 155, 155));
            V3 longueur = new V3(200, 0, 0);
            V3 largeur = new V3(0, 200, 0);
            V3 hauteur = new V3(0, 0, 200);
            Parallelepipede3D ParallelepipedeA = new Parallelepipede3D(new V3(500, 500, 200), longueur, largeur,hauteur, Couleur.m_Green,lumiereb);
            ParallelepipedeA.Draw();
        }
    }
}
