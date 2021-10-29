using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Parallelepipede3D : Objet3D
    {
        public float m_Longueur { get; set; }
        public float m_Largeur { get; set; }
        public float m_Hauteur { get; set; }

        public V3 m_Origine { get; set; }

        public Parallelepipede3D(V3 centre, float longueur, float largeur, float hauteur, Couleur couleur, Lumiere lumiere, float coefficient_diffus = 0.006f) : base(centre, couleur, lumiere, coefficient_diffus)
        {
            m_Longueur = longueur;
            m_Largeur = largeur;
            m_Hauteur = hauteur;
            m_Origine = centre - new V3((1 / 2) * longueur, (1 / 2) * largeur, (1 / 2) * hauteur);
        }

        public override void Draw(float pas)
        {
            for (float x = m_Origine.x; x < m_Origine.x+m_Longueur; x += pas)
            {
                for (float y = m_Origine.y; y < m_Origine.y+m_Largeur; y += pas)
                {
                    for(float z = m_Origine.z; z < m_Origine.z+m_Hauteur; z+=pas)
                    {
                        // calcul des coordoonées dans la scène 3D
                        float x3D = x;
                        float y3D = y;
                        float z3D = z;
                        V3 normalizedPixelNormal = (new V3(x3D - this.m_CentreObjet.x, y3D - this.m_CentreObjet.y, z3D - this.m_CentreObjet.z));
                        normalizedPixelNormal.Normalize();

                        // projection orthographique => repère écran

                        int x_ecran = (int)(x3D);
                        int y_ecran = (int)(z3D);

                        float u1 = (x) / (2 * IMA.PI);
                        float v1 = (y) / (2 * IMA.PI);

                        BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleurDiffuse(normalizedPixelNormal, u1, -v1));
                    }

                }
            }
        }
    }
}
