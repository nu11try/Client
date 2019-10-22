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
            TestName.Clear();
            TestID.Clear();
            MethodSelect.Items.Clear();
            ActiveSelect.IsChecked = false;
            AuthorSelect.Items.Clear();

            message.Add(IDTest);
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("UpdateTestChange", Data.ServiceSel, request);
            resMes = JsonConvert.DeserializeObject<Message>(response);
            if (resMes.args[0].Equals("error")) MessageBox.Show("Ошибка! Обратитесь к поддержке");                      
            else
            {
                response = server.SendMsg("GetAuthor", Data.ServiceSel);
                resMes2 = JsonConvert.DeserializeObject<Message>(response);                
                
                for (int i = 0; i < resMes2.args.Count; i++) AuthorSelect.Items.Add(resMes2.args[i]);

                try { AuthorSelect.Text = AuthorSelect.Items[0].ToString(); }
                catch { }
                               
                for (int i = 0; i < resMes.args.Count; i += 3)
                {
                    TestID.Text = IDTest;
                    TestName.Text = resMes.args[i];
                    AuthorSelect.SelectedIndex = AuthorSelect.Items.IndexOf(resMes.args[i + 1].ToString());
                    ActiveSelect.IsChecked = Convert.ToBoolean(resMes.args[i + 2].ToString());
                }
            }
            message = new Message();
            resMes = new Message();
            resMes = new Message();
        }
        private void SendTest(object sender, RoutedEventArgs e)
        {           
            try
            {
                message.Add(IDTest, AuthorSelect.SelectedItem.ToString(), ActiveSelect.IsChecked.Value.ToString());
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
