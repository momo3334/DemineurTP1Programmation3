using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demineur
{
    class Case
    {
        //Données membres...
        private String m_contenu;
        private Curseur m_curseur;
        private static int m_count = 0;

        //Constructeurs...

        public Case()
        {
            this.m_curseur = null;
            this.m_contenu = "\0";
            m_count++;
        }

        //Accesseurs...

        //TODO.. Verif de la donnée envoyé
        public string contenu
        {
            get { return m_contenu; }
            set { m_contenu = value; }
        }

        public String getContenu()
        {
            return this.m_contenu;
        }

        public int getCount()
        {
            return m_count;
        }

        //Ajoute un curseur à la case.
        public void addCursor()
        {
            this.m_curseur = new Curseur(Curseur.m_couleursPossibles.White);
        }

        //Supprime le curseur de la case.
        public void removeCursor()
        {
            this.m_curseur = null;
        }

        //Méthodes...

        //Draws the content this case.
        public void draw(bool endOfLine)
        {
            if (m_curseur == null)
            {
                if (endOfLine)
                {
                    Console.WriteLine(" " + getContenu() + " |");
                }
                else
                {
                    Console.Write(" " + getContenu() + " |");
                }
            }
            else
            {
                if (endOfLine)
                {
                    Console.Write(" ");
                    m_curseur.draw(getContenu());
                    Console.WriteLine(" |");
                }
                else
                {
                    Console.Write(" ");
                    m_curseur.draw(getContenu());
                    Console.Write(" |");
                }
            }

        }

    }
}
