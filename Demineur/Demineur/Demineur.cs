using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demineur
{
    class Demineur
    {
        //Données membres...
        private Partie m_partie;

        static void Main(string[] args)
        {
            Demineur game = new Demineur();

            game.startNewGame();
        }

        //Constructeur...
        public Demineur()
        {

        }

        //Méthodes...

        public void startNewGame()
        {
            this.m_partie = new Partie();
            m_partie.start();
        }
    }
}
