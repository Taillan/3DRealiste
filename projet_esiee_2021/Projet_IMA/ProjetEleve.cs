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
            Lumiere key_lumiere = new Lumiere(new V3(1, -.8f, 0), new Couleur(255, 255, 255)*.7f);
            Lumiere fill_lumiere = new Lumiere(new V3(-1, -.8f, 0), new Couleur(255, 255, 255) * .3f);
            Sphere3D SphereA = new Sphere3D(new V3(200, 200, 300), 150, key_lumiere, fill_lumiere, texture, bump_texture);
            SphereA.Draw();
        }
    }
}
