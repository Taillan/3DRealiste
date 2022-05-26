﻿namespace Projet_IMA
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
        /// <param name="couleur">Couleur de la lumière</param>
        public Sphere3D_Lumiere(V3 centre, float rayon, Couleur couleur) : base(centre, rayon, null, null, 0, 0, 0, 0, 0)
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
        /// <returns>Couleur de la lumière</returns>
        public override Couleur getCouleur(V3 PixelPosition, float u, float v)
        {
            return m_Couleur;
        }

        #endregion

    }
}
