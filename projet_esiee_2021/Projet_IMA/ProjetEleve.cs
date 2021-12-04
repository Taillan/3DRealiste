using System.Collections.Generic;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            BitmapEcran.s_Lumieres = new List<Lumiere>();
            BitmapEcran.s_Objets = new List<Objet3D>();

            Lumiere KeyLumiere= new Lumiere(new V3(1, -.8f, 0), new Couleur(255, 255, 255) * .7f);
            Lumiere FillLumiere= new Lumiere(new V3(-1, -.8f, 0), new Couleur(255, 255, 255) * .3f);
            
            //BitmapEcran.Lumieres.Add(FillLumiere);
            BitmapEcran.s_Lumieres.Add(KeyLumiere);

            Texture gold = new Texture("gold.jpg");
            Texture texture_murs = new Texture("brick01.jpg");

            Texture texture2 = new Texture("caillou.jpg");
            Texture texture3 = new Texture("rock.jpg");
            Texture stone2 = new Texture("stone2.jpg");
            Texture wood = new Texture("wood.jpg");

            Texture bump_texture = new Texture("bump38.jpg");
            Texture bump_texture_murs = new Texture("brick01.jpg");

            Parallelogramme3D MurGauche = new Parallelogramme3D(new V3(0, 0, 0), new V3(200, 300, 300), new V3(0, 0, 300), texture_murs, bump_texture_murs);
            Parallelogramme3D MurFond = new Parallelogramme3D(new V3(200, 300, 300), new V3(800, 0, 0), new V3(0, 0, 300), texture_murs, bump_texture_murs);
            Parallelogramme3D Sol = new Parallelogramme3D(new V3(0, 0, 0), new V3(1000, 0, 0), new V3(0, 300, 300), stone2, stone2);
            Parallelogramme3D MurMilieu = new Parallelogramme3D(new V3(600, 0, 0), new V3(200, 300, 250), new V3(0, 0, 250), texture_murs, bump_texture_murs);
            Sphere3D SphereOr = new Sphere3D(new V3(300, 0, 300), 150, gold, bump_texture);
            Sphere3D SphereStone = new Sphere3D(new V3(900, 200, 400), 70, stone2, bump_texture);

            BitmapEcran.s_Objets.Add(Sol);
            BitmapEcran.s_Objets.Add(MurGauche);
            BitmapEcran.s_Objets.Add(MurFond);
            BitmapEcran.s_Objets.Add(MurMilieu);

            BitmapEcran.s_Objets.Add(SphereStone);
            BitmapEcran.s_Objets.Add(SphereOr);

            //RayCast
            BitmapEcran.DrawAll();
        }
    }
}
