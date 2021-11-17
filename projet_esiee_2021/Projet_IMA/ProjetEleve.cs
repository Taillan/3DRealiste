using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            Texture texture = new Texture("gold.jpg");
            Texture texture_murs = new Texture("brick01.jpg");

            Texture texture2 = new Texture("caillou.jpg");
            Texture texture3 = new Texture("rock.jpg");
            Texture texture4 = new Texture("stone2.jpg");

            Texture bump_texture = new Texture("bump38.jpg");
            Texture bump_texture_murs = new Texture("brick01.jpg");

            Lumiere key_lumiere = new Lumiere(new V3(1, -.8f, 0), new Couleur(255, 255, 255)*.7f);
            Lumiere fill_lumiere = new Lumiere(new V3(-1, -.8f, 0), new Couleur(255, 255, 255) * .3f);

            ArrayList scene = new ArrayList();
            Parallelogramme3D RectangleA = new Parallelogramme3D(new V3(0, 0, 0), new V3(200, 100, 250), new V3(0, 150, 300), key_lumiere, fill_lumiere, texture_murs, bump_texture_murs);
            Parallelogramme3D RectangleB = new Parallelogramme3D(new V3(200, 200, 250), new V3(500, 0, 0), new V3(0, 0, 300), key_lumiere, fill_lumiere, texture_murs, bump_texture_murs);
            Parallelogramme3D RectangleC = new Parallelogramme3D(new V3(700, 0, 250), new V3(200, 100, -250), new V3(0, 150, 300), key_lumiere, fill_lumiere, texture_murs, bump_texture_murs);
            Parallelogramme3D RectangleD = new Parallelogramme3D(new V3(50, 0, 25), new V3(800, 100, 0), new V3(100, 0, 300), key_lumiere, fill_lumiere, texture_murs, bump_texture_murs);

            scene.Add(RectangleD);
            scene.Add(RectangleA);
            scene.Add(RectangleB);
            scene.Add(RectangleC);
            Sphere3D SphereA = new Sphere3D(new V3(300, 200, 300), 150, key_lumiere, fill_lumiere, texture, bump_texture);
            Sphere3D SphereB = new Sphere3D(new V3(400, 200, 350), 50, key_lumiere, fill_lumiere, texture2, bump_texture);
            Sphere3D SphereC = new Sphere3D(new V3(700, 200, 100), 80, key_lumiere, fill_lumiere, texture3, bump_texture);
            Sphere3D SphereD = new Sphere3D(new V3(650, 200, 400), 70, key_lumiere, fill_lumiere, texture4, bump_texture);
            scene.Add(SphereA);
            scene.Add(SphereB);
            scene.Add(SphereC);
            scene.Add(SphereD);

            //Normal display
            foreach (Objet3D objet in scene)
            {
                objet.Draw();
            }

            //RayCast
            DrawAll();
        }
    }
}
