using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
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
        Message testsRes = new Message();
        List<TestsViewClass> TestsListView;
        List<TestsInfoClass> TestsListInfo;
        List<TestsInfoClass> columns = new List<TestsInfoClass>();
        List<dynamic> myItems = new List<dynamic>();
        string response = "";
        string request = "";
        string stend;
        bool select = false;
        BackgroundWorker bw;
        GridView gridView = new GridView();
        public StatisticTest()
        {
            InitializeComponent();
            date.Content = DateTime.Now.ToString("dd.MM.yyyy");
            string w = server.SendMsg("GetStends", Data.ServiceSel);
            message = JsonConvert.DeserializeObject<Message>(w);
            int flag = 0;
            for (int i = 0; i < message.args.Count; i++)
            {
                StendSelected.Items.Add(message.args[i]);
                if (message.args[i] == Data.StendSel)
                    flag = i;
            }

            StendSelected.SelectedIndex = flag;
            message.Add(StendSelected.SelectedItem.ToString());
            message = new Message();


            /*this.TestsInfo.Items.Clear();
            gridView = new GridView();
            this.TestsInfo.View = gridView;
            stend = StendSelected.SelectedItem.ToString();
            bw = new BackgroundWorker();
            StendSelected.IsEnabled = false;

            bw.DoWork += (obj, ea) =>
            {
                UpdateTestsView();
                UpdateTestsInfo();                              
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) =>
            {
                wait.Opacity = 0;
                TestsView.ItemsSource = TestsListView;
                gridView.Columns.Clear();

                foreach (var column in columns)
                {
                    var binding = new Binding(column.Date);

                    gridView.Columns.Add(new GridViewColumn { Header = column.Date, DisplayMemberBinding = binding });
                }
                foreach (dynamic item in myItems)
                {
                    this.TestsInfo.Items.Add(item);
                }
                StendSelected.IsEnabled = true;

                TestsView.SelectionChanged += TestsView_SelectionChanged;
                TestsInfo.SelectionChanged += TestsInfo_SelectionChanged;
            };         */   
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
            
            TestsListView = new List<TestsViewClass>();

            try
            {
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTestResult", Data.ServiceSel, request));
                testsRes = message;
                if (message.args[0] == "no_result")
                {
                    //MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
                    return;
                }


                Dictionary<string, string> ress = new Dictionary<string, string>();
                Message tmp = new Message();
                tmp.Add(stend);
                Message mess = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetVersion", Data.ServiceSel, JsonConvert.SerializeObject(tmp)));
                for (var i = 0; i < message.args.Count; i += 8)
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
                    if (message.args[i + 7] == "errors")
                        test.Jira = "/DashBoardClient;component/Images/red.png";
                    if (message.args[i + 7] == "issue")
                        test.Jira = "/DashBoardClient;component/Images/yellow.png";
                    if (message.args[i + 7] == "no issue")
                        test.Jira = "/DashBoardClient;component/Images/green.png";

                    TestsListView.Add(test);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                UpdateTestsView();
            }
        }
        private void UpdateTestsInfo()
        {
            myItems = new List<dynamic>();
            List<string> rowName = new List<string>();
            List<string> flag = new List<string>();
            Dictionary<string, Dictionary<string, string>> listNameTest = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, Dictionary<string, string>> listNameTest1 = new Dictionary<string, Dictionary<string, string>>();
            List<string> sort = new List<string>();
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
                for (var i = 0; i < message.args.Count; i += 7)
                {
                    if (!listDate.Contains(message.args[i] + "\n" + message.args[i + 5].Replace(".", ":").Replace("_", "__"))) listDate.Add(message.args[i] + "\n" + message.args[i + 5].Replace(".", ":").Replace("_", "__"));
                    for (var j = 0; j < message.args.Count; j += 7)
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
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetVersions", Data.ServiceSel, request));

                foreach (var dic in listNameTest)
                {
                    Dictionary<string, string> d = new Dictionary<string, string>();
                    for (var i = 0; i < message.args.Count; i++)
                    {
                        if (!dic.Value.ContainsKey(message.args[i]))
                        {
                            d.Add(message.args[i], "-");
                        }
                        else
                        {
                            d.Add(message.args[i], dic.Value[message.args[i]]);
                        }
                    }
                    listNameTest1.Add(dic.Key, d);
                }
            }
            catch
            {
                //MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
            }

            foreach (var dic in listNameTest1)
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

            columns = new List<TestsInfoClass>();
            try { myItemValues = (IDictionary<string, object>)myItems[0]; }
            catch { return; }

            foreach (var pair in myItemValues)
            {
                TestsInfoClass column = new TestsInfoClass();

                column.Date = pair.Key;
                column.Result = pair.Key;

                columns.Add(column);
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
        private void Jira_Click(object sender, RoutedEventArgs e)
        {
            Jira jira = new Jira((sender as Button).Tag.ToString());
            jira.ShowDialog();
            wait.Opacity = 1;
            message = new Message();
            message.Add(StendSelected.SelectedItem.ToString());

            request = JsonConvert.SerializeObject(message);
            message = new Message();
            this.TestsInfo.Items.Clear();
            gridView = new GridView();
            this.TestsInfo.View = gridView;
            stend = StendSelected.SelectedItem.ToString();
            bw = new BackgroundWorker();
            this.IsEnabled = false;
            this.TestsInfo.Visibility = Visibility.Hidden;
            TestsView.Visibility = Visibility.Hidden;
            wait.Opacity = 1;
            StendSelected.IsEnabled = false;
            bw.DoWork += (obj, ea) =>
            {

                UpdateTestsView();
                UpdateTestsInfo();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) =>
            {

                wait.Opacity = 0;
                TestsView.ItemsSource = TestsListView;
                gridView.Columns.Clear();

                foreach (var column in columns)
                {
                    var binding = new Binding(column.Date);

                    gridView.Columns.Add(new GridViewColumn { Header = column.Date, DisplayMemberBinding = binding });
                }
                foreach (dynamic item in myItems)
                {
                    this.TestsInfo.Items.Add(item);
                }
                this.IsEnabled = true;
                this.TestsInfo.Visibility = Visibility.Visible;
                TestsView.Visibility = Visibility.Visible;
            };
        }
        private void SelectStend(object sender, SelectionChangedEventArgs e)
        {
            wait.Opacity = 1;
            message = new Message();
            message.Add(StendSelected.SelectedItem.ToString());

            request = JsonConvert.SerializeObject(message);
            message = new Message();
            this.TestsInfo.Items.Clear();
            gridView = new GridView();
            this.TestsInfo.View = gridView;
            stend = StendSelected.SelectedItem.ToString();
            bw = new BackgroundWorker();
            this.IsEnabled = false;
            this.TestsInfo.Visibility = Visibility.Hidden;
            TestsView.Visibility = Visibility.Hidden;
           
            bw.DoWork += (obj, ea) =>
            {
                string error = server.SendMsg("CheckErrors", Data.ServiceSel);
                UpdateTestsView();
                UpdateTestsInfo();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) =>
            {
                this.IsEnabled = true;
                this.TestsInfo.Visibility = Visibility.Visible;
                TestsView.Visibility = Visibility.Visible;
                wait.Opacity = 0;
                TestsView.ItemsSource = TestsListView;
                gridView.Columns.Clear();

                foreach (var column in columns)
                {
                    var binding = new Binding(column.Date);

                    gridView.Columns.Add(new GridViewColumn { Header = column.Date, DisplayMemberBinding = binding });
                }
                foreach (dynamic item in myItems)
                {
                    this.TestsInfo.Items.Add(item);
                }
            };
        }
        private void ShowResult(object sender, RoutedEventArgs e)
        {
            Message mess = new Message();
            wait.Opacity = 1;
            this.IsEnabled = false;
            this.TestsInfo.Visibility = Visibility.Hidden;
            TestsView.Visibility = Visibility.Hidden;
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) =>
            {

                mess.Add(Data.ServiceSel);
                response = server.SendMsg("GetPathToResult", Data.ServiceSel, JsonConvert.SerializeObject(mess));
                mess = JsonConvert.DeserializeObject<Message>(response); ;
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) =>
            {
                this.IsEnabled = true;
                this.TestsInfo.Visibility = Visibility.Visible;
                TestsView.Visibility = Visibility.Visible;
                wait.Opacity = 0;
                System.Diagnostics.Process.Start("file://pur-test01/ATST/" + mess.args[0].Replace("Z:\\\\", "").Replace("\\\\", "/") + "/" + (sender as Button).Tag.ToString() + "/Res1/Report/run_results.html");
            };

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
    }
}
