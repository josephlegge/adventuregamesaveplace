using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class DamagePotion : Potion
    {

        public int Strength { get; set; }

        public override string AffectPlayer(Player player)
        {
            return $"You used a bad potion.\n{player.TakeDamage(Strength, "poison")}";
        }
    }
}
