using Jeu_de_cartes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_cartes
{
    abstract class Card
    {
        public int Id
        {
            get;
            set;
        }
       public abstract CardType CardType
        {
            get;
            set;
        }

        public abstract CardAttribute CardAttribute
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String Description
        {
            get;
            set;
        }
    }
}
