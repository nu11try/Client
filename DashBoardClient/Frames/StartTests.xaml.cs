using Newtonsoft.Json;
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
        public class Tests
        {
            public Tests()
            {
                id = new List<string>();
                start = new List<string>();
                time = new List<string>();
                dependon = new List<string>();
                restart = new List<string>();
            }
            public List<string> id { get; set; }
            public List<string> start { get; set; }
            public List<string> time { get; set; }
            public List<string> dependon { get; set; }
            public List<string> restart { get; set; }
        }
        OpenTestList testList;
        readonly ServerConnect server = new ServerConnect();
        Message response;

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
                Message message = new Message();
                string request = JsonConvert.SerializeObject(message);
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ProjectName, request));
                if (response.args[0] == "no_packs")
                {
                    MessageBox.Show("Нет добавленных наборов");
                    return;
                }
                for (var i = 0; i < response.args.Count; i++)
                {
                    PacksWithTest pack = new PacksWithTest();
                    Tests tests = JsonConvert.DeserializeObject<Tests>(response.args[2]);
                    pack.ID = response.args[0];
                    pack.Name = response.args[1];
                    pack.Count = (tests.id.Count).ToString();
                    pack.RestartCount = response.args[4];
                    pack.Time = response.args[3];
                    pack.IP = response.args[5];
                    if (response.args[6] == "no_start") pack.Status = "Не запущено";
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

            Message message = new Message();
            foreach (PacksWithTest listItem in PackListView.SelectedItems) message.Add(listItem.ID);
            string request = JsonConvert.SerializeObject(message);
            if(response.args.Count != 0) response = JsonConvert.DeserializeObject<Message>(server.SendMsg("StartPackTests", Data.ProjectName, request));
            else MessageBox.Show("Не выбрано ни одного набора!");
            if (response.args[0] == "OK") MessageBox.Show("Набор(ы) отправлен(ы) на запуск!");
            if (response.args[0] == "ERROR") MessageBox.Show("Произошла ошибка запуска!");
            if (response.args[0] == "START") MessageBox.Show("Один из выбранных наборов находится в режиме запуска!");            
            UpdateList();
        }
    }
}
