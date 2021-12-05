using System.Drawing;

namespace Projet_IMA
{
    /// <summary>
    /// Permet de définir une couleur à trois composantes rouge, verte et bleue.
    /// </summary>
    public struct Couleur
    {
        #region Attributs

        /// <summary>
        /// Composantes Rouge, Verte et Bleue (RVB) de la couleur comprises entre 0 et 1
        /// </summary>
        public float m_R, m_V, m_B;
        /// <summary>
        /// Attribut statique qui définit une couleur rouge
        /// </summary>
        public static Couleur s_Red   = new Couleur(1, 0, 0);
        /// <summary>
        /// Attribut statique qui définit une couleur verte
        /// </summary>
        public static Couleur s_Green = new Couleur(0, 1, 0);
        /// <summary>
        /// Attribut statique qui définit une couleur bleue
        /// </summary>
        public static Couleur s_Blue  = new Couleur(0, 0, 1);
        /// <summary>
        /// Attribut statique qui définit une couleur noire
        /// </summary>
        public static Couleur s_Void = new Couleur(0, 0, 0);

        #endregion

        #region Constructeurs

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
        /// Constructeur par copie de la couleur à partir d'une autre couleur
        /// </summary>
        /// <param name="c">Couleur qu'on veut copier pour construire notre nouvelle couleur</param>
        public Couleur(Couleur c)
        {
            this.m_R = c.m_R;
            this.m_V = c.m_V;
            this.m_B = c.m_B;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Renvoie les niveaux de gris associés à la couleur
        /// Nécessaire pour le Bump Map.
        /// </summary>
        /// <returns>Valeur du niveau de gris de la coueleur</returns>
        public float GreyLevel()
        {
            return (m_R + m_B + m_V) / 3.0f;
        }


        /// <summary>
        /// Permet de set la couleur depuis les valeurs en octet (255)
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
        /// Permet de transposer une Color C# de la classe System.Drawing.Color 
        /// en une "Couleur" (notre propre classe)
        /// </summary>
        /// <param name="cc">Référence de la couleur dont on veut modifier les composantes R,V,B</param>
        /// <param name="c">Color qu'on veut transposer.</param>
        static public void Transpose(ref Couleur cc, System.Drawing.Color c)
        {
            cc.m_R = (float)(c.R / 255.0);
            cc.m_V = (float)(c.G / 255.0);
            cc.m_B = (float)(c.B / 255.0);
        }


        /// <summary>
        /// Vérifie que les composantes R,V,B de la couleur en float sont bien inférieures à 1.0f,
        /// Mise à 1.0f sinon.
        /// </summary>
        public void check()
        {
            if (m_R > 1.0) m_R = 1.0f;
            if (m_V > 1.0) m_V = 1.0f;
            if (m_B > 1.0) m_B = 1.0f;
        }

        /// <summary>
        /// Renvoie la valeur de la couleur au format octet(255,255,255)
        /// </summary>
        /// <param name="RR">Composante rouge de la couleur au format octet</param>
        /// <param name="VV">Composante verte de la couleur au format octet</param>
        /// <param name="BB">Composante bleue de la couleur au format octet</param>
        public void To255(out byte RR, out byte VV, out byte BB)
        {
            RR = (byte)(m_R * 255);
            VV = (byte)(m_V * 255);
            BB = (byte)(m_B * 255);
        }


        /// <summary>
        /// Permet de convertir une Couleur en une Color C# de la classe System.Drawing.Color
        /// </summary>
        /// <returns>Color déterminée à partir d'une Couleur</returns>
        public Color Convertion()
        {
            check();
            byte RR, VV, BB;
            To255(out RR, out VV, out BB);
            return Color.FromArgb(RR, VV, BB);
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
