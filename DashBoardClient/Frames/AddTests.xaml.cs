using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddTests.xaml
    /// </summary>
    public partial class AddTests : Page
    {
        TestFormAdd formAdd;
        readonly ServerConnect server = new ServerConnect();
        public List<AddedTests> TestsList { get; set; }
        Message response = new Message();

        public AddTests()
        {
            Thread thread = Waiter.ShowWaiter();
            InitializeComponent();
            UpdateList();
            Waiter.AbortWaiter(thread);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            formAdd = new TestFormAdd();
            formAdd.ShowDialog();

            UpdateList();
        }

        private void ChangeBtnTest(object sender, RoutedEventArgs e)
        {
            TestFormChange testChange = new TestFormChange((sender as Button).Tag.ToString());
            testChange.ShowDialog();

            UpdateList();
        }

        private void ShowCheckList(object sender, RoutedEventArgs e)
        {
            FormShowCheckList checklist = new FormShowCheckList((sender as Button).Tag.ToString());
            checklist.ShowDialog();
        }

        private void UpdateList()
        {
            TestsList = new List<AddedTests>();
            try
            {
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTests", Data.ServiceSel));

                for (var i = 0; i < response.args.Count; i += 3)
                {
                    AddedTests test = new AddedTests();
                    test.ID = response.args[i];
                    test.Name = response.args[i + 1];
                    test.Author = response.args[i + 2];
                    Message message = new Message();
                    message.Add("", "", response.args[i]);
                    string request = JsonConvert.SerializeObject(message);
                    test.Kp = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetKPInfo", Data.ServiceSel, request)).args[0];
                    test.Kp = test.Kp.Equals("error") ? "" : test.Kp;
                    TestsList.Add(test);
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }

            DataContext = this;
            TestsListView.ItemsSource = TestsList;
        }
    }
}
