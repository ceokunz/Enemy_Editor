using Enemy_Editor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemy_Editor.Armors
{
    class EnchantedSteelArmor : IArmor, IArmorEffect
    {
        public string Name { get; set; }
        public int Armor { get; set; }
        public bool IsEnchanted { get; set; }


        public List<IArmorEffect> Effects { get; set; }

        public EnchantedSteelArmor()
        {
            Name = "Броня будь здоров";
            Armor = 50;
        }

        
        
    }
}
