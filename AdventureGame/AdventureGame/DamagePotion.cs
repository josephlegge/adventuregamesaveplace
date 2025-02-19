using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    /// <summary>
    /// Class for a damaging potion.
    /// </summary>
    internal class DamagePotion : Potion
    {
        /// <summary>
        /// Damage potion strength property
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// Override method that damages player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override string AffectPlayer(Player player)
        {
            return $"You used a bad potion.\n{player.TakeDamage(Strength, "poison")}";
        }
    }
}
