using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventureGame
{
    /// <summary>
    /// Abstract weapon class
    /// </summary>
    internal abstract class Weapon : Item
    {
        /// <summary>
        /// Weapon's damage property
        /// </summary>
        public int Damage { get; set; }
    }
}
