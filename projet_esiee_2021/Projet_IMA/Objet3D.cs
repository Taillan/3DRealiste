using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Objet3D
    {
        protected V3 m_CentreObjet { get; set; } 
        protected Lumiere m_KeyLumiere { get; set; }
        protected Lumiere m_FillLumiere { get; set; }
        protected Texture m_Texture { get; set; }
        protected Texture m_BumpTexture { get; set; }
        protected float m_CoefficientDiffus { get; set; }
        protected float m_CoefficientSpeculaire { get; set; }
        protected float m_PuissanceSpeculaire { get; set; }
        protected float m_CoefficientBumpMap { get; set; }

        public Objet3D(V3 centre, Lumiere key_lumiere, Lumiere fill_lumiere, Texture texture, Texture bump_texture, float coefficient_diffus, float coefficient_speculaire, float puissance_speculaire, float coefficient_bumpmap)
        {
            m_CentreObjet = centre;
            m_KeyLumiere = key_lumiere;
            m_FillLumiere = fill_lumiere;
            m_CoefficientDiffus = coefficient_diffus;
            m_CoefficientSpeculaire = coefficient_speculaire;
            m_PuissanceSpeculaire = puissance_speculaire;
            m_Texture = texture;
            m_BumpTexture = bump_texture;
            m_CoefficientBumpMap = coefficient_bumpmap;
        }

        public abstract void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv);
        public abstract void Draw(float pas);


        public Couleur getCouleurAmbiante(Lumiere lumiere, float x_ecran, float y_ecran)
        {
            return m_Texture.LireCouleur(x_ecran, y_ecran) * lumiere.m_Couleur;
        }

        public Couleur getLowCouleurAmbiante(Lumiere lumiere, float x_ecran, float y_ecran)
        {
            return getCouleurAmbiante(lumiere, x_ecran,y_ecran) * .0008f;
        }

        public Couleur getCouleurDiffuse(Lumiere lumiere, V3 normalizedPixelNormal, float x_ecran, float y_ecran)
        {
            float cosAlpha = normalizedPixelNormal * lumiere.m_NormalizedDirection;
            if (cosAlpha > 0)
            {
                return getCouleurAmbiante(lumiere, x_ecran,y_ecran)  * (cosAlpha) * m_CoefficientDiffus;
            }
            else
            {
                return Couleur.m_Void;
            }
        }

        public Couleur getCouleurSpeculaire(Lumiere lumiere, V3 PixelPosition, V3 N, float x_ecran, float y_ecran)
        {
            V3 L = lumiere.m_Direction;
            V3 R = 2*N*(N*L)-L;
            V3 D = (BitmapEcran.GetCameraPosition() - PixelPosition);
            R.Normalize();
            D.Normalize();
            float RD = R * D;
            if ((RD) > 0)
            {
                return lumiere.m_Couleur * getCouleurAmbiante(lumiere, x_ecran, y_ecran) * m_CoefficientSpeculaire * (float)Math.Pow(RD, m_PuissanceSpeculaire);
            }
            else
            {
                return Couleur.m_Void;
            }
        }

        public Couleur getCouleur(Lumiere lumiere, V3 PixelPosition, float u, float v)
        {
            V3 N = getBumpedNormal(PixelPosition,u,v);
            //N.Normalize();
            Couleur Ambiant = getLowCouleurAmbiante(lumiere, u, v);
            Couleur Diffus = getCouleurDiffuse(lumiere, N, u, v);
            if (Diffus != Couleur.m_Void)
            {
                Couleur Speculaire = getCouleurSpeculaire(lumiere, PixelPosition, N, u, v);
                return Ambiant + Diffus + Speculaire;
            }
            else
            {
                return Ambiant + Diffus;
            }
        }

        public V3 getBumpedNormal(V3 PixelPosition, float u, float v)
        {
            V3 N = (PixelPosition - m_CentreObjet);
            N.Normalize();

            float K = m_CoefficientBumpMap;

            getDerivedCoords(u, v, out V3 dMdu, out V3 dMdv);

            this.m_BumpTexture.Bump(u, v, out float dhdu, out float dhdv);
            V3 T2 = dMdu ^ (N * dhdv);
            V3 T3 = (N * dhdu) ^ dMdv;

            return N + K * (T2 + T3);
        }
    }
}
