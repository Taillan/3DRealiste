using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Projet_IMA
{
    public struct Couleur
    {
        public float m_R, m_V, m_B;	// composantes R,V,B comprises entre 0 et 1

        public static Couleur m_Red   = new Couleur(1, 0, 0);
        public static Couleur m_Green = new Couleur(0, 1, 0);
        public static Couleur m_Blue  = new Couleur(0, 0, 1);
        public static Couleur m_Void = new Couleur(0, 0, 0);

        // constructeurs

        #region "Constructeurs"

        /// <summary>
        /// Constructeur de la couleur à partir de 3 valeur RGB compris entre 0 et 1
        /// </summary>
        /// <param name="R">Intensité du Rouge</param>
        /// <param name="V">Intensité du Vert</param>
        /// <param name="B">Intensité du Bleu</param>
        public Couleur(float R, float V, float B)
        {
            this.m_R = R;
            this.m_V = V;
            this.m_B = B;
        }


        /// <summary>
        /// Constructeur de la couleur à partir d'une couleur
        /// </summary>
        /// <param name="c"></param>
        public Couleur(Couleur c)
        {
            this.m_R = c.m_R;
            this.m_V = c.m_V;
            this.m_B = c.m_B;
        }

        #endregion

        #region "methodes"


        /// <summary>
        /// Renvoi le niveux de gris
        /// </summary>
        /// <returns>Valeur dun niveau de gris de la coueleur</returns>
        public float GreyLevel()						// utile pour le Bump Map
        {
            return (m_R + m_B + m_V) / 3.0f;
        }


        /// <summary>
        /// Set la couleur depuis des valeurs en 255
        /// </summary>
        /// <param name="RR">Intensite du rouge</param>
        /// <param name="VV">Intensite du vert</param>
        /// <param name="BB">Intensite du bleu</param>
        public void From255(byte RR, byte VV, byte BB)
        {
            m_R = (float)(RR / 255.0);
            m_V = (float)(VV / 255.0);
            m_B = (float)(BB / 255.0);
        }

        /// <summary>
        /// Transpose  ???????
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="c"></param>
        static public void Transpose(ref Couleur cc, System.Drawing.Color c)
        {
            cc.m_R = (float)(c.R / 255.0);
            cc.m_V = (float)(c.G / 255.0);
            cc.m_B = (float)(c.B / 255.0);
        }


        /// <summary>
        /// Verifie que les valeurs des couleurs soit bien entre 0 et 1
        /// </summary>
        public void check()
        {
            if (m_R > 1.0) m_R = 1.0f;
            if (m_V > 1.0) m_V = 1.0f;
            if (m_B > 1.0) m_B = 1.0f;
        }

        /// <summary>
        /// Renvoi la valeur de la couleur au format (255,255,255)
        /// </summary>
        /// <param name="RR"></param>
        /// <param name="VV"></param>
        /// <param name="BB"></param>
        public void To255(out byte RR, out byte VV, out byte BB)
        {
            RR = (byte)(m_R * 255);
            VV = (byte)(m_V * 255);
            BB = (byte)(m_B * 255);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Color Convertion()
        {
            check();
            byte RR, VV, BB;
            To255(out RR, out VV, out BB);
            return Color.FromArgb(RR, VV, BB);
        }
        #endregion


        #region "Surcharge des opérateur"

        public static Couleur operator +(Couleur a, Couleur b)
        {
            return new Couleur(a.m_R + b.m_R, a.m_V + b.m_V, a.m_B + b.m_B);
        }

        public static Couleur operator -(Couleur a, Couleur b)
        {
            return new Couleur(a.m_R - b.m_R, a.m_V - b.m_V, a.m_B - b.m_B);
        }

        public static Couleur operator -(Couleur a)
        {
            return new Couleur(-a.m_R, -a.m_V, -a.m_B);
        }

        public static Couleur operator *(Couleur a, Couleur b)
        {
            return new Couleur(a.m_R * b.m_R, a.m_V * b.m_V, a.m_B * b.m_B);
        }

        public static Couleur operator *(float a, Couleur b)
        {
            return new Couleur(a * b.m_R, a * b.m_V, a * b.m_B);
        }

        public static Couleur operator *(Couleur b, float a)
        {
            return new Couleur(a * b.m_R, a * b.m_V, a * b.m_B);
        }

        public static Couleur operator /(Couleur b, float a)
        {
            return new Couleur(b.m_R / a, b.m_V / a, b.m_B / a);
        }

        public static bool operator ==(Couleur a, Couleur b)
        {
            return a.m_R == b.m_R  && a.m_V == b.m_V && a.m_B == b.m_B;
        }
        public static bool operator !=(Couleur a, Couleur b)
        {
            return a.m_R != b.m_R && a.m_V != b.m_V && a.m_B != b.m_B;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Couleur && Equals((Couleur)obj);
        }

        public override int GetHashCode() => this.GetHashCode();

        #endregion
    }
}
