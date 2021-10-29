using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Parallelepipede3D : Objet3D
    {
        public float Longueur { get; set; }
        public float Largeur { get; set; }
        public float Hauteur { get; set; }

        public V3 Origine { get; set; }

        public Parallelepipede3D(V3 centre, float longueur, float largeur, float hauteur, Couleur couleur, Lumiere lumiere, float coefficient_diffus = 0.006f) : base(centre, couleur, lumiere, coefficient_diffus)
        {
            Longueur = longueur;
            Largeur = largeur;
            Hauteur = hauteur;
            Origine = centre - new V3((1 / 2) * longueur, (1 / 2) * largeur, (1 / 2) * hauteur);
        }

        public override void Draw(float pas)
        {
            for (float x = Origine.x; x < Origine.x+Longueur; x += pas)
            {
                for (float y = Origine.y; y < Origine.y+Largeur; y += pas)
                {
                    for(float z = Origine.z; z < Origine.z+Hauteur; z+=pas)
                    {
                        // calcul des coordoonées dans la scène 3D
                        float x3D = x;
                        float y3D = y;
                        float z3D = z;
                        V3 normalizedPixelNormal = (new V3(x3D - this.CentreObjet.x, y3D - this.CentreObjet.y, z3D - this.CentreObjet.z));
                        normalizedPixelNormal.Normalize();

                        // projection orthographique => repère écran

                        int x_ecran = (int)(x3D);
                        int y_ecran = (int)(z3D);

                        BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleurDiffuse(normalizedPixelNormal));
                    }

                }
            }
        }
    }
}
