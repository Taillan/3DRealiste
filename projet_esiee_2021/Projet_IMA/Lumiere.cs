using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Lumiere
    {
        private V3 directionLumiere;
        private Couleur couleur;

        public Lumiere(V3 directionLumiere, Couleur couleur)
        {
            this.directionLumiere = directionLumiere;
            this.couleur = couleur;

        }
        public V3 getDirectionLumiere()
        {
            return this.directionLumiere;
        }
        
        public Couleur getCouleur()
        {
            return this.couleur;
        }

        public void setDirectionLumiere(V3 point)
        {
            this.directionLumiere = point;
        }

        public void setCouleur(Couleur Couleur)
        {
            this.couleur = Couleur;
        }


    }
}