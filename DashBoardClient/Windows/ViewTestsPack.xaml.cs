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
using System.Windows.Shapes;

namespace DashBoardClient
{
    /// <summary>
    /// Логика взаимодействия для ViewTestsPack.xaml
    /// </summary>
    public class TestsList
    {        
        public string ID { get; set; }        
        public string NewName { get; set; }        
        public string Time { get; set; }
        public string Restart { get; set; }
        public string ResultExec { get; set; }
        public string TimeExec { get; set; }
    }

    public partial class ViewTestsPack : Window
    {
        List<TestsList> list;
        
        Message message = new Message();
        string response;
        string request;

        readonly ServerConnect server = new ServerConnect();
        string IDPack = "";

        public ViewTestsPack(string TAG)
        {
            IDPack = TAG;
            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            list = new List<TestsList>();
            message = new Message();
            try
            {
                message.Add(IDPack);
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("GetTestsThisPack", Data.ServiceSel, request);
                message = JsonConvert.DeserializeObject<Message>(response);
                for (var i = 0; i < message.args.Count; i += 4)
                {
                    TestsList test = new TestsList();
                    test.ID = message.args[i];
                    test.NewName = message.args[i+1];
                    if (message.args[i+2] == "default") test.Time = "По умолчанию";
                    else test.Time = message.args[i+2];
                    if (message.args[i+3] == "default") test.Restart = "По умолчанию";
                    else test.Restart = message.args[i+3];                    

                    list.Add(test);
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }
            message = new Message();
            DataContext = this;
            TestsList.ItemsSource = list;
        }
        private void ChangeTest(object sender, RoutedEventArgs e)
        {
            message.Add((sender as Button).Tag.ToString(), IDPack);
            request = JsonConvert.SerializeObject(message);
            PackTestsFormChange packTests = new PackTestsFormChange(request);
            packTests.ShowDialog();

            UpdateList();
        }
    }
}
