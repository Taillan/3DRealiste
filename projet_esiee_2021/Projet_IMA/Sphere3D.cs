using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere3D : Objet3D
    {
        #region Constructeurs
        public float m_Rayon { get; set; }

        /// <summary>
        /// Constructeur d'une Sphere3D
        /// </summary>
        /// <param name="centre">Centre de la sphère</param>
        /// <param name="rayon">Rayon de la sphère</param>
        /// <param name="lumiere">Lumière appliquée sur la sphère</param>
        /// <param name="texture">Texture appliquée sur la sphère</param>
        /// <param name="bump_texture">Texture de bump appliquée sur la sphère</param>
        /// <param name="coefficient_diffus">Coefficient de diffus de la sphère, plus le coefficient est faible, plus le diffus sera "fondu"</param>
        /// <param name="coefficient_speculaire">Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"</param>
        /// <param name="puissance_speculaire">Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand</param>
        /// <param name="coefficient_bumpmap">Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé.</param>
        public Sphere3D(V3 centre, float rayon,  Lumiere lumiere,Texture texture, Texture bump_texture, float coefficient_diffus = .006f, float coefficient_speculaire = .0001f, float puissance_speculaire=60, float coefficient_bumpmap=.005f) : base(centre, lumiere, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap)
        {
            this.m_Rayon = rayon;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">Position du vecteur u qui permet de tracer la sphère</param>
        /// <param name="v">Position du vecteur v qui permet de tracer la sphère</param>
        /// <param name="dMdu">Dérivée de M (position du point actuel) en fonction de u</param>
        /// <param name="dMdv">Dérivée de M (position du point actuel) en fonction d v</param>
        public void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv)
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
        /// Fonction décrivant comment dessiner d'une Sphere3D
        /// </summary>
        /// <param name="pas">Ecart entre chaque point tracé à l'écran</param>
        public override void Draw(float pas=.005f)
        {
            for (float u = 0; u < 2 * IMA.PI; u += pas)
            {
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += pas)
                {
                    // calcul des coordoonées dans la scène 3D
                    float x3D = m_Rayon * IMA.Cosf(v) * IMA.Cosf(u) + this.m_CentreObjet.x;
                    float y3D = m_Rayon * IMA.Cosf(v) * IMA.Sinf(u) + this.m_CentreObjet.y;
                    float z3D = m_Rayon * IMA.Sinf(v) + this.m_CentreObjet.z;
                    V3 PixelPosition = new V3(x3D, y3D, z3D);
                    
                    getDerivedCoords(u, v, out V3 dMdu, out V3 dMdv);
                    
                    // projection orthographique => repère écran

                    int x_ecran = (int)(PixelPosition.x);
                    int y_ecran = (int)(PixelPosition.z);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(PixelPosition,u,v,dMdu,dMdv));
                }
            }
        }
        #endregion
    }
}
