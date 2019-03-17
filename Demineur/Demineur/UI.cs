using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace Demineur
{
    class UI
    {
        //THIS IS WORK IN PROGRESS...
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        //[StructLayout(LayoutKind.Explicit)]
        //public struct CharUnion
        //{
        //    [FieldOffset(0)] public char UnicodeChar;
        //    [FieldOffset(0)] public byte AsciiChar;
        //}

        //[StructLayout(LayoutKind.Explicit)]
        //public struct CharInfo
        //{
        //    [FieldOffset(0)] public CharUnion Char;
        //    [FieldOffset(2)] public short Attributes;
        //}

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }



        //ENDS OF WORK IN PROGRESS...


        //Données membres...
        public KeyListener m_keyListener;
        private String m_nomJoueur;

        //Constructeurs...

        public UI()
        {
            this.m_keyListener = new KeyListener();
            this.m_nomJoueur = "Aucun nom!";
        }

        //Méthodes...

        public void showStartMessage()
        {
            Console.WriteLine("+--------------------------+");
            Console.WriteLine("|                          |");
            Console.WriteLine("|  Bienvenue au Démineur!  |");
            Console.WriteLine("|                          |");
            Console.WriteLine("+--------------------------+");
        }

        public String askPlayerName()
        {
            String playerName = "\0";

            while (playerName == "\0" || playerName == "")
            {
                Console.Write("Veuillez entrer votre nom de joueur:");
                playerName = Console.ReadLine();
            }
            this.m_nomJoueur = playerName;
            return playerName;
        }

        public void showPlayerName()
        {
            Console.WriteLine("+-------------------------------------------------+");
            Console.Write("| Nom du joueur:" + m_nomJoueur );
            for (int i = 0; i < 34 - m_nomJoueur.Length; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("|");
            Console.WriteLine("+-------------------------------------------------+");
        }

        public void showDifficulty(char difficulty)
        {
            Console.WriteLine("+-------------------------------------------------+");
            Console.Write("| Niveau de difficulté du tableau: ");
            switch (difficulty)
            {
                case 'A':
                    Console.WriteLine(" Facile        |");
                    break;
                case 'B':
                    Console.WriteLine(" Intermédiaire |");
                    break;
                case 'C':
                    Console.WriteLine(" Difficile     |");
                    break;
                case 'D':
                    Console.WriteLine(" Extrême       |");
                    break;

                default:
                    break;
            }
            Console.WriteLine("+-------------------------------------------------+");
        }

        public void showBoardSize(int size)
        {
            Console.WriteLine("+-------------------------------------------------+");
            Console.Write("| Grosseur du plateau : " + size + " cases.");
            for (int i = 0; i < 19 - size.ToString().Length; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("|");
            Console.WriteLine("+-------------------------------------------------+");
        }

        public char askSize()
        {
            Curseur curseur = new Curseur();
            int curseurPos = 0;
            char size = '\0';

            String[] grosseurPossibles = { "A) Petit", "B) Moyen", "C) Grand" };



            while (size != 'e')
            {
                //Réaffichage du menu pour permettre au curseur de bouger sans influencer le reste de l'affichage.
                Console.Clear();
                showStartMessage();
                showPlayerName();


                if (curseurPos == 0)
                {
                    Console.WriteLine("Veuillez choisir la grosseur du plateau : \n");
                    Console.Write(" >"); curseur.draw(grosseurPossibles[0]); Console.Write("\n");
                    Console.WriteLine("  " + grosseurPossibles[1]);
                    Console.WriteLine("  " + grosseurPossibles[2]);
                    Console.WriteLine("");
                }
                if (curseurPos == 1)
                {
                    Console.WriteLine("Veuillez choisir la grosseur du plateau : \n");
                    Console.WriteLine("  " + grosseurPossibles[0]);
                    Console.Write(" >"); curseur.draw(grosseurPossibles[1]); Console.Write("\n");
                    Console.WriteLine("  " + grosseurPossibles[2]);
                    Console.WriteLine("");
                }
                if (curseurPos == 2)
                {
                    Console.WriteLine("Veuillez choisir la grosseur du plateau : \n");
                    Console.WriteLine("  " + grosseurPossibles[0]);
                    Console.WriteLine("  " + grosseurPossibles[1]);
                    Console.Write(" >"); curseur.draw(grosseurPossibles[2]); Console.Write("\n");
                    Console.WriteLine("");
                }

                size = m_keyListener.listen();

                switch (size)
                {
                    case 'u':
                        if (curseurPos - 1 >= 0)
                        {
                            curseurPos--;
                        }
                        break;
                    case 'd':
                        if (curseurPos + 1 < grosseurPossibles.Length)
                        {
                            curseurPos++;
                        }
                        break;
                    default:
                        break;
                }
            }


            Console.WriteLine("");
            switch (curseurPos)
            {
                case 0:
                    Console.Write("Choix > A");
                    Console.WriteLine("Vous avez choisi un plateau de petite taille.");
                    return 'A';
                    break;
                case 1:
                    Console.Write("Choix > B");
                    Console.WriteLine("Vous avez choisi un plateau de taille moyenne.");
                    return 'B';
                    break;
                case 2:
                    Console.Write("Choix > C");
                    Console.WriteLine("Vous avez choisi un plateau de grande taille.");
                    return 'C';
                    break;
            }

            return '\0';
        }

        public char askDifficulty()
        {
            Curseur curseur = new Curseur();
            int curseurPos = 0;
            char difficulty = '\0';
            String[] difficultéPossibles = { "  A) Facile", "  B) Intermédiaire", "  C) Difficile", "  D) Extrême" };

            while (difficulty != 'e')
            {
                //Réaffichage du menu pour permettre au curseur de bouger sans influencer le reste de l'affichage.
                Console.Clear();
                showStartMessage();
                showPlayerName();


                if (curseurPos == 0)
                {
                    Console.WriteLine("Veuillez choisir le niveau de difficulté : \n");
                    Console.Write(" >"); curseur.draw(difficultéPossibles[0]); Console.Write("\n");
                    Console.WriteLine("  " + difficultéPossibles[1]);
                    Console.WriteLine("  " + difficultéPossibles[2]);
                    Console.WriteLine("  " + difficultéPossibles[3]);
                    Console.WriteLine("");
                }
                if (curseurPos == 1)
                {
                    Console.WriteLine("Veuillez choisir le niveau de difficulté : \n");
                    Console.WriteLine("  " + difficultéPossibles[0]);
                    Console.Write(" >"); curseur.draw(difficultéPossibles[1]); Console.Write("\n");
                    Console.WriteLine("  " + difficultéPossibles[2]);
                    Console.WriteLine("  " + difficultéPossibles[3]);
                    Console.WriteLine("");
                }
                if (curseurPos == 2)
                {
                    Console.WriteLine("Veuillez choisir le niveau de difficulté : \n");
                    Console.WriteLine("  " + difficultéPossibles[0]);
                    Console.WriteLine("  " + difficultéPossibles[1]);
                    Console.Write(" >"); curseur.draw(difficultéPossibles[2]); Console.Write("\n");
                    Console.WriteLine("  " + difficultéPossibles[3]);
                    Console.WriteLine("");
                }
                if (curseurPos == 3)
                {
                    Console.WriteLine("Veuillez choisir le niveau de difficulté : \n");
                    Console.WriteLine("  " + difficultéPossibles[0]);
                    Console.WriteLine("  " + difficultéPossibles[1]);
                    Console.WriteLine("  " + difficultéPossibles[2]);
                    Console.Write(" >"); curseur.draw(difficultéPossibles[3]); Console.Write("\n");
                    Console.WriteLine("");
                }

                difficulty = m_keyListener.listen();

                switch (difficulty)
                {
                    case 'u':
                        if (curseurPos - 1 >= 0)
                        {
                            curseurPos--;
                        }
                        break;
                    case 'd':
                        if (curseurPos + 1 < difficultéPossibles.Length)
                        {
                            curseurPos++;
                        }
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("");
            switch (curseurPos)
            {
                case 0:
                    Console.Write("Choix > A");
                    Console.WriteLine("Vous avez choisi facile.");
                    return 'A';
                    break;
                case 1:
                    Console.Write("Choix > B");
                    Console.WriteLine("Vous avez choisi intermédiaire.");
                    return 'B';
                    break;
                case 2:
                    Console.Write("Choix > C");
                    Console.WriteLine("Vous avez choisi difficile.");
                    return 'C';
                    break;
                case 3:
                    Console.Write("Choix > D");
                    Console.WriteLine("Vous avez choisi extrême!");
                    return 'C';
                    break;
            }

            Console.WriteLine("");
            return '\0';
        }

        public char waitForMove()
        {
            char move = this.m_keyListener.listen();
            return move;
        }

        public void showUnfoundMineCount(int mineLeft)
        {
            Console.WriteLine("+-------------------------------------------------+");
            Console.Write("| Mines restantes :" + mineLeft + " mines.");
            for (int i = 0; i < 24 - mineLeft.ToString().Length; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("|");
            Console.WriteLine("+-------------------------------------------------+");
        }


        public void showDeathMessage()
        {
            Console.WriteLine("\nVous avez explosé!! Meilleure chance la prochaine fois...");
            Console.ReadKey();
        }

        public void showWinMessage()
        {
            Console.WriteLine("\nFélicitation vous avez trouvé toute les bombes! Bien joué!");
            Console.ReadKey();
        }

        //TESTING
        //Les dimensions du tableau sont interprété en nombre de caractères.
        public void test(CharInfo[] boardToDraw, int largeur, int hauteur)
        {
            SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);



            if (!h.IsInvalid)
            {
                SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = (short)largeur, Bottom = (short)hauteur };



                for (int i = 0; i < boardToDraw.Length; ++i)
                {

                   // boardToDraw[i].Char.AsciiChar = (byte)testC[i];
                }

                bool b = WriteConsoleOutput(h, boardToDraw,
                  new Coord() { X = (short) largeur, Y = (short) hauteur },
                  new Coord() { X = 0, Y = 0 },
                ref rect);

            }
            Console.SetCursorPosition(0, hauteur + 1);
        }
        //ENDS OF TESTING..
    }
}
