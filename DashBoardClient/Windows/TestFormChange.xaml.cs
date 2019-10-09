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
    public partial class TestFormChange : Window
    {
        ServerConnect server = new ServerConnect();
        List<string> response = new List<string>();
        string[] testInfo = new string[] { };

        string IDTest = "";
        public TestFormChange(string TAG)
        {
            InitializeComponent();
            // получаем ID теста через тег кнопки
            IDTest = TAG;
            GetTestsForListView();
        }

        private void GetTestsForListView()
        {
            TestName.Clear();
            TestID.Clear();
            MethodSelect.Items.Clear();
            ActiveSelect.IsChecked = false;
            AuthorSelect.Items.Clear();
            response.Clear();


            response.Add(server.SendMsg("updateTestChange", "ai", IDTest));

            if (response[0] == "error") MessageBox.Show("Произошла ошибка!");
            else
            {                
                response.Add(server.SendMsg("getAuthor", "ai"));

                testInfo = response[1].Split('╡');
                for (int i = 0; i < testInfo.Length - 1; i++) AuthorSelect.Items.Add(testInfo[i]);

                try { AuthorSelect.Text = AuthorSelect.Items[0].ToString(); }
                catch { MessageBox.Show("Нет тестов на добавление!"); }

                testInfo = response[0].Split('╡');
                testInfo = testInfo[0].Split('±');
                for (int i = 0; i < testInfo.Length; i++)
                {
                    TestID.Text = IDTest;
                    TestName.Text = testInfo[0];
                    AuthorSelect.SelectedIndex = AuthorSelect.Items.IndexOf(testInfo[1].ToString());
                    ActiveSelect.IsChecked = Convert.ToBoolean(testInfo[4].ToString());
                }
                
                
            }
        }
        private void SendTest(object sender, RoutedEventArgs e)
        {           
            try
            {
                string paramTest = IDTest + "±" + TestName.Text.ToString() + "±" + AuthorSelect.SelectedItem.ToString() + "±" + ActiveSelect.IsChecked.Value.ToString();
                if (server.SendMsg("updateTest", "ai", paramTest) == "OK") MessageBox.Show("Поздравляем! Информация по тесту {0} обновлена!", IDTest);
                else MessageBox.Show("Ошибка! Попробуйте позже или обратитесь в поддержку");
                GetTestsForListView();
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
