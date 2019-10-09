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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Auth : Window
    {
        ServerConnect server = new ServerConnect();
        HelpedClass.FileSystemClass fileSystem = new HelpedClass.FileSystemClass();
        MainWindow mainWindow = new MainWindow();

        public Auth()
        {
            InitializeComponent();
            List<string> SavedParam = new List<string>();
            SavedParam = fileSystem.ReadConfigFile();            

            if (SavedParam.Count > 0)
            {
                Data.Token = SavedParam[3];
                Data.Security = SavedParam[4];
                Data.NameUser = SavedParam[2];

                mainWindow.Show();

                this.Close();
            }
        }

        private void EnterAuth(object sender, RoutedEventArgs e)
        {                 
            string login = loginAuth.Text.ToString();
            string password = passAuth.Password.ToString();
            string auth_response = "";

            if (login != "" && password != "")
            {
                auth_response = server.Auth(login, password);
                if (auth_response == "") MessageBox.Show("Проблема с сервером");
                else if (auth_response == "no") MessageBox.Show("Неверные данные");
                else
                {
                    Data.Token = auth_response.Split('±')[0];
                    Data.Security = auth_response.Split('±')[1];
                    Data.NameUser = auth_response.Split('±')[2];
                    fileSystem.WriteConfigFile("Login:" + login + ";Password:" + password + ";User:" + Data.NameUser + ";Token:" + Data.Token + ";Security:" + Data.Security + "\n");
                    
                    mainWindow.Show();

                    this.Close();
                }
            }
            else MessageBox.Show("Данные не заполнены");            
        }

        private void SelectEditAuth(object sender, RoutedEventArgs e)
        {
            if (loginAuth.Text.ToString() == "Логин") loginAuth.Text = "";
            else if (passAuth.Password.ToString() == "Password") passAuth.Password = "";            
        }

        private void HoverBtn(object sender, MouseEventArgs e)
        {
            //btnAuth.Background = new SolidColorBrush(Color.FromRgb(43, 170, 201));
        }
    }
}
