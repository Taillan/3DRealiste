using System.Collections.Generic;

namespace Projet_IMA
{
    /// <summary>
    /// Classe statique centrale permettant d'ajouter des objets à notre scène qui vont être tracés
    /// par la classe Ecran.
    /// </summary>
    static class ProjetEleve
    {
        public static void Go()
        {
            BitmapEcran.s_Lumieres = new List<Lumiere>();
            BitmapEcran.s_Objets = new List<Objet3D>();

            List<Objet3D> Scene1 = new List<Objet3D>();
            List<Objet3D> Scene2 = new List<Objet3D>();

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
            Parallelogramme3D SolLumiere = new Parallelogramme3D_Lumiere(new Couleur(1,1,1),new V3(0, 0, 0), new V3(0, 300, 0), new V3(BitmapEcran.s_LargeurEcran, 0, 0) , stone2, stone2);
            Parallelogramme3D MurMilieu = new Parallelogramme3D(new V3(600, 0, 0), new V3(200, 300, 250), new V3(0, 0, 250), texture_murs, bump_texture_murs);
            Sphere3D SphereOr = new Sphere3D(new V3(300, 0, 300), 100, gold, bump_texture);
            Sphere3D_Lumiere SphereLumiere = new Sphere3D_Lumiere(new Couleur(1,1,1),new V3(300, 0, 300), 100, gold, bump_texture);
            Parallelogramme3D_Lumiere MurMilieuLumiere = new Parallelogramme3D_Lumiere(new Couleur(1, 255, 255),new V3(600, 0, 0), new V3(200, 300, 250), new V3(0, 0, 250), texture_murs, bump_texture_murs);
            Sphere3D SphereStone = new Sphere3D(new V3(900, 200, 400), 70, stone2, bump_texture);

            Scene1.Add(SolLumiere);
            Scene1.Add(MurGauche);
            Scene1.Add(MurFond);
            Scene1.Add(MurMilieu);
            Scene1.Add(SphereStone);
            Scene1.Add(SphereOr);

            int Hauteur = BitmapEcran.s_HauteurEcran;
            int Largeur = BitmapEcran.s_LargeurEcran;
            int Profondeur = 1000;
            Parallelogramme3D Sol2 = new Parallelogramme3D(new V3(0, 0, 0), new V3(Largeur, 0, 0), new V3(0,Profondeur, 0), Texture.s_White, bump_texture_murs);
            Parallelogramme3D SolLumiere2 = new Parallelogramme3D_Lumiere(new Couleur(1, 1, 1), new V3(0, 0, 0), new V3(0, Profondeur, 0), new V3(Largeur, 0, 0), stone2, stone2);
            Parallelogramme3D MurGauche2 = new Parallelogramme3D(new V3(0, Profondeur, 0), new V3(0, 0, Hauteur), new V3(0, -Profondeur, 0), Texture.s_Green, bump_texture_murs);
            Parallelogramme3D MurFond2 = new Parallelogramme3D(new V3(0, Profondeur, 0), new V3(Largeur, 0, 0), new V3(0, 0, Hauteur), Texture.s_Blue, bump_texture_murs);
            Parallelogramme3D MurDroit2 = new Parallelogramme3D(new V3(Largeur, Profondeur, 0), new V3(0, -Profondeur, 0), new V3(0, 0, Hauteur), Texture.s_Red, bump_texture_murs);
            Parallelogramme3D Plafond2 = new Parallelogramme3D(new V3(0, 0, Hauteur), new V3(0, Profondeur, 0), new V3(Largeur, 0, 0), Texture.s_Purple, Texture.s_Purple);
            Sphere3D SphereOr2 = new Sphere3D(new V3(300, Profondeur-100, 300), 100, gold, bump_texture);
            Sphere3D SphereStone2 = new Sphere3D(new V3(800, 200, 400), 70, stone2, bump_texture);
            Sphere3D_Lumiere SphereLumiere2 = new Sphere3D_Lumiere(new Couleur(1, 1, 1), new V3(100, Profondeur/4, Hauteur-100), 100, gold, bump_texture);
            Sphere3D_Lumiere SphereLumiere3 = new Sphere3D_Lumiere(new Couleur(1, 1, 1), new V3(870, 200, 60), 60, gold, bump_texture);
            Sphere3D SphereVerte2 = new Sphere3D(new V3(780, 250, 50), 50, Texture.s_Green, bump_texture);
            Parallelogramme3D LumierePlafond2 = new Parallelogramme3D_Lumiere(new Couleur(1,1,1),new V3((Largeur/2)-200, (Profondeur/2)-200, Hauteur-1), new V3(0, 400, 0), new V3(400, 0, 0), Texture.s_Blue, Texture.s_Blue);
            Sphere3D SphereRouge2 = new Sphere3D(new V3(120, Profondeur-60, 60), 60, Texture.s_Red, bump_texture);
            Parallelepipede3D CubeViolet2 = new Parallelepipede3D(new V3(50,Profondeur/2.1f, 0), new V3(0, 100, 0), new V3(100, 0, 0), new V3(0, 0, 100), Texture.s_Purple, Texture.s_Purple);
            //Scene2.Add(SolLumiere2);
            Scene2.Add(Sol2);
            Scene2.Add(MurGauche2);
            Scene2.Add(MurFond2);
            Scene2.Add(MurDroit2);
            Scene2.Add(Plafond2);
            Scene2.Add(SphereStone2);
            Scene2.Add(SphereOr2);
            //Scene2.Add(SphereLumiere2);
            //Scene2.Add(SphereLumiere3);
            Scene2.Add(SphereVerte2);
            Scene2.Add(SphereRouge2);
            Scene2.Add(LumierePlafond2);
            foreach(Parallelogramme3D P in CubeViolet2.m_Faces)
            {
                Scene2.Add(P);
            }




            BitmapEcran.s_Objets = Scene2;

            //RayCast
            BitmapEcran.DrawAll();
        }
    }
}
