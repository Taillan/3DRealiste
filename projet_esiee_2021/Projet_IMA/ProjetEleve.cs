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
            List<Objet3D> Scene3 = new List<Objet3D>();
            List<Objet3D> Scene4 = new List<Objet3D>();

            /* Coordonnées fixes utiles pour la scène */
            int Hauteur = BitmapEcran.s_HauteurEcran;
            int Largeur = BitmapEcran.s_LargeurEcran;
            int Profondeur = 1000;
            V3 Milieu = new V3(BitmapEcran.s_LargeurEcran / 2, 0f, 0f);
            V3 MilieuStart = Milieu - new V3(Largeur / 2, 0f, 0f);
            V3 MilieuEnd = Milieu + new V3(Largeur / 2, 0f, 0f);

            /* Lumières utilisées par le SIMPLE RENDER et le VPL */
            Lumiere KeyLumiereDessus = new Lumiere(new V3(0, 0, 1f), new Couleur(1, 1, 1), new V3(Largeur / 2, Profondeur / 2, Hauteur - .2f));
            Lumiere KeyLumiereCote = new Lumiere(new V3(1, -.8f, 0), new Couleur(1, 1, 1), new V3(Largeur / 2, Profondeur / 2, Hauteur - .2f));
            BitmapEcran.s_Lumieres.Add(KeyLumiereCote);

            /* Textures */
            Texture gold = new Texture("gold.jpg");
            Texture texture_murs = new Texture("brick01.jpg");
            Texture texture2 = new Texture("caillou.jpg");
            Texture texture3 = new Texture("rock.jpg");
            Texture stone2 = new Texture("stone2.jpg");
            Texture wood = new Texture("wood.jpg");
            Texture bump_texture = new Texture("bump38.jpg");
            Texture bump_texture_murs = new Texture("brick01.jpg");

            /* SCENE1 */
            Parallelogramme3D MurGauche = new Parallelogramme3D(new V3(0, 0, 0), new V3(200, 300, 300), new V3(0, 0, 300), texture_murs, bump_texture_murs);
            Parallelogramme3D MurFond = new Parallelogramme3D(new V3(200, 300, 300), new V3(800, 0, 0), new V3(0, 0, 300), texture_murs, bump_texture_murs);
            Parallelogramme3D Sol = new Parallelogramme3D(new V3(0, 0, 0), new V3(1000, 0, 0), new V3(0, 300, 300), stone2, stone2);
            Parallelogramme3D MurMilieu = new Parallelogramme3D(new V3(600, 0, 0), new V3(200, 300, 250), new V3(0, 0, 250), texture_murs, bump_texture_murs);
            Sphere3D SphereOr = new Sphere3D(new V3(300, 0, 300), 100, gold, bump_texture);
            Sphere3D SphereStone = new Sphere3D(new V3(900, 200, 400), 70, stone2, bump_texture);

            Scene1.Add(Sol);
            Scene1.Add(MurGauche);
            Scene1.Add(MurFond);
            Scene1.Add(MurMilieu);
            Scene1.Add(SphereStone);
            Scene1.Add(SphereOr);

            /* SCENE2 */
            Parallelogramme3D Sol2 = new Parallelogramme3D(new V3(0f, 0f, 0f), new V3(Largeur, 0f, 0f), new V3(0f, Profondeur, 0f), Texture.s_White);
            Parallelogramme3D SolLumiere2 = new Parallelogramme3D_Lumiere(new V3(0f, 0f, 0f), new V3(0f, Profondeur, 0f), new V3(Largeur, 0f, 0f), new Couleur(1, 1, 1));
            Parallelogramme3D MurGauche2 = new Parallelogramme3D(new V3(0f, 0f, 0f), new V3(0f, Profondeur, 0f), new V3(0f, 0f, Hauteur), Texture.s_Red);
            Parallelogramme3D MurFond2 = new Parallelogramme3D(new V3(0f, Profondeur, 0f), new V3(Largeur, 0f, 0f), new V3(0f, 0f, Hauteur), Texture.s_White);
            Parallelogramme3D MurDroit2 = new Parallelogramme3D(new V3(Largeur, 0f, 0f), new V3(0f, 0f, Hauteur), new V3(0f, Profondeur, 0f), Texture.s_Green);
            Parallelogramme3D Plafond2 = new Parallelogramme3D(new V3(0f, 0f, Hauteur), new V3(0f, Profondeur, 0f), new V3(Largeur, 0f, 0f), Texture.s_White);
            Parallelogramme3D PlafondLumiere2 = new Parallelogramme3D_Lumiere(new V3(0f, 0f, Hauteur), new V3(0f, Profondeur, 0f), new V3(Largeur, 0f, 0f), new Couleur(1, 1, 1));
            Sphere3D SphereOr2 = new Sphere3D(new V3(300, Profondeur - 100, 300), 100, gold, gold);
            Sphere3D SphereStone2 = new Sphere3D(new V3(800, 200, 400), 70, stone2, bump_texture);
            Sphere3D_Lumiere SphereLumiere2 = new Sphere3D_Lumiere(new V3(100, Profondeur / 4, Hauteur - 100), 100, new Couleur(1, 1, 1));
            Sphere3D_Lumiere SphereLumiere3 = new Sphere3D_Lumiere(new V3(870, 200, 60), 60, new Couleur(1, 1, 1));
            Sphere3D SphereVerte2 = new Sphere3D(new V3(780, 250, 50), 50, Texture.s_Green);
            Parallelogramme3D LumierePlafond2 = new Parallelogramme3D_Lumiere(new V3((Largeur / 2) - 200, (Profondeur / 2) - 200, Hauteur - 0.1f), new V3(0f, 400, 0f), new V3(400, 0f, 0f), new Couleur(1, 1, 1));
            Sphere3D SphereRouge2 = new Sphere3D(new V3(120, Profondeur - 60, 60), 60, Texture.s_Red);
            Parallelepipede3D CubeViolet2 = new Parallelepipede3D(new V3(50, Profondeur / 2.1f, 0f), new V3(0f, 100, 0f), new V3(100, 0f, 0f), new V3(0f, 0f, 100), Texture.s_Purple, Texture.s_Purple);
            Parallelepipede3D Parrallelepipede2 = new Parallelepipede3D(new V3(10, Profondeur / 2.5f, 0f), new V3(0f, 100, 0f), new V3(100, 0f, 0f), new V3(0f, 0f, 400), Texture.s_Purple);
            
            //Scene2.Add(SolLumiere2);
            Scene2.Add(Sol2);
            Scene2.Add(MurGauche2);
            Scene2.Add(MurFond2);
            Scene2.Add(MurDroit2);
            Scene2.Add(Plafond2);
            //Scene2.Add(PlafondLumiere2);
            //Scene2.Add(SphereStone2);
            Scene2.Add(SphereOr2);
            //Scene2.Add(SphereLumiere2);
            //Scene2.Add(SphereLumiere3);
            Scene2.Add(SphereVerte2);
            Scene2.Add(SphereRouge2);
            Scene2.Add(LumierePlafond2);
            foreach (Parallelogramme3D P in Parrallelepipede2.m_Faces)
            {
                Scene2.Add(P);
            }

            /* SCENE3 */
            Largeur = BitmapEcran.s_LargeurEcran - 200;
            Milieu = new V3(BitmapEcran.s_LargeurEcran / 2, 0f, 0f);
            MilieuStart = Milieu - new V3(Largeur / 2, 0f, 0f);
            MilieuEnd = Milieu + new V3(Largeur / 2, 0f, 0f);
            Parallelogramme3D Sol3 = new Parallelogramme3D(MilieuStart, new V3(Largeur, 0f, 0f), new V3(0f, Profondeur, 0f), Texture.s_White);
            Parallelogramme3D MurGauche3 = new Parallelogramme3D(MilieuStart, new V3(0f, Profondeur, 0f), new V3(0f, 0f, Hauteur), Texture.s_Red);
            Parallelogramme3D MurDroit3 = new Parallelogramme3D(MilieuEnd, new V3(0f, 0f, Hauteur), new V3(0f, Profondeur, 0f), Texture.s_Green);
            Parallelogramme3D MurFond3 = new Parallelogramme3D(MilieuStart + new V3(0f, Profondeur, 0f), new V3(Largeur, 0f, 0f), new V3(0f, 0f, Hauteur), Texture.s_White);
            Parallelogramme3D Plafond3 = new Parallelogramme3D(MilieuStart + new V3(0f, 0f, Hauteur), new V3(0f, Profondeur, 0f), new V3(Largeur, 0f, 0f), Texture.s_White);
            Parallelepipede3D Parrallelepipede3 = new Parallelepipede3D(MilieuStart + new V3(10, Profondeur / 2.5f, 0f), new V3(0f, 100, 0f), new V3(100, 0f, 0f), new V3(0f, 0f, 400), Texture.s_Purple);
            Sphere3D SphereOr3 = new Sphere3D(Milieu + new V3(0, Profondeur - 100, 300), 100, gold, gold);
            int sizeLumiere = 200;
            Parallelogramme3D LumierePlafond3 = new Parallelogramme3D_Lumiere(Milieu + new V3(-sizeLumiere / 2, (Profondeur / 2) - sizeLumiere / 2, Hauteur - 1), new V3(0f, sizeLumiere, 0f), new V3(sizeLumiere, 0f, 0f), new Couleur(1, 1, 1));
            foreach (Parallelogramme3D P in Parrallelepipede3.m_Faces)
            {
                Scene3.Add(P);
            }
            Scene3.Add(Sol3);
            Scene3.Add(MurGauche3);
            Scene3.Add(MurFond3);
            Scene3.Add(MurDroit3);
            Scene3.Add(Plafond3);
            Scene3.Add(SphereOr3);
            Scene3.Add(LumierePlafond3);

            /* SCENE4 */
            Hauteur = BitmapEcran.s_HauteurEcran;
            Largeur = BitmapEcran.s_LargeurEcran;
            Profondeur = 1000;
            Parallelogramme3D Sol4 = new Parallelogramme3D(new V3(0, 0, 0), new V3(Largeur, 0, 0), new V3(0, Profondeur, 0), Texture.s_White);
            Parallelogramme3D MurGauche4 = new Parallelogramme3D(new V3(0, 0, 0), new V3(0, Profondeur, 0), new V3(0, 0, Hauteur), Texture.s_Red);
            Parallelogramme3D MurFond4 = new Parallelogramme3D(new V3(0, Profondeur, 0), new V3(Largeur, 0, 0), new V3(0, 0, Hauteur), Texture.s_Blue);
            Parallelogramme3D MurDroit4 = new Parallelogramme3D(new V3(Largeur, Profondeur, 0), new V3(0, -Profondeur, 0), new V3(0, 0, Hauteur), Texture.s_Green);
            Parallelogramme3D Plafond4 = new Parallelogramme3D(new V3(0, 0, Hauteur), new V3(0, Profondeur, 0), new V3(Largeur, 0, 0), Texture.s_Purple, Texture.s_Purple);
            Sphere3D SphereOr4 = new Sphere3D(new V3(300, Profondeur - 100, 300), 100, gold, bump_texture);
            Sphere3D SphereStone4 = new Sphere3D(new V3(800, 200, 400), 70, stone2, bump_texture);
            Sphere3D_Lumiere SphereLumiere4 = new Sphere3D_Lumiere(new V3(100, Profondeur / 4, Hauteur - 100), 100, new Couleur(1, 1, 1));
            Sphere3D_Lumiere SphereLumiere5 = new Sphere3D_Lumiere(new V3(870, 200, 60), 60,new Couleur(1, 1, 1));
            Sphere3D SphereVerte4 = new Sphere3D(new V3(780, 250, 50), 50, Texture.s_Green, bump_texture);
            Parallelogramme3D LumierePlafond4 = new Parallelogramme3D_Lumiere(new V3((Largeur / 2) - 200, (Profondeur / 2) - 200, Hauteur - 1), new V3(0, 400, 0), new V3(400, 0, 0), new Couleur(1f, 1f, 1f));
            Sphere3D SphereRouge4 = new Sphere3D(new V3(120, Profondeur - 60, 60), 60, Texture.s_Red, bump_texture);
            Parallelepipede3D CubeViolet4 = new Parallelepipede3D(new V3(50, Profondeur / 2.1f, 0), new V3(0, 100, 0), new V3(100, 0, 0), new V3(0, 0, 100), Texture.s_Purple, Texture.s_Purple);
            Scene4.Add(Sol4);
            Scene4.Add(MurGauche4);
            Scene4.Add(MurFond4);
            Scene4.Add(MurDroit4);
            Scene4.Add(Plafond4);
            Scene4.Add(SphereStone4);
            Scene4.Add(SphereOr4);
            //Scene4.Add(SphereLumiere4);
            //Scene4.Add(SphereLumiere5);
            Scene4.Add(SphereVerte4);
            Scene4.Add(SphereRouge4);
            Scene4.Add(LumierePlafond4);
            foreach (Parallelogramme3D P in CubeViolet4.m_Faces)
            {
                Scene4.Add(P);
            }

            if (Global.render_mode == Global.RenderMode.SIMPLE)
            {
                BitmapEcran.s_Objets = Scene1;
            }
            else
            {
                BitmapEcran.s_Objets = Scene4;
            }
            /* Fonction d'affichage */
            BitmapEcran.DrawAll();
        }
    }
}
