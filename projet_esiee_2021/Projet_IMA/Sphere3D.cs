using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere3D : Objet3D
    {
        private float m_Rayon { get; set; }

        public Sphere3D(V3 centre, float rayon,  Lumiere key_lumiere, Lumiere fill_lumiere,Texture texture, Texture bump_texture, float coefficient_diffus = .005f, float coefficient_speculaire = .00005f, float puissance_speculaire=60, float coefficient_bumpmap=.005f, float pas=.005f) : base(centre, key_lumiere, fill_lumiere, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas)
        {
            this.m_Rayon = rayon;
        }

        protected override V3 getCoords(float u, float v)
        {
            float x3D = m_Rayon * IMA.Cosf(v) * IMA.Cosf(u) + this.m_CentreObjet.x;
            float y3D = m_Rayon * IMA.Cosf(v) * IMA.Sinf(u) + this.m_CentreObjet.y;
            float z3D = m_Rayon * IMA.Sinf(v) + this.m_CentreObjet.z;
            return new V3(x3D, y3D, z3D);
        }

        protected override void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv)
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

        public override bool testIntersection(V3 origineRayon, V3 directionRayon, out float t, out V3 PixelPosition, out float u, out float v)
        {
            V3 Ro = origineRayon;
            V3 Rd = directionRayon;
            V3 C = m_CentreObjet;
            float A = Rd * Rd;
            float B = 2*Ro * Rd - 2 * Rd * C;
            float r = m_Rayon;
            float D = (Ro * Ro) - (2 * Ro * C) + (C * C) - (r * r);
            float DELTA = (B * B) - (4 * A * D);
            float t1 = (float)(-B - Math.Sqrt(DELTA)) / (2 * A);
            float t2 = (float)(-B + Math.Sqrt(DELTA)) / (2 * A);
            if (t1 > 0 && t2 > 0)
            {
                PixelPosition = Ro + t1 * Rd;
                t = t1;
                IMA.Invert_Coord_Spherique(PixelPosition, r, out u, out v);
                return true;
            } 
            else if (t1 < 0 && t2 > 0)
            {
                PixelPosition = Ro + t2 * Rd;
                t = t2;
                IMA.Invert_Coord_Spherique(PixelPosition, r, out u, out v);
                return true;
            }
            else 
            {
                PixelPosition = new V3(0, 0, 0);
                IMA.Invert_Coord_Spherique(PixelPosition, r, out u, out v);
                t = 0;
                return false;
            }
        }

        protected override V3 getNormal(V3 PixelPosition)
        {
            return (PixelPosition - m_CentreObjet);
        }

        public override void Draw()
        {
            for (float u = 0; u < 2 * IMA.PI; u += m_Pas)
            {
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += m_Pas)
                {
                    // calcul des coordoonées dans la scène 3D
                    V3 PixelPosition = getCoords(u,v);

                    // projection orthographique => repère écran
                    int x_ecran = (int)(PixelPosition.x);
                    int y_ecran = (int)(PixelPosition.z);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(PixelPosition,u,v));
                }
            }
        }
    }
}
