using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_cartes.Classes
{
    internal class Spell : SpellTrap
    {
        public override CardType CardType
        {
            get
            {
                return CardType.Spell;
            }
            set
            {
                CardType = CardType.Spell;
            }
        }
        public override CardAttribute CardAttribute
        {
            get
            {
                return CardAttribute.Spell;
            }
            set
            {
                CardAttribute = CardAttribute.Spell;
            }
        }

        public override Icon Icon
        {
            get { return Icon; }
            set => Icon = value;
        }
    }
}
