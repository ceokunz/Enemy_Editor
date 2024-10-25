using Enemy_Editor.Armors;
using Enemy_Editor.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Enemy_Editor.Classes
{
    public class EnemyTemplateList : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //Список противников из класса CEnemyTemplate
        private ObservableCollection<EnemyTemplate> _enemies;
        public ObservableCollection<EnemyTemplate> Enemies
        {
            get { return _enemies; }
            set
            {
                _enemies = value;
                //OnPropertyChanged(nameof(Enemies));
            }
        }
        public EnemyTemplateList()
        {
            Enemies = new ObservableCollection<EnemyTemplate>();
        }

        public void AddEnemy(
            string name,
            string iconName,
            int baseLife,
            double lifeMod,
            int baseGold,
            double goldMod,
            double spawnChance
            )
        {
            _enemies.Add(
                new EnemyTemplate(name, iconName, baseLife, lifeMod, baseGold, goldMod, spawnChance)
                );

            //OnPropertyChanged(nameof(Enemies));
        }

        public void AddEnemy(EnemyTemplate e)
        {
            _enemies.Add(e);
            //OnPropertyChanged(nameof(Enemies));

        }

        public EnemyTemplate GetByName(string name)
        {
            return (EnemyTemplate)_enemies.Where(e => e.Name == name);
        }

        public EnemyTemplate GetById(int id)
        {
            return _enemies.ElementAt(id);
        }

        public void DeleteByName(string name)
        {
            _enemies.Remove((EnemyTemplate)_enemies.FirstOrDefault(e => e.Name == name));
        }

        public void DeleteById(int id)
        {
            _enemies.RemoveAt(id);
        }

        public List<string> GetNames()
        {
            return _enemies.Where(e => !string.IsNullOrEmpty(e.Name)).Select(e => e.Name).ToList();
        }

        public void SaveToJson()
        {
            var options = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,  // Включаем красивое форматирование
                TypeNameHandling = TypeNameHandling.Objects
            };

            string jsonString = JsonConvert.SerializeObject(_enemies, options);
            // Сохранение JSON в файл
            File.WriteAllText("EnemysList.json", jsonString);
        }

        public void LoadFromJson()
        {

            string jsonFromFile = File.ReadAllText("EnemysList.json");

            ObservableCollection<EnemyTemplate> _e = JsonConvert.DeserializeObject<ObservableCollection<EnemyTemplate>>(jsonFromFile);

            //JsonDocument doc = JsonDocument.Parse(jsonFromFile);
            ////Добавление новой записи в список класса из json
            //foreach (JsonElement element in doc.RootElement.EnumerateArray())
            //{
            //    string Name = element.GetProperty("Name").ToString();
            //    IArmor Armor = element.GetProperty("Armor") as LeatherArmor;

            // Создание нового экземпляра класса Person с помощью конструктора
            //Person person = new Person(age, firstName, secondName, height);
            //// Добавление объекта в список
            //people.Add(person);

            foreach (var _enemy in _e)
            {

                AddEnemy(_enemy);
            }
        }

        public void SaveToXml()
        {
            var serializer = new XmlSerializer(typeof(EnemyTemplate));
            using (var writer = new StreamWriter("character.xml"))
            {
                serializer.Serialize(writer, Enemies);
            }
        }





    }

        

        
    
}


