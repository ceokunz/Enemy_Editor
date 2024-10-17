using Enemy_Editor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemy_Editor.Classes
{
    public class EnemyTemplate : IEnemyTemplateList
    {
        public string Name
        {
            get => name;
            set => name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string IconName
        {
            get => iconName;
            set => iconName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int BaseLife
        {
            get => baseLife;
            set => baseLife = value;
        }

        public double LifeModifier
        {
            get => lifeModifier;
            set => lifeModifier = value;
        }

        public int BaseGold
        {
            get => baseGold;
            set => baseGold = value;
        }

        public double GoldModifier
        {
            get => goldModifier;
            set => goldModifier = value;
        }

        public double SpawnChance
        {
            get => spawnChance;
            set => spawnChance = value;
        }

        //Название противника
        string name;
        //Название иконки
        string iconName;
        //Атрибуты здоровья
        int baseLife;
        double lifeModifier;

        //Атрибуты золота за победу над противником
        int baseGold;
        double goldModifier;
        //Шанс на появление
        double spawnChance;

        public void SaveToJson()
        {
            throw new NotImplementedException();
        }
    }
}
