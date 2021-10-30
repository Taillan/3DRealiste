using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Objet3D
    {
        protected V3 m_CentreObjet { get; set; }
        protected Lumiere m_Lumiere { get; set; }
        protected Texture m_texture { get; set; }
        protected float m_CoefficientDiffus { get; set; }
        protected float m_CoefficientSpeculaire { get; set; }

        protected float m_PuissanceSpeculaire { get; set; }

        public Objet3D(V3 centre, Lumiere lumiere, Texture texture, float coefficient_diffus)
        {
            m_CentreObjet = centre;
            m_Lumiere = lumiere;
            m_CoefficientDiffus = coefficient_diffus;
            m_CoefficientSpeculaire = .0008f;
            m_PuissanceSpeculaire = 200;
            m_texture = texture;
        }

        public abstract void Draw(float pas);

        public Couleur getCouleurAmbiante(float x_ecran, float y_ecran)
        {
            return m_texture.LireCouleur(x_ecran, y_ecran) * m_Lumiere.m_Couleur;
        }

        public Couleur getLowCouleurAmbiante(float x_ecran, float y_ecran)
        {
            return getCouleurAmbiante(x_ecran,y_ecran) * .0008f;
        }

        public Couleur getCouleurDiffuse(V3 normalizedPixelNormal, float x_ecran, float y_ecran)
        {
            float cosAlpha = normalizedPixelNormal * m_Lumiere.m_NormalizedDirection;
            if (cosAlpha > 0)
            {
                return getCouleurAmbiante(x_ecran,y_ecran)  * (cosAlpha) * m_CoefficientDiffus;
            }
            else
            {
                return Couleur.m_Void;
            }
        }

        public Couleur getCouleurSpeculaire(V3 PixelPosition, V3 N, float x_ecran, float y_ecran)
        {
            V3 L = m_Lumiere.m_Direction;
            V3 R = 2*N*(N*L)-L;
            R.Normalize();
            V3 D = (BitmapEcran.GetCameraPosition() - PixelPosition);
            D.Normalize();
            return m_Lumiere.m_Couleur * getCouleurAmbiante(x_ecran,y_ecran) * m_CoefficientSpeculaire * (float)Math.Pow(R * D, m_PuissanceSpeculaire);
        }
    }
}
