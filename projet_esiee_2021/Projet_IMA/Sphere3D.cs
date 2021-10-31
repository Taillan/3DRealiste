using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere3D : Objet3D
    {
        public float m_Rayon { get; set; }

        public Sphere3D(V3 centre, float rayon,  Lumiere lumiere,Texture texture, Texture bump_texture, float coefficient_diffus = .006f, float coefficient_speculaire = .0001f, float puissance_speculaire=60, float coefficient_bumpmap=.005f) : base(centre, lumiere, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap)
        {
            this.m_Rayon = rayon;
        }

        public void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv)
        {
            float dxdu = m_Rayon * IMA.Cosf(v) * (-IMA.Sinf(u));
            float dxdv = m_Rayon * (-IMA.Sinf(v)) * IMA.Cosf(v);

            float dydu = m_Rayon * IMA.Cosf(v) * IMA.Cosf(u);
            float dydv = m_Rayon * (-IMA.Sinf(v)) * IMA.Sinf(u);

            float dzdu = 0;
            float dzdv = m_Rayon * IMA.Cosf(v);

            dMdu = new V3(dxdu, dydu, dzdu);
            dMdv = new V3(dxdv, dydv, dzdv);
        }

        public override void Draw(float pas=.005f)
        {
            for (float u = 0; u < 2 * IMA.PI; u += pas)
            {
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += pas)
                {
                    // calcul des coordoonées dans la scène 3D
                    float x3D = m_Rayon * IMA.Cosf(v) * IMA.Cosf(u) + this.m_CentreObjet.x;
                    float y3D = m_Rayon * IMA.Cosf(v) * IMA.Sinf(u) + this.m_CentreObjet.y;
                    float z3D = m_Rayon * IMA.Sinf(v) + this.m_CentreObjet.z;
                    V3 PixelPosition = new V3(x3D, y3D, z3D);
                    
                    getDerivedCoords(u, v, out V3 dMdu, out V3 dMdv);
                    
                    // projection orthographique => repère écran

                    int x_ecran = (int)(PixelPosition.x);
                    int y_ecran = (int)(PixelPosition.z);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(PixelPosition,u,v,dMdu,dMdv));
                }
            }
        }
    }
}
