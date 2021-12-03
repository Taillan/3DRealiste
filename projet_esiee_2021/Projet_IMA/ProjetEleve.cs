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
            Lumiere KeyLumiere= new Lumiere(new V3(1, -.8f, 0), new Couleur(255, 255, 255) * .7f);
            Lumiere FillLumiere= new Lumiere(new V3(-1, -.8f, 0), new Couleur(255, 255, 255) * .3f);
            //BitmapEcran.Lumieres.Add(FillLumiere);
            BitmapEcran.Lumieres.Add(KeyLumiere);
            Texture texture = new Texture("gold.jpg");
            Texture texture_murs = new Texture("brick01.jpg");

            Texture texture2 = new Texture("caillou.jpg");
            Texture texture3 = new Texture("rock.jpg");
            Texture stone2 = new Texture("stone2.jpg");

            Texture bump_texture = new Texture("bump38.jpg");
            Texture bump_texture_murs = new Texture("brick01.jpg");


            ArrayList scene = new ArrayList();

            Parallelogramme3D MurGauche = new Parallelogramme3D(new V3(0, 0, 0), new V3(200, 300, 300), new V3(0, 0, 300), texture_murs, bump_texture_murs);
            Parallelogramme3D MurFond = new Parallelogramme3D(new V3(200, 300, 300), new V3(800, 0, 0), new V3(0, 0, 300), texture_murs, bump_texture_murs);
            Parallelogramme3D Sol = new Parallelogramme3D(new V3(0, 0, 0), new V3(1000, 0, 0), new V3(0, 300, 300), stone2, stone2);
            Parallelogramme3D MurMilieu = new Parallelogramme3D(new V3(600, 0, 0), new V3(200, 300, 250), new V3(0, 0, 250), texture_murs, bump_texture_murs);

            scene.Add(Sol);
            scene.Add(MurGauche);
            scene.Add(MurFond);
            scene.Add(MurMilieu);

            Sphere3D SphereA = new Sphere3D(new V3(300, 0, 300), 150, texture, bump_texture);
            //Sphere3D SphereB = new Sphere3D(new V3(400, 200, 350), 50, key_lumiere, fill_lumiere, texture2, bump_texture);
            //Sphere3D SphereC = new Sphere3D(new V3(700, 200, 100), 80, key_lumiere, fill_lumiere, texture3, bump_texture);
            Sphere3D SphereD = new Sphere3D(new V3(900, 200, 400), 70, stone2, bump_texture);
   
            scene.Add(SphereD);
            scene.Add(SphereA);
            //scene.Add(SphereB);
            //scene.Add(SphereC);

            //Normal display
            /*foreach (Objet3D objet in scene)
            {
                objet.Draw();
            }*/

            //RayCast
            BitmapEcran.DrawAll(scene);
        }
    }
}
