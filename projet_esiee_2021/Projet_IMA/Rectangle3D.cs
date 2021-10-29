using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Rectangle3D
    {
        private V3 m_Origine;
        private V3 m_Cote1;
        private V3 m_Cote2;
        private Couleur m_CRect;

        public Rectangle3D(V3 Origine, V3 Cote1, V3 Cote2)
        {
            this.m_Origine = Origine;
            this.m_Cote1 = Cote1;
            this.m_Cote2 = Cote2;
            this.m_CRect = Couleur.m_Red;
        }
        public Rectangle3D(V3 Origine, V3 Cote1, V3 Cote2, Couleur C)
        {
            this.m_Origine = Origine;
            this.m_Cote1 = Cote1;
            this.m_Cote2 = Cote2;
            this.m_CRect = C;
        }

        public void setOrigine(V3 Coordone)
        {
            this.m_Origine = Coordone;
        }

        public V3 getOrigine()
        {
            return this.m_Origine;
        }

        public void setCote1(V3 Coordone)
        {
            this.m_Cote1 = Coordone;
        }

        public V3 getCote1()
        {
            return this.m_Cote1;
        }
        public void setCote2(V3 Coordone)
        {
            this.m_Cote2 = Coordone;
        }

        public V3 getCote2()
        {
            return this.m_Cote2;
        }

        public void DrawRectangle3D(float pas)
        {
            for (float u = 0; u < 1; u += pas)  // echantillonage fnt paramétrique
                for (float v = 0; v < 1; v += pas)
                {

                    V3 P3D = m_Origine + u * m_Cote1 + v * m_Cote2;

                    // projection orthographique => repère écran

                    int x_ecran = (int)(P3D.x);
                    int y_ecran = (int)(P3D.y);
                    for (int i = 0; i < 100; i++)  // pour ralentir et voir l'animation - devra être retiré
                        BitmapEcran.DrawPixel(x_ecran, y_ecran, m_CRect);
                }
        }
    }
}
