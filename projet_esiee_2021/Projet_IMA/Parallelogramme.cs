using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Parallelogramme : Objet3D
    {
        public V3 m_Longueur { get; set; }
        public V3 m_Largeur { get; set; }
        public V3 m_Hauteur { get; set; }
        public V3 m_Origine { get; set; }

        public Parallelogramme(V3 centre, V3 longueur, V3 largeur, Lumiere key_lumiere, Lumiere fill_lumiere, Texture texture, Texture bump_texture, float coefficient_diffus = 0.006f, float coefficient_speculaire = .0001f, float puissance_speculaire = 60, float coefficient_bumpmap = .005f) : base(centre, key_lumiere, fill_lumiere, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap)
        {
            m_Longueur = longueur;
            m_Largeur = largeur;
            m_Origine = centre - (1 / 2) * longueur + (1 / 2) * largeur;
        }

        public void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv)
        {
            float dxdu = m_Longueur.x;
            float dxdv = m_Largeur.x;

            float dydu = m_Longueur.y;
            float dydv = m_Largeur.y;

            float dzdu = m_Longueur.z;
            float dzdv = m_Largeur.z;

            dMdu = new V3(dxdu, dydu, dzdu);
            dMdv = new V3(dxdv, dydv, dzdv);
        }

        public override void Draw(float pas = .005f)
        {
            for (float u = 0; u < 1; u += pas)
            {
                for (float v = 0; v < 1; v += pas)
                {
                    float x3D = m_Origine.x + u * m_Longueur.x + v * m_Largeur.x;
                    float y3D = m_Origine.y + u * m_Longueur.y + v * m_Largeur.y;
                    float z3D = m_Origine.z + u * m_Longueur.z + v * m_Largeur.z;
                    V3 PixelPosition = new V3(x3D, y3D, z3D);

                    getDerivedCoords(u, v, out V3 dMdu, out V3 dMdv);

                    // projection orthographique => repère écran

                    int x_ecran = (int)(PixelPosition.x);
                    int y_ecran = (int)(PixelPosition.y);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(this.m_FillLumiere, PixelPosition, u, v, dMdu, dMdv) + getCouleur(this.m_KeyLumiere, PixelPosition, u, v, dMdu, dMdv));
                }
            }
        }
    }
}
