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
        List<string> response = new List<string>();
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
            response.Clear();
            response.Add(server.SendMsg("getTestsForPack", Data.ProjectName));

            if (response[0] == "no_tests_for_pack") MessageBox.Show("Нет тестов на добавление в набор!");
            else
            {                
                tests = response[0].Split('╡');
                
                for (int i = 0; i < tests.Length - 1; i++)
                {
                    TestsInPack.Items.Add(tests[i].Split('±')[0]);
                }
                if (TestsInPack.Items.Count == 0) MessageBox.Show("Нет тестов на добавление в набор!");
                else
                {
                    response.Add(server.SendMsg("getIPPc", Data.ProjectName));
                    if (response[1] == "no_ip") MessageBox.Show("Нет доступных машин!");
                    else
                    {
                        ip = response[1].Split('╡');
                        for (int i = 0; i < ip.Length - 1; i++)
                        {
                            IPList.Items.Add(ip[i].Split('±')[0] + " - " + ip[i].Split('±')[1]);
                        }
                        if (IPList.Items.Count == 0) MessageBox.Show("Нет доступных машин!");
                        else IPList.SelectedIndex = 0;
                    }
                }
            }
        }

        private void SendPack(object sender, RoutedEventArgs e)
        {
            string tests = "";
            for (int i = 0; i < TestsInPack.SelectedItems.Count; i++) tests += TestsInPack.SelectedItems[i] + "╟";
            if (NamePack.Text == ""  || TimeTest.Text == "" || tests == "") MessageBox.Show("Не все данные выбраны!");
            else
            {
                try
                {                    
                    string paramTest = NamePack.Text + "±" +
                        tests + "±" + TimeTest.Text + "±" + CountRestart.Text + "±" + IPList.Text;

                    if (server.SendMsg("addPack", Data.ProjectName, paramTest) == "OK") MessageBox.Show("Поздравляем! Набор добавлен!");

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
