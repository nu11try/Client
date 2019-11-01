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
    public partial class TestFormChange : Window
    {
        ServerConnect server = new ServerConnect();
        Message message = new Message();
        Message resMes = new Message();
        Message resMes2 = new Message();
        string request = "";
        string response = "";

        string IDTest = "";
        public TestFormChange(string TAG)
        {
            InitializeComponent();
            // получаем ID теста через тег кнопки
            IDTest = TAG;
            GetTestsForListView();
        }

        private void GetTestsForListView()
        {
            TestID.Clear();
            MethodSelect.Items.Clear();
            ActiveSelect.IsChecked = false;

            message = new Message();
            message.Add(IDTest);
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("UpdateTestChange", Data.ServiceSel, request);
            resMes = JsonConvert.DeserializeObject<Message>(response);



            message = new Message();
            message.Add("", "","--");
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("GetKPInfo", Data.ServiceSel, request);
            Message resMes2 = JsonConvert.DeserializeObject<Message>(response);

            message = new Message();
            message.Add("", "", IDTest);
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("GetKPInfo", Data.ServiceSel, request);
            Message resMes3 = JsonConvert.DeserializeObject<Message>(response);

            if (resMes.args[0].Equals("error")) MessageBox.Show("Ошибка! Обратитесь к поддержке");                      
            else
            {


                for (int i = 0; i < resMes2.args.Count; i += 4) MethodSelect.Items.Add(resMes2.args[i] + " (" + resMes2.args[i + 3] + ")");
                if (!resMes3.args[0].Equals("error"))
                {
                    MethodSelect.Items.Add(resMes3.args[0] + " (" + resMes3.args[3] + ")");
                    MethodSelect.Text = resMes3.args[0] + " (" + resMes3.args[3] + ")".ToString();
                }
                TestID.Text = IDTest;      
                ActiveSelect.IsChecked = Convert.ToBoolean(resMes.args[2].ToString());
            }
            message = new Message();
        }
        private void SendTest(object sender, RoutedEventArgs e)
        {           
            try
            {
                string idDoc = MethodSelect.SelectedItem.ToString().Split('(')[0];
                idDoc = idDoc.Substring(0, idDoc.Length - 1);
                message.Add(IDTest, Data.NameUser, ActiveSelect.IsChecked.Value.ToString(), idDoc);
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("UpdateTest", Data.ServiceSel, request);
                if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK")) 
                    MessageBox.Show("Поздравляем! Информация по тесту " + IDTest + " обновлена!");                
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
