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
using System.Collections.ObjectModel;

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
        public string Dependon { get; set; }
        public string Image { get; set; }
        public string Func { get; set; }
        public string Browser { get; set; }
    }

    public partial class ViewTestsPack : Window
    {


        int flag = 0;
        private Point startPoint = new Point();
        private ObservableCollection<TestsList> Items = new ObservableCollection<TestsList>();
        private int startIndex = -1;

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

            Items = new ObservableCollection<TestsList>();
            message = new Message();
            try
            {
                message.Add(IDPack);
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("GetTestsThisPack", Data.ServiceSel, request);
                message = JsonConvert.DeserializeObject<Message>(response);
                Message dep;
                TestsList test;
                for (var i = 0; i < message.args.Count; i += 8)
                {
                    test = new TestsList();
                    test.ID = message.args[i];
                    test.NewName = message.args[i + 1];
                    if (message.args[i + 2] == "default") test.Time = "По умолчанию";
                    else test.Time = message.args[i + 2];
                    if (message.args[i + 3] == "default") test.Restart = "По умолчанию";
                    else test.Restart = message.args[i + 3];
                    if (message.args[i + 4] == "default") test.Browser = "По умолчанию";
                    else test.Browser = message.args[i + 4];
                    dep = JsonConvert.DeserializeObject<Message>(message.args[i + 5]);
                    if (dep.args[0].Equals("not"))
                    {
                        test.Dependon = "Нет зависимостей";
                        test.Image = "/DashBoardClient;component/Images/open.png";
                        test.Func = "add";
                    }
                    else
                    {
                        dep.args.ForEach(elem => test.Dependon = test.Dependon + elem + "\n");
                        test.Dependon = test.Dependon.Trim();
                        test.Image = "/DashBoardClient;component/Images/sver.png";
                        test.Func = "remove";
                    }
                    Items.Add(test);
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }

            TestList.ItemsSource = Items;
        }

        private void ChangeTest(object sender, RoutedEventArgs e)
        {
            message = new Message();
            message.Add((sender as Button).Tag.ToString(), IDPack);
            request = JsonConvert.SerializeObject(message);
            PackTestsFormChange packTests = new PackTestsFormChange(request);
            packTests.ShowDialog();

            UpdateList();
        }
        private void Depen(object sender, RoutedEventArgs e)
        {
            string id = (sender as Button).Tag.ToString();
            ObservableCollection<TestsList> Items1 = new ObservableCollection<TestsList>();
            int j = 0;
            int f = 0;

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ID == id)
                {
                    j = i;
                }
                if (Items[i].Func == "ok")
                {
                    f = i;
                }
                Items1.Add(Items[i]);
            }
            if (Items1[j].Func.Equals("ok"))
            {
                if (f != j) return;
                flag = 0;
                Message testIds = new Message();
                var l = TestList.SelectedItems;
                for (int i = 0; i < l.Count; i++)
                {
                    if (((TestsList)l[i]).ID != Items1[j].ID)
                        testIds.Add(((TestsList)l[i]).ID);
                }
                if (testIds.args.Count != 0)
                {
                    string testIdsS = JsonConvert.SerializeObject(testIds);
                    message = new Message();
                    message.Add(IDPack, Items1[j].ID, "last", testIdsS, "last", "last", "last");
                    request = JsonConvert.SerializeObject(message);
                    response = server.SendMsg("UpdateTestOfPack", Data.ServiceSel, request);
                    UpdateList();
                }
                else
                {
                    UpdateList();
                    return;
                }
            }
            if (Items1[j].Func.Equals("add"))
            {
                if (f != 0) return;
                flag = 1;
                Items1[j].Image = "/DashBoardClient;component/Images/ok.png";
                Items1[j].Func = "ok";
                for (int i = j + 1; i < Items1.Count;)
                {
                    Items1.RemoveAt(i);

                }
                Items = Items1;
                TestList.ItemsSource = Items;
            }

            if (Items1[j].Func.Equals("remove"))
            {
                message = new Message();
                message.Add(IDPack, Items1[j].ID, "last", "{\"args\":[\"not\"]}", "last", "last", "last");
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("UpdateTestOfPack", Data.ServiceSel, request);
                UpdateList();
            }


        }


        private void TestList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get current mouse position
            startPoint = e.GetPosition(null);
        }

        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void TestList_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == 1) return;
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                       Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem == null) return;           // Abort
                                                            // Find the data behind the ListViewItem
                TestsList item = (TestsList)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                if (item == null) return;                   // Abort
                                                            // Initialize the drag & drop operation
                startIndex = TestList.SelectedIndex;
                DataObject dragData = new DataObject("tests", item);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void TestList_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("tests") || sender != e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void TestList_Drop(object sender, DragEventArgs e)
        {
            int index = -1;

            if (e.Data.GetDataPresent("tests") && sender == e.Source)
            {
                // Get the drop ListViewItem destination
                ListView listView = sender as ListView;
                ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem == null)
                {
                    // Abort
                    e.Effects = DragDropEffects.None;
                    return;
                }
                // Find the data behind the ListViewItem
                TestsList item = (TestsList)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                // Move item into observable collection 
                // (this will be automatically reflected to TestList.ItemsSource)
                e.Effects = DragDropEffects.Move;
                index = Items.IndexOf(item);
                if (startIndex >= 0 && index >= 0)
                {
                    Items.Move(startIndex, index);
                }
                startIndex = -1;
                Message message = new Message();
                Message ids = new Message();
                for (int i = 0; i < Items.Count; i++)
                {
                    ids.Add(Items.ElementAt<TestsList>(i).ID);
                }
                message.Add(IDPack, JsonConvert.SerializeObject(ids));
                String request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("ChangePositionList", Data.ServiceSel, request);
            }
        }
    }
}
