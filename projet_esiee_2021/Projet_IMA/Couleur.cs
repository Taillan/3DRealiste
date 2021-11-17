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

        #region Constructeurs
        /// <summary>
        /// Constructeur de couleur à partir de 3 valeur RVB comprises entre 0 et 1
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
        /// Constructeur de couleur à partir d'une autre couleur
        /// </summary>
        /// <param name="c">Couleur à copier</param>
        public Couleur(Couleur c)
        {
            this.m_R = c.m_R;
            this.m_V = c.m_V;
            this.m_B = c.m_B;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Définit la couleur depuis les valeurs en 255
        /// </summary>
        /// <param name="RR">Intensité du rouge</param>
        /// <param name="VV">Intensité du vert</param>
        /// <param name="BB">Intensité du bleu</param>
        public void From255(byte RR, byte VV, byte BB)
        {
            m_R = (float)(RR / 255.0);
            m_V = (float)(VV / 255.0);
            m_B = (float)(BB / 255.0);
        }

        /// <summary>
        /// Permet de transposer notre couleur de notre propre objet "Couleur"
        /// en couleur système C# (System.Drawing.Color)
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="c"></param>
        static public  void Transpose(ref Couleur cc, System.Drawing.Color c)
        {
            cc.m_R = (float) (c.R / 255.0);
            cc.m_V = (float) (c.G / 255.0);
            cc.m_B = (float) (c.B / 255.0);
        }

        /// <summary>
        /// Vérifie que les valeurs R,V,B sont inférieures à 1.0f
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
        /// <param name="RR">Valeur de retour en octet pour le rouge</param>
        /// <param name="VV">Valeur de retour en octet pour le vert</param>
        /// <param name="BB">Valeur de retour en octet pour le bleu</param>
        public void To255(out byte RR, out byte VV, out byte BB)
        {
            RR = (byte)(m_R * 255);
            VV = (byte)(m_V * 255);
            BB = (byte)(m_B * 255);
        }

        /// <summary>
        /// Convertit la couleur finale (objet Color et non Couleur) pour être interprétée par l'écran (Ecran.cs)
        /// </summary>
        /// <returns>Color finale</returns>
        public Color Convertion()
        {
            check();
            byte RR, VV, BB;
            To255(out RR, out VV, out BB);
            return Color.FromArgb(RR, VV, BB);
        }
        /// <summary>
        /// Renvoie les niveaux de gris de la couleur, utile pour le BumpMap
        /// </summary>
        /// <returns>Le niveau de gris de la couleur</returns>
        public float GreyLevel()
        {
            return (m_R + m_B + m_V) / 3.0f;
        }
        #endregion

        #region Surcharge des opérateurs
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
