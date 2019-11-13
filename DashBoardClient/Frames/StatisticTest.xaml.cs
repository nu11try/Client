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
        public StatisticTest()
        {
            InitializeComponent();
            UpdateTestsView();
            UpdateTestsInfo();
            TestsView.ItemsSource = TestsListView;
            //TestsInfo.ItemsSource = TestsListInfo;    
            TestsView.SelectionChanged += TestsView_SelectionChanged;
            TestsInfo.SelectionChanged += TestsInfo_SelectionChanged;
        }

        private void TestsInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestsView.SelectedItem = TestsView.Items[TestsInfo.SelectedIndex];
        }

        private void TestsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestsInfo.SelectedItem = TestsInfo.Items[TestsView.SelectedIndex];
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

                List<string> ids = new List<string>();
                for (var i = 0; i < message.args.Count; i += 4)
                {
                    if (!ids.Contains(message.args[i]))
                    {
                        TestsViewClass test = new TestsViewClass();
                        test.Count = TestsListView.Count + 1;
                        test.Name = message.args[i];
                        if (message.args[i + 1] == "Passed") test.ResultTest = "/DashBoardClient;component/Images/ok.png";
                        if (message.args[i + 1] == "Failed") test.ResultTest = "/DashBoardClient;component/Images/no.png";
                        if (message.args[i + 1] == "Warning") test.ResultTest = "/DashBoardClient;component/Images/warning.png";
                        if (message.args[i + 2] == "DEPENDEN ERROR") test.ResultTest = "/DashBoardClient;component/Images/link_break.png";
                        if (message.args[i + 2] == "TIMEOUT") test.ResultTest = "/DashBoardClient;component/Images/time.png";
                        test.Jira = "";
                        //test.Author = testForList[5];
                        ids.Add(test.Name);
                        TestsListView.Add(test);
                    }
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
            List<string> flag = new List<string>();
            Dictionary<string, Dictionary<string,string>> listNameTest = new Dictionary<string, Dictionary<string, string> > ();
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
                List<string> listDate = new List<string>();
                Dictionary<string, string> bufList = new Dictionary<string, string>();
                for (var i = 0; i < message.args.Count; i += 5)
                {
                    if (!listDate.Contains(message.args[i])) listDate.Add(message.args[i]);
                        for (var j = 0; j < message.args.Count; j += 5)
                    {
                        if (message.args[i + 4].Equals(message.args[j + 4]))
                        {
                            
                                // ПОМЕНЯТЬ ИНДЕКС
                                try
                                {
                                    bufList.Add(message.args[j], message.args[j + 1].Equals("Failed") && (!message.args[j + 3].Equals("TIMEOUT") && !message.args[j + 3].Equals("DEPENDEN ERROR")) ? "FAILED" : message.args[j + 3]);
                                }
                                catch { }
                        }
                    }
                    try
                    {
                        if (!flag.Contains(message.args[i + 4])) listNameTest.Add(message.args[i + 4], bufList);
                        flag.Add(message.args[i + 4]);
                    }
                    catch { }
                    bufList = new Dictionary<string, string>();
                }

                foreach (var dic in listNameTest)
                {
                    for (var i = 0; i < listDate.Count; i++)
                    {
                        Dictionary<string, string> di = dic.Value;
                        if (!dic.Value.ContainsKey(listDate[i]))
                        {
                            dic.Value.Add(listDate[i], "-");
                        }
                    }
                }

            }
            catch
            {
                MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
            }
            foreach (var dic in listNameTest)
            {
                myItem = new System.Dynamic.ExpandoObject();
                myItemValues = (IDictionary<string, object>)myItem;
                foreach (var column in dic.Value)
                {
                    try { myItemValues[column.Key] = Math.Ceiling(Double.Parse(column.Value.ToString())).ToString(); }
                    catch { myItemValues[column.Key] = column.Value; }
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
