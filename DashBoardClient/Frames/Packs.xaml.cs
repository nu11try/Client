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
    /// Логика взаимодействия для Packs.xaml
    /// </summary>
    public partial class Packs : Page
    {
        readonly ServerConnect server = new ServerConnect();
        public List<PacksWithTest> PackList { get; set; }
        Message message;
        string response;
        string[] packsList;
        public Packs()
        {
            InitializeComponent();
            UpdateList();           
        }

        private void UpdateList()
        {
            PackList = new List<PacksWithTest>();

            try
            {
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ProjectName));                                
                if (message.args[0].Equals("no_packs"))
                {
                    MessageBox.Show("Нет добавленных наборов");
                    return;
                }                

                for (var i = 0; i < message.args.Count; i += 7)
                {

                    PacksWithTest pack = new PacksWithTest();
                    pack.ID = message.args[i];
                    pack.Name = message.args[i+1];
                    pack.NewName = message.args[i+2];
                    pack.Count = "0";
                    pack.Result = message.args[i+5];
                    pack.Time = message.args[i+3];
                    pack.RestartCount = message.args[i+4];

                    PackList.Add(pack);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
            DataContext = this;

            DataContext = this;
            PackListView.ItemsSource = PackList;
        }

        private void AddPack(object sender, RoutedEventArgs e)
        {
            PackFormAdd addPack = new PackFormAdd();
            try { addPack.ShowDialog(); }
            catch { MessageBox.Show(""); }            
            UpdateList();
        }
        private void ChangePack(object sender, RoutedEventArgs e)
        {
            PackFormChange packChange = new PackFormChange((sender as Button).Tag.ToString());
            packChange.ShowDialog();
            UpdateList();
        }
        private void ChangeTests(object sender, RoutedEventArgs e)
        {
            ViewTestsPack viewTests = new ViewTestsPack((sender as Button).Tag.ToString());
            try { viewTests.ShowDialog(); }
            catch { MessageBox.Show("s"); }
            
            UpdateList();
        }
    }
}
