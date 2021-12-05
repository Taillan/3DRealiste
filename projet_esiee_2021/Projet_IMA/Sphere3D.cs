namespace Projet_IMA
{
    /// <summary>
    /// Définit une sphère qu'on peut placer dans l'espace, héritant directement de la classe Objet3D.
    /// </summary>
    class Sphere3D : Objet3D
    {
        #region Attributs
        /// <summary>
        /// Rayon de la Sphere3D
        /// </summary>
        private float m_Rayon { get; set; }
        #endregion

        #region Constructeurs
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
        public Sphere3D(V3 centre, float rayon,Texture texture, Texture bump_texture, float coefficient_diffus = .005f, float coefficient_speculaire = .00005f, float puissance_speculaire=60, float coefficient_bumpmap=.005f, float pas=.005f) : base(centre, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas)
        {
            this.m_Rayon = rayon;
        }

        #endregion

        #region Méthodes héritées

        /// <summary>
        /// Calcule les coordonnées du Pixel 3D de l'objet grâce aux positions u et v sur la texture 2D.
        /// </summary>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Coordonnées du Pixel 3D associé aux positions u et v de la texture 2D</returns>
        protected override V3 getCoords(float u, float v)
        {
            float x3D = m_Rayon * IMA.Cosf(v) * IMA.Cosf(u) + this.m_CentreObjet.x;
            float y3D = m_Rayon * IMA.Cosf(v) * IMA.Sinf(u) + this.m_CentreObjet.y;
            float z3D = m_Rayon * IMA.Sinf(v) + this.m_CentreObjet.z;
            
            return new V3(x3D, y3D, z3D);
        }

        /// <summary>
        /// Calcule les dérivées partielles de la position du Pixel 3D (M) en fonction des positions u et v de la texture 2D.
        /// Nécessaire pour pouvoir déterminer le bump mapping.
        /// </summary>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet</param>
        /// <param name="dMdu">Dérivée de M (position du point actuel) en fonction de u</param>
        /// <param name="dMdv">Dérivée de M (position du point actuel) en fonction d v</param>
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
        /// Permet de savoir si le rayon passé en paramètre rentre en intersection avec l'Objet3D.
        /// Si oui, il retourne le Pixel3D où se trouve l'intersection 
        /// ainsi que les coordonnées u & v du pixel 2d de la texture associée à ce Pixel 3D.
        /// </summary>
        /// <param name="OrigineRayon">Origine du rayon dont on veut tester l'intersection</param>
        /// <param name="DirectionRayon">Direction du rayon dont on veut tester l'intersection</param>
        /// <param name="DistanceIntersection">Longueur du rayon de l'origine jusqu'à l'intersection trouvée</param>
        /// <param name="PixelPosition">Position du pixel où a eu lieu l'intersection</param>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet associées au point d'intersection</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet associées au point d'intersection</param>
        /// <returns>Vrai s'il y a une intersection, faux sinon.</returns>
        public override bool IntersectionRayon(V3 OrigineRayon, V3 DirectionRayon, out float DistanceIntersection, out V3 PixelPosition, out float u, out float v)
        {
            u = 0;
            v = 0;
            DistanceIntersection = 0;
            PixelPosition = new V3(0,0,0);
            float A = DirectionRayon * DirectionRayon;
            float B = 2 * OrigineRayon * DirectionRayon - 2 * DirectionRayon * m_CentreObjet;
            float D = (OrigineRayon * OrigineRayon) - (2 * OrigineRayon * m_CentreObjet) + (m_CentreObjet * m_CentreObjet) - (m_Rayon * m_Rayon);
            float DELTA = (B * B) - (4 * A * D);
            if (DELTA > 0)
            {
                float t1 = (-B - IMA.Sqrtf(DELTA)) / (2 * A);
                float t2 = (-B + IMA.Sqrtf(DELTA)) / (2 * A);
                if (t1 > 0 && t2 > 0)
                {
                    PixelPosition = OrigineRayon + (t1 * DirectionRayon);
                    DistanceIntersection = t1;
                }
                else if (t1 < 0 && t2 > 0)
                {
                    PixelPosition = OrigineRayon + (t2 * DirectionRayon);
                    DistanceIntersection = t2;
                }
                v = -IMA.Asinf((PixelPosition.z - this.m_CentreObjet.z) / m_Rayon);
                u = -IMA.Acosf((PixelPosition.x - this.m_CentreObjet.x) / (m_Rayon * IMA.Cosf(v)));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calcule la normale du pixel passé en paramètre
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut obtenir la normale</param>
        /// <returns>Normale du pixel passé en paramètre</returns>
        protected override V3 getNormal(V3 PixelPosition)
        {
            V3 normal = (PixelPosition - m_CentreObjet);
            normal.Normalize();
            return normal;
        }

        /// <summary>
        /// Fonction permettant de dessiner une Sphere3D dans son entièreté
        /// </summary>
        public override void Draw()
        {
            for (float u = 0; u < 2 * IMA.PI; u += m_Pas)
            {
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += m_Pas)
                {
                    // Calcul des coordoonées dans la scène 3D
                    V3 PixelPosition = getCoords(u,v);

                    // Projection orthographique => repère écran
                    int x_ecran = (int)(PixelPosition.x);
                    int y_ecran = (int)(PixelPosition.z);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(PixelPosition,u,v));
                }
            }
        }

        #endregion
    }
}
