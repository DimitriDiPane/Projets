using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_cartes.Classes
{
    internal class Monster : Card
    {
        public override CardType CardType
        {
            get
            {
                return CardType.Monster;
            }
            set
            {
                CardType = CardType.Monster;
            }
        }

        public override CardAttribute CardAttribute
        {
            get;
            set;
        }

        public int Stars
        {
            get;
            set;
        }

        public SubType SubType
        {
            get;
            set;
        }

        public int ATK
        {
            get;
            set;
        }
        public int DEF
        {
            get;
            set;
        }

    }
}
