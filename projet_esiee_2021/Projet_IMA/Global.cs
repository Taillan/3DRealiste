using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_IMA
{
    internal class Global
    {
        public static int rayon;
        public static int thread;
        public static RenderMode render_mode=RenderMode.PATH_TRACING;
        public enum RenderMode { SIMPLE, PATH_TRACING, VPL, RAY_TRACING };

    }
}
