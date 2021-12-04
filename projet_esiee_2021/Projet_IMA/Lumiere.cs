namespace Projet_IMA
{
    class Lumiere
    {
        public V3 m_Direction { get; set; }
        public V3 m_NormalizedDirection { get; set; }
        public Couleur m_Couleur { get; set; }


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

    }
}