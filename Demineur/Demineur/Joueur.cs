using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demineur
{
    class Joueur
    {
        //Données membres...
        String m_nom;

        //Constructeurs...
        public Joueur()
        {
            this.m_nom = "Aucun nom!";
        }

        //Accesseurs...

       public void setName(String nom)
        {
            this.m_nom = nom;
        }

        public String getName()
        {
            return this.m_nom;
        }

        //Méthodes...
    }
}
