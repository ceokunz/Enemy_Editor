using Enemy_Editor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemy_Editor.Armors
{
    public class SteelArmor : IArmor
    {
        public string Name { get; set; }
        public int Armor { get; set; }
        public bool IsEnchanted { get; set; }

        public SteelArmor()
        {
            Name = "Стальная жопа";
            Armor = 50;
        }
    }
}
