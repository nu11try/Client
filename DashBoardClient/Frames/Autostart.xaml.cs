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
    /// Логика взаимодействия для Autostart.xaml
    /// </summary>
    public partial class Autostart : Page
    {
        
        readonly ServerConnect server = new ServerConnect();
        public List<AutoClass> AutoList { get; set; }
        Message response;
        BackgroundWorker bw;
        public Autostart()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            this.IsEnabled = false;
            AutoListView.Visibility = Visibility.Hidden;
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                this.IsEnabled = true;
                AutoListView.Visibility = Visibility.Visible;
                AutoListView.ItemsSource = AutoList;
            };
            
        }

        private void UpdateList()
        {
            AutoList = new List<AutoClass>();
            try
            {
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetAutostart", Data.ServiceSel));
                if (response.args[0] == "error") return;
                for (var i = 0; i < response.args.Count; i += 8)
                {
                    AutoClass auto = new AutoClass();
                    auto.ID = response.args[i];
                    auto.Name = response.args[i + 1];
                    if (response.args[i + 5] == "regular") auto.Type = "Регулярно";
                    else if (response.args[i + 5] == "one") auto.Type = "Единоразово";
                    Message packs = JsonConvert.DeserializeObject<Message>(response.args[i + 7]);
                    auto.Pack = String.Join("\n", packs.args.ToArray());
                    auto.Time = response.args[i + 3];
                    Message days = JsonConvert.DeserializeObject<Message>(response.args[i + 2]);
                    auto.Day = String.Join("-", days.args.ToArray());
                    auto.Status = response.args[i + 6];

                    AutoList.Add(auto);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
        }

        public class AutoClass
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Pack { get; set; }
            public string Time { get; set; }
            public string Day { get; set; }
            public string Status { get; set; }
        }
        
        private void AddAutostart(object sender, RoutedEventArgs e)
        {
            //AutostartAdd autoAdd = new AutostartAdd((sender as Button).Tag.ToString());
            AutostartAddChange autoAdd = new AutostartAddChange();
            autoAdd.ShowDialog();
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            this.IsEnabled = false;
            AutoListView.Visibility = Visibility.Hidden;
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                this.IsEnabled = true;
                AutoListView.Visibility = Visibility.Visible;
                AutoListView.ItemsSource = AutoList;
            };
        }
        private void chgAutostart(object sender, RoutedEventArgs e)
        {
            AutostartAddChange autoChg = new AutostartAddChange((sender as Button).Tag.ToString());            
            autoChg.ShowDialog();
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            this.IsEnabled = false;
            AutoListView.Visibility = Visibility.Hidden;
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                this.IsEnabled = true;
                AutoListView.Visibility = Visibility.Visible;
                AutoListView.ItemsSource = AutoList;
            };
        }
        private void deleteAutostart(object sender, RoutedEventArgs e)
        {
            Message message = new Message();
            message.Add((sender as Button).Tag.ToString());
            server.SendMsg("DeleteAutostartNotOne", Data.ServiceSel, JsonConvert.SerializeObject(message));
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            this.IsEnabled = false;
            AutoListView.Visibility = Visibility.Hidden;
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                this.IsEnabled = true;
                AutoListView.Visibility = Visibility.Visible;
                AutoListView.ItemsSource = AutoList;
            };
        }
        private void stopAutostart(object sender, RoutedEventArgs e)
        {
            Message message = new Message();
            message.Add((sender as Button).Tag.ToString());
            server.SendMsg("StopAutotest", Data.ServiceSel, JsonConvert.SerializeObject(message));
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            this.IsEnabled = false;
            AutoListView.Visibility = Visibility.Hidden;
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                this.IsEnabled = true;
                AutoListView.Visibility = Visibility.Visible;
                AutoListView.ItemsSource = AutoList;
            };
        }
    }
}
