using System;
using System.Collections.Generic;
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //Список противников из класса CEnemyTemplate
        public List<EnemyTemplate> enemies { get; }
        public EnemyTemplateList()
        {
            enemies = new List<EnemyTemplate>();
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
            enemies.Add(
                new EnemyTemplate(name,iconName, baseLife, lifeMod, baseGold, goldMod,spawnChance)
                );
        }

        public void AddEnemy(EnemyTemplate e)
        {
            enemies.Add(e);
            
        }

        public EnemyTemplate GetByName( string name ) 
        {
            return (EnemyTemplate)enemies.Where(e => e.Name == name);
        }

        public EnemyTemplate GetById (int id)
        {
            return enemies.ElementAt(id);
        }

        public void DeleteByName(string name )
        {
            enemies.Remove((EnemyTemplate)enemies.Where(e => e.Name == name));
        }

        public void DeleteById(int id)
        {
            enemies.RemoveAt(id);
        }

        public List<string> GetNames()
        {
            return enemies.Where(e => !string.IsNullOrEmpty(e.Name)).Select(e => e.Name).ToList();
        }

        public void SaveToJson()
        {
            string jsonString = JsonSerializer.Serialize(enemies);
            // Сохранение JSON в файл
            File.WriteAllText("EnemysList.json", jsonString);
        }
    }
}
