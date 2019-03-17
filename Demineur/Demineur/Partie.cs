using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demineur
{
    class Partie
    {
        //Données membres...
        Plateau m_plateau;
        Joueur m_joueur;
        UI m_UI; //Interface utilisateur.
        bool m_alive;
        char m_difficulte;
        private int m_mineRestante;     //Le nombre de mines restantes dans le plateau.


        //Constructeurs...

        public Partie()
        {
            this.m_plateau = null;
            this.m_UI = new UI();
            this.m_joueur = new Joueur(); //TODO... FIND A USE FOR THE PLAYER CLASS.
            this.m_alive = true;
        }

        public char Difficulte
        {
            get { return m_difficulte; }
            set { m_difficulte = value; }
        }

        public int mineRestante
        {
            get { return m_mineRestante; }
            set { m_mineRestante = value; }
        }
        //Accesseurs...

        //Méthodes...
        public void start()
        {


            //Afficher le messge d'accueil.
            m_UI.showStartMessage();

            //Demander au joueur son nom.
            String nomJoueur = m_UI.askPlayerName();
            m_joueur.setName(nomJoueur);


            //Cacher le curseur par souci du détail.
            setCursorVisibility(false);

            //demander au joueur la difficulté du plateau
            m_difficulte = m_UI.askDifficulty();
            //Demander au joueur la grosseur du plateau.
            char grosseur = m_UI.askSize();
            m_plateau = new Plateau(grosseur);


            m_plateau.disperserMine(m_difficulte);

            m_mineRestante = m_plateau.mineRestante;
            //m_plateau.drawBoard();

            //TESTING
            Console.Clear(); //Netoyer la console avant de commencer la boucle d'affichage
            m_UI.test(m_plateau.toCharinfoArray(), (m_plateau.getLargeur() * 4 + 2), (m_plateau.getHauteur() * 2 + 1));
            m_UI.showPlayerName();
            m_UI.showDifficulty(m_difficulte);
            m_UI.showBoardSize(m_plateau.cases.Length);
            m_UI.showUnfoundMineCount(m_plateau.mineRestante);
            //ENDS OF TESTING



            while (m_alive && !checkForWin())
            {
                //Afficher le plateau et attendre un input.


                char input = m_UI.waitForMove();
                if (input != 'e' && input != 'm')
                {
                    m_plateau.moveCursor(input);
                }
                else
                {
                    if (input == 'm' && m_mineRestante - 1 >= 0)
                    {
                        if (!m_plateau.checkIfMarked())
                        {
                            m_mineRestante--;
                        }
                        else if (m_plateau.checkIfMarked() && m_mineRestante  <= m_plateau.mineRestante)
                        {
                            m_mineRestante++;
                        }
                    }
                    m_alive = m_plateau.move(input);
                }
                m_UI.test(m_plateau.toCharinfoArray(), (m_plateau.getLargeur() * 4 + 2), (m_plateau.getHauteur() * 2 + 1));
                m_UI.showPlayerName();
                m_UI.showDifficulty(m_difficulte);
                m_UI.showBoardSize(m_plateau.cases.Length);
                m_UI.showUnfoundMineCount(m_mineRestante);

            }

            if (checkForWin())
            {
                m_UI.showWinMessage();
            }
            else
            {
                m_UI.showDeathMessage();
            }

        }

        public void setCursorVisibility(bool visible)
        {
            Console.CursorVisible = visible;
        }

        public bool checkForWin()
        {
            if (m_plateau.mineRestante == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
