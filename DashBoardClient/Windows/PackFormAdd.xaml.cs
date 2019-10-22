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
    public partial class PackFormAdd : Window
    {
        ServerConnect server = new ServerConnect();
        
        Message message = new Message();
        Message resMes = new Message();
        Message testsList = new Message();

        string request = "";
        string response = "";

        string[] tests = new string[] { };
        string[] ip = new string[] { };
        public PackFormAdd()
        {
            InitializeComponent();
            GetPackForListView();
        }

        private void GetPackForListView()
        {
            IPList.Items.Clear();
            TestsInPack.Items.Clear();
<<<<<<< HEAD
=======
            response.Clear();
            response.Add(server.SendMsg("getTestsForPack", Data.ProjectName));
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d

            response = server.SendMsg("GetTestsForPack", Data.ServiceSel);
            resMes = JsonConvert.DeserializeObject<Message>(response);

            if (resMes.args[0].Equals("no_tests_for_pack")) MessageBox.Show("Нет тестов на добавление в набор!");
            else
            {                
                for (int i = 0; i < resMes.args.Count; i += 3)
                {
                    TestsInPack.Items.Add(resMes.args[0] + " (" + resMes.args[1] + ")");
                }
                if (TestsInPack.Items.Count == 0) MessageBox.Show("Нет тестов на добавление в набор!");
                else
                {
<<<<<<< HEAD
                    response = server.SendMsg("GetIPPc", Data.ServiceSel);
                    message = JsonConvert.DeserializeObject<Message>(response);

                    if (message.args.Count == 0) MessageBox.Show("Нет доступных машин!");
=======
                    response.Add(server.SendMsg("getIPPc", Data.ProjectName));
                    if (response[1] == "no_ip") MessageBox.Show("Нет доступных машин!");
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
                    else
                    {
                        for (int i = 0; i < message.args.Count; i += 2)
                        {
                            IPList.Items.Add(message.args[i] + " - " + message.args[i+1]);
                        }
                        if (IPList.Items.Count == 0) MessageBox.Show("Нет доступных машин!");
                        else IPList.SelectedIndex = 0;
                    }
                }
            }
        }

        private void SendPack(object sender, RoutedEventArgs e)
        {
            message = new Message();
            resMes = new Message();
            testsList = new Message();
            for (int i = 0; i < TestsInPack.SelectedItems.Count; i++) 
                testsList.Add(TestsInPack.SelectedItems[i].ToString().Split('(')[0].Substring(0, TestsInPack.SelectedItems[i].ToString().Split('(')[0].Length-1));
            if (NamePack.Text == "" || TimeTest.Text == "" || testsList.args.Count == 0) MessageBox.Show("Не все данные выбраны!");
            else
            {
                try
<<<<<<< HEAD
                {
                    message.Add(NamePack.Text, JsonConvert.SerializeObject(testsList), TimeTest.Text, CountRestart.Text, IPList.Text);
                    request = JsonConvert.SerializeObject(message);
                    response = server.SendMsg("AddPack", Data.ServiceSel, request);
                    if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK")) MessageBox.Show("Поздравляем! Набор добавлен!");
=======
                {                    
                    string paramTest = NamePack.Text + "±" +
                        tests + "±" + TimeTest.Text + "±" + CountRestart.Text + "±" + IPList.Text;

                    if (server.SendMsg("addPack", Data.ProjectName, paramTest) == "OK") MessageBox.Show("Поздравляем! Набор добавлен!");
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d

                    GetPackForListView();
                    if (TestsInPack.Items.Count < 1) MessageBox.Show("Нет тестов на добавление в набор!");
                }
                catch
                {
                    MessageBox.Show("Не все данные выбраны!");
                }
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
