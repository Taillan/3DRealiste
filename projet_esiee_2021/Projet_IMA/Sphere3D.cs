using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere3D : Objet3D
    {
        public float m_Rayon { get; set; }

        public Sphere3D(V3 centre, float rayon,  Lumiere lumiere,Texture texture, float coefficient_diffus = 0.006f) : base(centre, lumiere, texture, coefficient_diffus)
        {
            this.m_Rayon = rayon;
        }

        public override void Draw(float pas=.005f)
        {
            for (float u = 0; u < 2 * IMA.PI; u += pas)
            {  // echantillonage fnt paramétrique
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += pas)
                {
                    // calcul des coordoonées dans la scène 3D
                    float x3D = m_Rayon * IMA.Cosf(v) * IMA.Cosf(u) + this.m_CentreObjet.x;
                    float y3D = m_Rayon * IMA.Cosf(v) * IMA.Sinf(u) + this.m_CentreObjet.y;
                    float z3D = m_Rayon * IMA.Sinf(v) + this.m_CentreObjet.z;

                    V3 PixelPosition = new V3(x3D, y3D, z3D);
                    V3 NormalizedPixelNormal = PixelPosition - m_CentreObjet;
                    NormalizedPixelNormal.Normalize();

                    // projection orthographique => repère écran

                    int x_ecran = (int)(x3D);
                    int y_ecran = (int)(z3D);


                    float u1 = (u) / (2 * IMA.PI);
                    float v1 = (v) / (IMA.PI);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getLowCouleurAmbiante(u1,-v1) + getCouleurDiffuse(NormalizedPixelNormal, u1, -v1) + getCouleurSpeculaire(PixelPosition, NormalizedPixelNormal,u1,-v1));
                }
            }
        }
    }
}
