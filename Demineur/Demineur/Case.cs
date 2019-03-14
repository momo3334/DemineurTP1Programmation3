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
        private bool m_isMine;
        private bool m_isOpen;
        private char m_couleur;         //La couleur du plateau.
        public enum m_couleursPossibles { Black, Gray, Red }; //Couleur possible de la case

        //Constructeurs...

        public Case()
        {
            this.m_curseur = null;
            this.m_contenu = "\0";
            this.m_isOpen = false;
            this.m_couleur = 'G';
            m_count++;
        }

        //Accesseurs...

        //Change la couleur de fond de la case pour un des valeur possible
        public void setCouleur(m_couleursPossibles couleur)
        {
            switch (couleur)
            {
                case m_couleursPossibles.Black:
                    this.m_couleur = 'B';
                    break;
                case m_couleursPossibles.Gray:
                    this.m_couleur = 'G';
                    break;
                case m_couleursPossibles.Red:
                    this.m_couleur = 'R';
                    break;
                default:
                    this.m_couleur = 'B';
                    break;
            }
        }

        //TODO.. Verif de la donnée envoyé
        public string contenu
        {
            get { return m_contenu; }
            set { m_contenu = value; }
        }

        public bool isOpen
        {
            get { return m_isOpen; }
            set { m_isOpen = value; }
        }

        public bool isMine
        {
            get { return m_isMine; }
            set { m_isMine = value; }
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
            this.m_curseur = new Curseur(Curseur.m_couleursPossibles.Green);
        }

        //Supprime le curseur de la case.
        public void removeCursor()
        {
            this.m_curseur = null;
        }

        //Méthodes...

        //Mets à jour l'affichage du curseur
        public void updateCursor()
        {
            m_curseur.draw(m_contenu);
        }

        //Draws the content this case.
        public void draw(bool endOfLine)
        {
            if (m_curseur == null)
            {
                if (endOfLine)
                {
                    Console.Write(" ");
                    writeContent();
                    Console.WriteLine(" |");
                }
                else
                {
                    Console.Write(" ");
                    writeContent();
                    Console.Write(" |");
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

        public void writeContent()
        {
            switch (m_couleur)
            {
                case 'B':
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(contenu);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 'G':
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(contenu);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 'R':
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(contenu);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
        }



    }
}
