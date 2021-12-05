using System;
namespace Projet_IMA
{
    /// <summary>
    /// Classe référençant des attributs et méthodes statiques
    /// nécessaires aux opérations trigonométriques sur l'espace à trois dimensions.
    /// </summary>
    class IMA
    {
        #region Attributs statiques
        /// <summary>
        /// PI multiplié par deux.
        /// </summary>
        static public float DPI = (float) (Math.PI * 2);
        /// <summary>
        /// PI
        /// </summary>
        static public float PI  = (float) (Math.PI);
        /// <summary>
        /// PI divisé par deux.
        /// </summary>
        static public float PI2 = (float) (Math.PI / 2);
        /// <summary>
        /// Pi divisé par quatre.
        /// </summary>
        static public float PI4 = (float) (Math.PI / 4);
        #endregion

        #region Méthodes statiques

        /// <summary>
        /// Retourne le cosinus en float de l'angle passé en paramètre
        /// </summary>
        /// <param name="theta">Angle dont on veut calculer le cosinus en float</param>
        /// <returns>Cosinus en float de l'angle passé en paramètre</returns>
        static public float Cosf(float theta) { return (float) Math.Cos(theta); }
        /// <summary>
        /// Retourne le sinus en float de l'angle passé en paramètre
        /// </summary>
        /// <param name="theta">Angle dont on veut calculer le sinus en float</param>
        /// <returns>Sinus en float de l'angle passé en paramètre</returns>
        static public float Sinf(float theta) { return (float) Math.Sin(theta); }
        /// <summary>
        /// Retourne l'Arccosinus en float de l'angle passé en paramètre
        /// </summary>
        /// <param name="theta">Angle dont on veut calculer l'arccosinus en float</param>
        /// <returns>Arccosinus en float de l'angle passé en paramètre</returns>
        static public float Acosf(float theta) { return (float)Math.Acos(theta); }
        /// <summary>
        /// Retourne l'Arcsinus en float de l'angle passé en paramètre
        /// </summary>
        /// <param name="theta">Angle dont on veut calculer l'arcsinus en float</param>
        /// <returns>Arcsinus en float de l'angle passé en paramètre</returns>
        static public float Asinf(float theta) { return (float)Math.Asin(theta); }
        /// <summary>
        /// Méthode permettant d'obtenir la racine carrée en float de l'argument passé en paramètre
        /// </summary>
        /// <param name="v">Float dont on veut calculer la racine carrée</param>
        /// <returns>Racine carrée du float passé en paramètre</returns>
        static public float Sqrtf(float v)    { return (float) Math.Sqrt(v); }

        /// <summary>
        /// Nombre aléatoire
        /// </summary>
        static public Random Ran;
        /// <summary>
        /// Méthode initialisant le nombre aléatoire.
        /// </summary>
        static public void InitRand() { Ran = new Random(); }
        static public float RandNP(float v) { return ((float) Ran.NextDouble()-0.5f)*2*v; }
        static public float RandP(float v)  { return ((float)Ran.NextDouble() ) * v; }

        /// <summary>
        /// Méthode permettant de recalculer les coordonnées u et v de la texture associées au Pixel 3D d'une sphère passé en paramètre
        /// </summary>
        /// <param name="P">Position du pixel 3D</param>
        /// <param name="r">Rayon de la sphère</param>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet</param>
        static public  void Invert_Coord_Spherique(V3 P, float r, out float u, out float v)
        {
            P = P / r;
            if (P.z >= 1) { u =(float) IMA.PI2 ; v = 0; }
            else if (P.z <= -1) { u = (float)-IMA.PI2 ; v = 0; }
            else
            {
                v = (float) Math.Asin(P.z);
                float t = (float) (P.x / IMA.Cosf(v));
                if (t <= -1) { u = (float) IMA.PI; }
                else if (t >= 1) { u = 0; }
                else
                {
                    if (P.y < 0) u = (float) ( 2 * IMA.PI - Math.Acos(t));
                    else u = (float) Math.Acos(t);
                }
            }
        }
        #endregion
    }
}
