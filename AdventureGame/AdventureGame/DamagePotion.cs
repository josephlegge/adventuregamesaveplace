using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class DamagePotion : Potion
    {

        public int PotionIntensity { get; set; }

        public override string AffectPlayer(Player player)
        {
            return $"You used a bad potion and took {PotionIntensity} damage! {player.TakeDamage(PotionIntensity)}";
        }
    }
}
