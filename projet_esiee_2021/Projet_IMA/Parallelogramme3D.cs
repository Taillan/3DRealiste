﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Parallelogramme3D : Objet3D
    {
        private V3 m_Longueur { get; set; }
        private V3 m_Largeur { get; set; }
        private V3 m_Origine { get; set; }

        public Parallelogramme3D(V3 centre, V3 longueur, V3 largeur, Lumiere key_lumiere, Lumiere fill_lumiere, Texture texture, Texture bump_texture, float coefficient_diffus = 0.006f, float coefficient_speculaire = .0001f, float puissance_speculaire = 60, float coefficient_bumpmap = .005f, float pas = .001f) : base(centre, key_lumiere, fill_lumiere, texture, bump_texture, coefficient_diffus, coefficient_speculaire, puissance_speculaire, coefficient_bumpmap, pas)
        {
            m_Longueur = longueur;
            m_Largeur = largeur;
            m_Origine = centre - (1 / 2) * longueur + (1 / 2) * largeur;
        }

        protected override V3 getCoords(float u, float v)
        {
            float x3D = m_Origine.x + u * m_Longueur.x + v * m_Largeur.x;
            float y3D = m_Origine.y + u * m_Longueur.y + v * m_Largeur.y;
            float z3D = m_Origine.z + u * m_Longueur.z + v * m_Largeur.z;
            return new V3(x3D, y3D, z3D);
        }

        protected override void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv)
        {
            float dxdu = m_Longueur.x;
            float dxdv = m_Largeur.x;

            float dydu = m_Longueur.y;
            float dydv = m_Largeur.y;

            float dzdu = m_Longueur.z;
            float dzdv = m_Largeur.z;

            dMdu = new V3(dxdu, dydu, dzdu);
            dMdv = new V3(dxdv, dydv, dzdv);
        }

        protected override V3 getNormal(V3 PixelPosition)
        {
            V3 normal = m_Longueur ^ m_Largeur;
            normal.Normalize();
            return normal;
        }

        public override bool testIntersection(V3 origineRayon, V3 directionRayon)
        {
            // A FAIRE
            return true;
        }

        public override void Draw()
        {
            for (float u = 0; u < 1; u += m_Pas)
            {
                for (float v = 0; v < 1; v += m_Pas)
                {
                    V3 PixelPosition = getCoords(u, v);

                    // projection orthographique => repère écran

                    int x_ecran = (int)(PixelPosition.x);
                    int y_ecran = (int)(PixelPosition.z);

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, getCouleur(PixelPosition, u, v));
                }
            }
        }
    }
}
