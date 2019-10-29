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
    /// Логика взаимодействия для StatisticTest.xaml
    /// </summary>
    public partial class StatisticTest : Page
    {
        readonly ServerConnect server = new ServerConnect();
        Message message = new Message();
        List<TestsViewClass> TestsListView;
        List<TestsInfoClass> TestsListInfo;
        int TestsListCount = 0;
        string response = "";
        string[] testsList;
        public StatisticTest()
        {
            InitializeComponent();
            UpdateTestsView();
            UpdateTestsInfo();
            TestsView.ItemsSource = TestsListView;
            //TestsInfo.ItemsSource = TestsListInfo;            
        }

        private void UpdateTestsView()
        {
            TestsListView = new List<TestsViewClass>();
            try
            {
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTestResult", Data.ServiceSel));
                if (message.args[0] == "no_result")
                {
                    MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
                    return;
                }

                for (var i = 0; i < message.args.Count; i += 4)
                {
                    TestsViewClass test = new TestsViewClass();
                    test.Count += 1;
                    test.Name = message.args[i];
                    if (message.args[i + 1] == "Passed") test.ResultTest = "/DashBoardClient;component/Images/ok.png";
                    else test.ResultTest = "/DashBoardClient;component/Images/no.png";
                    test.Jira = "";
                    //test.Author = testForList[5];

                    TestsListView.Add(test);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
            DataContext = this;
        }
        private void UpdateTestsInfo()
        {
            GridView gridView = new GridView();
            this.TestsInfo.View = gridView;

            List<dynamic> myItems = new List<dynamic>();
            List<string> rowName = new List<string>();
            List<string> result = new List<string>();
            List<string> listNameTest = new List<string>();
            dynamic myItem;
            IDictionary<string, object> myItemValues;
            TestsListInfo = new List<TestsInfoClass>();
            try
            {
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTestResultInfo", Data.ServiceSel));
                if (message.args[0] == "no_result")
                {
                    MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
                    return;
                }

                List<string> bufList = new List<string>();
                for (var i = 0; i < message.args.Count; i += 4)
                {
                    rowName.Add(message.args[i]);
                    result.Add(message.args[i + 3]);
                    // ПОМЕНЯТЬ ИНДЕКС                                        
                    listNameTest.Add(message.args[i + 3]);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
            int index = 0;                               
            for (var i = 0; i < listNameTest.Count / rowName.Distinct().Count(); i++)
            {
                myItem = new System.Dynamic.ExpandoObject();
                foreach (string column in rowName.Distinct())
                {
                    myItemValues = (IDictionary<string, object>)myItem;
                    try { myItemValues[column] = Math.Ceiling(Double.Parse(listNameTest[index].ToString())).ToString(); }
                    catch { myItemValues[column] = listNameTest[index].ToString(); }
                    index++;
                }
                myItems.Add(myItem);
            }

            List<TestsInfoClass> columns = new List<TestsInfoClass>();
            try { myItemValues = (IDictionary<string, object>)myItems[0]; }
            catch { return; }

            foreach (var pair in myItemValues)
            {
                TestsInfoClass column = new TestsInfoClass();

                column.Date = pair.Key;
                column.Result = pair.Key;

                columns.Add(column);
            }

            // Add the column definitions to the list view
            gridView.Columns.Clear();

            foreach (var column in columns)
            {
                var binding = new Binding(column.Date);

                gridView.Columns.Add(new GridViewColumn { Header = column.Date, DisplayMemberBinding = binding });
            }

            // Add all items to the list
            foreach (dynamic item in myItems)
            {
                this.TestsInfo.Items.Add(item);
            }

            }
        private void TestsView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(TestsInfo, 0);
                ScrollViewer scrollViewer = VisualTreeHelper.GetChild(childVisual, 0) as ScrollViewer;
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
                }
            }
        }

        private void TestsInfo_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(TestsView, 0);
                ScrollViewer scrollViewer = VisualTreeHelper.GetChild(childVisual, 0) as ScrollViewer;
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
                }
            }
        }

        private class TestsViewClass
        {
            public int Count { get; set; }
            public string Name { get; set; }
            public string ResultTest { get; set; }
            public string Jira { get; set; }
            public string Author { get; set; }
        }

        public class TestsInfoClass
        {
            public string Result { get; set; }
            public string Date { get; set; }
            public string Version { get; set; }
        }
    }
}
