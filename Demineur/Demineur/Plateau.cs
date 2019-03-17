using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demineur
{
    class Plateau
    {
        //Données membres...
        private int m_largeur;                //Largeur(en nombre de cases) du plateau.
        private int m_hauteur;               //Hauteur(en nombre de cases) du plateau.
        private Case[] m_cases;             //Tableau contenant toute les cases du plateau.
        private int m_ligneCurseur;        //La ligne ou le curseur est présentement situé.
        private int m_colonneCurseur;     //La colonne ou le curseur est présentement situé.
        private int m_moveCount;         //Compteur du nombre de coups effectués.
        private int m_nbMine;     //Le nombre de mines restantes dans le plateau.


        //Constructeurs...

        //Constructeur par défault.
        public Plateau()
        {
            this.m_ligneCurseur = 0;
            this.m_colonneCurseur = 0;
            this.m_hauteur = 6;
            this.m_largeur = 6;
            this.m_cases = new Case[m_largeur * m_hauteur];

            for (int i = 0; i < m_cases.Length; i++)
            {
                m_cases[i] = new Case();
            }

            //Ajout du curseur à l'endroit spécifié.
            getCase(m_ligneCurseur, m_colonneCurseur).addCursor();
        }

        //Constructeur prenant comme argument la largeur et la hauteur du plateau.
        Plateau(int largeur, int hauteur)
        {
            if (largeur > 0 || hauteur > 0)
            {
                this.m_ligneCurseur = 0;
                this.m_colonneCurseur = 0;
                this.m_hauteur = hauteur;
                this.m_largeur = largeur;
                this.m_cases = new Case[m_largeur * m_hauteur];
            }
            else
            {
                Console.WriteLine("[Erreur]Le plateau n'à pas pu être initialisé avec les dimensions demandés. Veuillez utiliser des valeurs positives.");
            }

            //Ajout du curseur à l'endroit spécifié.
            getCase(m_ligneCurseur, m_colonneCurseur).addCursor();
        }

        //Constructeur prenant comme argument une des tailles prédéfinis.
        public Plateau(char taille)
        {
            switch (taille)
            {
                case 'A':
                    this.m_hauteur = 6;
                    this.m_largeur = 6;
                    break;
                case 'B':
                    this.m_hauteur = 8;
                    this.m_largeur = 8;
                    break;
                case 'C':
                    this.m_hauteur = 24;
                    this.m_largeur = 24;
                    break;
                default:
                    break;
            }

            this.m_ligneCurseur = 0;
            this.m_colonneCurseur = 0;
            this.m_cases = new Case[m_largeur * m_hauteur];

            for (int i = 0; i < m_cases.Length; i++)
            {
                m_cases[i] = new Case();
                m_cases[i].contenu = ' ';  //"·"
            }

            //Ajout du curseur à l'endroit spécifié.
            getCase(m_ligneCurseur, m_colonneCurseur).addCursor();
        }

        //Accesseurs...

        public int moveCount
        {
            set { m_moveCount = value; }
            get { return m_moveCount; }
        }

        public int mineRestante
        {
            set { m_nbMine = value; }
            get { return m_nbMine; }
        }

        public int getLargeur()
        {
            return this.m_largeur;
        }
        public void setLargeur(int largeur)
        {
            if (largeur > 0)
            {
                this.m_largeur = largeur;
            }
        }
        public int getHauteur()
        {
            return this.m_hauteur;
        }
        public void setHauteur(int hauteur)
        {
            if (hauteur > 0)
            {
                this.m_hauteur = hauteur;
            }
        }
        public int getLigneCurseur()
        {
            return this.m_ligneCurseur;
        }
        public void setLigneCurseur(int ligneCurseur)
        {
            if (ligneCurseur > 0)
            {
                this.m_ligneCurseur = ligneCurseur;
            }
        }
        public int getColonneCurseur()
        {
            return this.m_colonneCurseur;
        }

        public void setColonneCurseur(int colonneCurseur)
        {
            if (colonneCurseur > 0)
            {
                this.m_colonneCurseur = colonneCurseur;
            }
        }


        public Case[] cases
        {
            get { return m_cases; }
            set { m_cases = value; }
        }


        //Méthodes...

        public Case getCase(int line, int col)
        {
            if (line < m_hauteur && col < m_largeur && line >= 0 && col >= 0)
            {
                int indexInArray;

                indexInArray = (m_largeur * line) + col;

                return m_cases[indexInArray];
            }
            else
            {
                return null;
            }
        }

        //Moves the cursors in the specified direction.
        public void moveCursor(char direction)
        {
            if (validateDirection(direction))
            {
                getCase(m_ligneCurseur, m_colonneCurseur).removeCursor();
                switch (direction)
                {
                    case 'u':
                        m_ligneCurseur--;
                        break;
                    case 'd':
                        m_ligneCurseur++;
                        break;
                    case 'l':
                        m_colonneCurseur--;
                        break;
                    case 'r':
                        m_colonneCurseur++;
                        break;
                    default:
                        break;
                }
                getCase(m_ligneCurseur, m_colonneCurseur).addCursor();
            }
        }


        public bool validateDirection(char direction)
        {
            switch (direction)
            {
                case 'u':
                    if (m_ligneCurseur > 0)
                    {
                        return true;
                    }
                    break;
                case 'd':
                    if (m_ligneCurseur + 1 < m_hauteur)
                    {
                        return true;
                    }
                    break;
                case 'l':
                    if (m_colonneCurseur > 0)
                    {
                        return true;
                    }
                    break;
                case 'r':
                    if (m_colonneCurseur + 1 < m_largeur)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
                    break;
            }
            return false;
        }

        public void drawBoard()
        {
            Console.Clear();
            showBoardInformation();
            showBoard();
        }

        public void showBoardInformation()
        {

        }

        /**Draws the board with the cursor at the specified location.(Row Number, Column Number)
 * Note:Rows and Columns start at zero!
**/
        public void showBoard()
        {
            bool pairRowNumber;

            for (int i = 1; i <= (m_hauteur * 2) + 1; i++)
            {
                pairRowNumber = i % 2 == 0;

                //Only on odd line numbers
                if (!pairRowNumber)
                {
                    Console.Write("+");
                }
                //Only on pair line numbers
                else
                {
                    Console.Write("|");
                }

                for (int j = 0; j < m_largeur; j++)
                {
                    //Only on odd line numbers
                    if (!pairRowNumber)
                    {
                        if (j == m_largeur - 1)
                        {
                            Console.WriteLine("---+");
                        }
                        else
                        {
                            Console.Write("---+");
                        }
                    }

                    //Only on pair line numbers
                    else
                    {
                        if (j == m_largeur - 1)
                        {
                            getCase((i / 2 - 1), j).draw(true);

                        }
                        else
                        {
                            getCase((i / 2 - 1), j).draw(false);
                        }
                    }

                }

            }

        }

        public CharInfo[] toCharinfoArray()
        {
            bool pairRowNumber;
            int line = (m_largeur * 4) + 2;
            int col = (m_hauteur * 2) + 1;

            CharInfo[] convertedBoard = new CharInfo[line * col]; //Creating a CharInfo array that matches the size of the board

            for (int i = 0; i < (m_hauteur * 2) + 1; i++)
            {
                pairRowNumber = i % 2 == 0;


                //Only on odd line numbers
                if (!pairRowNumber)
                {
                    CharInfo c = new CharInfo();
                    c.Attributes = (short)(15 | 0 << 4);
                    c.Char.AsciiChar = (byte)'|';
                    convertedBoard[i * line] = c;
                }
                //Only on pair line numbers
                else
                {
                    //Console.Write("|");
                    CharInfo c = new CharInfo();
                    c.Attributes = (short)(15 | 0 << 4);
                    c.Char.AsciiChar = (byte)'+';
                    convertedBoard[(i * line)] = c;
                }

                for (int j = 0; j < m_largeur; j++)
                {
                    //Only on odd line numbers
                    if (!pairRowNumber)
                    {
                        if (j + 1 == m_largeur)
                        {
                            char[] convString;

                            if ((i / 2) == m_ligneCurseur && j == m_colonneCurseur)
                            {
                                convString = new char[] { '[', '0', ']', '|', '\n' };
                            }
                            else
                            {
                                convString = new char[] { ' ', '0', ' ', '|', '\n' };
                            }

                            convString[1] = getCase((i / 2), j).contenu;
                            short couleurFond = getCase((i / 2), j).getCouleurFond();
                            short couleur = getCase((i / 2), j).getCouleur();


                            for (int k = 0; k < convString.Length - 1; k++)
                            {
                                CharInfo c = new CharInfo();

                                //for a different type of cursor...
                                //if ((i / 2) == m_ligneCurseur && j == m_colonneCurseur && k == 1)
                                //{
                                //    c.Attributes = (short)(15 | (10 << 4));
                                //}
                                //else if (k == 1)
                                //{
                                //    c.Attributes = (short)(couleur | (couleurFond << 4));
                                //}
                                //else
                                //{
                                //    c.Attributes = (short)(15 | (0 << 4));
                                //}

                                if (k == 1)
                                {
                                    c.Attributes = (short)(couleur | (couleurFond << 4));
                                }
                                else if (k == 0 || k == 2)
                                {
                                    c.Attributes = (short)(10 | (0 << 4));
                                }
                                else
                                {
                                    c.Attributes = (short)(15 | (0 << 4));
                                }
                                c.Char.AsciiChar = (byte)convString[k];
                                convertedBoard[(i * line) + (j * (convString.Length - 1)) + (k + 1)] = c;
                            }


                            CharInfo lineBreak = new CharInfo();
                            lineBreak.Attributes = 0;
                            lineBreak.Char.AsciiChar = (byte)'\n';
                            convertedBoard[(i * line) + (j * (convString.Length)) + 1] = lineBreak;
                        }

                        else
                        {
                            char[] convString;

                            if ((i / 2) == m_ligneCurseur && j == m_colonneCurseur)
                            {
                                convString = new char[] { '[', '0', ']', '|' };
                            }
                            else
                            {
                                convString = new char[] { ' ', '0', ' ', '|' };
                            }

                            convString[1] = getCase((i / 2), j).contenu;
                            short couleurFond = getCase((i / 2), j).getCouleurFond();
                            short couleur = getCase((i / 2), j).getCouleur();

                            for (int k = 0; k < convString.Length; k++)
                            {
                                CharInfo c = new CharInfo();

                                //for a different type of cursor...
                                //if ((i / 2) == m_ligneCurseur && j == m_colonneCurseur && k == 1) 
                                //{
                                //    c.Attributes = (short)(15 | (10 << 4));
                                //}
                                //else if (k == 1)
                                //{
                                //    c.Attributes = (short)(couleur | (couleurFond << 4));
                                //}
                                //else
                                //{
                                //    c.Attributes = (short)(15 | (0 << 4));
                                //}


                                if (k == 1)
                                {
                                    c.Attributes = (short)(couleur | (couleurFond << 4));
                                }
                                else if (k == 0 || k == 2)
                                {
                                    c.Attributes = (short)(10 | (0 << 4));
                                }
                                else
                                {
                                    c.Attributes = (short)(15 | (0 << 4));
                                }
                                c.Char.AsciiChar = (byte)convString[k];
                                convertedBoard[(i * line) + (j * convString.Length) + (k + 1)] = c;
                            }
                        }
                    }

                    //Only on pair line numbers
                    else
                    {
                        if (j + 1 == m_largeur)
                        {
                            String toWrite = "---+";
                            char[] convString = toWrite.ToCharArray();
                            for (int k = 0; k < toWrite.Length; k++)
                            {
                                CharInfo c = new CharInfo();
                                c.Attributes = (short)(15 | 0 << 4);
                                c.Char.AsciiChar = (byte)convString[k];
                                convertedBoard[(i * line) + (j * toWrite.Length) + (k + 1)] = c;
                            }

                            CharInfo lineBreak = new CharInfo();
                            lineBreak.Attributes = 0;
                            lineBreak.Char.AsciiChar = (byte)'\n';
                            convertedBoard[(i * line) + ((j + 1) * toWrite.Length) + 1] = lineBreak;
                            //convertedBoard[()];
                            //Console.WriteLine("---+");
                        }
                        else
                        {
                            String toWrite = "---+";
                            char[] convString = toWrite.ToCharArray();
                            for (int k = 0; k < toWrite.Length; k++)
                            {
                                CharInfo c = new CharInfo();
                                c.Attributes = (short)(15 | 0 << 4);
                                c.Char.AsciiChar = (byte)convString[k];
                                convertedBoard[(i * line) + (j * toWrite.Length) + (k + 1)] = c;
                            }
                            //Console.Write("---+");
                        }
                    }

                }

            }

            return convertedBoard;
        }


        public void disperserMine(char difficulte)
        {
            switch (difficulte)
            {
                case 'A':
                    disperserMine(Convert.ToInt32(Math.Ceiling((m_hauteur * m_largeur) * 0.12)));
                    break;
                case 'B':
                    disperserMine(Convert.ToInt32(Math.Ceiling((m_hauteur * m_largeur) * 0.15)));
                    break;
                case 'C':
                    disperserMine(Convert.ToInt32(Math.Ceiling((m_hauteur * m_largeur) * 0.18)));
                    break;
                case 'D':
                    disperserMine(Convert.ToInt32(Math.Ceiling((m_hauteur * m_largeur) * 0.25)));
                    break;

                default:
                    break;
            }


        }

        //Divise le plateau en quatre section et met 1/4 des mines disponible dans chacune des quatres parties.
        public void disperserMine(int nbMine)
        {
            m_nbMine = nbMine;
            Random rdn = new Random(DateTime.Now.Millisecond);
            while (nbMine > 0)
            {
                int rdn1 = rdn.Next(0, m_cases.Length);
                if (m_cases[rdn1].contenu != 'B')
                {
                    //m_cases[rdn1].contenu = 'B';
                    m_cases[rdn1].setCouleur(Case.m_couleursPossibles.Red);
                    m_cases[rdn1].isMine = true;
                    nbMine--;
                }

            }







            //TEST CODE...
            //int quartNbMine = nbMine / 4;
            //int nbMineInit = nbMine;
            //int nbSection = 4;

            //for (int i = 0; i < nbSection; i++)
            //{
            //    if (i == 0)
            //    {
            //        while (nbMine > 3 * quartNbMine)
            //        {
            //            Random rndm = new Random(DateTime.Now.Millisecond);
            //            int ligne = rndm.Next(0, m_hauteur / 2 + 1);
            //            int colonne = rndm.Next(0, m_largeur / 2 + 1);

            //            if (getCase(ligne, colonne).getContenu() != "B")
            //            {
            //                getCase(ligne, colonne).setContenu("B");
            //                nbMine--;
            //            }

            //        }
            //    }
            //    if (i == 1)
            //    {
            //        while (nbMine > 2 * quartNbMine)
            //        {
            //            Random rndm = new Random(DateTime.Now.Millisecond);
            //            int ligne = rndm.Next(0 , m_hauteur / 2 + 1);
            //            int colonne = rndm.Next(m_largeur / 2 + 1, m_largeur);

            //            if (getCase(ligne, colonne).getContenu() != "B")
            //            {
            //                getCase(ligne, colonne).setContenu("B");
            //                nbMine--;
            //            }
            //        }
            //    }
            //    if (i == 2)
            //    {
            //        while (nbMine > quartNbMine)
            //        {
            //            Random rndm = new Random(DateTime.Now.Millisecond);
            //            int ligne = rndm.Next(m_hauteur / 2 + 1, m_hauteur);
            //            int colonne = rndm.Next(0, m_largeur / 2 + 1);

            //            if (getCase(ligne, colonne).getContenu() != "B")
            //            {
            //                getCase(ligne, colonne).setContenu("B");
            //                nbMine--;
            //            }
            //        }
            //    }
            //    if (i == 3)
            //    {
            //        while (nbMine > 0)
            //        {
            //            Random rndm = new Random(DateTime.Now.Millisecond);
            //            int ligne = rndm.Next(m_hauteur / 2 + 1, m_hauteur);
            //            int colonne = rndm.Next(m_largeur / 2 + 1, m_largeur);

            //            if (getCase(ligne, colonne).getContenu() != "B")
            //            {
            //                getCase(ligne, colonne).setContenu("B");
            //                nbMine--;
            //            }
            //        }
            //    }
            //}
        }

        public int countSurrondingMines(int ligne, int colonne)
        {
            int mineTouchingCount = 0;

            for (int currentLine = ligne - 1; currentLine <= ligne + 1; currentLine++)
            {
                for (int currentCol = colonne - 1; currentCol <= colonne + 1; currentCol++)
                {
                    if (getCase(currentLine, currentCol) != null)
                    {
                        if (getCase(currentLine, currentCol).isMine)
                        {
                            mineTouchingCount++;
                        }
                    }
                }
            }

            return mineTouchingCount;
        }

        public void ouvrirCase(int ligne, int colonne)
        {
            if (!(getCase(ligne, colonne).isOpen))
            {


                while (getCase(ligne, colonne) != null && getCase(ligne, colonne).isMine && m_moveCount == 1)
                {
                    getCase(ligne, colonne).isMine = false;
                    disperserMine(1);
                }


                if (getCase(ligne, colonne).isMine == false)
                {

                    int mineTouchingCount = countSurrondingMines(ligne, colonne);

                    if (mineTouchingCount != 0)
                    {
                        getCase(ligne, colonne).setContenu(mineTouchingCount);
                        getCase(ligne, colonne).setCouleurFond(Case.m_couleursPossibles.Black);
                    }
                    else
                    {
                        getCase(ligne, colonne).contenu = ' ';
                        getCase(ligne, colonne).setCouleurFond(Case.m_couleursPossibles.Black);
                    }
                    getCase(ligne, colonne).isOpen = true;

                    if (mineTouchingCount == 0)
                    {
                        for (int currentLine = ligne - 1; currentLine <= ligne + 1; currentLine++)
                        {
                            for (int currentCol = colonne - 1; currentCol <= colonne + 1; currentCol++)
                            {
                                //if (currentLine == ligne || currentCol == colonne)
                                //{
                                if (getCase(currentLine, currentCol) != null)
                                {
                                    if (!(currentLine == ligne && currentCol == colonne))
                                    {
                                        if (getCase(currentLine, currentCol).isOpen == false)
                                        {
                                            ouvrirCase(currentLine, currentCol);
                                        }

                                    }
                                }
                                //}
                            }
                        }
                    }
                }
            }
        }

        public bool checkIfMarked()
        {
            Case movedCase = getCase(m_ligneCurseur, m_colonneCurseur);
            if (movedCase.contenu == '!')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool move(char input)
        {
            Case movedCase = getCase(m_ligneCurseur, m_colonneCurseur);
            switch (input)
            {
                case 'e':
                    m_moveCount++;
                    if (!(movedCase.isMine))
                    {
                        ouvrirCase(m_ligneCurseur, m_colonneCurseur);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 'm':

                    m_moveCount++;

                    if (checkIfMarked() && movedCase.isMine)
                    {
                        movedCase.contenu = ' ';
                        m_nbMine++;
                    }
                    else if (!checkIfMarked() && movedCase.isMine)
                    {
                        movedCase.contenu = '!';
                        m_nbMine--;
                    }
                    else if (checkIfMarked())
                    {
                        movedCase.contenu = ' ';
                    }
                    else
                    {
                        movedCase.contenu = '!';
                    }


                    movedCase.setCouleur(Case.m_couleursPossibles.Red);
                    return true;
                    break;
                default:
                    return false;
                    break;
            }

        }
    }
}
