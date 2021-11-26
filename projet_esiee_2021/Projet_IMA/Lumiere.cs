using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Lumiere
    {
        #region Attributs
        public V3 m_Direction { get; set; }
        public V3 m_NormalizedDirection { get; set; }
        public Couleur m_Couleur { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la lumière
        /// </summary>
        /// <param name="directionLumiere">Vecteur précisant la direction de la lumière</param>
        /// <param name="couleur">Couleur de la lumière</param>
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