using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_IMA
{
    class Parallelepipede3D
    {
        /// <summary>
        /// Liste de toutes les faces du parallelepipede
        /// </summary>
        public List<Parallelogramme3D> m_Faces { get; set; }
        public Parallelepipede3D(V3 centre, V3 longueur, V3 largeur, V3 hauteur, Texture texture, Texture bump_texture, float coefficient_diffus = 1f, float coefficient_speculaire = .1f, float puissance_speculaire = 50, float coefficient_bumpmap = .005f, float pas = .001f)
        {
            m_Faces = new List<Parallelogramme3D>();

            m_Faces.Add(new Parallelogramme3D(centre, longueur, largeur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre, hauteur, longueur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre, largeur, hauteur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre + hauteur, largeur , longueur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre + largeur, longueur, hauteur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre + longueur, largeur, hauteur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
        }
    }
}
