using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    /// <summary>
    /// Class for a healing potion.
    /// </summary>
    internal class HealthPotion : Potion
    {
        /// <summary>
        /// Health potion strength property
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// Override method that heals the player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override string AffectPlayer(Player player)
        {
            return $"You used a health potion.{player.Heal(Strength)}";
        }
    }
}
