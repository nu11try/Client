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
<<<<<<< HEAD

        Message message = new Message();

        readonly ServerConnect server = new ServerConnect();       
        string response = "";
        string request = "";
=======
        readonly ServerConnect server = new ServerConnect();
        Message response;
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d

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
<<<<<<< HEAD
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ServiceSel));                
                if (message.args[0] == "no_packs")
=======
                Message message = new Message();
                string request = JsonConvert.SerializeObject(message);
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ProjectName, request));
                if (response.args[0] == "no_packs")
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
                {
                    MessageBox.Show("Нет добавленных наборов");
                    return;
                }
<<<<<<< HEAD
                for (var i = 0; i < message.args.Count; i += 7)
                {
                    PacksWithTest pack = new PacksWithTest();
                    pack.ID = message.args[i];
                    pack.Name = message.args[i + 1];
                    pack.Count = JsonConvert.DeserializeObject<TestsStartClass>(message.args[i + 2]).id.Count.ToString();
                    pack.RestartCount = message.args[i + 4];
                    pack.Time = message.args[i + 3];
                    pack.IP = message.args[i + 5];
                    if (message.args[i + 6] == "no_start") pack.Status = "Не запущено";
=======
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
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
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
            message = new Message();
        }

        private void OpenTestList(object sender, RoutedEventArgs e)
        {            
            testList = new OpenTestList((string)((Button)e.OriginalSource).Tag);            
            testList.ShowDialog();
        }

        private void StartPacks(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            string packs = "";
            foreach (PacksWithTest listItem in PackListView.SelectedItems) packs += listItem.ID + "±";
            if (packs.Length != 0) packs = packs.Substring(0, packs.Length - 1);
            if (packs != "")
            {
                message.Add(packs);
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("StartTests", Data.ServiceSel, request);
            }
            else MessageBox.Show("Не выбрано ни одного набора!");
            if (JsonConvert.DeserializeObject<Message>(response).args[0] == "OK") MessageBox.Show("Набор(ы) отправлен(ы) на запуск!");
            if (JsonConvert.DeserializeObject<Message>(response).args[0] == "ERROR") MessageBox.Show("Произошла ошибка запуска!");
            if (JsonConvert.DeserializeObject<Message>(response).args[0] == "START") MessageBox.Show("Один из выбранных наборов находится в режиме запуска!");            
=======

            Message message = new Message();
            foreach (PacksWithTest listItem in PackListView.SelectedItems) message.Add(listItem.ID);
            string request = JsonConvert.SerializeObject(message);
            if(response.args.Count != 0) response = JsonConvert.DeserializeObject<Message>(server.SendMsg("StartPackTests", Data.ProjectName, request));
            else MessageBox.Show("Не выбрано ни одного набора!");
            if (response.args[0] == "OK") MessageBox.Show("Набор(ы) отправлен(ы) на запуск!");
            if (response.args[0] == "ERROR") MessageBox.Show("Произошла ошибка запуска!");
            if (response.args[0] == "START") MessageBox.Show("Один из выбранных наборов находится в режиме запуска!");            
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
            UpdateList();
        }
    }
}
