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
    /// Логика взаимодействия для StartTests.xaml
    /// </summary>
    public partial class StartTests : Page
    {
        public List<PacksWithTest> PackList { get; set; }
        OpenTestList testList;

        Message message = new Message();

        readonly ServerConnect server = new ServerConnect();       
        string response = "";
        string request = "";

        public StartTests()
        {
            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            PackListView.ItemsSource = "";

            PackList = new List<PacksWithTest>();
            try
            {
                message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ServiceSel));                
                if (message.args[0] == "no_packs")
                {
                    MessageBox.Show("Нет добавленных наборов");
                    return;
                }
                for (var i = 0; i < message.args.Count; i += 7)
                {
                    PacksWithTest pack = new PacksWithTest();
                    pack.ID = message.args[i];
                    pack.Name = message.args[i + 1];
                    pack.Count = JsonConvert.DeserializeObject<TestsStartClass>(message.args[i + 2]).id.Count.ToString();
                    pack.RestartCount = message.args[i + 4];
                    pack.Time = message.args[i + 3];
                    pack.IP = message.args[i + 5];
                    if (message.args[i + 6] == "no_start") pack.Status = "Не запущено";
                    else pack.Status = "Запущено";
                     
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
            message = new Message();
        }

        private void OpenTestList(object sender, RoutedEventArgs e)
        {            
            testList = new OpenTestList((string)((Button)e.OriginalSource).Tag);            
            testList.ShowDialog();
        }

        private void StartPacks(object sender, RoutedEventArgs e)
        {
            string packs = "";
            foreach (PacksWithTest listItem in PackListView.SelectedItems) packs += listItem.ID + "±";
            if (packs.Length != 0) packs = packs.Substring(0, packs.Length - 1);
            if (packs != "")
            {
                message.Add(packs);
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("StartTests", Data.ServiceSel, request);
            }
            else MessageBox.Show("Не выбрано ни одного набора!");
            if (JsonConvert.DeserializeObject<Message>(response).args[0] == "OK") MessageBox.Show("Набор(ы) отправлен(ы) на запуск!");
            if (JsonConvert.DeserializeObject<Message>(response).args[0] == "ERROR") MessageBox.Show("Произошла ошибка запуска!");
            if (JsonConvert.DeserializeObject<Message>(response).args[0] == "START") MessageBox.Show("Один из выбранных наборов находится в режиме запуска!");            
            UpdateList();
        }
    }
}
