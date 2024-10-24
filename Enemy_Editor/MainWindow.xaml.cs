using System.Diagnostics;
using System.IO;
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
using Enemy_Editor.Classes;
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

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            IconList = new List<IconItem>();
            EnemyList = new EnemyTemplateList();

            OpenFolderDialog choofdlog = new OpenFolderDialog();

            if ((bool)choofdlog.ShowDialog())
            {
                LoadImages(choofdlog.FolderName);
            }

            IconListBox.ItemsSource = IconList;

            EnemyList.AddEnemy(new EnemyTemplate());

            DataContext = EnemyList;

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
            EnemyList.AddEnemy(new EnemyTemplate());
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
        }

        private void Element_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            //(sender as TextBox)?.SelectAll();
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
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