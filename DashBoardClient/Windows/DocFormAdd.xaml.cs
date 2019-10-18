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
        }

        public DocFormAdd(string id)
        {
            InitializeComponent();            
            GetDocInfo(id);
            action = "update";
            ID = id;
        }

        private void GetDocInfo(string id)
        {
            // 0 - pim         
            // 1 - date
            message.Add(id);
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("GetDocInfo", "ai", request);
            resMes = JsonConvert.DeserializeObject<Message>(response);
            if (resMes.args[0].Equals("error")) MessageBox.Show("Ошибка! Обратитесь к поддержке");
            else
            {                               
                PimLink.Text = resMes.args[0];
                DateBlock.Text = resMes.args[1];
            }
            message = new Message();
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
                        response = server.SendMsg("AddDoc", "ai", request);
                        if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK"))                        
                        {
                            MessageBox.Show("Поздравляем! Документ добавлен!");
                            PimLink.Text = "";
                            DateBlock.Text = "";
                        }
                    }
                    else if (action == "update")
                    {
                        message.Add(PimLink.Text, DateBlock.Text, ID);
                        request = JsonConvert.SerializeObject(message);
                        response = server.SendMsg("UpdateDoc", "ai", request);
                        if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK")) MessageBox.Show("Поздравляем! Документ обновлен!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не все данные выбраны!");
            }

            message = new Message();
            request = "";
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
