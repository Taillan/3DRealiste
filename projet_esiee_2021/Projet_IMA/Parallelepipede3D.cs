using System.Collections.Generic;

namespace Projet_IMA
{
    class Parallelepipede3D
    {
        /// <summary>
        /// Liste de toutes les faces du parallelepipede
        /// </summary>
        public List<Parallelogramme3D> m_Faces { get; set; }
        /// <summary>
        /// Constructeur d'un Parallelepipede3D
        /// </summary>
        /// <param name="centre">Centre de cet objet</param>
        /// <param name="longueur">Longueur de l'objet</param>
        /// <param name="largeur">Largeur de l'objet</param>
        /// <param name="hauteur">Hauteur de l'objet</param>
        /// <param name="lumiere">Lumiere applique à l'objet</param>
        /// <param name="texture">Texture applique à l'objet</param>
        /// <param name="bump_texture">Texture de bump appliquée sur la sphère</param>
        /// <param name="coefficient_diffus">Coefficient de diffus de la sphère, plus le coefficient est faible, plus le diffus sera "fondu"</param>
        /// <param name="coefficient_speculaire">Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"</param>
        /// <param name="puissance_speculaire">Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand</param>
        /// <param name="coefficient_bumpmap">Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé.</param>
        public Parallelepipede3D(V3 centre, V3 longueur, V3 largeur, V3 hauteur, Texture texture = null, Texture bump_texture = null, float coefficient_diffus = 1f, float coefficient_speculaire = .1f, float puissance_speculaire = 50, float coefficient_bumpmap = .005f, float pas = .001f)
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
