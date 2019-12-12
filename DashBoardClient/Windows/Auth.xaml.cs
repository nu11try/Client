using Newtonsoft.Json;
using System.Net;
using System.Windows;
using System.Windows.Media;
namespace DashBoardClient
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Auth : Window
    {
        ServerConnect server = new ServerConnect();
        HelpedClass.FileSystemClass fileSystem = new HelpedClass.FileSystemClass();
        MainWindow mainWindow = new MainWindow();

        Message message = new Message();
        Message configArg = new Message();

        string response;
        string request;

        public Auth()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            loginAuth.LostFocus += LoginAuth_LostFocus;
            loginAuth.GotFocus += LoginAuth_GotFocus;
            passAuth.LostFocus += PassAuth_LostFocus;
            passAuth.GotFocus += PassAuth_GotFocus;


            Message mess = new Message();
            mess.Add(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString());
            request = JsonConvert.SerializeObject(mess);
            response = server.SendMsg("getAuth", "", request);
            message = JsonConvert.DeserializeObject<Message>(response);
            if (message.args[0] != "no")
            {

                Data.Security = message.args[1];
                Data.NameUser = message.args[0];
                Data.ProjectName = message.args[3];
                Data.ServiceName = message.args[2];

                mainWindow.Show();

                this.Close();
            }
        }

        private void EnterAuth(object sender, RoutedEventArgs e)
        {
            string login = loginAuth.Text.ToString();
            string password = passAuth.Password.ToString();

            message = new Message();

            if (login != "" && password != "")
            {
                message.Add(login, password, Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString());
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("Auth", "", request);
                message = JsonConvert.DeserializeObject<Message>(response);
                if (message.args.Count == 0) MessageBox.Show("Проблема с сервером");
                else if (message.args[0] == "no") MessageBox.Show("Неверные данные");
                else
                {
                    Init();
                }
            }
            else MessageBox.Show("Данные не заполнены");
        }
        private void LoginAuth_LostFocus(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(loginAuth.Text))
            {
                loginAuth.Text = "Логин";
                loginAuth.Foreground = Brushes.Gray;
            }
        }
        private void LoginAuth_GotFocus(object sender, RoutedEventArgs e)
        {
            if (loginAuth.Text == "Логин")
            {
                loginAuth.Text = "";
                loginAuth.Foreground = Brushes.Black;
            }
        }
        private void PassAuth_LostFocus(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(passAuth.Password))
            {
                passAuth.Password = "Password";
                passAuth.Foreground = Brushes.Gray;
            }
        }
        private void PassAuth_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passAuth.Password == "Password")
            {
                passAuth.Password = "";
                passAuth.Foreground = Brushes.Black;
            }
        }
        private void SelectEditAuth(object sender, RoutedEventArgs e)
        {
            if (loginAuth.Text.ToString() == "Логин") loginAuth.Text = "";
            else if (passAuth.Password.ToString() == "Password") passAuth.Password = "";
        }
    }
}
