using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Enemy_Editor.Armors;
using Enemy_Editor.Classes;
using Enemy_Editor.Interfaces;
using Microsoft.Win32;

namespace Enemy_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<IconItem> IconList { get; set; }
        public EnemyTemplateList EnemyList { get; set; }    
        public EnemyTemplate CurrentEnemy { get; set; }
        public List<IArmor> ArmorTypes { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            IconList = new List<IconItem>();
            EnemyList = new EnemyTemplateList();
            ArmorTypes = new List<IArmor>();

            OpenFolderDialog choofdlog = new OpenFolderDialog();

            if ((bool)choofdlog.ShowDialog())
            {
                LoadImages(choofdlog.FolderName);
            }

            IconListBox.ItemsSource = IconList;

            EnemyList.AddEnemy(new EnemyTemplate { Armor = new NoArmor()});

            DataContext = EnemyList;

            Type[] types = GetClassesInNamespace("Enemy_Editor.Armors");

            foreach(Type t in types)
            {
                ArmorTypes.Add((IArmor)CreateInstance("Enemy_Editor.Armors", t.Name));
            }

            ArmorTypeComboBox.ItemsSource = ArmorTypes;
            ArmorTypeComboBox.DisplayMemberPath = "Name";

            ArmorTypeComboBox.SelectedIndex = 0;

            EnemyList.AddEnemy(new EnemyTemplate());

            //EnemyListBox.ItemsSource = EnemyList.Enemies;
            //EnemyListBox.DisplayMemberPath = "Name";


        }


        public void LoadImages(string path)
        {
            string filter = "*.png";
            string[] files = Directory.GetFiles(path, filter);
            foreach (string file in files)
            {
                IconList.Add(new IconItem(file));
            }
        }

        #region Menu Buttons

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            EnemyTemplate a = new EnemyTemplate();
            a.Armor = new NoArmor();

            EnemyList.AddEnemy(a);
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            EnemyList.DeleteByName((EnemyListBox.SelectedItem as EnemyTemplate)?.Name);
        }

        private void SaveEnemies_OnClick(object sender, RoutedEventArgs e)
        {
            EnemyList.SaveToJson();
        }

        public void LoadEnemies_OnClick(object sender, RoutedEventArgs e)
        {
            EnemyList.LoadFromJson();
        }
        #endregion



        private void IconListBox_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            (EnemyListBox.SelectedItem as EnemyTemplate)!.IconName = (IconListBox.Items.CurrentItem as IconItem)!.IconPath;
        }

        private void IconListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IconListBox.SelectedItem!= null)
                (EnemyListBox.SelectedItem as EnemyTemplate)!.IconName = (IconListBox.SelectedItem as IconItem)!.IconPath;
        }

        private void EnemyListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IconListBox.SelectedItem = null; //TODO: Make it more nice

            //var t = GetType((EnemyListBox.SelectedItem as EnemyTemplate).Armor;

            if (EnemyListBox.SelectedItem != null)
            {
                foreach (IArmor arm in ArmorTypeComboBox.Items)
                {
                    if((EnemyListBox.SelectedItem as EnemyTemplate).Armor?.GetType() == arm.GetType())
                    {
                        ArmorTypeComboBox.SelectedIndex = ArmorTypeComboBox.Items.IndexOf(arm);
                    }
                }
                
                    
                    //((EnemyListBox.SelectedItem as EnemyTemplate).Armor);
            }
                
        }

        private void ArmorTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ArmorTypeComboBox.SelectedItem != null && EnemyListBox.SelectedItem != null)
            {
                (EnemyListBox.SelectedItem as EnemyTemplate)!.Armor = (IArmor)ArmorTypeComboBox.SelectedItem;
            }
        }

        private void Element_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            //(sender as TextBox)?.SelectAll();
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        //Собиратель классов
        public static object CreateInstance(string namespaceName, string className)
        {
            // Получаем сборку, где находится класс (обычно это текущая сборка)
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Полное имя типа
            string fullTypeName = $"{namespaceName}.{className}";

            // Получаем Type для класса
            Type type = assembly.GetType(fullTypeName);
            if (type == null)
            {
                throw new ArgumentException($"Класс с именем {fullTypeName} не найден в сборке.");
            }

            // Создаем экземпляр типа
            return Activator.CreateInstance(type);
        }

        public Type[] GetClassesInNamespace(string namespaceName)
        {
            // Получаем текущую сборку (или можно указать другую сборку, если нужно)
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Находим все классы в указанном пространстве имен
            var types = assembly.GetTypes()
                                .Where(t => t.IsClass &&
                                            t.Namespace == namespaceName &&
                                            !t.IsAbstract)  // Исключаем абстрактные классы
                                .ToArray();

            return types;
        }

        
    }

    public class IconItem
    {
        public string IconPath { get; set; }
        public string IconName { get; set; }

        public IconItem(string iconPath)
        {
            IconPath = iconPath;

            string[] m = iconPath.Split(new char[] { '\\' });

            IconName = m.Last();
        }
    }
}