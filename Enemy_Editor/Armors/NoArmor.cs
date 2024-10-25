using Enemy_Editor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemy_Editor.Armors
{
    class NoArmor : IArmor
    {
        public string Name { get; set; }
        public int Armor { get; set; }

        public NoArmor()
        {
            Name = "Кожа";
            Armor = 0;
        }
    }
}
