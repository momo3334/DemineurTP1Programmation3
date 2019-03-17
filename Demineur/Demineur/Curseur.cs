using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demineur
{
    class Curseur
    {
        //Données membres...
        private char m_couleur;     //La couleur du curseur.

        public enum m_couleursPossibles { White, Green, Red };           //Possible colors for the cursor

        //Constructeurs...

        //Crer un curseur de la couleur par défault (blanc).
        public Curseur()
        {
            this.m_couleur = 'G';
        }

        //Crer un curseur de la couleur spécifié.
        public Curseur(m_couleursPossibles couleur)
        {
            setCouleur(couleur);
        }
        //Accesseurs...

        public void setCouleur(m_couleursPossibles couleur)
        {
            switch (couleur)
            {
                case m_couleursPossibles.White:
                    this.m_couleur = 'W';
                    break;
                case m_couleursPossibles.Green:
                    this.m_couleur = 'G';
                    break;
                case m_couleursPossibles.Red:
                    this.m_couleur = 'R';
                    break;
                default:
                    this.m_couleur = 'W';
                    break;
            }
        }

        public char getCouleur()
        {
            return this.m_couleur;
        }

        //Méthodes...

        //Dessine le curseur pour mettre en évidence la valeur envoyer en argument. 
        public void draw(char content)
        {
            switch (m_couleur)
            {
                case 'W':
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(content);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 'G':
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(content);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 'R':
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(content);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
        }

        //Dessine le curseur pour mettre en évidence la valeur envoyer en argument. 
        public void draw(String content)
        {
            switch (m_couleur)
            {
                case 'W':
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(content);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 'G':
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(content);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 'R':
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(content);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
        }
    }
}
