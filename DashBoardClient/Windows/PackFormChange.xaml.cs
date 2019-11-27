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
        TestsStartClass testList;

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
            TimeTest.TextChanged += TimeTest_TextChanged;
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
                Browser.Text = resMes.args[6];
                
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
                testList = JsonConvert.DeserializeObject<TestsStartClass>(resMes.args[5]);
                if (resMes2.args.Count != 1)
                    for (int i = 0; i < resMes2.args.Count; i = i + 3) TestsInPack.Items.Add(resMes2.args[i]);

                for (int i = 0; i < testList.id.Count; i++) if(!TestsInPack.Items.Contains(testList.id[i])) TestsInPack.Items.Add(testList.id[i]);
                for (int i = 0; i < testList.id.Count; i++) TestsInPack.SelectedItems.Add(testList.id[i]);
               

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

                response = server.SendMsg("GetStends", Data.ServiceSel);

               Message stend = JsonConvert.DeserializeObject<Message>(response);

                for (int i = 0; i < stend.args.Count; i ++)
                {
                    Stend.Items.Add(stend.args[i]);
                }
                Stend.SelectedIndex = IPList.Items.IndexOf(resMes.args[7]);
                Stend.Text = resMes.args[7];
            }
            message = new Message();
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
            for (int i = 0; i < TestsInPack.SelectedItems.Count; i++) testsList.Add(TestsInPack.SelectedItems[i].ToString());
            if (NamePack.Text == "" || TimeTest.Text == "" || testsList.args.Count == 0) MessageBox.Show("Не все данные выбраны!");
            else
            {
                TestsStartClass tests = new TestsStartClass();
                Message removeTe = new Message();
                for (int i = 0; i < TestsInPack.Items.Count; i++)
                {
                    if (!testsList.args.Contains(TestsInPack.Items[i].ToString()))
                    {
                        removeTe.Add(TestsInPack.Items[i].ToString());
                    }
                }
                Message dep;
                Message newDep;
                try
                {
                    for (int j = 0; j < testList.id.Count;j++)
                    {
                        for(int q = 0; q < testsList.args.Count; q++)
                        {
                            if (testList.id[j].Equals(testsList.args[q]))
                            {
                                
                                testsList.args.RemoveAt(q);
                                
                                q--;
                                
                                tests.restart.Add(testList.restart[j]);
                                tests.time.Add(testList.time[j]);
                                tests.browser.Add(testList.browser[j]);
                                tests.duplicate.Add(testList.duplicate[j]);
                                if (tests.id.Count() == 0)
                                {
                                    tests.start.Add("первый");
                                    tests.dependon.Add("{\"args\":[\"not\"]}");
                                }
                                else
                                {
                                    tests.start.Add(tests.id.Contains(testList.start[j]) ? testList.start[j] : tests.id.Last());
                                    dep = JsonConvert.DeserializeObject<Message>(testList.dependon[j]);
                                    newDep = new Message();
                                    for (int f = 0; f < dep.args.Count; f++)
                                    {
                                        if (tests.id.Contains(dep.args[f]))
                                        {
                                            newDep.args.Add(dep.args[f]);
                                        }
                                    }
                                    tests.dependon.Add(newDep.args.Count == 0 ? "{\"args\":[\"not\"]}" : JsonConvert.SerializeObject(newDep));
                                }
                                tests.id.Add(testList.id[j]);
                            }
                        }
                    }
                   for(int j = 0; j < testsList.args.Count; j++)
                    {
                        tests.restart.Add("default");
                        tests.time.Add("default");
                        tests.browser.Add("default");
                        tests.duplicate.Add("not");
                        if (tests.id.Count() == 0)
                        {
                            tests.start.Add("первый");
                            tests.dependon.Add("{\"args\":[\"not\"]}");
                        }
                        else
                        {
                            tests.start.Add(tests.id.Last());
                            tests.dependon.Add("{\"args\":[\"not\"]}");
                        }
                        tests.id.Add(testsList.args[j]);

                    }
                    
                    string te = JsonConvert.SerializeObject(tests);
                    string re = JsonConvert.SerializeObject(removeTe);
                    message.Add(IDPack.Text, NamePack.Text, te, TimeTest.Text, CountRestart.Text, IPList.Text, re, Browser.Text, Stend.Text);
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
