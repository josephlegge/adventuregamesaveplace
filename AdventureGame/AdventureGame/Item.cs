using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal abstract class Item
    {
        public int Uses { get; set; }
        public string ItemName { get; set; }
    }
}
