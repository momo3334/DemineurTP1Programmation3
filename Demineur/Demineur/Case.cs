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
        private char m_contenu;
        private Curseur m_curseur;
        private static int m_count = 0;
        private bool m_isMine;
        private bool m_isOpen;
        private short m_couleur;          //La couleur du plateau.
        private short m_couleurFond;     //La couleur de fond de la case.
        public  enum  m_couleursPossibles { Black = 0, Gray = 7, Red = 12, Blue = 9, Green = 10, Magenta = 13, Yellow = 14, Cyan = 11, White = 15, DarkYellow = 6}; //Couleur possible de la case

        //Constructeurs...

        public Case()
        {
            this.m_curseur = null;
            this.m_contenu = '\0';
            this.m_isOpen = false;
            this.m_couleur = (short)m_couleursPossibles.White;
            this.m_couleurFond = 7;
            m_count++;
        }

        //Accesseurs...


            //JAI PAS FINI LE SWITCH DES COULEURS!!!!!!!!


        //Change la couleur de fond de la case pour un des valeur possible
        public void setCouleurFond(m_couleursPossibles couleur)
        {
            m_couleurFond = (short)couleur;
            //switch (couleur)
            //{
            //    case m_couleursPossibles.Black:
            //        this.m_couleurFond = 0;
            //        break;
            //    case m_couleursPossibles.Gray:
            //        this.m_couleurFond = 7;
            //        break;
            //    case m_couleursPossibles.Green:
            //        this.m_couleurFond = 10;
            //        break;
            //    case m_couleursPossibles.Blue:
            //        this.m_couleurFond = 9;
            //        break;
            //    case m_couleursPossibles.Cyan:
            //        this.m_couleurFond = 11;
            //        break;
            //    case m_couleursPossibles.Red:
            //        this.m_couleurFond = 12;
            //        break;
            //    default:
            //        this.m_couleurFond = 9;
            //        break;
            //}
        }

        //Retourne la couleur de fond de la case pour un des valeur possible
        public short getCouleurFond()
        {
            return m_couleurFond;
        }

        public void setCouleur(m_couleursPossibles couleur)
        {
            switch (couleur)
            {
                case m_couleursPossibles.Black:
                    this.m_couleur = 9;
                    break;
                case m_couleursPossibles.Gray:
                    this.m_couleur = 7;
                    break;
                case m_couleursPossibles.Green:
                    this.m_couleur = 10;
                    break;
                case m_couleursPossibles.Blue:
                    this.m_couleur = 9;
                    break;
                case m_couleursPossibles.Cyan:
                    this.m_couleur = 11;
                    break;
                case m_couleursPossibles.Red:
                    this.m_couleur = 12;
                    break;
                default:
                    this.m_couleur = 9;
                    break;
            }
        }


        //Retourne la couleur du contenu de la case pour un des valeur possible
        public short getCouleur()
        {
            return m_couleur;
        }

        //TODO.. Verif de la donnée envoyé
        public char contenu
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

        public char getContenu()
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

        public void setContenu(int mine)
        {
            m_contenu = (char)(48 + mine); //Converting the returned int to a char representation(0 = 48 in ASCII table. So 48(0) + the number of mine touching this case.)

            switch (mine)
            {
                case 1:
                    setCouleur(m_couleursPossibles.Blue);
                    break;
                case 2:
                    setCouleur(m_couleursPossibles.Green);
                    break;
                case 3:
                    setCouleur(m_couleursPossibles.Red);
                    break;
                case 4:
                    setCouleur(m_couleursPossibles.Magenta);
                    break;
                case 5:
                    setCouleur(m_couleursPossibles.Yellow);
                    break;
                case 6:
                    setCouleur(m_couleursPossibles.Cyan);
                    break;
                case 7:
                    setCouleur(m_couleursPossibles.DarkYellow);
                    break;
                case 8:
                    setCouleur(m_couleursPossibles.Gray);
                    break;
                default:
                    break;
            }
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
            switch (m_couleurFond)
            {
                case (short)m_couleursPossibles.Black:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(contenu);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case (short)m_couleursPossibles.Gray:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(contenu);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case (short)m_couleursPossibles.Red:
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
