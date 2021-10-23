using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            Lumiere lumiere = new Lumiere(new V3(1, -1.5f, 1), new Couleur(150, 255, 255));

            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Sphère en 3D
            /// 
            //////////////////////////////////////////////////////////////////////////
            Sphere3D SphereA = new Sphere3D(new V3(300, 200, 300), 150, Couleur.Red);
            SphereA.DrawSphere(0.005f, lumiere);
        }
    }
}
