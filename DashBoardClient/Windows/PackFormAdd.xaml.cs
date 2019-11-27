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
            TimeTest.TextChanged += TimeTest_TextChanged;
            response = server.SendMsg("GetTestsForPack", Data.ServiceSel);
            resMes = JsonConvert.DeserializeObject<Message>(response);

            if (resMes.args[0].Equals("no_tests_for_pack")) MessageBox.Show("Нет тестов на добавление в набор!");
            else
            {                
                for (int i = 0; i < resMes.args.Count; i += 3)
                {
                    TestsInPack.Items.Add(resMes.args[i] + " (" + resMes.args[i+1] + ")");
                }
                if (TestsInPack.Items.Count == 0) MessageBox.Show("Нет тестов на добавление в набор!");
                else
                {
                    response = server.SendMsg("GetIPPc", Data.ServiceSel);
                    message = JsonConvert.DeserializeObject<Message>(response);

                    if (message.args.Count == 0) MessageBox.Show("Нет доступных машин!");
                    else
                    {
                        for (int i = 0; i < message.args.Count; i += 2)
                        {
                            IPList.Items.Add(message.args[i] + " - " + message.args[i+1]);
                        }
                        if (IPList.Items.Count == 0) MessageBox.Show("Нет доступных машин!");
                        else IPList.SelectedIndex = 0;
                    }
                    response = server.SendMsg("GetStends", Data.ServiceSel);
                    message = JsonConvert.DeserializeObject<Message>(response);
                    if (message.args.Count == 0) MessageBox.Show("Нет доступных стендов!");
                    else
                    {
                        for (int i = 0; i < message.args.Count; i ++)
                        {
                            Stend.Items.Add(message.args[i]);
                        }
                        if (Stend.Items.Count == 0) MessageBox.Show("Нет доступных стендов!");
                        else Stend.SelectedIndex = 0;
                    }
                }
            }
        }


        private void TimeTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double tmp = Int32.Parse(TimeTest.Text) / 60;
                double tmp1 = Int32.Parse(TimeTest.Text) % 60;
                Math.Round(tmp);
                TimeMin.Content = "(" + tmp + " мин ";
                if (tmp1 != 0) TimeMin.Content += tmp1 + " c";
                TimeMin.Content += ")";
            }
            catch
            {
                TimeMin.Content = "(0 мин)";
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
                {
                    message.Add(NamePack.Text, JsonConvert.SerializeObject(testsList), TimeTest.Text, CountRestart.Text, IPList.Text, Browser.Text, Stend.Text);
                    request = JsonConvert.SerializeObject(message);
                    response = server.SendMsg("AddPack", Data.ServiceSel, request);
                    if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK")) MessageBox.Show("Поздравляем! Набор добавлен!");

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
