using System;
namespace Projet_IMA
{
    /// <summary>
    /// Classe abstraite définissant un Objet3D pouvant être placé dans l'espace
    /// </summary>
    abstract class Objet3D
    {
        #region Attributs

        /// <summary>
        /// Centre de l'Objet3D
        /// </summary>
        protected V3 m_CentreObjet { get; set; }
        /// <summary>
        /// Texture appliquée sur l'Objet3D
        /// </summary>
        protected Texture m_Texture { get; set; }
        /// <summary>
        /// Texture de bump appliquée sur l'Objet3D
        /// </summary>
        private Texture m_BumpTexture { get; set; }
        /// <summary>
        /// Coefficient de diffus de l'Objet3D, plus le coefficient est faible, plus le diffus sera "fondu"
        /// </summary>
        private float m_CoefficientDiffus { get; set; }
        /// <summary>
        /// Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"
        /// </summary>
        private float m_CoefficientSpeculaire { get; set; }
        /// <summary>
        /// Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand
        /// </summary>
        private float m_PuissanceSpeculaire { get; set; }
        /// <summary>
        /// Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé
        /// </summary>
        private float m_CoefficientBumpMap { get; set; }
        /// <summary>
        /// Ecart entre le placement des pixels de l'objet. Plus l'écart est grand, moins de pixels seront dessinés
        /// </summary>
        protected float m_Pas { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur d'un objet 3D
        /// </summary>
        /// <param name="centre">Centre de l'Objet3D</param>
        /// <param name="texture">Texture appliquée sur l'Objet3D</param>
        /// <param name="bump_texture">Texture de bump appliquée sur l'Objet3D</param>
        /// <param name="coefficient_diffus">Coefficient de diffus de l'Objet3D, plus le coefficient est faible, plus le diffus sera "fondu"</param>
        /// <param name="coefficient_speculaire">Coefficient spéculaire, plus le coefficient est faible, plus le spéculaire sera "fondu"</param>
        /// <param name="puissance_speculaire">Puissance spéculaire, plus la puissance est élevée, moins le spéculaire sera grand</param>
        /// <param name="coefficient_bumpmap">Coefficient de Bump Mapping, plus il sera élevé, plus l'effet 3D sera élevé</param>
        /// <param name="pas">Ecart entre le placement des pixels de l'objet. Plus l'écart est grand, moins de pixels seront dessinés</param>
        public Objet3D(V3 centre, Texture texture, Texture bump_texture, float coefficient_diffus, float coefficient_speculaire, float puissance_speculaire, float coefficient_bumpmap, float pas)
        {
            m_CentreObjet = centre;
            m_CoefficientDiffus = coefficient_diffus;
            m_CoefficientSpeculaire = coefficient_speculaire;
            m_PuissanceSpeculaire = puissance_speculaire;
            m_Texture = texture;
            m_BumpTexture = bump_texture;
            m_CoefficientBumpMap = coefficient_bumpmap;
            m_Pas = pas;
        }
        #endregion

        #region Méthodes abstraites

        /// <summary>
        /// Calcule les coordonnées du Pixel 3D de l'objet grâce aux positions u et v sur la texture 2D.
        /// </summary>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Coordonnées du Pixel 3D associé aux positions u et v de la texture 2D</returns>
        protected abstract V3 getCoords(float u, float v);

        /// <summary>
        /// Calcule les dérivées partielles de la position du Pixel 3D (M) en fonction des positions u et v de la texture 2D.
        /// Nécessaire pour pouvoir déterminer le bump mapping.
        /// </summary>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet</param>
        /// <param name="dMdu">Dérivée de M (position du point actuel) en fonction de u</param>
        /// <param name="dMdv">Dérivée de M (position du point actuel) en fonction d v</param>
        protected abstract void getDerivedCoords(float u, float v, out V3 dMdu, out V3 dMdv);

        /// <summary>
        /// Calcule la normale du pixel passé en paramètre
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut obtenir la normale</param>
        /// <returns>Normale du pixel passé en paramètre</returns>
        protected abstract V3 getNormal(V3 PixelPosition);

        /// <summary>
        /// Classe abstraite définissant comment dessiner l'objet héritant de cette classe
        /// </summary>
        /// <param name="pas">Écart entre chaque point tracé à l'écran</param>
        public abstract void Draw();
          
        /// <summary>
        /// Permet de savoir si le rayon passé en paramètre rentre en intersection avec l'Objet3D.
        /// Si oui, il retourne le Pixel3D où se trouve l'intersection 
        /// ainsi que les coordonnées u & v du pixel 2d de la texture associée à ce Pixel 3D.
        /// </summary>
        /// <param name="OrigineRayon">Origine du rayon dont on veut tester l'intersection</param>
        /// <param name="DirectionRayon">Direction du rayon dont on veut tester l'intersection</param>
        /// <param name="DistanceIntersection">Longueur du rayon de l'origine jusqu'à l'intersection trouvée</param>
        /// <param name="PixelPosition">Position du pixel où a eu lieu l'intersection</param>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet associées au point d'intersection</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet associées au point d'intersection</param>
        /// <returns>Vrai s'il y a une intersection, faux sinon.</returns>
        public abstract bool IntersectionRayon(V3 OrigineRayon, V3 DirectionRayon, out float DistanceIntersection, out V3 PixelPosition, out float u, out float v);

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Permet de retourner la couleur de la texture sur les coordonées données
        /// </summary>
        /// <param name="u">Position du vecteur u qui pointe sur le pixel de l'objet</param>
        /// <param name="v">Position du vecteur v qui pointe sur le pixel de l'objet</param>
        /// <returns>Couleur du pixel pointé</returns>
        protected virtual Couleur getCouleurPixel(float u, float v)
        {
            return m_Texture.LireCouleur(u, v);
        }

        /// <summary>
        /// Permet d'obtenir un vecteur aléatoire normalisé. Utilisé dans plusieurs méthodes
        /// notamment le PathTracer, pour permettre de générer un vecteur aléatoire depuis un point
        /// pour calculer des potentielles intersections avec une lampe.
        /// </summary>
        /// <returns>Retourne un vecteur aléatoire normalisé</returns>
        private V3 getRandomVector()
        {
            Random rnd = Program.s_Random;
            V3 vec;
            double theta = 2 * IMA.PI * rnd.NextDouble();
            double phi = Math.Acos(2 * rnd.NextDouble() - 1.0);
            double x = Math.Sin(theta) * Math.Cos(phi);
            double y = Math.Cos(theta);
            double z = Math.Sin(phi) * Math.Sin(theta);
            vec = new V3((float)x, (float)y, (float)z);
            vec.Normalize();
            return vec;
        }

        private V3 getRandomVectorInHemisphere(V3 N)
        {
            V3 vec = getRandomVector();
            if (vec * N < 0)
            {
                vec = -vec;
            }
            return vec;
        }
        /// <summary>
        /// Renvoie la couleur ambiante du pixel correspondant aux coordonnées de la texture de l'objet.
        /// </summary>
        /// <param name="lumiere">Lumière appliquée sur l'objet</param>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Couleur ambiante du pixel passé en paramètre</returns>
        private Couleur getCouleurAmbiante(Lumiere lumiere, float u, float v)
        {
            return getCouleurPixel(u, v) * lumiere.m_Couleur;
        }

        /// <summary>
        /// Renvoie la couleur ambiante attenuée du pixel correspondant aux coordonnées de la texture de l'objet.
        /// </summary>
        /// <param name="lumiere">Lumière appliquée sur l'objet</param>
        /// <param name="u">Coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Couleur ambiante attenuée du pixel passé en paramètre</returns>
        public Couleur getLowCouleurAmbiante(Lumiere lumiere, float u, float v)
        {
            return getCouleurAmbiante(lumiere, u,v) * .0008f;
        }

        /// <summary>
        /// Calcule la couleur diffuse du pixel passé en paramère
        /// </summary>
        /// <param name="lumiere">Lumière appliquée sur l'objet</param>
        /// <param name="pixelNormal">Vecteur décrivant la normale du pixel dont on veut obtenir la couleur diffuse</param>
        /// <param name="u">Position des coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Position des coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Couleur diffuse du pixel passé en paramètre</returns>
        private Couleur getCouleurDiffuse(Lumiere lumiere, V3 pixelNormal, float u, float v)
        {
            float cosAlpha = pixelNormal * lumiere.m_NormalizedDirection;
            if (cosAlpha > 0)
            {
                return getCouleurAmbiante(lumiere,u , v)  * (cosAlpha) * m_CoefficientDiffus;
            }
            else
            {
                return Couleur.s_Void;
            }
        }

        /// <summary>
        /// Calcule la couleur spéculaire du pixel passé en paramère
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut trouver la couleur spéculaire</param>
        /// <param name="N">Normale associée au pixel passé en paramètre</param>
        /// <param name="u">Position des coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Position des coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Couleur spéculaire du pixel passé en paramètre</returns>
        private Couleur getCouleurSpeculaire(Lumiere lumiere, V3 PixelPosition, V3 N, float u, float v)
        {
            V3 L = lumiere.m_Direction;
            V3 R = 2*N*(N*L)-L;
            V3 D = (BitmapEcran.s_CameraPosition - PixelPosition);
            R.Normalize();
            D.Normalize();
            float RD = R * D;
            if ((RD) > 0)
            {
                return lumiere.m_Couleur * getCouleurAmbiante(lumiere, u, v)  * (float)Math.Pow(RD, m_PuissanceSpeculaire); // *m_CoefficientSpeculaire
            }
            else
            {
                return Couleur.s_Void;
            }
        }

        /// <summary>
        /// Calcule la normale bumpée du pixel actuel grâce à la texture de bumping de l'objet
        /// </summary>
        /// <param name="PixelPosition"></param>
        /// <param name="u">Position des coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Position des coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Normale bumpée du pixel actuel</returns>
        private V3 getBumpedNormal(V3 PixelPosition, float u, float v)
        {
            V3 N = getNormal(PixelPosition);

            float K = m_CoefficientBumpMap;
            getDerivedCoords(u, v, out V3 dMdu, out V3 dMdv);
            this.m_BumpTexture.Bump(u, v, out float dhdu, out float dhdv);

            return N + K * ((dMdu ^ (N * dhdv)) + ((N * dhdu) ^ dMdv));
        }

        /// <summary>
        /// Permet de déterminer si le pixel de l'objet est obstrué par un autre objet qui lui cache la lumière passée en paramètre
        /// </summary>
        /// <param name="_lumiere">Direction de la lumière dont on veut tester l'obstruction</param>
        /// <param name="_PixelPosition">Position du pixel dont on veut tester l'obstruction</param>
        /// <returns>Vrai si le pixel est obstrué, faux sinon.</returns>
        private bool isInShadow(V3 _lumiereDirection, V3 _PixelPosition)
        {
            foreach (Objet3D autres_objets in BitmapEcran.s_Objets)
            {
                if (autres_objets != this)
                {
                    if (autres_objets.IntersectionRayon(_PixelPosition, _lumiereDirection, out _, out V3 PixelPosition2, out _, out _))
                    {
                        if (BitmapEcran.s_CameraPosition - PixelPosition2 < BitmapEcran.s_CameraPosition - _PixelPosition)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Calcule la couleur totale du pixel passé en paramère
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut trouver la couleur spéculaire</param>
        /// <param name="u">Position des coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Position des coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Couleur totale du pixel passé en paramètre</returns>
        public virtual Couleur getCouleur(V3 PixelPosition, float u, float v, RenderMode RM)
        {
            Couleur finalColor = new Couleur(0,0,0);
            if (RM==RenderMode.PATH_TRACING)
            {
                finalColor = PathTracer(PixelPosition, u, v, 1);
            }
            else if (RM == RenderMode.SIMPLE)
            {
                finalColor = SimpleRender(PixelPosition, u, v);
            }
            return finalColor;
        }

        /// <summary>
        /// Technique de render simple, utilisée pour la première partie du projet
        /// </summary>
        /// <param name="PixelPosition">Position du pixel dont on veut trouver la couleur spéculaire</param>
        /// <param name="u">Position des coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Position des coordonnées en ordonnées de la texture l'objet</param>
        /// <returns>Couleur totale du pixel passé en paramètre</returns>
        private Couleur SimpleRender(V3 PixelPosition, float u, float v)
        {
            Couleur finalColor = Couleur.s_Void;
            Random rnd = Program.s_Random;
            //Paramètrage du SoftShadow
            //n = nombre de rayon calculé
            //Alpha = taille du tilt
            int n = 100;
            float Alpha = 0.2f;

            foreach (Lumiere lumiere in BitmapEcran.s_Lumieres)
            {
                V3 N = getBumpedNormal(PixelPosition, u, v);
                Couleur Ambiant = getLowCouleurAmbiante(lumiere, u, v);
                Couleur Diffus = getCouleurDiffuse(lumiere, N, u, v);

                for (int i = 0; i < n; i++)
                {
                    V3 r = new V3((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble());
                    r.Normalize();
                    V3 ShadowVector = lumiere.m_Direction + Alpha * r;
                    ShadowVector.Normalize();

                    if (isInShadow(ShadowVector, PixelPosition))
                    {
                        finalColor += Ambiant;
                    }
                    else
                    {
                        if (Diffus != Couleur.s_Void)
                        {
                            Couleur Speculaire = getCouleurSpeculaire(lumiere, PixelPosition, N, u, v);
                            finalColor += Ambiant + Diffus + Speculaire;
                        }
                        else
                        {
                            finalColor += Ambiant + Diffus;
                        }
                    }
                } 
            }
            return finalColor/n;
        }

        /// <summary>
        /// Pour chaque point des objets rentrant en intersection avec la caméra,
        /// on lance des rayons pour le Pathtracer qui vont apporter une 
        /// contribution uniquement s’ils touchent une des lampes. 
        /// </summary>
        /// <param name="PixelPosition">Pixel dont on veut obtenir la couleur</param>
        /// <param name="u">Position des coordonnées en abscisses de la texture l'objet</param>
        /// <param name="v">Position des coordonnées en ordonnées de la texture l'objet</param>
        /// <param name="PathTracerLevel"></param>
        /// <returns>Retourne la couleur du pixel passé en paramètre en utilisant le PathTracing</returns>
        public Couleur PathTracer(V3 PixelPosition, float u, float v, int nbVectors)
        {
            Couleur finalColor = new Couleur(0, 0, 0);

            V3 N = getBumpedNormal(PixelPosition, u, v);

            Couleur total = new Couleur(0, 0, 0);
            for (int i = 0; i < nbVectors; i++)
            {
                V3 R = getRandomVectorInHemisphere(N);
                float DistanceIntersectionMax = float.MaxValue;
                foreach (Objet3D objet in BitmapEcran.s_Objets)
                {
                    if (objet.IntersectionRayon(PixelPosition, R, out float DistanceIntersection, out V3 IntersectedPixel, out float pU, out float pV))
                    {
                        if (DistanceIntersection > 0 && DistanceIntersection < DistanceIntersectionMax)
                        {
                            DistanceIntersectionMax = DistanceIntersection;
                            if (objet.isLumiere())
                            {
                                total = objet.getCouleurPixel(pU, pV) * 2f;
                            }
                            else
                            {
                                Lumiere lumiere_locale = new Lumiere(R, objet.getCouleurPixel(pU, pV));
                                Couleur Diffus = getCouleurDiffuse(lumiere_locale, N, u, v);
                                Couleur Speculaire = getCouleurSpeculaire(lumiere_locale, PixelPosition, N, u, v);
                                total = (Diffus + Speculaire);
                            }

                        }
                    }
                }
                Lumiere lumiere_totale = new Lumiere(R, total);
                finalColor += getCouleurDiffuse(lumiere_totale, N, u, v) + getCouleurSpeculaire(lumiere_totale, PixelPosition, N, u, v);
            }
            return finalColor / (float)nbVectors;
        }

        /// <summary>
        /// Permet de savoir si l'objet est de type lumière
        /// </summary>
        /// <returns>Vrai si l'objet est une lumière, faux sinon</returns>
        public virtual bool isLumiere()
        {
            return false;
        }

        #endregion
    }
}
