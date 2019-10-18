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
    public partial class KPFormAdd : Window
    {
        ServerConnect server = new ServerConnect();
        string action = "add";
        string ID = "";
        string IDKP = "";

        Message message = new Message();
        Message resMes = new Message();
        string request = "";
        string response = "";
        public KPFormAdd(string id, string idKP, string actionType)
        {
            InitializeComponent();
            GetKPInfo(id, idKP);
            if (actionType == "update") action = "update";
            ID = id;
            IDKP = idKP;
        }

        private void GetKPInfo(string id, string idKP)
        {
            // 0 - name         
            // 1 - date
            message.Add(id, idKP);
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("GetKPInfo", "ai", request);
            resMes = JsonConvert.DeserializeObject<Message>(response);
            if (resMes.args[0].Equals("error") && action == "update") MessageBox.Show("Ошибка! Обратитесь к поддержке");
            else
            {
                if (!resMes.args[0].Equals("error"))
                {
                    NameKP.Text = resMes.args[0];
                    DateBlock.Text = resMes.args[1];
                }
                else {
                    message = new Message(); 
                    return; 
                }
            }
            message = new Message();
        }

        private void SendDoc(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameKP.Text == "" || DateBlock.Text == "")
                {
                    MessageBox.Show("Не все данные выбраны!");
                    return;
                }
                else
                {
                    // Последний элемент - это STEP ASCC                    
                    if (action == "add")
                    {
                        message.Add(NameKP.Text, DateBlock.Text, Data.NameUser, ID, "0");
                        request = JsonConvert.SerializeObject(message);
                        response = server.SendMsg("AddKP", "ai", request);
                        if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK"))
                        {
                            MessageBox.Show("Поздравляем! КП добавлен!");
                            NameKP.Text = "";
                            DateBlock.Text = "";
                        }
                    }
                    else if (action == "update")
                    {
                        message.Add(IDKP, NameKP.Text, DateBlock.Text, Data.NameUser, ID);
                        request = JsonConvert.SerializeObject(message);
                        response = server.SendMsg("UpdateKP", "ai", request);
                        if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK")) MessageBox.Show("Поздравляем! КП обновлен!");
                    }
                }
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
