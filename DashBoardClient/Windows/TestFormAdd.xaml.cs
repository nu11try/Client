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
    /// <summary>
    /// Логика взаимодействия для TestFormAdd.xaml
    /// </summary>
    public partial class TestFormAdd : Window
    {
        ServerConnect server = new ServerConnect();

        Message message = new Message();
        Message resMes = new Message();
        Message resMes2 = new Message();
        string request = "";
        string response = "";

        string[] tests = new string[] { };
        public TestFormAdd()
        {
            InitializeComponent();
            GetTestsForListView();
        }

        private void GetTestsForListView()
        {
            TestSelect.Items.Clear();

            message.Add("no_add");
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("GetTests", Data.ServiceSel, request);
            resMes = JsonConvert.DeserializeObject<Message>(response);
            if (resMes.args.Count == 0)
            {
                MessageBox.Show("Нет тестов на добавление!");
                return;
            }
            else
            {
                response = server.SendMsg("GetAuthor", Data.ServiceSel);
                resMes2 = JsonConvert.DeserializeObject<Message>(response);


                for (int i = 0; i < resMes.args.Count; i += 3) TestSelect.Items.Add(resMes.args[i] + " (" + resMes.args[i + 1] + ")");

                try
                {
                    TestSelect.Text = TestSelect.Items[0].ToString();
                }
                catch
                {
                    MessageBox.Show("Нет тестов на добавление!");
                }
            }
            message = new Message();
        }
        private void SendTest(object sender, RoutedEventArgs e)
        {            
            try
            {
                string IDtest = TestSelect.SelectedItem.ToString().Split('(')[0];
                IDtest = IDtest.Substring(0, IDtest.Length-1);
                message.Add(IDtest, Data.NameUser, ActiveSelect.IsChecked.Value.ToString());
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("AddTest", Data.ServiceSel, request);
                if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK"))
                    MessageBox.Show("Поздравляем! Тест " + IDtest + " добавлен!");
                else MessageBox.Show("Ошибка! Попробуйте позже или обратитесь в поддержку");
                GetTestsForListView();
            }
            catch
            {
                MessageBox.Show("Не все данные выбраны!");
            }
            message = new Message();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
