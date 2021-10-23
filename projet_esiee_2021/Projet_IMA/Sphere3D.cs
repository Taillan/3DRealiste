using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere3D
    {

        private V3 CentreSphere;
        private float Rayon;
        private Couleur CSphere;

        public Sphere3D(V3 Centre, float R)
        {
            this.CentreSphere = Centre;
            this.Rayon = R;
            this.CSphere = Couleur.Red;
        }
        public Sphere3D(V3 Centre, float R, Couleur C)
        {
            this.CentreSphere = Centre;
            this.Rayon = R;
            this.CSphere = C;
        }

        public Sphere3D(float x, float y,float z, float R)
        {
            this.CentreSphere = new V3(x, y, z);
            this.Rayon = R;
            this.CSphere = Couleur.Red;
        }

        public float getRayon()
        {
            return this.Rayon;
        }
        public V3 getCentreSphere()
        {
            return this.CentreSphere;
        }
        public Couleur getCouleur()
        {
            return this.CSphere;
        }

        public void setRayon(float R)
        {
            this.Rayon = R;
        }
        public void setCentreSphere(V3 Centre)
        {
            this.CentreSphere = Centre;
        }
        public void setCouleur(Couleur Couleur)
        {
            this.CSphere = Couleur;
        }



        public void DrawSphere(float pas, Lumiere lumiere)
        {
            V3 normalizedLumiereDirection = lumiere.getDirectionLumiere();
            normalizedLumiereDirection.Normalize();

            Couleur nvCouleurAmbiante = new Couleur(this.CSphere * lumiere.getCouleur());

            for (float u = 0; u < 2 * IMA.PI; u += pas){  // echantillonage fnt paramétrique
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += pas)
                {
                    // calcul des coordoonées dans la scène 3D
                    float x3D = Rayon * IMA.Cosf(v) * IMA.Cosf(u) + this.CentreSphere.x;
                    float y3D = Rayon * IMA.Cosf(v) * IMA.Sinf(u) + this.CentreSphere.y;
                    float z3D = Rayon * IMA.Sinf(v) + this.CentreSphere.z;

                    V3 normalizedPixelNormal = (new V3(x3D - this.CentreSphere.x, y3D - this.CentreSphere.y, z3D - this.CentreSphere.z));
                    normalizedPixelNormal.Normalize();

                    float NL = normalizedPixelNormal * normalizedLumiereDirection;
                    Couleur nvCouleurDiffuse = nvCouleurAmbiante * 0;
                    if (NL > 0)
                    {
                        nvCouleurDiffuse = nvCouleurAmbiante * (normalizedPixelNormal * normalizedLumiereDirection) * 0.009f;
                    }

                    // projection orthographique => repère écran

                    int x_ecran = (int)(x3D);
                    int y_ecran = (int)(z3D);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, nvCouleurDiffuse);
                }
            }
        }
    }
}
