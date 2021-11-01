using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Parallelepipede3D : Objet3D
    {
        public V3 m_Longueur { get; set; }
        public V3 m_Largeur { get; set; }
        public V3 m_Hauteur { get; set; }
        public V3 m_Origine { get; set; }

        /// <summary>
        /// Constructeur d'un Parallelepipede 3D
        /// </summary>
        /// <param name="centre">Centre de cet objet</param>
        /// <param name="longueur">Longueur de l'objet</param>
        /// <param name="largeur">Largeur de l'objet</param>
        /// <param name="hauteur">Hauteur de l'objet</param>
        /// <param name="lumiere">Lumiere applique à l'objet</param>
        /// <param name="texture">Texture applique à l'objet</param>
        /// <param name="bump_texture"></param>
        /// <param name="coefficient_diffus"></param>
        /// <param name="coefficient_speculaire"></param>
        /// <param name="puissance_speculaire"></param>
        /// <param name="coefficient_bumpmap"></param>
        public Parallelepipede3D(V3 centre, V3 longueur, V3 largeur, V3 hauteur, Lumiere lumiere, Texture texture, Texture bump_texture ,float coefficient_diffus = 0.006f, float coefficient_speculaire = .0001f, float puissance_speculaire = 60, float coefficient_bumpmap=.005f) : base(centre, lumiere, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap)
        {
            m_Longueur = longueur;
            m_Largeur = largeur;
            m_Hauteur = hauteur;
            m_Origine = centre - (1 / 2) * longueur + (1 / 2) * largeur + (1 / 2) * hauteur;
        }

        /// <summary>
        /// Fonction décrivant comment dessiner un Parallelepipede3D
        /// </summary>
        /// <param name="pas">Ecart entre chaque point tracé à l'écran</param>
        public override void Draw(float pas=.005f)
        {
            for (float i_longueur = 0; i_longueur < 1; i_longueur += pas)
            {
                for (float i_largeur = 0; i_largeur < 1; i_largeur += pas)
                {
                    for(float i_hauteur = 0; i_hauteur < 1; i_hauteur+=pas)
                    {
                        // calcul des coordoonées dans la scène 3D
                        V3 P3D = m_Origine + i_longueur * m_Longueur + i_largeur * m_Largeur + i_hauteur * m_Hauteur;

                        // projection orthographique => repère écran

                        int x_ecran = (int)(P3D.x);
                        int y_ecran = (int)(P3D.z);

                        float u = (P3D.x) / (i_longueur);
                        float v = (P3D.y) / (i_largeur);

                        //BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(P3D, u, v));
                    }

                }
            }
        }
    }
}
