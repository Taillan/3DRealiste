using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Lumiere
    {
        public V3 m_Direction { get; set; }
        public V3 m_NormalizedDirection { get; set; }
        public Couleur m_Couleur { get; set; }

        public Lumiere(V3 directionLumiere, Couleur couleur)
        {
            this.m_Direction = directionLumiere;
            this.m_Couleur = couleur;
            this.m_NormalizedDirection = m_Direction;
            this.m_NormalizedDirection.Normalize();
        }

    }
}