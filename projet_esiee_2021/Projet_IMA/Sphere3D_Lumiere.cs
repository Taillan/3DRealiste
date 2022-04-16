namespace Projet_IMA
{
    /// <summary>
    /// Définit une sphère de type lumière. Hérite des méthodes de la sphère mais a aussi
    /// la particularité d'émettre de la lumière.
    /// </summary>
    class Sphere3D_Lumiere : Sphere3D
    {
        #region Attributs
        /// <summary>
        /// Couleur de la lumière
        /// </summary>
        private Couleur m_Couleur { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'une Sphere3D lumiere
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
        public Sphere3D_Lumiere(V3 centre, float rayon, Couleur couleur, float coefficient_diffus = .005f, float coefficient_speculaire = .00005f, float puissance_speculaire = 60, float coefficient_bumpmap = .005f, float pas = .005f) : base(centre,rayon, null, null, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas)
        {
            this.m_Couleur = couleur;
        }
        #endregion

        #region Méthodes héritées

        /// <summary>
        /// Permet de savoir si l'objet est de type lumière
        /// </summary>
        /// <returns>Vrai si l'objet est une lumière, faux sinon</returns>
        public override bool isLumiere()
        {
            return true;
        }

        /// <summary>
        /// Permet de retourner la couleur de la texture sur les coordonées données.
        /// Si c'est un objet texturé
        /// </summary>
        /// <param name="u">Position du vecteur u qui pointe sur le pixel de l'objet</param>
        /// <param name="v">Position du vecteur v qui pointe sur le pixel de l'objet</param>
        /// <returns>Couleur du pixel pointé</returns>
        protected override Couleur getCouleurPixel(float u, float v)
        {
            return m_Couleur;
        }

        /// <summary>
        /// Redéfinition de la méthode getCouleur() d'un Objet3D.
        /// Puisque l'objet est une lampe, on retourne simplement
        /// la couleur de la lampe.
        /// </summary>
        /// <param name="PixelPosition">Position du pixel sur l'Objet3D dont on veut connaître la couleur</param>
        /// <param name="u">Position des coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Position des coordonnées en ordonnées de la texture l'objet</param>
        /// <returns></returns>
        public override Couleur getCouleur(V3 PixelPosition, float u, float v, RenderMode RM=RenderMode.PATH_TRACING)
        {
            return m_Couleur;
        }

        #endregion

    }
}
