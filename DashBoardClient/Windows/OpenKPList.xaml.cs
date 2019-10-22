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

namespace DashBoardClient
{
    public class KP
    {
        // ID-шник теста
        public string ID { get; set; }
        // Первончальное имя теста
        public string Name { get; set; }
        // Ответственный за выполнение
        public string Author { get; set; }
        // Время выполнения
        public string Date { get; set; }
        // Результат
        public string Assc { get; set; }
        public string Test { get; set; }
    }

    public partial class OpenKPList : Window
    {
        List<KP> KPList;
        readonly ServerConnect server = new ServerConnect();
        string id = "";

        Message message = new Message();
        Message resMes = new Message();
        string request = "";
        string response = "";
        public OpenKPList(string ID)
        {            
            InitializeComponent();
            id = ID;
            Update(id);
        }

        private void Update(string ID)
        {
            KPList = new List<KP>();
            try
            {
                message.Add(ID);
                request = JsonConvert.SerializeObject(message);
<<<<<<< HEAD
                response = server.SendMsg("GetKPForDoc", Data.ServiceSel, request);
=======
                response = server.SendMsg("GetKPForDoc", Data.ProjectName, request);
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
                resMes = JsonConvert.DeserializeObject<Message>(response);
                if (!resMes.args[0].Equals("no_kp"))                
                {
                    for (var i = 0; i < resMes.args.Count; i += 6)
                    {
                        KP kp = new KP();
                        kp.ID = resMes.args[i];
                        kp.Name = resMes.args[i + 1];
                        kp.Assc = resMes.args[i + 2];
                        kp.Author = resMes.args[i + 3];
                        kp.Date = resMes.args[i + 4];
                        kp.Test = resMes.args[i + 5];

                        KPList.Add(kp);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
                return;
            }

            DataContext = this;
            KPListView.ItemsSource = KPList;
            message = new Message();    
        }

        private void AddKP(object sender, RoutedEventArgs e)
        {
            //KPFormAdd kPFormAdd = new KPFormAdd((sender as Button).Tag.ToString());
            KPFormAdd kPFormAdd = new KPFormAdd(id, "", "add");
            kPFormAdd.ShowDialog();
            Update(id);
        }
        private void UpdateKP(object sender, RoutedEventArgs e)
        {
            //KPFormAdd kPFormAdd = new KPFormAdd((sender as Button).Tag.ToString());            
            KPFormAdd kPFormAdd = new KPFormAdd(id, (sender as Button).Tag.ToString(), "update");
            kPFormAdd.ShowDialog();
            Update(id);
        }
    }
}
