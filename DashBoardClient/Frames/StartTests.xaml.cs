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
    /// Логика взаимодействия для StartTests.xaml
    /// </summary>
    public partial class StartTests : Page
    {
        public List<PacksWithTest> PackList { get; set; }
        OpenTestList testList;
        readonly ServerConnect server = new ServerConnect();       
        string response = "";
        string[] packsList;

        public StartTests()
        {
            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            PackListView.ItemsSource = "";

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
                    pack.Count = (testsCount.Length - 1).ToString();
                    pack.RestartCount = packsForList[4];
                    pack.Time = packsForList[3];
                    pack.IP = packsForList[5];
                    if (packsForList[6] == "no_start") pack.Status = "Не запущено";
                    else pack.Status = "Запущено";
                     
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

        private void OpenTestList(object sender, RoutedEventArgs e)
        {            
            testList = new OpenTestList((string)((Button)e.OriginalSource).Tag);            
            testList.ShowDialog();
        }

        private void StartPacks(object sender, RoutedEventArgs e)
        {
            string packs = "";
            foreach (PacksWithTest listItem in PackListView.SelectedItems) packs += listItem.ID + "±";
            if (packs.Length != 0) packs = packs.Substring(0, packs.Length - 1);
            if (packs != "") response = server.SendMsg("startPackTests", "ai", packs);
            else MessageBox.Show("Не выбрано ни одного набора!");
            if (response == "OK") MessageBox.Show("Набор(ы) отправлен(ы) на запуск!");
            if (response == "ERROR") MessageBox.Show("Произошла ошибка запуска!");
            if (response == "START") MessageBox.Show("Один из выбранных наборов находится в режиме запуска!");            
            UpdateList();
        }
    }
}
