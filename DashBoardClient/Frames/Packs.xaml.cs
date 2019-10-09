using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashBoardClient
{
    /// <summary>
    /// Логика взаимодействия для Packs.xaml
    /// </summary>
    public partial class Packs : Page
    {
        readonly ServerConnect server = new ServerConnect();
        public List<PacksWithTest> PackList { get; set; }
        string response = "";
        string[] packsList;
        public Packs()
        {
            InitializeComponent();
            UpdateList();           
        }

        private void UpdateList()
        {
            PackList = new List<PacksWithTest>();

            try
            {
                response = server.SendMsg("getPacksForList", "ai");
                packsList = response.Split('╡');
                if (packsList[0] == "no_packs")
                {
                    MessageBox.Show("Нет добавленных наборов");
                    return;
                }                

                for (var i = 0; i < packsList.Length - 1; i++)
                {
                    PacksWithTest pack = new PacksWithTest();
                    string[] packsForList = packsList[i].Split('±');
                    string[] testsCount = packsForList[2].Split('▲');
                    pack.ID = packsForList[0];
                    pack.Name = packsForList[1];
                    pack.NewName = packsForList[2];
                    pack.Count = (testsCount.Length - 1).ToString();
                    pack.Result = packsForList[5];
                    pack.Time = packsForList[3];
                    pack.RestartCount = packsForList[4];

                    PackList.Add(pack);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
            DataContext = this;

            DataContext = this;
            PackListView.ItemsSource = PackList;
        }

        private void AddPack(object sender, RoutedEventArgs e)
        {
            PackFormAdd addPack = new PackFormAdd();
            try { addPack.ShowDialog(); }
            catch { MessageBox.Show(""); }            
            UpdateList();
        }
        private void ChangePack(object sender, RoutedEventArgs e)
        {
            PackFormChange packChange = new PackFormChange((sender as Button).Tag.ToString());
            packChange.ShowDialog();
            UpdateList();
        }
        private void ChangeTests(object sender, RoutedEventArgs e)
        {
            ViewTestsPack viewTests = new ViewTestsPack((sender as Button).Tag.ToString());
            try { viewTests.ShowDialog(); }
            catch { MessageBox.Show("s"); }
            
            UpdateList();
        }
    }
}
