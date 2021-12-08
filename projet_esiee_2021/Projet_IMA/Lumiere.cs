﻿namespace Projet_IMA
{
    /// <summary>
    /// Définit une lumière et ses caractéristiques dont la couleur et la direction.
    /// </summary>
    class Lumiere
    {
        #region Attributs

        /// <summary>
        /// Direction de la lumière
        /// </summary>
        public V3 m_Direction { get; set; }
        /// <summary>
        /// Vecteur de direction de la lumière normalisé
        /// </summary>
        public V3 m_NormalizedDirection { get; set; }
        /// <summary>
        /// Couleur de la lumière
        /// </summary>
        public Couleur m_Couleur { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la lumiere
        /// </summary>
        /// <param name="directionLumiere">Vecteur precisant la direction de la lumiere</param>
        /// <param name="couleur">Couleur de la lumiere</param>
        public Lumiere(V3 directionLumiere, Couleur couleur)
        {
            this.m_Direction = directionLumiere;
            this.m_Couleur = couleur;
            this.m_NormalizedDirection = m_Direction;
            this.m_NormalizedDirection.Normalize();
        }
        #endregion
    }
}