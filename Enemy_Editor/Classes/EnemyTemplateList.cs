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
                new EnemyTemplate(name,iconName, baseLife, lifeMod, baseGold, goldMod,spawnChance)
                );

            //OnPropertyChanged(nameof(Enemies));
        }

        public void AddEnemy(EnemyTemplate e)
        {
            _enemies.Add(e);
            //OnPropertyChanged(nameof(Enemies));

        }

        public EnemyTemplate GetByName( string name ) 
        {
            return (EnemyTemplate)_enemies.Where(e => e.Name == name);
        }

        public EnemyTemplate GetById (int id)
        {
            return _enemies.ElementAt(id);
        }

        public void DeleteByName(string name )
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
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,  // Включаем красивое форматирование
            };

            string jsonString = JsonSerializer.Serialize(_enemies, options);
            // Сохранение JSON в файл
            File.WriteAllText("EnemysList.json", jsonString);
        }

        public void LoadFromJson()
        {

            


            string jsonFromFile = File.ReadAllText("EnemysList.json");

            ObservableCollection<EnemyTemplate> _e = JsonSerializer.Deserialize<ObservableCollection<EnemyTemplate>>(jsonFromFile);
            
            foreach (var _enemy in _e)
            {
                AddEnemy(_enemy);   
            }

            // Парсинг JSON
            //JsonDocument doc = JsonDocument.Parse(jsonFromFile);
            ////Добавление новой записи в список класса из json
            //foreach (JsonElement element in doc.RootElement.EnumerateArray())
            //{
            //    int age = element.GetProperty("age").GetInt32();
            //    string firstName = element.GetProperty("first_name").GetString();
            //    string secondName = element.GetProperty("second_name").GetString();
            //    double height = element.GetProperty("height").GetDouble();
            //    // Создание нового экземпляра класса Person с помощью конструктора
            //    Person person = new Person(age, firstName, secondName, height);
            //    // Добавление объекта в список
            //    people.Add(person);
            //}
            //// Вывод данных на экран
            //foreach (var person in people)
            //{
            //    Console.WriteLine($"Age: {person.Age()}, Name: {person.FirstName()}
            //{ person.SecondName()}, Height: { person.Height()}
            //");
        }

        
    }
}
