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
            Sphere3D SphereA = new Sphere3D(new V3(200, 200, 300), 150, Couleur.Red, lumiere);
            SphereA.Draw();

            Lumiere lumiereb = new Lumiere(new V3(1, 0, 1), new Couleur(155, 155, 155));
            Parallelepipede3D ParallelepipedeA = new Parallelepipede3D(new V3(500, 500, 200), 150, 150,150, Couleur.Green,lumiereb);
            ParallelepipedeA.Draw(1);
        }
    }
}
