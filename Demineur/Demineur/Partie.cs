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
        bool m_running;
        char m_difficulte;


        //Constructeurs...

        public Partie()
        {
            this.m_plateau = null;
            this.m_UI = new UI();
            this.m_joueur = new Joueur(); //TODO... FIND A USE FOR THE PLAYER CLASS.
            this.m_running = true;
        }

        public char Difficulte {
            get { return m_difficulte; }
            set { m_difficulte = value; }  
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

            m_plateau.drawBoard();

            while (m_running)
            {
                //Afficher le plateau et attendre un input.
                m_plateau.moveCursor(m_UI.waitForMove());
            }
        }

        public void setCursorVisibility(bool visible)
        {
            Console.CursorVisible = visible;
        }
    }
}
