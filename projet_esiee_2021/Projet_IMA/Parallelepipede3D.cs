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

        public Parallelepipede3D(V3 centre, V3 longueur, V3 largeur, V3 hauteur, Lumiere lumiere, Texture texture, float coefficient_diffus = 0.006f) : base(centre, lumiere, texture, coefficient_diffus)
        {
            m_Longueur = longueur;
            m_Largeur = largeur;
            m_Hauteur = hauteur;
            m_Origine = centre - (1 / 2) * longueur + (1 / 2) * largeur + (1 / 2) * hauteur;
        }

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
                        V3 normalizedPixelNormal = P3D;
                        normalizedPixelNormal.Normalize();

                        // projection orthographique => repère écran

                        int x_ecran = (int)(P3D.x);
                        int y_ecran = (int)(P3D.z);

                        float u1 = (P3D.x) / (2 * IMA.PI);
                        float v1 = (P3D.y) / (2 * IMA.PI);

                        BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleurDiffuse(normalizedPixelNormal, u1, -v1));
                    }

                }
            }
        }
    }
}
