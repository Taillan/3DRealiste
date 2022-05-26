namespace Projet_IMA
{
    /// <summary>
    /// Définit un parallélogramme qu'on peut placer dans l'espace, héritant directement de la classe Objet3D.
    /// </summary>
    class Parallelogramme3D : Objet3D
    {
        #region Attributs
        /// <summary>
        /// Longueur du parallelogramme
        /// </summary>
        private V3 m_Longueur { get; set; }
        /// <summary>
        /// Largeur du parallelogramme
        /// </summary>
        private V3 m_Largeur { get; set; }
        /// <summary>
        /// Origine du Parallelogramme3D (à situer "en bas à gauche" du parallelogramme)
        /// D'où on applique les vecteurs m_Longueur et m_Largeur pour le tracer.
        /// </summary>
        private V3 m_Origine { get; set; }
        /// <summary>
        /// Vecteur normal au Parallelogramme3D
        /// </summary>
        private V3 m_Normale { get; set; }
        /// <summary>
        /// Vecteur constant correspondant à <c>(m_Largeur ^ m_Normale) / ((m_Longueur ^ m_Largeur).Norm());</c>
        /// Nécessaire pour déterminer une intersection avec un autre objet
        /// </summary>
        private V3 m_K { get; set; }
        /// <summary>
        /// Vecteur constant correspondant à <c>(m_Longueur ^ - m_Normale) / ((m_Longueur ^ m_Largeur).Norm());</c>
        /// Nécessaire pour déterminer une intersection avec un autre objet
        /// </summary>
        private V3 m_K2 { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un Parallelogramme3D
        /// </summary>
        /// <param name="centre">Centre de cet objet</param>
        /// <param name="longueur">Longueur de l'objet</param>
        /// <param name="largeur">Largeur de l'objet</param>
        /// <param name="hauteur">Hauteur de l'objet</param>
        /// <param name="lumiere">Lumiere applique à l'objet</param>
        /// <param name="texture">Texture applique à l'objet</param>
        /// <param name="bump_texture">Texture de bump appliquée sur la sphère</param>
        /// <param name="coefficient_diffus">Coefficient de diffus de la sphère, plus le coefficient est faible, plus le diffus sera "fondu"</param>
        /// <param name="coefficient_speculaire">Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"</param>
        /// <param name="puissance_speculaire">Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand</param>
        /// <param name="coefficient_bumpmap">Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé.</param>
        public Parallelogramme3D(V3 centre, V3 longueur, V3 largeur, Texture texture = null, Texture bump_texture = null, float coefficient_diffus = 1f, float coefficient_speculaire = .001f, float puissance_speculaire = 60, float coefficient_bumpmap = .005f, float pas = .001f) : base(centre, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas)
        {
            m_Longueur = longueur;
            m_Largeur = largeur;
            m_Origine = centre;
            //m_Origine = centre - (1 / 2) * longueur - (1 / 2) * largeur;
            m_Normale = getNormal(new V3(0, 0, 0));
            m_K = (m_Largeur ^ m_Normale) / ((m_Longueur ^ m_Largeur).Norm());
            m_K2 = (m_Longueur ^ -m_Normale) / ((m_Longueur ^ m_Largeur).Norm());
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
            float x3D = m_Origine.x + u * m_Longueur.x + v * m_Largeur.x;
            float y3D = m_Origine.y + u * m_Longueur.y + v * m_Largeur.y;
            float z3D = m_Origine.z + u * m_Longueur.z + v * m_Largeur.z;
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
        /// Calcule la normale du pixel passé en paramètre
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut obtenir la normale</param>
        /// <returns>Normale du pixel passé en paramètre</returns>
        protected override V3 getNormal(V3 PixelPosition)
        {
            V3 normal = m_Longueur ^ m_Largeur;
            normal.Normalize();
            return normal;
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
            V3 A = m_Origine;
            V3 n = m_Normale;
            DistanceIntersection = ((A - OrigineRayon)*n) / (DirectionRayon * n);
            PixelPosition = OrigineRayon + DistanceIntersection * DirectionRayon;
            V3 AI = PixelPosition - A;
            u = m_K * AI;
            v = m_K2 * AI;
            if((u>=0 && u<=1) && (v>=0 && v<=1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
