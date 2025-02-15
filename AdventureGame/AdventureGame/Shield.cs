using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventureGame
{
    internal class Shield : Item
    {
        public Shield(int uses)
        {
            Uses = uses;
            ItemName = "Shield";
        }

        public string Damage()
        {
            Uses--;
            if (Uses > 0)
            {
                return "and your shield was damaged.";
            }
            else
            {
                return "and your shield broke!";
            }

        }
    }
}
