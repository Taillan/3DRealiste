namespace Projet_IMA
{
    internal class Global
    {
        /// <summary>
        /// Nombre de rayons utilisés pour le Path Tracing
        /// </summary>
        public static int NbRayonsPT;
        /// <summary>
        /// Nombre de threads utilisés pour le multithreading
        /// </summary>
        public static int NbThreads = 1;
        /// <summary>
        /// Mode de rendu
        /// </summary>
        public static RenderMode render_mode=RenderMode.PATH_TRACING;
        public enum RenderMode { SIMPLE, PATH_TRACING, VPL, RAY_TRACING };

    }
}
