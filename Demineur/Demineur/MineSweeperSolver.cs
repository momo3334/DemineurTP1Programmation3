using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demineur
{
    public class MineSweeperSolver
    {
        private static char[] tempBoard;
        private static int dimension;
        private static int[] play = new int[3];

        public int[] possiblePlay(string board)
        {

            tempBoard = board.ToCharArray();
            dimension = (int)Math.Sqrt(tempBoard.Length);
            play = new int[3];


            play[0] = 1;
            play[1] = 1;

            for (int i = 0; i < tempBoard.Length; i++)
            {
                char content = getCase(getLine(i), getCol(i));
                if ((int)content - 48 >= 0 && content != ' ' && content != '!')
                {
                    int openedCase = countOpenedCase(getLine(i), getCol(i));

                    if (8 - openedCase == ((int)content - 48) && content != '0')
                    {
                        flagClosedCase(i);
                    }
                }
            }

            for (int i = 0; i < tempBoard.Length; i++)
            {

                char content = getCase(getLine(i), getCol(i));
                if ((int)content - 48 >= 0 && content != ' ' && content != '!')
                {
                    int flaggedCase = countFlaggedCase(getLine(i), getCol(i));

                    if (flaggedCase == ((int)content - 48) && content != '0')
                    {
                        int[] coord = getFirstpossibleCase(getLine(i), getCol(i));
                        if (coord[0] != -1 && coord[1] != -1)
                        {
                            play[0] = coord[0];
                            play[1] = coord[1];
                            return play;
                        }
                    }
                }
            }

            double[] probabilite = new double[tempBoard.Length];
            for (int j = 0; j < tempBoard.Length; j++)
            {
                probabilite[j] = 100;
            }
            for (int i = 0; i < tempBoard.Length; i++)
            {


                char content = getCase(getLine(i), getCol(i));
                if ((int)content - 48 > 0 && content != ' ' && content != '!')
                {
                    double convContent = (double)content - 48;
                    double nbFlag = countFlaggedCase(getLine(i), getCol(i));
                    double nbOpen = countOpenedCase(getLine(i), getCol(i));
                    double nbClosed = countClosedCase(getLine(i), getCol(i));

                    double mineleft = nbClosed - nbFlag;

                    probabilite[i] = convContent / nbClosed; 
                }
            }

            int tempIndex; 
            //QuickSort(probabilite, 0, probabilite.Length - 1);
            for (int i = 0; i < probabilite.Length - 1; i++)
            {
                for (int j = i + 1; j < probabilite.Length; j++)
                {
                    if (probabilite[i] < probabilite[j])
                    {
                        tempIndex = i;
                    }
                }
            }

            play[2] = 50;

            return play;
        }

        public char getCase(int line, int col)
        {
            if (line < dimension && col < dimension && line >= 0 && col >= 0)
            {
                int indexInArray;

                indexInArray = (dimension * line) + col;

                return tempBoard[indexInArray];
            }
            else
            {
                return '\0';
            }
        }

        public void setCase(int line, int col, char valeur)
        {
            if (line < dimension && col < dimension && line >= 0 && col >= 0)
            {
                int indexInArray;

                indexInArray = (dimension * line) + col;

                tempBoard[indexInArray] = valeur;
            }
        }

        public int getLine(int index)
        {
            return index / dimension;
        }
        public int getCol(int index)
        {
            return index - (getLine(index) * dimension);
        }

        //Rule number1
        public int countOpenedCase(int line, int col)
        {
            int nbOpenedCase = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((!(i == 0 && j == 0)) && line + i >= 0 && col + i >= 0 && line + i <= dimension - 1 && col + i <= dimension - 1)
                    {
                        char content = getCase(line + i, col + j); //contenu de la case courante.

                        if (((int)content - 48) >= 0 && content != ' ' && content != '!')
                        {
                            nbOpenedCase++;
                        }
                    }
                }
            }


            return nbOpenedCase;
        }


        public int countClosedCase(int line, int col)
        {
            int nbClosedCase = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((!(i == 0 && j == 0)) && line + i >= 0 && col + i >= 0 && line + i <= dimension - 1 && col + i <= dimension - 1)
                    {
                        char content = getCase(line + i, col + j); //contenu de la case courante.

                        if (content == ' ')
                        {
                            nbClosedCase++;
                        }
                    }
                }
            }


            return nbClosedCase;
        }

        public int[] getFirstpossibleCase(int line, int col)
        {
            int[] coord = new int[2];
            coord[0] = -1;
            coord[1] = -1;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((!(i == 0 && j == 0)) && line + i >= 0 && col + i >= 0 && line + i <= dimension - 1 && col + i <= dimension - 1)
                    {
                        char content = getCase(line + i, col + j); //contenu de la case courante.

                        if (content != '!' && content == ' ')
                        {
                            coord[0] = line + i;
                            coord[1] = col + j;
                        }
                    }
                }
            }

            return coord;
        }

        public int countFlaggedCase(int line, int col)
        {
            int nbFlaggedCase = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((!(i == 0 && j == 0)) && line + i >= 0 && col + i >= 0 && line + i <= dimension - 1 && col + i <= dimension - 1)
                    {
                        char content = getCase(line + i, col + j); //contenu de la case courante.

                        if (content == '!')
                        {
                            nbFlaggedCase++;
                        }
                    }
                }
            }


            return nbFlaggedCase;
        }

        public void flagClosedCase(int index)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    char content = getCase(getLine(index) + i, getCol(index) + j); //contenu de la case courante.

                    if (content == ' ')
                    {
                        setCase(getLine(index) + i, getCol(index) + j, '!');
                        play[0] = getLine(index) + i;
                        play[1] = getCol(index) + j;
                    }

                }
            }
        }


        public void QuickSort(int[] a, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int num = a[start];

            int i = start, j = end;

            while (i < j)
            {
                while (i < j && a[j] > num)
                {
                    j--;
                }

                a[i] = a[j];

                while (i < j && a[i] < num)
                {
                    i++;
                }

                a[j] = a[i];
            }

            a[i] = num;
            QuickSort(a, start, i - 1);
            QuickSort(a, i + 1, end);
        }
    }
}
