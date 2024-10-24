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
            EnemyList.AddEnemy("123","123",50,2,100,2,52);

            EnemyListBox.ItemsSource = EnemyList.enemies;
            EnemyListBox.DisplayMemberPath = "Name";

            //EnemyList.AddEnemy("5552", "126215", 50, 2, 100, 2, 52);


            CurrentEnemy = new EnemyTemplate();
            GridWithData.DataContext = CurrentEnemy;

            

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

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            EnemyList.AddEnemy(CurrentEnemy);

            CurrentEnemy = new EnemyTemplate();
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