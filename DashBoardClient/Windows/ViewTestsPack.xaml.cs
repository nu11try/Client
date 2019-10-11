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
        string response = "";
        string[] testsList;
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

            try
            {
                response = server.SendMsg("getTestsThisPack", "ai", IDPack);
                testsList = response.Split('╡');
                for (var i = 0; i < testsList.Length - 1; i++)
                {
                    TestsList test = new TestsList();
                    string[] testForList = testsList[i].Split('±');
                    test.ID = testForList[0];
                    test.NewName = testForList[1];
                    if (testForList[2] == "default") test.Time = "По умолчанию";
                    else test.Time = testForList[1];
                    if (testForList[3] == "default") test.Restart = "По умолчанию";
                    else test.Restart = testForList[3];                    

                    list.Add(test);
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }

            DataContext = this;
            TestsList.ItemsSource = list;
        }
        private void ChangeTest(object sender, RoutedEventArgs e)
        {
            PackTestsFormChange packTests = new PackTestsFormChange((sender as Button).Tag.ToString());
            packTests.ShowDialog();

            UpdateList();
        }
    }
}
