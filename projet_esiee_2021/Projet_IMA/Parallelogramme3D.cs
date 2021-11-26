using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Parallelogramme3D : Objet3D
    {
        #region Attributs
        private V3 m_Longueur { get; set; }
        private V3 m_Largeur { get; set; }
        private V3 m_Origine { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un Parallelogramme3D
        /// </summary>
        /// <param name="centre">Centre du Parallelogramme3D</param>
        /// <param name="longueur">Vecteur longueur du Parallelogramme3D</param>
        /// <param name="largeur">Vecteur largeur du Parallelogramme3D</param>
        /// <param name="key_lumiere">Lumière principale appliquée sur le Parallelogramme3D</param>
        /// <param name="fill_lumiere">Lumière secondaire appliquée sur le Parallelogramme3D</param>
        /// <param name="texture">Texture appliquée sur le Parallelogramme3D</param>
        /// <param name="bump_texture">Texture de bump appliquée sur le Parallelogramme3D</param>
        /// <param name="coefficient_diffus">Coefficient de diffus du Parallelogramme3D, plus le coefficient est faible, plus le diffus sera "fondu"</param>
        /// <param name="coefficient_speculaire">Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"</param>
        /// <param name="puissance_speculaire">Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand</param>
        /// <param name="coefficient_bumpmap">Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé.</param>
        /// <param name="pas">Pas du Parallelogramme3D, plus le pas est grand, plus l'Objet aura de pixels et donc prendra du temps à être tracé</param>
        public Parallelogramme3D(V3 centre, V3 longueur, V3 largeur, Lumiere key_lumiere, Lumiere fill_lumiere, Texture texture, Texture bump_texture, float coefficient_diffus = 0.006f, float coefficient_speculaire = .0001f, float puissance_speculaire = 60, float coefficient_bumpmap = .005f, float pas = .001f) : base(centre, key_lumiere, fill_lumiere, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas)
        {
            m_Longueur = longueur;
            m_Largeur = largeur;
            m_Origine = centre - (1 / 2) * longueur + (1 / 2) * largeur;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode déterminant les coordonnées du pixel à tracer en fonction des coordonnées paramétriques (u,v)
        /// </summary>
        /// <param name="u">Coordonnée paramétrique u</param>
        /// <param name="v">Coordonnée paramétrique v</param>
        /// <returns>Position du pixel à tracer</returns>
        protected override V3 getCoords(float u, float v)
        {
            float x3D = m_Origine.x + u * m_Longueur.x + v * m_Largeur.x;
            float y3D = m_Origine.y + u * m_Longueur.y + v * m_Largeur.y;
            float z3D = m_Origine.z + u * m_Longueur.z + v * m_Largeur.z;
            return new V3(x3D, y3D, z3D);
        }

        /// <summary>
        /// Méthode déterminant les dérivées partielles des coordonnées du pixel à tracer
        /// en fonction des coordonnées paramétriques (u,v)
        /// </summary>
        /// <param name="u">Coordonnée paramétrique u</param>
        /// <param name="v">Coordonnée paramétrique v</param>
        /// <param name="dMdu">Dérivée partielle des coordonnées du pixel en fonction de u</param>
        /// <param name="dMdv">Dérivée partielle des coordonnées du pixel en fonction de v</param>
        protected override void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv)
        {
            float dxdu = m_Longueur.x;
            float dxdv = m_Largeur.x;

            float dydu = m_Longueur.y;
            float dydv = m_Largeur.y;

            float dzdu = m_Longueur.z;
            float dzdv = m_Largeur.z;

            dMdu = new V3(dxdu, dydu, dzdu);
            dMdv = new V3(dxdv, dydv, dzdv);
        }

        /// <summary>
        /// Méthode déterminant la normale du pixel passé en paramètre
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut trouver la normale</param>
        /// <returns>La normale du pixel passé en paramètre</returns>
        protected override V3 getNormal(V3 PixelPosition)
        {
            V3 normal = m_Longueur ^ m_Largeur;
            normal.Normalize();
            return normal;
        }

        /// <summary>
        /// Méthode abstraite permettant de tracer le Parallelogramme3D
        /// </summary>
        public override void Draw()
        {
            for (float u = 0; u < 1; u += m_Pas)
            {
                for (float v = 0; v < 1; v += m_Pas)
                {
                    V3 PixelPosition = getCoords(u, v);

                    // projection orthographique => repère écran

                    int x_ecran = (int)(PixelPosition.x);
                    int y_ecran = (int)(PixelPosition.z);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(PixelPosition, u, v));
                }
            }
        }
        #endregion
    }
}
