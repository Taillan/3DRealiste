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

        /// <summary>
        /// Longueur du parallelepipede
        /// </summary>
        private V3 m_Longueur { get; set; }
        /// <summary>
        /// Largeur du parallelepipede
        /// </summary>
        private V3 m_Largeur { get; set; }
        /// <summary>
        /// Hauteur du parallelepipede
        /// </summary>
        private V3 m_Hauteur { get; set; }
        /// <summary>
        /// Origine du parallelepipede3D (à situer "en bas à gauche" du parallelepipede)
        /// D'où on applique les vecteurs m_Longueur et m_Largeur et m_Hauteur pour le tracer.
        /// </summary>
        private V3 m_Origine { get; set; }
        public Parallelepipede3D(V3 centre, V3 longueur, V3 largeur, V3 hauteur, Texture texture, Texture bump_texture, float coefficient_diffus = 0.006f, float coefficient_speculaire = .0001f, float puissance_speculaire = 60, float coefficient_bumpmap = .005f, float pas = .001f)
        {
            /*m_Longueur = longueur;
            m_Largeur = largeur;
            m_Hauteur = hauteur;*/
            //V3 origine = centre - (1 / 2) * longueur - (1 / 2) * largeur - (1/2) * hauteur;
            m_Faces = new List<Parallelogramme3D>();

            m_Faces.Add(new Parallelogramme3D(centre, longueur, largeur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre, hauteur, longueur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre, largeur, hauteur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre + hauteur, largeur , longueur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre + largeur, longueur, hauteur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));
            m_Faces.Add(new Parallelogramme3D(centre + longueur, largeur, hauteur, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas));

            /*m_Faces.Add(face1);
            m_Faces.Add(face2);
            m_Faces.Add(face3);
            m_Faces.Add(face4);
            m_Faces.Add(face5);
            m_Faces.Add(face6);*/
        }
    }
}
