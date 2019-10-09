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
        string response = "";
        readonly ServerConnect server = new ServerConnect();
        string[] docList;
        string id = "";

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
                response = server.SendMsg("getKPForDoc", "ai", ID);
                docList = response.Split('╡');

                if (docList[0] != "no_kp")
                {
                    for (var i = 0; i < docList.Length - 1; i++)
                    {
                        KP kp = new KP();
                        string[] docForList = docList[i].Split('±');
                        kp.ID = docForList[0];
                        kp.Name = docForList[1];
                        kp.Assc = docForList[2];
                        kp.Author = docForList[3];
                        kp.Date = docForList[4];
                        kp.Test = docForList[5];

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
