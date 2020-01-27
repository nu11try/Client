using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashBoardClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    static class Data
    {
        public static string TestsSelectedForStart { get; set; }
        public static string Token { get; set; }
        public static string Security { get; set; }
        public static string NameUser { get; set; }
        public static string ProjectName { get; set; }
        public static string ServiceName { get; set; }
        public static string ProjectSel { get; set; }
        public static string ServiceSel { get; set; }
        public static string IPServer { get; set; }
        public static string ProjectFolder { get; set; }
        public static string StendSel { get; set; }
        public static string Stend { get; set; }
        public static bool Abort { get; set; }
    }
    public class Message
    {
        public Message() { args = new List<string>(); }
        public void Add(params string[] tmp)
        {
            for (int i = 0; i < tmp.Length; i++) args.Add(tmp[i]);
        }
        public List<string> args { get; set; }
    }
    public class TestsStartClass
    {
        public TestsStartClass()
        {
            id = new List<string>();
            start = new List<string>();
            time = new List<string>();
            dependon = new List<string>();
            restart = new List<string>();
            browser = new List<string>();
            duplicate = new List<string>();
        }
        public List<string> id { get; set; }
        public List<string> start { get; set; }
        public List<string> time { get; set; }
        public List<string> dependon { get; set; }
        public List<string> restart { get; set; }
        public List<string> browser { get; set; }
        public List<string> duplicate { get; set; }
    }
    public partial class MainWindow : Window
    {
        static string parameters = "advfirewall firewall add rule " +
                              "name=\"{0}\" " +
                              "dir={1} " +
                              "action={2} " +
                              "protocol={3} " +
                              "localport={4}";

        static string rulename = "dashboard";
        static string direction = "in"; // in,out
        static string action = "allow"; // allow,block,bypass

        static string parametersDel = "advfirewall firewall delete rule " +
                              "name=\"{0}\" " +
                              "protocol={1} " +
                              "localport={2}";
        public static MainViewModel _vm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            EditRule("TCP", 8888, 1);
            EditRule("UDP", 8888, 1);
            EditRule("TCP", 8889, 1);
            EditRule("UDP", 8889, 1);
            EditRule("TCP", 8890, 1);
            EditRule("UDP", 8890, 1);

            EditRule("TCP", 8888, 0);
            EditRule("UDP", 8888, 0);
            EditRule("TCP", 8889, 0);
            EditRule("UDP", 8889, 0);
            EditRule("TCP", 8890, 0);
            EditRule("UDP", 8890, 0);
            Data.Abort = false;
            try
            {
                versionLabel.Content = "Версия - " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch
            {
                versionLabel.Content = "Версия - unknown";
            }
            try
            {
                /*
                var push = new Thread(Push);
                push.Start();
                var testsNow = new Thread(TestsNow);
                testsNow.Start();*/
            }
            catch { }
        }
        public void Push()
        {
            string response;
            ServerConnect server = new ServerConnect();

            while (true)
            {
                try
                {
                    Thread.Sleep(500);
                    Action action1 = () => SelectProj();
                    Dispatcher.Invoke(action1);
                    if (Data.ServiceSel != null)
                    {
                        response = server.SendMsg("GetPush", Data.ServiceSel);
                        Message mess = JsonConvert.DeserializeObject<Message>(response);
                        if (mess.args[0] == "push")
                        {
                            if (mess.args[1] == "pack")
                            {
                                Action action = () => _vm.SuccCastMess("Набор \"" + mess.args[2] + "\" пройден");
                                Dispatcher.Invoke(action);
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch { }
            }
        }
        public void TestsNow()
        {
            string response;
            ServerConnect server = new ServerConnect();
            int flag = 1;
            while (true)
            {
                try
                {
                    Action action1 = () => SelectProj();
                    Dispatcher.Invoke(action1);
                    if (Data.ServiceSel != null)
                    {
                        response = server.SendMsg("GetNowTests", Data.ServiceSel, "{\"args\":[\"" + flag + "\"]}");
                        Message mess = JsonConvert.DeserializeObject<Message>(response);
                        flag = 0;
                        string text = "";
                        Action action;
                        if (mess.args.Count == 0) action = () => nowTests.Text = text;
                        for (int i = 0; i < mess.args.Count; i += 3)
                        {
                            if (mess.args[i + 1] != null && mess.args[i + 1] != "not")
                            {
                                DateTime time = DateTime.Now;
                                int sec = time.DayOfYear * 24 * 60 * 60 + time.Hour * 60 * 60 + time.Minute * 60 + time.Second;
                                text += mess.args[i] + "\n" + mess.args[i + 1] + " - " + (sec - Int32.Parse(mess.args[i + 2])) + "c\n";
                            }
                        }
                        action = () => nowTests.Text = text;
                        Dispatcher.Invoke(action);
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch { }
            }
        }
        private void StartTests(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelecterProject.Text.ToString() != "")
                {
                    SelectProj();
                    Frame.Navigate(new StartTests());
                }
                else MessageBox.Show("Не выбран проект!");
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void AddTest(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelecterProject.Text.ToString() != "")
                {
                    SelectProj();
                    Frame.Navigate(new AddTests());
                }
                else MessageBox.Show("Не выбран проект!");
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void Packs(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelecterProject.Text.ToString() != "")
                {
                    SelectProj();
                    Frame.Navigate(new Packs());
                }
                else MessageBox.Show("Не выбран проект!");
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void Autostart(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelecterProject.Text.ToString() != "")
                {
                    SelectProj();
                    Frame.Navigate(new Autostart());
                }
                else MessageBox.Show("Не выбран проект!");
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void Doc(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelecterProject.Text.ToString() != "")
                {
                    SelectProj();
                    Frame.Navigate(new Doc());
                }
                else MessageBox.Show("Не выбран проект!");
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void ChangeProject(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(new ClearPage());
            /*ComboBox comboBox = (ComboBox)sender;
            string selectedItem = comboBox.SelectedItem.ToString();            
            Data.ProjectName = selectedItem.ToString();*/
            FreeRAM.Free();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Message message = new Message();
                message = JsonConvert.DeserializeObject<Message>(Data.ProjectName);

                foreach (var proj in message.args) SelecterProject.Items.Add(proj);
                SelecterProject.SelectedIndex = 0;
                LabelUserName.Content = Data.NameUser;
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void StatisticTest(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelecterProject.Text.ToString() != "")
                {
                    SelectProj();
                    Frame.Navigate(new StatisticTest());
                }
                else MessageBox.Show("Не выбран проект!");
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void SelecterProject_Loaded(object sender, RoutedEventArgs e)
        {
            SelecterProject.SelectedIndex = 0;
        }
        private void OperacTest(object sender, RoutedEventArgs e)
        {
            try
            {
                //MessageBox.Show("Находится в разработке");       
                if (SelecterProject.Text.ToString() != "")
                {
                    SelectProj();
                    Frame.Navigate(new Charts());
                }
                else MessageBox.Show("Не выбран проект!");
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        public void SelectProj()
        {
            try
            {
                Message project = new Message();

                project = JsonConvert.DeserializeObject<Message>(Data.ProjectName);

                Data.ProjectSel = SelecterProject.SelectedItem.ToString();
                Data.ServiceSel = JsonConvert.DeserializeObject<Message>(Data.ServiceName).args[project.args.IndexOf(SelecterProject.SelectedItem.ToString())];
                Data.StendSel = JsonConvert.DeserializeObject<Message>(Data.Stend).args[project.args.IndexOf(SelecterProject.SelectedItem.ToString())];
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void GetSettings(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Settings settings = new Settings();
                settings.ShowDialog();
            }
            catch { }
            finally
            {
                FreeRAM.Free();
            }
        }
        private void CloseProcess(object sender, EventArgs e)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("DashBoardClient")) proc.Kill();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        static void EditRule(string protocol, int localport, int type)
        {
            try
            {
                if (type == 0)
                {
                    ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\System32\netsh.exe");
                    info.Arguments = String.Format(parameters, rulename, direction, action, protocol, localport);
                    info.UseShellExecute = false;
                    info.CreateNoWindow = true;
                    var process = Process.Start(info);
                }
                else
                {
                    ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\System32\netsh.exe");
                    info.Arguments = String.Format(parametersDel, rulename, protocol, localport);
                    info.UseShellExecute = false;
                    info.CreateNoWindow = true;
                    var process = Process.Start(info);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Thread.Sleep(500);
        }
    }
}
