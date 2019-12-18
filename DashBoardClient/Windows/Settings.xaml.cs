using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media;
namespace DashBoardClient
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Settings : Window
    {
        ServerConnect server = new ServerConnect();
        HelpedClass.FileSystemClass fileSystem = new HelpedClass.FileSystemClass();
        MainWindow mainWindow = new MainWindow();

        Message message = new Message();
        Message configArg = new Message();

        string response;
        string request;

        public Settings()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            IPEdit.Text = Data.IPServer;
        }
        private void ExitAcc(object sender, RoutedEventArgs e)
        {
            Message mess = new Message();
            mess.Add(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString());
            request = JsonConvert.SerializeObject(mess);
            response = server.SendMsg("ExitAuth", "", request);
            message = JsonConvert.DeserializeObject<Message>(response);
            if (!message.args[0].Equals("0"))
            {
                Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                MessageBox.Show("Нет данных об авторизации!");
                Process.GetCurrentProcess().Kill();
            }
        }

        private void SetIP(object sender, RoutedEventArgs e)
        {
            Data.IPServer = IPEdit.Text.ToString();
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\server.conf", IPEdit.Text.ToString());
            this.Close();
        }
    }
}
