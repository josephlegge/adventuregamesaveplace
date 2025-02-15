using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventureGame
{
    internal abstract class Weapon : Item
    {
        public int Damage { get; set; }
    }
}
