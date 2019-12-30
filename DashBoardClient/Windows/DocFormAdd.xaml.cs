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
    public partial class DocFormAdd : Window
    {
        ServerConnect server = new ServerConnect();
        string action = "add";
        string ID = "";

        Message message = new Message();
        Message resMes = new Message();
        string request = "";
        string response = "";
        public DocFormAdd()
        {
            InitializeComponent();
            DateBlock.SelectedDate = DateTime.Today;
        }

        public DocFormAdd(string id)
        {
            InitializeComponent();            
            GetDocInfo(id);
            action = "update";
            ID = id;
            DateBlock.SelectedDate = DateTime.Today;
        }

        private void GetDocInfo(string id)
        {
            // 0 - pim         
            // 1 - date
            message.Add(id);
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("GetDocInfo", Data.ServiceSel, request);

            resMes = JsonConvert.DeserializeObject<Message>(response);
            if (resMes.args[0].Equals("error")) MessageBox.Show("Ошибка! Обратитесь к поддержке");
            else
            {                               
                PimLink.Text = resMes.args[0];
                DateBlock.Text = resMes.args[1];
            }
            message = new Message();
            DateBlock.SelectedDate = DateTime.Today;
        }

        private void SendDoc(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PimLink.Text == "" || DateBlock.Text == "")
                {
                    MessageBox.Show("Не все данные выбраны!");
                    return;
                }
                else
                {                    
                    if (action == "add")
                    {
                        message.Add(PimLink.Text, DateBlock.Text);
                        request = JsonConvert.SerializeObject(message);
                        response = server.SendMsg("AddDoc", Data.ServiceSel, request);
                        if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK"))                        
                        {
                            MessageBox.Show("Поздравляем! Документ добавлен!");
                            PimLink.Text = "";
                            DateBlock.Text = "";
                        }
                        if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("ISSET")) MessageBox.Show("Добавление невозможно! Такой документ уже существует!", "Ошибка");
                    }
                    else if (action == "update")
                    {
                        message.Add(PimLink.Text, DateBlock.Text, ID);
                        request = JsonConvert.SerializeObject(message);
                        response = server.SendMsg("UpdateDoc", Data.ServiceSel, request);
                        if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK")) MessageBox.Show("Поздравляем! Документ обновлен!");
                        if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("ISSET")) MessageBox.Show("Обновление невозможно! Такой документ уже существует!", "Ошибка");                        
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не все данные выбраны!");
            }

            message = new Message();
            request = "";
            DateBlock.SelectedDate = DateTime.Today;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
