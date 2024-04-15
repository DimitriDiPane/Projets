using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_cartes.Classes
{
    abstract class SpellTrap : Card
    {
        public override CardType CardType
        {
            get
            {
                return CardType;
            }
            set
            {
                CardType = value;
            }
        }
        public override CardAttribute CardAttribute
        {
            get
            {
                return CardAttribute;
            }
            set
            {
                CardAttribute = value;
            }
        }

        public abstract Icon Icon
        {
            get;
            set;
        }
    }
}
