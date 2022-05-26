using System;
namespace Projet_IMA
{
    /// <summary>
    /// Permet de définir un vecteur 3D avec une composante x pour la largeur, y pour la profondeur et z pour la hauteur
    /// </summary>
    struct V3
    {
        #region Attributs

        /// <summary>
        /// Composante x du vecteur (abscisse/largeur)
        /// </summary>
        public float x;
        /// <summary>
        /// Composante y du vecteur (profondeur)
        /// </summary>
        public float y;
        /// <summary>
        /// Composante z du vecteur (ordonnée/hauteur)
        /// </summary>
        public float z;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur naturel en fonction de composantes float
        /// </summary>
        /// <param name="_x">Composante x du vecteur en float</param>
        /// <param name="_y">Composante y du vecteur en float</param>
        /// <param name="_z">Composante z du vecteur en float</param>
        public V3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        /// <summary>
        /// Constructeur naturel en fonction de composantes int
        /// </summary>
        /// <param name="_x">Composante x du vecteur en int</param>
        /// <param name="_y">Composante y du vecteur en int</param>
        /// <param name="_z">Composante z du vecteur en int</param>
        V3(int _x, int _y, int _z)
        {
            x = (float)_x;
            y = (float)_y;
            z = (float)_z;
        }

        /// <summary>
        /// Constructeur par copie
        /// </summary>
        /// <param name="t">Vecteur dont on veut copier les attributs pour la construction du nôtre.</param>
        public V3(V3 t)
        {
            x = t.x;
            y = t.y;
            z = t.z;
        }

        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Méthode retournant la norme du vecteur.
        /// </summary>
        /// <returns>Norme du vecteur</returns>
        public float Norm()
        {
            return (float) IMA.Sqrtf(x * x + y * y + z * z);
        }

        /// <summary>
        /// Méthode retournant la norme du vecteur au carré.
        /// </summary>
        /// <returns>Norme du vecteur au carré.</returns>
        public float Norme2()
        {
            return x * x + y * y + z * z;
        }

        /// <summary>
        /// Méthode permettant de normaliser le vecteur
        /// </summary>
        public void Normalize()
        {
            float n = Norm();
            if (n == 0) return;
            x /= n;
            y /= n;
            z /= n;
        }

        /// <summary>
        /// Permet d'obtenir un vecteur aléatoire normalisé. Utilisé dans plusieurs méthodes
        /// notamment le PathTracer, pour permettre de générer un vecteur aléatoire depuis un point
        /// pour calculer des potentielles intersections avec une lampe.
        /// </summary>
        /// <returns>Retourne un vecteur aléatoire normalisé</returns>
        public static V3 getRandomVector()
        {
            V3 vec;
            double theta = 2 * Math.PI * Program.random.Value.NextDouble();
            double phi = Math.Acos(2 * Program.random.Value.NextDouble() - 1.0);
            double x = Math.Sin(theta) * Math.Cos(phi);
            double y = Math.Sin(phi) * Math.Sin(theta);
            double z = Math.Cos(theta);
            vec = new V3((float)x, (float)y, (float)z);
            vec.Normalize();
            return vec;
        }

        /// <summary>
        /// Permet d'obtenir un vecteur aléatoire normalisé. Utilisé dans plusieurs méthodes
        /// notamment le PathTracer, pour permettre de générer un vecteur aléatoire depuis un point
        /// pour calculer des potentielles intersections avec une lampe.
        /// </summary>
        /// <returns>Retourne un vecteur aléatoire normalisé</returns>
        public static V3 getRandomVectorInHemisphere(V3 N)
        {
            V3 vec = getRandomVector();
            if (vec * N < 0)
            {
                vec = -vec;
            }
            return vec;

        }
        #endregion

        #region Surcharge des opérateurs
        public static V3 operator +(V3 a, V3 b)
        {
            V3 t;
            t.x = a.x + b.x;
            t.y = a.y + b.y;
            t.z = a.z + b.z;
            return t;
        }

        public static V3 operator -(V3 a, V3 b)
        {
            V3 t;
            t.x = a.x - b.x;
            t.y = a.y - b.y;
            t.z = a.z - b.z;
            return t;
        }

        public static V3 operator -(V3 a)
        {
            V3 t;
            t.x = -a.x;
            t.y = -a.y;
            t.z = -a.z;
            return t;
        }

        public static V3 operator ^ (V3 a, V3 b)// produit vectoriel
        {
            V3 t;
            t.x = a.y * b.z - a.z * b.y;
            t.y = a.z * b.x - a.x * b.z;
            t.z = a.x * b.y - a.y * b.x;
            return t;
        }

        public static float operator * (V3 a,V3 b)// produit scalaire
        {
            return a.x*b.x+a.y*b.y+a.z*b.z;
        }

        public static float operator /(V3 a, V3 b)         
        {
            return a.x / b.x + a.y / b.y + a.z / b.z;
        }



        public static V3 operator *(float a, V3 b)
        {
            V3 t;
            t.x = b.x*a;
            t.y = b.y*a;
            t.z=  b.z*a;
            return t;
        }

        public static V3 operator *(V3 b, float a)
        {
            V3 t;
            t.x = b.x*a;
            t.y = b.y*a;
            t.z=  b.z*a;
            return t;
        }

        public static V3 operator /(V3 b, float a)
        {
            V3 t;
            t.x = b.x/a;
            t.y = b.y/a;
            t.z=  b.z/a;
            return t;
        }

        public static bool operator <(V3 a, V3 b)
        {
            return a.Norm() < b.Norm();
        }

        public static bool operator >(V3 a, V3 b)
        {
            return a.Norm() > b.Norm();
        }

        public static float prod_scal(ref V3 u, ref V3 v)
        {
            return u.x * v.x + u.y * v.y + u.z * v.z;
        }
        #endregion
    }
}
