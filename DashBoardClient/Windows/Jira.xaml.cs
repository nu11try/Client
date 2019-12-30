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
    /// <summary>
    /// Логика взаимодействия для FormShowCheckList.xaml
    /// </summary>
    /// 

    public class Errors
    {
        public string link { get; set; }
        public string name { get; set; }
        public string type { get; set; }

        public string data { get; set; }
        public string status { get; set; }
        public string executor { get; set; }
    }
    public partial class Jira : Window
    {
        string id;
        readonly ServerConnect server = new ServerConnect();
        List<Errors> EList;
        BackgroundWorker bw;
        public Jira(string TAG)
        {
            id = TAG;
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                Update();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                JiraList.ItemsSource = EList;
            };
        }
        private void Update()
        {
            EList = new List<Errors>();
            Message args = new Message();
            args.Add(id);
            server.SendMsg("CheckErrors", Data.ServiceSel, JsonConvert.SerializeObject(args));
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetErrors", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            for (int i = 0; i < res.args.Count; i += 6)
            {
                Errors error = new Errors();
                error.name = res.args[i];
                error.link = res.args[i + 1];
                error.type = res.args[i + 2];
                error.data = res.args[i + 3];
                error.executor = res.args[i + 5];
                error.status = res.args[i + 4];
                EList.Add(error);
            }         
        }
        private void AddBug(object sender, RoutedEventArgs e)
        {
            AddBug AddBug = new AddBug(id);
            AddBug.ShowDialog();
            Update();
        }
        private void DeleteBug(Object sender, RoutedEventArgs e)
        {
            string link = (sender as Button).Tag.ToString();
            Message args = new Message();
            args.Add(id, link);
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("DeleteBug", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            Update();
        }

        private void JiraList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (JiraList.SelectedItems.Count == 1)
            {
                Errors error = (Errors)JiraList.SelectedItem;
                System.Diagnostics.Process.Start("https://job-jira.otr.ru/browse/" + error.link);
            }
        }
    }
}
