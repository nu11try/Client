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
        /// <summary>
        /// Токен авторизации
        /// </summary>
        public static string Token { get; set; }
        /// <summary>
        /// Уровень безопасности
        /// для работы в тех или иных сервисах
        /// </summary>
        public static string Security { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public static string NameUser { get; set; }
        public static string ProjectName { get; set; }
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

    public partial class MainWindow : Window
    {        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartTests(object sender, RoutedEventArgs e)
        {
            if (SelecterProject.Text.ToString() != "") Frame.Navigate(new StartTests());
            else MessageBox.Show("Не выбран проект!");
        }

        private void AddTest(object sender, RoutedEventArgs e)
        {            
            if (SelecterProject.Text.ToString() != "") Frame.Navigate(new AddTests());
            else MessageBox.Show("Не выбран проект!");
        }

        private void Packs(object sender, RoutedEventArgs e)
        {            
            if (SelecterProject.Text.ToString() != "") Frame.Navigate(new Packs());
            else MessageBox.Show("Не выбран проект!");
        }

        private void Autostart(object sender, RoutedEventArgs e)
        {            
            if (SelecterProject.Text.ToString() != "") Frame.Navigate(new Autostart());
            else MessageBox.Show("Не выбран проект!");
        }

        private void Doc(object sender, RoutedEventArgs e)
        {
            if (SelecterProject.Text.ToString() != "") Frame.Navigate(new Doc());
            else MessageBox.Show("Не выбран проект!");
        }

        private void ChangeProject(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(new ClearPage());            
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;            
            Data.ProjectName = selectedItem.Content.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LabelUserName.Content = Data.NameUser;
        }

        private void StatisticTest(object sender, RoutedEventArgs e)
        {
            if (SelecterProject.Text.ToString() != "") Frame.Navigate(new StatisticTest());
            else MessageBox.Show("Не выбран проект!");
        }
        private void SelecterProject_Loaded(object sender, RoutedEventArgs e)
        {
            SelecterProject.SelectedIndex = 0;
        }

        private void OperacTest(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Находится в разработке");
        }
    }
}
