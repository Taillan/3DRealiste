using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Objet3D
    {
        protected V3 m_CentreObjet { get; set; }
        protected Couleur m_CouleurObjet { get; set; }
        protected Couleur m_CouleurAmbiante { get; set; }
        protected Lumiere m_Lumiere { get; set; }
        protected Texture m_texture { get; set; }
        protected float m_CoefficientDiffus { get; set; }
        
        public Objet3D(V3 centre, Couleur couleur, Lumiere lumiere, float coefficient_diffus)
        {
            m_CentreObjet = centre;
            m_CouleurObjet = couleur;
            m_Lumiere = lumiere;
            m_CoefficientDiffus = coefficient_diffus;
          //  CouleurAmbiante = new Couleur(this.CouleurObjet * Lumiere.Couleur);
            m_texture = new Texture("brick01.jpg");
        }

        public abstract void Draw(float pas);

        public Couleur getCouleurDiffuse(V3 normalizedPixelNormal, float x_ecran, float y_ecran)
        {
            m_CouleurObjet = m_texture.LireCouleur(x_ecran, y_ecran);
            m_CouleurAmbiante  = new Couleur(this.m_CouleurObjet * m_Lumiere.m_Couleur);
            float NL = normalizedPixelNormal * m_Lumiere.m_NormalizedDirection;
            Couleur nvCouleurDiffuse = m_CouleurAmbiante  * 0.0008f;
            if (NL > 0)
            {
                nvCouleurDiffuse += m_CouleurAmbiante  * (normalizedPixelNormal * m_Lumiere.m_NormalizedDirection) * m_CoefficientDiffus;//0.006f
            }
            return nvCouleurDiffuse;
        }

        public Couleur getCouleurSpeculaire(float x3D,float y3D,float z3D)
        {
            V3 normalizedPixelNormal = (new V3(x3D - this.m_CentreObjet.x, y3D - this.m_CentreObjet.y, z3D - this.m_CentreObjet.z));
            normalizedPixelNormal.Normalize();
            V3 N = new V3(x3D-this.m_CentreObjet.x, y3D-this.m_CentreObjet.y, z3D-this.m_CentreObjet.z);
            V3 L = m_Lumiere.m_Direction;
            /*L = Lumiere.NormalizedDirection;
            N = normalizedPixelNormal;*/
            V3 R = 2*N*(N*L)-L;
            R.Normalize();
            V3 CameraPosition = new V3((BitmapEcran.GetWidth() / 2), ((float)-1.5 * BitmapEcran.GetWidth()), (BitmapEcran.GetHeight() / 2));
            V3 PixelPosition = new V3(x3D, y3D, z3D);
            V3 D = (CameraPosition - PixelPosition);
            D.Normalize();
            return m_Lumiere.m_Couleur * (float)Math.Pow(R * D, 200);
        }
    }
}
