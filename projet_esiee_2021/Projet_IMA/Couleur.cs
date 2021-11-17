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

        public void From255(byte RR, byte VV, byte BB)
        {
            m_R = (float)(RR / 255.0);
            m_V = (float)(VV / 255.0);
            m_B = (float)(BB / 255.0);
        }

        static public  void Transpose(ref Couleur cc, System.Drawing.Color c)
        {
            cc.m_R = (float) (c.R / 255.0);
            cc.m_V = (float) (c.G / 255.0);
            cc.m_B = (float) (c.B / 255.0);
        }

        public void check()
        {
            if (m_R > 1.0) m_R = 1.0f;
            if (m_V > 1.0) m_V = 1.0f;
            if (m_B > 1.0) m_B = 1.0f;
        }

        public void To255(out byte RR, out byte VV, out byte BB)
        {
            RR = (byte)(m_R * 255);
            VV = (byte)(m_V * 255);
            BB = (byte)(m_B * 255);
        }

        public Color Convertion()
        {
            check();
            byte RR, VV, BB;
            To255(out RR, out VV, out BB);
            return Color.FromArgb(RR, VV, BB);
        }

        public Couleur(float R, float V, float B)
        {
            this.m_R = R;
            this.m_V = V;
            this.m_B = B;
        }

        public Couleur(Couleur c)
        {
            this.m_R = c.m_R;
            this.m_V = c.m_V;
            this.m_B = c.m_B;
        }

        // méthodes

        public float GreyLevel()						// utile pour le Bump Map
        {
            return (m_R + m_B + m_V) / 3.0f;
        }

        // opérateurs surchargés

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
    }
}
