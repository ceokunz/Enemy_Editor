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
using Microsoft.Win32;

namespace Enemy_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<IconItem> EnemiesList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            EnemiesList = new List<IconItem>();

            OpenFolderDialog choofdlog = new OpenFolderDialog();

            if ((bool)choofdlog.ShowDialog())
            {
                LoadImages(choofdlog.FolderName);
            }

            IconListBox.ItemsSource = EnemiesList;
        }


        public void LoadImages(string path)
        {
            string filter = "*.png";
            string[] files = Directory.GetFiles(path, filter);
            foreach (string file in files)
            {
                EnemiesList.Add(new IconItem(file));
            }
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