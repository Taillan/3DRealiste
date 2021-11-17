using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Objet3D
    {
        #region Attributs
        protected V3 m_CentreObjet { get; set; } 
        private Lumiere m_KeyLumiere { get; set; }
        private Lumiere m_FillLumiere { get; set; }
        private Texture m_Texture { get; set; }
        private Texture m_BumpTexture { get; set; }
        private float m_CoefficientDiffus { get; set; }
        private float m_CoefficientSpeculaire { get; set; }
        private float m_PuissanceSpeculaire { get; set; }
        private float m_CoefficientBumpMap { get; set; }
        protected float m_Pas { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un Objet3D
        /// </summary>
        /// <param name="centre">Centre de l'Objet3D</param>
        /// <param name="key_lumiere">Lumière principale appliquée sur l'Objet3D</param>
        /// <param name="fill_lumiere">Lumière secondaire appliquée sur l'Objet3D</param>
        /// <param name="texture">Texture appliquée sur l'Objet3D</param>
        /// <param name="bump_texture">Texture de bump appliquée sur l'Objet3D</param>
        /// <param name="coefficient_diffus">Coefficient de diffus de la sphère, plus le coefficient est faible, plus le diffus sera "fondu"</param>
        /// <param name="coefficient_speculaire">Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"</param>
        /// <param name="puissance_speculaire">Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand</param>
        /// <param name="coefficient_bumpmap">Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé.</param>
        /// <param name="pas">Pas de l'Objet3D, plus le pas est grand, plus l'Objet aura de pixels et donc prendra du temps à être tracé</param>
        public Objet3D(V3 centre, Lumiere key_lumiere, Lumiere fill_lumiere, Texture texture, Texture bump_texture, float coefficient_diffus, float coefficient_speculaire, float puissance_speculaire, float coefficient_bumpmap, float pas)
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
            m_Pas = pas;
        }
        #endregion

        #region Méthodes abstraites
        /// <summary>
        /// Méthode abstraite déterminant les coordonnées du pixel à tracer en fonction des coordonnées paramétriques (u,v)
        /// </summary>
        /// <param name="u">Coordonnée paramétrique u</param>
        /// <param name="v">Coordonnée paramétrique v</param>
        /// <returns>Position du pixel à tracer</returns>
        protected abstract V3 getCoords(float u, float v);
        /// <summary>
        /// Méthode abstraite déterminant les dérivées partielles des coordonnées du pixel à tracer
        /// en fonction des coordonnées paramétriques (u,v)
        /// </summary>
        /// <param name="u">Coordonnée paramétrique u</param>
        /// <param name="v">Coordonnée paramétrique v</param>
        /// <param name="dMdu">Dérivée partielle des coordonnées du pixel en fonction de u</param>
        /// <param name="dMdv">Dérivée partielle des coordonnées du pixel en fonction de v</param>
        protected abstract void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv);
        /// <summary>
        /// Méthode abstraite déterminant la normale du pixel passé en paramètre
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut trouver la normale</param>
        /// <returns>La normale du pixel passé en paramètre</returns>
        protected abstract V3 getNormal(V3 PixelPosition);
        /// <summary>
        /// Méthode abstraite permettant de tracer l'Objet3D
        /// </summary>
        public abstract void Draw();
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode permettant d'obtenir la couleur ambiante du pixel
        /// sur les coordonnées X & Y du repère écran
        /// </summary>
        /// <param name="lumiere">Lumière appliquée sur l'Objet3D</param>
        /// <param name="x_ecran">Coordonnées X du pixel sur le repère écran</param>
        /// <param name="y_ecran">Coordonnées Y du pixel sur le repère écran</param>
        /// <returns>Couleur ambiante</returns>
        private Couleur getCouleurAmbiante(Lumiere lumiere, float x_ecran, float y_ecran)
        {
            return m_Texture.LireCouleur(x_ecran, y_ecran) * lumiere.m_Couleur;
        }

        /// <summary>
        /// Méthode permettant d'obtenir la couleur ambiante abaissée du pixel
        /// sur les coordonnées X & Y du repère écran
        /// </summary>
        /// <param name="lumiere">Lumière appliquée sur l'Objet3D</param>
        /// <param name="x_ecran">Coordonnées X du pixel sur le repère écran</param>
        /// <param name="y_ecran">Coordonnées Y du pixel sur le repère écran</param>
        /// <returns>Couleur ambiante du pixel passée en paramètre</returns>
        private Couleur getLowCouleurAmbiante(Lumiere lumiere, float x_ecran, float y_ecran)
        {
            return getCouleurAmbiante(lumiere, x_ecran,y_ecran) * .0008f;
        }

        /// <summary>
        /// Méthode permettant d'obtenir la couleur diffuse du pixel
        /// sur les coordonnées X & Y du repère écran
        /// </summary>
        /// <param name="lumiere">Lumière appliquée sur l'Objet3D</param>
        /// <param name="normalizedPixelNormal">Normale du pixel dont on cherche la couleur diffuse</param>
        /// <param name="x_ecran">Coordonnées X du pixel sur le repère écran</param>
        /// <param name="y_ecran">Coordonnées Y du pixel sur le repère écran</param>
        /// <returns>Couleur diffuse du pixel passée en paramètre</returns>
        private Couleur getCouleurDiffuse(Lumiere lumiere, V3 normalizedPixelNormal, float x_ecran, float y_ecran)
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

        /// <summary>
        /// Méthode permettant d'obtenir la couleur spéculaire du pixel
        /// sur les coordonnées X & Y du repère écran
        /// </summary>
        /// <param name="lumiere">Lumière appliquée sur l'Objet3D</param>
        /// <param name="PixelPosition">Pixel dont on cherche la couleur spéculaire</param>
        /// <param name="x_ecran">Coordonnées X du pixel sur le repère écran</param>
        /// <param name="y_ecran">Coordonnées Y du pixel sur le repère écran</param>
        /// <returns>Couleur spéculaire du pixel passée en paramètre</returns>
        private Couleur getCouleurSpeculaire(Lumiere lumiere, V3 PixelPosition, V3 N, float x_ecran, float y_ecran)
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

        /// <summary>
        /// Méthode permettant d'obtenir la couleur totale du pixel passé en paramètre
        /// selon les coordonnées paramétriques (u,v)
        /// </summary>
        /// <param name="PixelPosition"></param>
        /// <param name="u">Coordonnée paramétrique u</param>
        /// <param name="v">Coordonnée paramétrique v</param>
        /// <returns>Couleur totale du pixel passé en paramètre</returns>
        protected Couleur getCouleur(V3 PixelPosition, float u, float v)
        {
            Lumiere[] lumieres = { m_FillLumiere, m_KeyLumiere };
            Couleur finalColor = Couleur.m_Void;
            foreach (Lumiere lumiere in lumieres) {
                V3 N = getBumpedNormal(PixelPosition, u, v);
                //N.Normalize();
                Couleur Ambiant = getLowCouleurAmbiante(lumiere, u, v);
                Couleur Diffus = getCouleurDiffuse(lumiere, N, u, v);
                if (Diffus != Couleur.m_Void)
                {
                    Couleur Speculaire = getCouleurSpeculaire(lumiere, PixelPosition, N, u, v);
                    finalColor += Ambiant + Diffus + Speculaire;
                }
                else
                {
                    finalColor += Ambiant + Diffus;
                }
            }
            return finalColor;
        }
        /// <summary>
        /// Permet d'obtenir la normale "Bumpée" du pixel passé en paramètre
        /// </summary>
        /// <param name="PixelPosition">Coordonnées du pixel dont on veut obtenir la normale bumpée</param>
        /// <param name="u">Coordonnée paramétrique u</param>
        /// <param name="v">Coordonnée paramétrique v</param>
        /// <returns>Normale bumpée du pixel passé en paramètre</returns>
        private V3 getBumpedNormal(V3 PixelPosition, float u, float v)
        {
            V3 N = getNormal(PixelPosition);
            N.Normalize();

            float K = m_CoefficientBumpMap;
            getDerivedCoords(u, v, out V3 dMdu, out V3 dMdv);
            this.m_BumpTexture.Bump(u, v, out float dhdu, out float dhdv);

            return N + K * ((dMdu ^ (N * dhdv)) + ((N * dhdu) ^ dMdv));
        }
        #endregion
    }
}
