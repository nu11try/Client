using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        Message message;
        BackgroundWorker bw;
        public Packs()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                PackListView.ItemsSource = PackList;
            };

        }

        private void UpdateList()
        {
            PackList = new List<PacksWithTest>();
            try
            {
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ServiceSel));                                
                TestsStartClass tests = new TestsStartClass();
                for (var i = 0; i < message.args.Count; i += 7)
                {
                    tests = JsonConvert.DeserializeObject<TestsStartClass>(message.args[i + 2]);
                    PacksWithTest pack = new PacksWithTest();
                    pack.ID = message.args[i];
                    pack.Name = message.args[i+1];
                    pack.Count = tests.id.Count.ToString();
                    pack.Result = message.args[i+5];
                    pack.Time = message.args[i+3];
                    pack.RestartCount = message.args[i+4];

                    PackList.Add(pack);
                }
            }
            catch
            {
                //MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }

            
        }

        private void AddPack(object sender, RoutedEventArgs e)
        {
            PackFormAdd addPack = new PackFormAdd();
            try { addPack.ShowDialog(); }
            catch { MessageBox.Show(""); }
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                PackListView.ItemsSource = PackList;
            };
        }
        private void ChangePack(object sender, RoutedEventArgs e)
        {
            PackFormChange packChange = new PackFormChange((sender as Button).Tag.ToString());
            packChange.ShowDialog();
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                PackListView.ItemsSource = PackList;
            };
        }
        private void ChangeTests(object sender, RoutedEventArgs e)
        {
            ViewTestsPack viewTests = new ViewTestsPack((sender as Button).Tag.ToString());
            try { viewTests.ShowDialog(); }
            catch { MessageBox.Show("s"); }

            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                PackListView.ItemsSource = PackList;
            };
        }
        private void DeletePack_Click(object sender, RoutedEventArgs e)
        {
            Message message = new Message();
            message.Add((sender as Button).Tag.ToString());
            server.SendMsg("DeletePack", Data.ServiceSel, JsonConvert.SerializeObject(message));
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                PackListView.ItemsSource = PackList;
            };
        }
    }
}
