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
    public partial class TestFormAdd : Window
    {
        ServerConnect server = new ServerConnect();
        List<string> response = new List<string>();
        string[] tests = new string[] { };
        public TestFormAdd()
        {
            InitializeComponent();
            GetTestsForListView();
        }

        private void GetTestsForListView()
        {
            TestSelect.Items.Clear();
            AuthorSelect.Items.Clear();
            response.Clear();
            response.Add(server.SendMsg("getTests", "ai"));

            if (response[0] == "all_tests_added_in_database") MessageBox.Show("Нет тестов на добавление!");
            else
            {                
                response.Add(server.SendMsg("getAuthor", "ai"));

                tests = response[0].Split('╡');
                for (int i = 0; i < tests.Length - 1; i++) TestSelect.Items.Add(tests[i]);

                tests = response[1].Split('╡');
                for (int i = 0; i < tests.Length - 1; i++) AuthorSelect.Items.Add(tests[i]);

                try
                {
                    TestSelect.Text = TestSelect.Items[0].ToString();
                    AuthorSelect.Text = AuthorSelect.Items[0].ToString();
                }
                catch
                {
                    MessageBox.Show("Нет тестов на добавление!");
                }
            }
        }
        private void SendTest(object sender, RoutedEventArgs e)
        {
            try
            {
                string paramTest = TestSelect.SelectedItem.ToString() + "±" + AuthorSelect.SelectedItem.ToString() + "±" + ActiveSelect.IsChecked.Value.ToString();
                if (server.SendMsg("addTest", "ai", paramTest) == "OK") MessageBox.Show("Поздравляем! Тест добавлен!");
                GetTestsForListView();
                try
                {
                    TestSelect.Text = TestSelect.Items[0].ToString();
                    AuthorSelect.Text = AuthorSelect.Items[0].ToString();
                }
                catch
                {
                    MessageBox.Show("Нет тестов на добавление!");                    
                }
            }
            catch
            {
                MessageBox.Show("Не все данные выбраны!");
            }            
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
