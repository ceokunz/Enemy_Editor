using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Enemy_Editor.Interfaces;
using Newtonsoft.Json;

namespace Enemy_Editor.Classes
{
    public class EnemyTemplate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IArmor _armor;

        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IArmor Armor
        {
            get
            {
                return _armor;
            }
            set
            {
                _armor = value;
            }
        }

        [JsonInclude]
        public string Name
        {
            get => name;
            set
            {
                name = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged("Name");
            }
        }

        [JsonInclude]
        public string IconName
        {
            get => iconName;
            set
            {
                iconName = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged("IconName");
            }
        }

        [JsonInclude]
        public int BaseLife
        {
            get => baseLife;
            set => baseLife = value;
        }

        [JsonInclude]
        public double LifeModifier
        {
            get => lifeModifier;
            set => lifeModifier = value;
        }

        [JsonInclude]
        public int BaseGold
        {
            get => baseGold;
            set => baseGold = value;
        }

        [JsonInclude]
        public double GoldModifier
        {
            get => goldModifier;
            set => goldModifier = value;
        }

        [JsonInclude]
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

        public EnemyTemplate()
        {
            name = "<Новый противник>";
            iconName = "";
        }

        public EnemyTemplate(
            string name,
            string iconName,
            int baseLife,
            double lifeMod,
            int baseGold,
            double goldMod,
            double spawnChance
            )
        {
            this.name = name;
            this.iconName = iconName;
            this.baseLife = baseLife;
            this.lifeModifier = lifeMod;
            this.baseGold = baseGold;
            this.goldModifier = goldMod;
            this.spawnChance = spawnChance;
        }
    }
}
