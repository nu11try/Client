using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
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
        public MainWindow mainWindow;

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

           
            try
            {
                Message services = JsonConvert.DeserializeObject<Message>(Data.ServiceName);
                Message project = JsonConvert.DeserializeObject<Message>(Data.ProjectName);
                ServerConnect server = new ServerConnect();
                Message mess = new Message();
                mess.Add(Data.NameUser);
                /*Message message = JsonConvert.DeserializeObject<Message>(JsonConvert.DeserializeObject<Message>(server.SendMsg("ShowServises", Data.ServiceSel, JsonConvert.SerializeObject(mess))).args[0]);

                for (var i = 0; project.args.Count > i; i++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = services.args[i];
                    checkBox.Margin = new Thickness(
                       i / 4 * 130, (i * 20) - i / 4 * 80, 0, 0);
                    if (message.args.Contains(checkBox.Name))
                        checkBox.IsChecked = true;
                    projects.Children.Add(checkBox);
                    TextBlock textBox = new TextBlock();
                    textBox.Text = project.args[i];

                    textBox.Margin = new Thickness(
                        i / 4 * 130 + 20, (i * 20) - i / 4 * 80, 20, 20);
                    textBox.Foreground = Brushes.White;
                    projects.Children.Add(textBox);
                }*/
            }
            catch { }
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
            try
            {
                Message mess = new Message();
                mess.Add(Data.NameUser);
                for (int i = 0; i < projects.Children.Count; i += 2)
                {
                    CheckBox checkBox = (CheckBox)projects.Children[i];
                    if (checkBox.IsChecked == true)
                        mess.Add(checkBox.Name);
                }
                ServerConnect server = new ServerConnect();
                if (Data.NameUser != null)
                {
                    /*server.SendMsg("UpdateServises", Data.ServiceSel, JsonConvert.SerializeObject(mess));
                    server.SendMsg("UpdateSession", Data.NameUser, "{\"args\":[\"" + Data.Id + "\"]}");*/

                    /*Message message = JsonConvert.DeserializeObject<Message>(server.SendMsg("CheckNowTests", Data.ServiceSel));
                    mainWindow.TestsNow(message);*/
                }
            }
            catch { }
            this.Close();
        }
    }
}
