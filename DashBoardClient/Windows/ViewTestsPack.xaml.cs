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
        public string ResultExec { get; set; }
        public string TimeExec { get; set; }
    }

    public partial class ViewTestsPack : Window
    {

        private Point startPoint = new Point();
        private ObservableCollection<TestsList> Items = new ObservableCollection<TestsList>();
        private int startIndex = -1;
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
            TestList.Items.Clear();
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

                    Items.Add(test);
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }
            message = new Message();
            DataContext = this;
            TestList.ItemsSource = Items;
        }
        private void ChangeTest(object sender, RoutedEventArgs e)
        {
            message.Add((sender as Button).Tag.ToString(), IDPack);
            request = JsonConvert.SerializeObject(message);
            PackTestsFormChange packTests = new PackTestsFormChange(request);
            packTests.ShowDialog();

            UpdateList();
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
                startIndex = -1;        // Done!
            }
        }
    }
}
