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
                response = server.SendMsg("getTestsResult", "ai");
                testsList = response.Split('╡');
                if (testsList[0] == "no_result")
                {
                    MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
                    return;
                }

                for (var i = 0; i < testsList.Length - 1; i++)
                {
                    TestsViewClass test = new TestsViewClass();
                    string[] testForList = testsList[i].Split('±');
                    test.Count += i + 1;
                    TestsListCount++;
                    test.Name = testForList[0];
                    if (testForList[1] == "Passed") test.ResultTest = "/DashBoardClient;component/Images/ok.png";
                    else test.ResultTest = "/DashBoardClient;component/Images/no.png";
                    test.Jira = "";
                    test.Author = testForList[5];

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
            List<List<string>> listNameTest = new List<List<string>>();
            dynamic myItem;
            IDictionary<string, object> myItemValues;
            TestsListInfo = new List<TestsInfoClass>();
            try
            {
                response = server.SendMsg("getTestsResultInfo", "ai");
                testsList = response.Split('╡');
                if (testsList[0] == "no_result")
                {
                    MessageBox.Show("Нет результатов выполнения тестов или произошла ошибка!");
                    return;
                }

                List<string> bufList = new List<string>();
                for (var i = 0; i < testsList.Length - 1; i++)
                {
                    string[] testForList = testsList[i].Split('±');
                    rowName.Add(testForList[0]);
                    result.Add(testForList[3]);
                    // ПОМЕНЯТЬ ИНДЕКС
                    bufList.Add(testForList[3]);
                    if (bufList.Count == TestsListCount)
                    {
                        listNameTest.Add(bufList);
                        bufList = new List<string>();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }

            // Populate the objects with dynamic columns                                  
            int iFirst = 0;
            int iThecond = 0;
            for (var i = 0; i < TestsListCount; i++)
            {
                myItem = new System.Dynamic.ExpandoObject();
                foreach (string column in rowName.Distinct())
                {
                    myItemValues = (IDictionary<string, object>)myItem;
                    try { myItemValues[column] = Math.Ceiling(Double.Parse(listNameTest[iFirst][iThecond].ToString())).ToString(); }
                    catch
                    {
                        myItemValues[column] = listNameTest[iFirst][iThecond].ToString();
                        //myItemValues[column] = "0";
                    }
                    iFirst++;
                }
                iThecond++;
                iFirst = 0;
                myItems.Add(myItem);
            }

            // Assuming that all objects have same columns - using first item to determine the columns
            List<TestsInfoClass> columns = new List<TestsInfoClass>();
            try
            {
                myItemValues = (IDictionary<string, object>)myItems[0];
            }
            catch
            {
                return;
            }

            // Key is the column, value is the value
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
