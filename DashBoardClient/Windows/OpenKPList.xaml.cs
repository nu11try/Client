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
using System.Windows.Shapes;

namespace DashBoardClient
{
    public class KP
    {
        // ID-шник теста
        public string ID { get; set; }
        // Первончальное имя теста
        public string Name { get; set; }
        // Ответственный за выполнение
        public string Author { get; set; }
        // Время выполнения
        public string Date { get; set; }
        // Результат
        public string Assc { get; set; }
        public string Test { get; set; }
    }

    public partial class OpenKPList : Window
    {
        List<KP> KPList;
        readonly ServerConnect server = new ServerConnect();
        string id = "";

        Message message = new Message();
        Message resMes = new Message();
        string request = "";
        string response = "";
        BackgroundWorker bw;
        public OpenKPList(string ID)
        {
            //Thread thread = Waiter.ShowWaiter();
            InitializeComponent();
            id = ID;
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                Update();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                KPListView.ItemsSource = KPList;
            };
        }

        private void Update()
        {
            KPList = new List<KP>();
            try
            {
                message.Add(id);
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("GetKPForDoc", Data.ServiceSel, request);
                resMes = JsonConvert.DeserializeObject<Message>(response);
                if (!resMes.args[0].Equals("no_kp"))                
                {
                    for (var i = 0; i < resMes.args.Count; i += 6)
                    {
                        KP kp = new KP();
                        kp.ID = resMes.args[i];
                        kp.Name = resMes.args[i + 1];
                        kp.Assc = resMes.args[i + 2];
                        kp.Author = resMes.args[i + 3];
                        kp.Date = resMes.args[i + 4];
                        Message tests = JsonConvert.DeserializeObject<Message>(resMes.args[i + 5]);
                        tests.args.ForEach(elem => kp.Test = kp.Test + elem + "\n");
                        kp.Test = kp.Test.Trim();

                        KPList.Add(kp);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
                return;
            }

           
            message = new Message();    
        }

        private void AddKP(object sender, RoutedEventArgs e)
        {
            //KPFormAdd kPFormAdd = new KPFormAdd((sender as Button).Tag.ToString());
            KPFormAdd kPFormAdd = new KPFormAdd(id, "", "add");
            kPFormAdd.ShowDialog();
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                Update();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                KPListView.ItemsSource = KPList;
            };
        }
        private void UpdateKP(object sender, RoutedEventArgs e)
        {
            //KPFormAdd kPFormAdd = new KPFormAdd((sender as Button).Tag.ToString());            
            KPFormAdd kPFormAdd = new KPFormAdd(id, (sender as Button).Tag.ToString(), "update");
            kPFormAdd.ShowDialog();
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            bw.DoWork += (obj, ea) => {
                Update();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                KPListView.ItemsSource = KPList;
            };
        }

        private void DeletePack_Click(object sender, RoutedEventArgs e)
        {
            Message message = new Message();
            message.Add((sender as Button).Tag.ToString());
            for(int i = 0; i < KPListView.Items.Count; i++)
            {
                KP kP = (KP)KPListView.Items[i];
                if(kP.Test != "not" && (sender as Button).Tag.ToString() == kP.ID)
                {
                    MessageBox.Show("К кп привязан тест!");
                    return;
                }
            }
            server.SendMsg("DeleteKP", Data.ServiceSel, JsonConvert.SerializeObject(message));
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            bw.DoWork += (obj, ea) => {
                Update();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                KPListView.ItemsSource = KPList;
            };
        }
    }
}
