using Enemy_Editor.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Enemy_Editor.Classes
{
    public class EnemyTemplate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
            set => iconName = value ?? throw new ArgumentNullException(nameof(value));
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
    }
}
