using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Lumiere
    {
        public V3 Direction { get; set; }
        public V3 NormalizedDirection { get; set; }
        public Couleur Couleur { get; set; }

        public Lumiere(V3 directionLumiere, Couleur couleur)
        {
            this.Direction = directionLumiere;
            this.Couleur = couleur;
            this.NormalizedDirection = Direction;
            this.NormalizedDirection.Normalize();
        }

    }
}