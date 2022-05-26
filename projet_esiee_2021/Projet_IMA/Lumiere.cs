namespace Projet_IMA
{
    /// <summary>
    /// Définit une lumière et ses caractéristiques dont la couleur et la direction.
    /// </summary>
    internal class Lumiere
    {
        #region Attributs

        /// <summary>
        /// Position de la lumière
        /// Utilisé uniquement pour le VPL
        /// </summary>
        internal V3 m_Position { get; set; }
        /// <summary>
        /// Direction de la lumière
        /// </summary>
        internal V3 m_Direction { get; set; }
        /// <summary>
        /// Vecteur de direction de la lumière normalisé
        /// </summary>
        internal V3 m_NormalizedDirection { get; set; }
        /// <summary>
        /// Couleur de la lumière
        /// </summary>
        internal Couleur m_Couleur { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la lumiere
        /// </summary>
        /// <param name="directionLumiere">Vecteur precisant la direction de la lumiere</param>
        /// <param name="couleur">Couleur de la lumiere</param>
        /// <param name="positionLumiere">Vecteur precisant la position de la lumiere, utilisé uniquement pour le VPL</param>
        internal Lumiere(V3 directionLumiere, Couleur couleur, V3 positionLumiere = new V3())
        {
            this.m_Direction = directionLumiere;
            this.m_Couleur = couleur;
            this.m_Position = positionLumiere;
            this.m_NormalizedDirection = m_Direction;
            this.m_NormalizedDirection.Normalize();
        }
        #endregion
    }
}
