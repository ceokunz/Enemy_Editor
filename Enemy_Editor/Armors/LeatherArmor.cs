using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enemy_Editor.Interfaces;

namespace Enemy_Editor.Armors
{
    internal class LeatherArmor : IArmor
    {
        public string Name { get; set; }
        public int Armor { get; set; }

        public LeatherArmor()
        {
            Name = "Кожак";
            Armor = 15;
        }
    }
}
