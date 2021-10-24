using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere3D : Objet3D
    {
        public float Rayon { get; set; }

        public Sphere3D(V3 centre, float rayon, Couleur couleur, Lumiere lumiere, float coefficient_diffus = 0.006f) : base(centre, couleur, lumiere, coefficient_diffus)
        {
            this.Rayon = rayon;
        }

        public override void Draw(float pas=0.005f)
        {
            for (float u = 0; u < 2 * IMA.PI; u += pas)
            {  // echantillonage fnt paramétrique
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += pas)
                {
                    // calcul des coordoonées dans la scène 3D
                    float x3D = Rayon * IMA.Cosf(v) * IMA.Cosf(u) + this.CentreObjet.x;
                    float y3D = Rayon * IMA.Cosf(v) * IMA.Sinf(u) + this.CentreObjet.y;
                    float z3D = Rayon * IMA.Sinf(v) + this.CentreObjet.z;

                    // projection orthographique => repère écran

                    int x_ecran = (int)(x3D);
                    int y_ecran = (int)(z3D);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleurDiffuse(x3D,y3D,z3D));
                }
            }
        }
    }
}
