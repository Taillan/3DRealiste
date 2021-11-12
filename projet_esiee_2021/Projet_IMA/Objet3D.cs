﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Objet3D
    {
        protected V3 m_CentreObjet { get; set; } 
        protected Lumiere m_Lumiere { get; set; }
        protected Texture m_Texture { get; set; }
        protected Texture m_BumpTexture { get; set; }
        protected float m_CoefficientDiffus { get; set; }
        protected float m_CoefficientSpeculaire { get; set; }
        protected float m_PuissanceSpeculaire { get; set; }
        protected float m_CoefficientBumpMap { get; set; }

        #region Constructeur
        /// <summary>
        /// Constructeur d'un objet 3D
        /// </summary>
        /// <param name="centre">Centre de l'Objet3D</param>
        /// <param name="lumiere">Lumière appliquée sur l'Objet3D</param>
        /// <param name="texture">Texture appliquée sur l'Objet3D</param>
        /// <param name="bump_texture">Texture de bump appliquée sur l'Objet3D</param>
        /// <param name="coefficient_diffus">Coefficient de diffus de la sphère, plus le coefficient est faible, plus le diffus sera "fondu"</param>
        /// <param name="coefficient_speculaire">Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"</param>
        /// <param name="puissance_speculaire">Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand</param>
        /// <param name="coefficient_bumpmap">Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé.</param>
        public Objet3D(V3 centre, Lumiere lumiere, Texture texture, Texture bump_texture, float coefficient_diffus, float coefficient_speculaire, float puissance_speculaire, float coefficient_bumpmap)
        {
            m_CentreObjet = centre;
            m_Lumiere = lumiere;
            m_CoefficientDiffus = coefficient_diffus;
            m_CoefficientSpeculaire = coefficient_speculaire;
            m_PuissanceSpeculaire = puissance_speculaire;
            m_Texture = texture;
            m_BumpTexture = bump_texture;
            m_CoefficientBumpMap = coefficient_bumpmap;
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// Classe abstraite définissant comment dessiner l'objet héritant de cette classe
        /// </summary>
        /// <param name="pas">Écart entre chaque point tracé à l'écran</param>
        public abstract void Draw(float pas);

        /// <summary>
        /// Renvoi la couleur de resultante de l'application de la lumière
        /// </summary>
        /// <param name="x_ecran">Positionnement en X sur l'écran du point interrogé</param>
        /// <param name="y_ecran">Positionnement en Y sur l'écran du point interrogé</param>
        /// <returns></returns>
        public Couleur getCouleurAmbiante(float x_ecran, float y_ecran)
        {
            return m_Texture.LireCouleur(x_ecran, y_ecran) * m_Lumiere.m_Couleur;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x_ecran">Positionnement en X sur l'écran du point interrogé</param>
        /// <param name="y_ecran">Positionnement en Y sur l'écran du point interrogé</param>
        /// <returns></returns>
        public Couleur getLowCouleurAmbiante(float x_ecran, float y_ecran)
        {
            return getCouleurAmbiante(x_ecran,y_ecran) * .0008f;
        }

        /// <summary>
        /// Calcule la couleur diffuse du pixel passé en paramère
        /// </summary>
        /// <param name="normalizedPixelNormal">Vecteur décrivant la normale au point x,y</param>
        /// <param name="x_ecran">Positionnement en X sur l'écran du point interrogé</param>
        /// <param name="y_ecran">Positionnement en Y sur l'écran du point interrogé</param>
        /// <returns>Couleur diffuse du pixel passé en paramètre</returns>
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

        /// <summary>
        /// Calcule la couleur spéculaire du pixel passé en paramère
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut trouver la couleur spéculaire</param>
        /// <param name="N">Normale associée au pixel passé en paramètre</param>
        /// <param name="x_ecran">Position x de l'écran du pixel passé en paramètre</param>
        /// <param name="y_ecran">Position y de l'écran du pixel passé en paramètre</param>
        /// <returns>Couleur spéculaire du pixel passé en paramètre</returns>
        public Couleur getCouleurSpeculaire(V3 PixelPosition, V3 N, float x_ecran, float y_ecran)
        {
            V3 L = m_Lumiere.m_Direction;
            V3 R = 2*N*(N*L)-L;
            V3 D = (BitmapEcran.GetCameraPosition() - PixelPosition);
            R.Normalize();
            D.Normalize();
            float RD = R * D;
            if ((RD) > 0)
            {
                return m_Lumiere.m_Couleur * getCouleurAmbiante(x_ecran, y_ecran) * m_CoefficientSpeculaire * (float)Math.Pow(RD, m_PuissanceSpeculaire);
            }
            else
            {
                return Couleur.m_Void;
            }
        }

        /// <summary>
        /// Calcule la couleur totale du pixel passé en paramère
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut trouver la couleur spéculaire</param>
        /// <param name="u">Position du vecteur u qui pointe sur le pixel de l'objet</param>
        /// <param name="v">Position du vecteur v qui pointe sur le pixel de l'objet</param>
        /// <param name="dMdu">Dérivée du point M(position du pixel) en fonction du vecteur u</param>
        /// <param name="dMdv">Dérivée du point M(position du pixel) en fonction du vecteur v</param>
        /// <returns>Couleur totale du pixel passé en paramètre</returns>
        public Couleur getCouleur(V3 PixelPosition, float u, float v, V3 dMdu, V3 dMdv)
        {
            V3 N = getBumpedNormal(PixelPosition,u,v,dMdu,dMdv);
            N.Normalize();
            Couleur Ambiant = getLowCouleurAmbiante(u, v);
            Couleur Diffus = getCouleurDiffuse(N, u, v);
            if (Diffus != Couleur.m_Void)
            {
                Couleur Speculaire = getCouleurSpeculaire(PixelPosition, N, u, v);
                return Ambiant + Diffus + Speculaire;
            }
            else
            {
                return Ambiant + Diffus;
            }
        }

        /// <summary>
        /// Calcule la normale bumpée du pixel actuel
        /// </summary>
        /// <param name="PixelPosition"></param>
        /// <param name="u">Position du vecteur u qui pointe sur le pixel de l'objet</param>
        /// <param name="v">Position du vecteur v qui pointe sur le pixel de l'objet</param>
        /// <param name="dMdu">Dérivée du point M(position du pixel) en fonction du vecteur u</param>
        /// <param name="dMdv">Dérivée du point M(position du pixel) en fonction du vecteur v</param>
        /// <returns>Normale bumpée du pixel actuel</returns>
        public V3 getBumpedNormal(V3 PixelPosition, float u, float v, V3 dMdu, V3 dMdv)
        {
            V3 N = (PixelPosition - m_CentreObjet);
            N.Normalize();

            float K = m_CoefficientBumpMap;

            this.m_BumpTexture.Bump(u, v, out float dhdu, out float dhdv);
            V3 T2 = dMdu ^ (N * dhdv);
            V3 T3 = (N * dhdu) ^ dMdv;

            return N + K * (T2 + T3);
        }

        #endregion
    }
}
