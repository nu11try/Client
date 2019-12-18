using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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
        string response = "";
        string request = "";
        bool select = false;
        public StatisticTest()
        {
            InitializeComponent();

            message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetStends", Data.ServiceSel));
            for (int i = 0; i < message.args.Count; i++)
            {
                StendSelected.Items.Add(message.args[i]);
            }

            StendSelected.SelectedIndex = 0;
            message = new Message();
            message.Add(StendSelected.SelectedItem.ToString());
            request = JsonConvert.SerializeObject(message);
            message = new Message();

            //Thread thread = Waiter.ShowWaiter();
            UpdateTestsView();
            UpdateTestsInfo();
            //Waiter.AbortWaiter(thread);

            //TestsInfo.ItemsSource = TestsListInfo;    
            TestsView.SelectionChanged += TestsView_SelectionChanged;
            TestsInfo.SelectionChanged += TestsInfo_SelectionChanged;
        }

        private void TestsInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TestsView.SelectedItem = TestsView.Items[TestsInfo.SelectedIndex];
            }
            catch
            {

            }
        }

        private void TestsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TestsInfo.SelectedItem = TestsInfo.Items[TestsView.SelectedIndex];
            }
            catch
            {

            }
        }

        private void UpdateTestsView()
        {
            server.SendMsg("CheckErrors", Data.ServiceSel);
            TestsListView = new List<TestsViewClass>();
            TestsView.ItemsSource = TestsListView;
            try
            {
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTestResult", Data.ServiceSel, request));
                if (message.args[0] == "no_result")
                {
                    //MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
                    return;
                }


                Dictionary<string, string> ress = new Dictionary<string, string>();
                Message mess = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetVersion", Data.ServiceSel));
                for (var i = 0; i < message.args.Count; i += 7)
                {
                    TestsViewClass test = new TestsViewClass();
                    test.Count = TestsListView.Count + 1;
                    test.Name = message.args[i];
                    if (message.args[i + 4] != mess.args[0])
                    {
                        if (message.args[i + 1] == "Passed") test.ResultTest = "/DashBoardClient;component/Images/ok_no_version.png";
                        if (message.args[i + 1] == "Failed") test.ResultTest = "/DashBoardClient;component/Images/bug_no_version.png";
                        if (message.args[i + 1] == "Warning") test.ResultTest = "/DashBoardClient;component/Images/warning_no_version.png";
                        if (message.args[i + 2] == "DEPENDEN ERROR") test.ResultTest = "/DashBoardClient;component/Images/dependon_no_version.png";
                        if (message.args[i + 2] == "TIMEOUT") test.ResultTest = "/DashBoardClient;component/Images/clock_no_version.png";
                        if (message.args[i + 2] == "no_version") test.ResultTest = "/DashBoardClient;component/Images/server_error_no_version.png";
                    }
                    else
                    {
                        if (message.args[i + 1] == "Passed") test.ResultTest = "/DashBoardClient;component/Images/ok.png";
                        if (message.args[i + 1] == "Failed") test.ResultTest = "/DashBoardClient;component/Images/bug.png";
                        if (message.args[i + 1] == "Warning") test.ResultTest = "/DashBoardClient;component/Images/warning.png";
                        if (message.args[i + 2] == "DEPENDEN ERROR") test.ResultTest = "/DashBoardClient;component/Images/dependon.png";
                        if (message.args[i + 2] == "TIMEOUT") test.ResultTest = "/DashBoardClient;component/Images/time.png";
                        if (message.args[i + 2] == "no_version") test.ResultTest = "/DashBoardClient;component/Images/server_error.png";
                    }

                    test.Author = message.args[i + 5];
                    test.Id = message.args[i + 6];
                    Message args = new Message();
                    args.Add(message.args[i + 6]);
                    Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetErrorsStatus", Data.ServiceSel, JsonConvert.SerializeObject(args)));
                    if (res.args[0] == "errors")
                        test.Jira = "/DashBoardClient;component/Images/red.png";
                    if (res.args[0] == "issue")
                        test.Jira = "/DashBoardClient;component/Images/yellow.png";
                    if (res.args[0] == "no issue")
                        test.Jira = "/DashBoardClient;component/Images/green.png";

                    TestsListView.Add(test);
                }
            }
            catch
            {
                //MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
            DataContext = this;
            TestsView.ItemsSource = TestsListView;

        }
        private void UpdateTestsInfo()
        {
            this.TestsInfo.Items.Clear();
            GridView gridView = new GridView();
            this.TestsInfo.View = gridView;

            List<dynamic> myItems = new List<dynamic>();
            List<string> rowName = new List<string>();
            List<string> flag = new List<string>();
            Dictionary<string, Dictionary<string, string>> listNameTest = new Dictionary<string, Dictionary<string, string>>();
            dynamic myItem;
            IDictionary<string, object> myItemValues;
            TestsListInfo = new List<TestsInfoClass>();
            try
            {
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTestResultInfo", Data.ServiceSel, request));
                if (message.args[0] == "no_result")
                {
                    //MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
                    return;
                }
                List<string> listDate = new List<string>();
                Dictionary<string, string> bufList = new Dictionary<string, string>();
                for (var i = 0; i < message.args.Count; i += 6)
                {
                    if (!listDate.Contains(message.args[i])) listDate.Add(message.args[i] + "\n" + message.args[i + 5].Replace(".", ":").Replace("_", "__"));
                    for (var j = 0; j < message.args.Count; j += 6)
                    {
                        if (message.args[i + 4].Equals(message.args[j + 4]))
                        {
                            try
                            {
                                bufList.Add(message.args[j] + "\n" + message.args[j + 5].Replace(".", ":").Replace("_", "__"), message.args[j + 1].Equals("Failed") && (!message.args[j + 3].Equals("TIMEOUT") && !message.args[j + 3].Equals("no_version") && !message.args[j + 3].Equals("DEPENDEN ERROR")) ? "FAILED" : message.args[j + 3]);
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
                //MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
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
            public string Id { get; set; }
        }

        public class TestsInfoClass
        {
            public string Result { get; set; }
            public string Date { get; set; }
            public string Version { get; set; }
        }

        private void Jira_Click(object sender, RoutedEventArgs e)
        {
            Jira jira = new Jira((sender as Button).Tag.ToString());
            jira.ShowDialog();
            Thread thread = Waiter.ShowWaiter();
            UpdateTestsView();
            UpdateTestsInfo();
            Waiter.AbortWaiter(thread);
        }

        private void SelectStend(object sender, SelectionChangedEventArgs e)
        {
            Thread thread = null;
            select = true;
            if (select)
            {
                thread = Waiter.ShowWaiter();
            }
            message = new Message();
            message.Add(StendSelected.SelectedItem.ToString());

            request = JsonConvert.SerializeObject(message);
            message = new Message();

            UpdateTestsView();
            UpdateTestsInfo();
            try
            {
                Waiter.AbortWaiter(thread);
            }
            catch { }
        }
        private void ShowResult(object sender, RoutedEventArgs e)
        {
            Thread thread = Waiter.ShowWaiter();
            try
            {
                Message mess = new Message();
                mess.Add(Data.ServiceSel);
                response = server.SendMsg("GetPathToResult", Data.ServiceSel, JsonConvert.SerializeObject(mess));
                mess = JsonConvert.DeserializeObject<Message>(response);
                System.Diagnostics.Process.Start("file://pur-test01/ATST/" + mess.args[0].Replace("Z:\\\\", "").Replace("\\\\", "/") + "/" + (sender as Button).Tag.ToString() + "/Res1/Report/run_results.html");
                Waiter.AbortWaiter(thread);
            }
            catch
            {
                Waiter.AbortWaiter(thread);
                MessageBox.Show("Нет результата по тесту!");
            }
        }
    }
}
