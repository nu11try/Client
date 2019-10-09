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
    public partial class PackFormChange : Window
    {
        ServerConnect server = new ServerConnect();
        List<string> response = new List<string>();
        string[] pack = new string[] { };
        string[] ip = new string[] { };
        string[] perform = new string[] { };
        string IdPack;
        public PackFormChange(string TAG)
        {
            InitializeComponent();
            IdPack = TAG;
            PackForChange();
        }

        private void PackForChange()
        {
            IPList.Items.Clear();
            TestsInPack.Items.Clear();
            response.Clear();

            response.Add(server.SendMsg("getPackChange", "ai", IdPack));
            response.Add(server.SendMsg("getTestsForPack", "ai"));

            perform = response[0].Split('╡');

            //"4±4±local - 127.0.0.1±900±0±DEG_AI_0503737-9▲DEG_AI_0503129-4вапвапа▲"

            if (response[0] == "error") MessageBox.Show("Ошибка! Обратитесь к поддержке");
            else
            {
                pack = response[0].Split('±');

                IDPack.Text = pack[0];
                NamePack.Text = pack[1];
                TimeTest.Text = pack[3];
                CountRestart.Text = pack[4];

                string[] testList = pack[5].Split('▲');

                if (response[1] == "no_tests_for_pack")
                {
                    for (int i = 0; i < testList.Length - 1; i++) TestsInPack.Items.Add(testList[i]);
                }
                else
                {
                    string[] tests = response[1].Split('╡');
                    for (int i = 0; i < tests.Length - 1; i++) TestsInPack.Items.Add(tests[i].Split('±')[2]);
                    for (int i = 0; i < testList.Length - 1; i++) TestsInPack.Items.Add(testList[i]);
                }

                for (int i = 0; i < testList.Length - 1; i++)
                {                   
                    TestsInPack.SelectedItems.Add(testList[i]);
                }

                response.Add(server.SendMsg("getIPPc", "ai"));
                if (response[2] == "no_ip")
                {
                    IPList.Items.Add(pack[1]);
                }
                else
                {
                    ip = response[2].Split('╡');
                    for (int i = 0; i < ip.Length - 1; i++)
                    {
                        IPList.Items.Add(ip[i].Split('±')[0] + " - " + ip[i].Split('±')[1]);
                    }                    
                }
                IPList.SelectedIndex = IPList.Items.IndexOf(pack[2]);
            }
        }

        private void SendPack(object sender, RoutedEventArgs e)
        {
            string tests = "";
            for (int i = 0; i < TestsInPack.SelectedItems.Count; i++) tests += TestsInPack.SelectedItems[i] + "╟";
            if (NamePack.Text == "" || TimeTest.Text == "" || tests == "") MessageBox.Show("Не все данные выбраны!");
            else
            {
                try
                {
                    string paramTest = IDPack.Text + "±" + NamePack.Text + "±" +
                        tests + "±" + TimeTest.Text + "±" + CountRestart.Text + "±" + IPList.Text;

                    if (server.SendMsg("updatePackChange", "ai", paramTest) == "OK") MessageBox.Show("Поздравляем! Набор изменен!");

                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Не все данные выбраны!");
                }
            }
        }
    }
}
