using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Doc.xaml
    /// </summary>
    public partial class Bugs : Page
    {
        readonly ServerConnect server = new ServerConnect();
        List<Bug> BugList;
        List<Bug> BugClosedList;
        public class Bug
        {
            public string link { get; set; }
            public string name { get; set; }
            public string type { get; set; }

            public string data { get; set; }
            public string status { get; set; }
            public string executor { get; set; }
            public string tests { get; set; }
        }
        BackgroundWorker bw;
        public Bugs()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                server.SendMsg("CheckErrors", Data.ServiceSel);
                Update();
                UpdateClosed();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                BugsList.ItemsSource = BugList;
                ClosedBugsList.ItemsSource = BugClosedList;
            };


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddAllBug AddBug = new AddAllBug("-");
            AddBug.ShowDialog();
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            bw.DoWork += (obj, ea) => {
                Update();
                UpdateClosed();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                BugsList.ItemsSource = BugList;
                ClosedBugsList.ItemsSource = BugClosedList;
            };
        }
        private void EditBug(object sender, RoutedEventArgs e)
        {
            string link = (sender as Button).Tag.ToString();
            AddAllBug AddBug = new AddAllBug(link);
            AddBug.ShowDialog();
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            bw.DoWork += (obj, ea) => {
                Update();
                UpdateClosed();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                BugsList.ItemsSource = BugList;
                ClosedBugsList.ItemsSource = BugClosedList;
            };
        }

        private void DeleteClosedBug(object sender, RoutedEventArgs e)
        {
            string link = (sender as Button).Tag.ToString();
            Message args = new Message();
            args.Add(link);
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("DeleteBug", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            bw.DoWork += (obj, ea) => {
                UpdateClosed();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                ClosedBugsList.ItemsSource = BugClosedList;
            };
        }
        private void DeleteBug(object sender, RoutedEventArgs e)
        {
            string link = (sender as Button).Tag.ToString();
            Message args = new Message();
            args.Add(link);
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("DeleteBug", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            bw = new BackgroundWorker();
            wait.Opacity = 1;
            bw.DoWork += (obj, ea) => {
                Update();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                BugsList.ItemsSource = BugList;
            };
        }
        private void UpdateClosed()
        {
            BugClosedList = new List<Bug>();
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetClosedBug", Data.ServiceSel));
            for (int i = 0; i < res.args.Count; i += 7)
            {
                Bug bug = new Bug();
                bug.name = res.args[i];
                bug.link = res.args[i + 1];
                bug.type = res.args[i + 2];
                bug.data = res.args[i + 3];
                bug.executor = res.args[i + 5];
                bug.status = res.args[i + 4];
                bug.tests = res.args[i + 6].Replace(",","\n");
                BugClosedList.Add(bug);
            }
        }
        private void Update()
        {
            BugList = new List<Bug>();
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetBug", Data.ServiceSel));
            for (int i = 0; i < res.args.Count; i += 7)
            {
                Bug bug = new Bug();
                bug.name = res.args[i];
                bug.link = res.args[i + 1];
                bug.type = res.args[i + 2];
                bug.data = res.args[i + 3];
                bug.executor = res.args[i + 5];
                bug.status = res.args[i + 4];
                bug.tests = res.args[i + 6].Replace(",", "\n");
                BugList.Add(bug);
            }
        }
    }
}
