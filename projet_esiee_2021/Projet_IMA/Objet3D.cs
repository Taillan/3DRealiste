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

        public Couleur getCouleurDiffuse(float x3D, float y3D, float z3D)
        {
            V3 normalizedPixelNormal = (new V3(x3D - this.CentreObjet.x, y3D - this.CentreObjet.y, z3D - this.CentreObjet.z));
            normalizedPixelNormal.Normalize();

            float NL = normalizedPixelNormal * Lumiere.NormalizedDirection;
            Couleur nvCouleurDiffuse = CouleurAmbiante * 0.0008f;
            if (NL > 0)
            {
                nvCouleurDiffuse += CouleurAmbiante * (normalizedPixelNormal * Lumiere.NormalizedDirection) * CoefficientDiffus;//0.006f
            }
            return nvCouleurDiffuse;
        }
    }
}
