using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_cartes.Classes
{
    internal class Trap : SpellTrap
    {
        public override CardType CardType
        {
            get
            {
                return CardType.Trap;
            }
            set
            {
                CardType = CardType.Trap;
            }
        }
        public override CardAttribute CardAttribute
        {
            get
            {
                return CardAttribute.Trap;
            }
            set
            {
                CardAttribute = CardAttribute.Trap;
            }
        }

        public override Icon Icon
        {
            get { return Icon; }
            set => Icon = value;
        }
    }
}
