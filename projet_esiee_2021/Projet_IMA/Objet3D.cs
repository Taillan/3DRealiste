using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Objet3D
    {
        protected V3 CentreObjet { get; set; }
        protected Couleur CouleurObjet { get; set; }
        protected Couleur CouleurAmbiante{ get; set; }
        protected Lumiere Lumiere { get; set; }
        protected float CoefficientDiffus { get; set; }
        
        public Objet3D(V3 centre, Couleur couleur, Lumiere lumiere, float coefficient_diffus)
        {
            CentreObjet = centre;
            CouleurObjet = couleur;
            Lumiere = lumiere;
            CoefficientDiffus = coefficient_diffus;
            CouleurAmbiante = new Couleur(this.CouleurObjet * Lumiere.Couleur);
        }

        public abstract void Draw(float pas);

        public Couleur getCouleurDiffuse(V3 normalizedPixelNormal)
        {
            float NL = normalizedPixelNormal * Lumiere.NormalizedDirection;
            Couleur nvCouleurDiffuse = CouleurAmbiante * 0.0008f;
            if (NL > 0)
            {
                nvCouleurDiffuse += CouleurAmbiante * (normalizedPixelNormal * Lumiere.NormalizedDirection) * CoefficientDiffus;//0.006f
            }
            return nvCouleurDiffuse;
        }

        public Couleur getCouleurSpeculaire(float x3D,float y3D,float z3D)
        {
            V3 normalizedPixelNormal = (new V3(x3D - this.CentreObjet.x, y3D - this.CentreObjet.y, z3D - this.CentreObjet.z));
            normalizedPixelNormal.Normalize();
            V3 N = new V3(x3D-this.CentreObjet.x, y3D-this.CentreObjet.y, z3D-this.CentreObjet.z);
            V3 L = Lumiere.Direction;
            /*L = Lumiere.NormalizedDirection;
            N = normalizedPixelNormal;*/
            V3 R = 2*N*(N*L)-L;
            R.Normalize();
            V3 CameraPosition = new V3((BitmapEcran.GetWidth() / 2), ((float)-1.5 * BitmapEcran.GetWidth()), (BitmapEcran.GetHeight() / 2));
            V3 PixelPosition = new V3(x3D, y3D, z3D);
            V3 D = (CameraPosition - PixelPosition);
            D.Normalize();
            return Lumiere.Couleur * (float)Math.Pow(R * D, 200);
        }
    }
}
