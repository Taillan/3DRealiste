using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere3D : Objet3D
    {
        #region Attributs
        private float m_Rayon { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'une Sphere3D
        /// </summary>
        /// <param name="centre">Centre de la Sphere3D</param>
        /// <param name="rayon">Rayon de la Sphere3D</param>
        /// <param name="key_lumiere">Lumière principale appliquée sur la Sphere3D</param>
        /// <param name="fill_lumiere">Lumière secondaire appliquée sur la Sphere3D</param>
        /// <param name="texture">Texture appliquée sur la Sphere3D</param>
        /// <param name="bump_texture">Texture de bump appliquée sur la Sphere3D</param>
        /// <param name="coefficient_diffus">Coefficient de diffus de la Sphere3D, plus le coefficient est faible, plus le diffus sera "fondu"</param>
        /// <param name="coefficient_speculaire">Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"</param>
        /// <param name="puissance_speculaire">Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand</param>
        /// <param name="coefficient_bumpmap">Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé.</param>
        /// <param name="pas">Pas de la Sphere3D, plus le pas est grand, plus l'Objet aura de pixels et donc prendra du temps à être tracé</param>
        public Sphere3D(V3 centre, float rayon,  Lumiere key_lumiere, Lumiere fill_lumiere,Texture texture, Texture bump_texture, float coefficient_diffus = .005f, float coefficient_speculaire = .00005f, float puissance_speculaire=60, float coefficient_bumpmap=.005f, float pas=.005f) : base(centre, key_lumiere, fill_lumiere, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas)
        {
            this.m_Rayon = rayon;
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
            float x3D = m_Rayon * IMA.Cosf(v) * IMA.Cosf(u) + this.m_CentreObjet.x;
            float y3D = m_Rayon * IMA.Cosf(v) * IMA.Sinf(u) + this.m_CentreObjet.y;
            float z3D = m_Rayon * IMA.Sinf(v) + this.m_CentreObjet.z;
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
            float dxdu = m_Rayon * IMA.Cosf(v) * (-IMA.Sinf(u));
            float dxdv = m_Rayon * (-IMA.Sinf(v)) * IMA.Cosf(v);

            float dydu = m_Rayon * IMA.Cosf(v) * IMA.Cosf(u);
            float dydv = m_Rayon * (-IMA.Sinf(v)) * IMA.Sinf(u);

            float dzdu = 0;
            float dzdv = m_Rayon * IMA.Cosf(v);

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
            return (PixelPosition - m_CentreObjet);
        }

        /// <summary>
        /// Méthode abstraite permettant de tracer la Sphere3D
        /// </summary>
        public override void Draw()
        {
            for (float u = 0; u < 2 * IMA.PI; u += m_Pas)
            {
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += m_Pas)
                {
                    // calcul des coordoonées dans la scène 3D
                    V3 PixelPosition = getCoords(u,v);

                    // projection orthographique => repère écran
                    int x_ecran = (int)(PixelPosition.x);
                    int y_ecran = (int)(PixelPosition.z);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(PixelPosition,u,v));
                }
            }
        }
        #endregion
    }
}
