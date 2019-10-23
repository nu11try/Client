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
    public partial class PackFormChange : Window
    {
        ServerConnect server = new ServerConnect();
        Message message = new Message();
        Message testsList = new Message();
        Message resMes = new Message();
        Message resMes2 = new Message();
        Message ip = new Message();
        string request = "";
        string response = "";

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

            message.Add(IdPack);
            request = JsonConvert.SerializeObject(message);
            response = server.SendMsg("GetPackChange", Data.ServiceSel, request);
            resMes = JsonConvert.DeserializeObject<Message>(response);

            response = server.SendMsg("GetTestsForPack", Data.ServiceSel);

            resMes2 = JsonConvert.DeserializeObject<Message>(response);

            //perform = response[0].Split('╡');

            //"4±4±local - 127.0.0.1±900±0±DEG_AI_0503737-9▲DEG_AI_0503129-4вапвапа▲"

            if (resMes.args[0] == "error") MessageBox.Show("Ошибка! Обратитесь к поддержке");
            else
            {
                IDPack.Text = resMes.args[0];
                NamePack.Text = resMes.args[1];
                TimeTest.Text = resMes.args[3];
                CountRestart.Text = resMes.args[4];

                TestsStartClass testList = JsonConvert.DeserializeObject<TestsStartClass>(resMes.args[5]);


                for (int i = 0; i < testList.id.Count; i++) TestsInPack.Items.Add(testList.id[i]);
                for (int i = 0; i < testList.id.Count; i++) TestsInPack.SelectedItems.Add(testList.id[i]);
                for (int i = 0; i < resMes2.args.Count; i = i + 3) TestsInPack.Items.Add(resMes2.args[i]);

                response = server.SendMsg("GetIPPc", Data.ServiceSel);

                ip = JsonConvert.DeserializeObject<Message>(response);

                if (ip.args[0].Equals("no_ip"))
                {
                    IPList.Items.Add(resMes.args[2]);
                }
                else
                {
                    for (int i = 0; i < ip.args.Count; i += 2)
                    {
                        IPList.Items.Add(ip.args[i] + " - " + ip.args[i + 1]);
                    }
                }
                IPList.SelectedIndex = IPList.Items.IndexOf(resMes.args[2]);
            }
            message = new Message();
        }

        private void SendPack(object sender, RoutedEventArgs e)
        {
            message = new Message();
            for (int i = 0; i < TestsInPack.SelectedItems.Count; i++) testsList.Add(TestsInPack.SelectedItems[i].ToString());
            if (NamePack.Text == "" || TimeTest.Text == "" || testsList.args.Count == 0) MessageBox.Show("Не все данные выбраны!");
            else
            {
                try
                {
                    message.Add(IDPack.Text, NamePack.Text, JsonConvert.SerializeObject(testsList), TimeTest.Text, CountRestart.Text, IPList.Text);
                    request = JsonConvert.SerializeObject(message);
                    response = server.SendMsg("UpdatePackChange", Data.ServiceSel, request);

                    if (JsonConvert.DeserializeObject<Message>(response).args[0].Equals("OK")) MessageBox.Show("Поздравляем! Набор изменен!");

                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Не все данные выбраны!");
                }
            }
            message = new Message();
        }
    }


}
