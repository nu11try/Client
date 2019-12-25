using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Message response = new Message();

        int flag = 0;
        private Point startPoint = new Point();
        private ObservableCollection<AddedTests> Items = new ObservableCollection<AddedTests>();
        private int startIndex = -1;

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
            Items = new ObservableCollection<AddedTests>();
            try
            {
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTests", Data.ServiceSel));

                for (var i = 0; i < response.args.Count; i += 5)
                {
                    AddedTests test = new AddedTests();
                    test.ID = response.args[i];
                    test.Name = response.args[i + 1];
                    test.Author = response.args[i + 2];
                    Message message = new Message();
                    message.Add("", "", response.args[i]);
                    string request = JsonConvert.SerializeObject(message);
                    test.Kp = response.args[i + 4];
                    test.Sort = response.args[i + 3];
                    Items.Add(test);
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }

            DataContext = this;
            List.ItemsSource = Items;
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
                AddedTests item = (AddedTests)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                if (item == null) return;                   // Abort
                                                            // Initialize the drag & drop operation
                startIndex = List.SelectedIndex;
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
                AddedTests item = (AddedTests)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                // Move item into observable collection 
                // (this will be automatically reflected to TestList.ItemsSource)
                e.Effects = DragDropEffects.Move;
                index = Items.IndexOf(item);
                Message ids = new Message();
                ids.Add(Items.ElementAt<AddedTests>(startIndex).ID, Items.ElementAt<AddedTests>(index).Sort);
                if (startIndex >= 0 && index >= 0)
                {
                    Items.Move(startIndex, index);
                }

                Message message = new Message();
                for (int i = 0; i < Items.Count; i++)
                {
                    ids.Add(Items[i].ID, Items[i].Sort);
                }

                message.Add(JsonConvert.SerializeObject(ids));
                String request = JsonConvert.SerializeObject(message);
                server.SendMsg("ChangePositionTests", Data.ServiceSel, request);
                startIndex = -1;
            }
        }

        private void DeleteTest_Click(object sender, RoutedEventArgs e)
        {
            Message message = new Message();
            message.Add((sender as Button).Tag.ToString());
            server.SendMsg("DeleteTest", Data.ServiceSel, JsonConvert.SerializeObject(message));
            Thread thread = Waiter.ShowWaiter();
            UpdateList();
            Waiter.AbortWaiter(thread);
        }      
    }
}
