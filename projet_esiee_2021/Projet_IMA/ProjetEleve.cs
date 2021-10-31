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
            Texture texture = new Texture("gold.jpg");
            Texture bump_texture = new Texture("bump38.jpg");
            Lumiere lumiere = new Lumiere(new V3(1, -1, 1), new Couleur(140, 140, 140));
            Sphere3D SphereA = new Sphere3D(new V3(200, 200, 300), 150, lumiere, texture, bump_texture);
            SphereA.Draw();
        }
    }
}
