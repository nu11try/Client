using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для Doc.xaml
    /// </summary>
    public partial class AddTests : Page
    {
        private Point startPoint = new Point();
        private ObservableCollection<DocClass> Items = new ObservableCollection<DocClass>();
        private int startIndex = -1;
        readonly ServerConnect server = new ServerConnect();
        public List<DocClass> DocList { get; set; }
        Message response;
        BackgroundWorker bw;
        public AddTests()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            DocListView.Visibility = Visibility.Hidden;
            this.IsEnabled = false;
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                this.IsEnabled = true;
                DocListView.Visibility = Visibility.Visible;
                DocListView.ItemsSource = Items;
            };


        }

        private void AddTests_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            MessageBox.Show("wd");
        }

        private void UpdateList()
        {
            Items = new ObservableCollection<DocClass>();
            try
            {
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetDocument", Data.ServiceSel));
                if (response.args[0] == "no_doc") return;

                for (var i = 0; i < response.args.Count; i += 3)
                {
                    DocClass doc = new DocClass();
                    doc.ID = response.args[i];


                    if (response.args[i + 1].Contains(".doc"))
                    {
                        doc.Pim = response.args[i + 1].Split('/').Last().Replace(".doc", "");
                    }
                    if (response.args[i + 1].Contains(".docx"))
                    {
                        doc.Pim = response.args[i + 1].Split('/').Last().Replace(".docx", "");
                    }
                    doc.Date = response.args[i + 2];

                    Items.Add(doc);
                }
                DocClass doc1 = new DocClass();
                doc1.ID = "Технические";
                doc1.Pim = "Технические";
                Items.Add(doc1);
            }

            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
        }

        public class DocClass
        {
            public string ID { get; set; }
            public string Pim { get; set; }
            public string Date { get; set; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            server.SendMsg("GetTestsPath", Data.ServiceSel);
            TestFormAdd formAdd = new TestFormAdd();
            formAdd.ShowDialog();

            bw = new BackgroundWorker();
            wait.Opacity = 1;
            this.IsEnabled = false;
            DocListView.Visibility = Visibility.Hidden;
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                this.IsEnabled = true;
                DocListView.Visibility = Visibility.Visible;
                DocListView.ItemsSource = Items;
            };
        }

        private void OpenKPDoc(object sender, RoutedEventArgs e)
        {
            TestsShow testsShow = new TestsShow((sender as Button).Tag.ToString());

            testsShow.ShowDialog();
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
                DocClass item = (DocClass)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                if (item == null) return;                   // Abort
                                                            // Initialize the drag & drop operation
                startIndex = DocListView.SelectedIndex;
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
                DocClass item = (DocClass)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                // Move item into observable collection 
                // (this will be automatically reflected to TestList.ItemsSource)
                e.Effects = DragDropEffects.Move;
                index = Items.IndexOf(item);
                if (startIndex >= 0 && index >= 0)
                {
                    Items.Move(startIndex, index);
                }

                Message ids = new Message();

                for (int i = 0; i < Items.Count; i++)
                {
                    ids.Add(Items.ElementAt<DocClass>(i).ID);
                }
                String request = JsonConvert.SerializeObject(ids);
                server.SendMsg("ChangePositionDoc", Data.ServiceSel, request);
                startIndex = -1;
            }
        }
    }
}
