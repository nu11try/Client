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
using System.Threading;
using System.ComponentModel;

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
        Message message = new Message();
        readonly ServerConnect server = new ServerConnect();
        string response = "";
        string request = "";
        BackgroundWorker bw;
        public StartTests()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            this.IsEnabled = false;
            PackListView.Visibility = Visibility.Hidden;
            
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                this.IsEnabled = true;
                PackListView.Visibility = Visibility.Visible;
                PackListView.ItemsSource = PackList;
            };

        }

        private void UpdateList()
        {
            PackList = new List<PacksWithTest>();
            try
            {
                Message message1 = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ServiceSel));
                if (message1.args[0] == "no_packs")
                {
                    MessageBox.Show("Нет добавленных наборов");
                    return;
                }
                for (var i = 0; i < message1.args.Count; i += 10)
                {
                    PacksWithTest pack = new PacksWithTest();
                    pack.ID = message1.args[i];
                    pack.Name = message1.args[i + 1];
                    pack.Count = JsonConvert.DeserializeObject<TestsStartClass>(message1.args[i + 2]).id.Count.ToString();
                    pack.RestartCount = message1.args[i + 4];
                    pack.Time = message1.args[i + 3];
                    pack.IP = message1.args[i + 5];
                    if (message1.args[i + 6] == "no_start") pack.Status = "Не запущено";
                    else pack.Status = "Запущено";
                    if (message1.args[i + 7] == "Passed") pack.Result = "/DashBoardClient;component/Images/ok.png";
                    if (message1.args[i + 7] == "Failed") pack.Result = "/DashBoardClient;component/Images/bug.png";
                    if (message1.args[i + 7] == "-") pack.Result = "/DashBoardClient;component/Images/dependon_no_version.png";
                    pack.LastTime = message1.args[i + 8];
                    pack.LastTimeEnd = message1.args[i + 9];
                    PackList.Add(pack);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    UpdateList();
                }
                catch { }
                //MessageBox.Show(ex.Message);
                //MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }

            message = new Message();
        }

        private void OpenTestList(object sender, RoutedEventArgs e)
        {
            testList = new OpenTestList((string)((Button)e.OriginalSource).Tag);
            testList.ShowDialog();
        }

        private void StartPacks(object sender, RoutedEventArgs e)
        {
            foreach (PacksWithTest listItem in PackListView.SelectedItems) message.Add(listItem.ID);

            if (message.args.Count != 0)
            {
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("StartTests", Data.ServiceSel, request);
                if (JsonConvert.DeserializeObject<Message>(response).args[0] == "OK")
                    if (message.args.Count == 1)
                    {
                        MainWindow._vm.SuccCastMess("Набор " + message.args[0] + " отправлен на запуск");
                    }
                    else
                    {
                        string temp = "Наборы - ";
                        for (int i = 0; i < message.args.Count; i++)
                        {
                            temp += message.args[i] + ", ";
                        }
                        temp += "отправлен на запуск";
                        MainWindow._vm.SuccCastMess(temp);
                    }
                if (JsonConvert.DeserializeObject<Message>(response).args[0] == "ERROR") MainWindow._vm.ErrCastMess("Произошла ошибка запуска!");
                if (JsonConvert.DeserializeObject<Message>(response).args[0] == "START") MainWindow._vm.WarCastMess("Один из выбранных наборов находится в режиме запуска!");
                bw = new BackgroundWorker();
                this.IsEnabled = false;
                PackListView.Visibility = Visibility.Hidden;
                wait.Opacity = 1;
                bw.DoWork += (obj, ea) => {
                    UpdateList();
                };
                bw.RunWorkerAsync();
                bw.RunWorkerCompleted += (obj, ea) => {

                    wait.Opacity = 0;
                    this.IsEnabled = true;
                    PackListView.Visibility = Visibility.Visible;
                    PackListView.ItemsSource = PackList;
                };

            }
            else MessageBox.Show("Не выбрано ни одного набора!");

        }

        private void StopTests(object sender, RoutedEventArgs e)
        {
            foreach (PacksWithTest listItem in PackListView.SelectedItems) message.Add(listItem.ID);

            if (message.args.Count != 0)
            {
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("StopTests", Data.ServiceSel, request);
                if (JsonConvert.DeserializeObject<Message>(response).args[0] == "OK") MessageBox.Show("Набор(ы) отправлен(ы) на остановку!");
                if (JsonConvert.DeserializeObject<Message>(response).args[0] == "ERROR") MessageBox.Show("Произошла ошибка остановки!");
                if (JsonConvert.DeserializeObject<Message>(response).args[0] == "NO_START") MessageBox.Show("Один из выбранных наборов не находится в режиме запуска!");
                bw = new BackgroundWorker();
                this.IsEnabled = false;
                PackListView.Visibility = Visibility.Hidden;
                wait.Opacity = 1;
                bw.DoWork += (obj, ea) => {
                    UpdateList();
                };
                bw.RunWorkerAsync();
                bw.RunWorkerCompleted += (obj, ea) => {
                    this.IsEnabled = true;
                    PackListView.Visibility = Visibility.Visible;
                    wait.Opacity = 0;
                    PackListView.ItemsSource = PackList;
                };
            }
            else MessageBox.Show("Не выбрано ни одного набора!");
        }
    }
}
