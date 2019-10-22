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
    /// Логика взаимодействия для PackTestsFormChange.xaml
    /// </summary>
    /// 

    // "DEG_AI_0503737±Первым±default±default±not"
    // "DEG_AI_0503129▲DEG_AI_0503387▲"
    public partial class PackTestsFormChange : Window
    {
        ServerConnect server = new ServerConnect();

        Message message = new Message();
        Message ids = new Message();
        Message resMes = new Message();
        Message resMes2 = new Message();
        string request = "";
        Message response;

        string[] perform = new string[] { };
        string[] testInfo = new string[] { };
        string[] testList = new string[] { };

        string IdPack;

        public PackTestsFormChange(string TAG)
        {
            ids = JsonConvert.DeserializeObject<Message>(TAG);
            InitializeComponent();

            try
            {

                message.Add(ids.args[0], ids.args[1]);
                request = JsonConvert.SerializeObject(message);
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTestPerform", Data.ServiceSel, request));
                response.args.ForEach(elem =>MessageBox.Show(elem));


                // Заполняем поле с имененм
                Name.Text = response.args[0];
                // Заполняем выпадающий список тестами (выполнение)
                Start.Items.Add("Первым");
                Message tests = JsonConvert.DeserializeObject<Message>(response.args[5]);
                for (int i = 0; i < tests.args.Count; i++) Start.Items.Add(tests.args[i]);
                Start.Text = response.args[1];
                // -------------------------
                // Заполняем выпадающий список тестами (зависимость)
                TestsSelect.Items.Add("Нет");
                for (int i = 0; i < tests.args.Count; i++) TestsSelect.Items.Add(tests.args[i]);
                // Выделяем элементы, если они имеются
                if (response.args[4] != "not")
                {
                    for (int i = 0; i < tests.args.Count; i++) TestsSelect.SelectedItems.Add(tests.args[i]);
                }
                else TestsSelect.SelectedIndex = 0;
                // -------------------------
                // Заполняется поле с временем выполнения теста
                if (response.args[2] == "default") Time.SelectedIndex = 0;
                else
                {
                    Time.SelectedIndex = 1;
                    TimeChange.Text = testInfo[2];
                }
                // -------------------------
                // Заполняется поле с количеством рестартов
                if (response.args[3] == "default") Restart.SelectedIndex = 0;
                else Restart.Text = testInfo[3];
                // -------------------------
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }
        }

        private void ChangeTime(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem.Content.ToString() == "Изменить") TimeChange.IsEnabled = true;
            else TimeChange.IsEnabled = false;
        }

        private void SendMsg(object sender, RoutedEventArgs e)
        {
            string start = "";
            string tests = "";
            string time = "";
            string restart = "";

            start = Start.Text;
            for (int i = 0; i < TestsSelect.SelectedItems.Count; i++)
            {
                if (TestsSelect.SelectedItems[i].ToString() == "Нет")
                {
                    tests = "not";
                    break;
                }
                else tests += TestsSelect.SelectedItems[i] + "▲";
            }
            if (TimeChange.IsEnabled) time = TimeChange.Text;
            else time = "default";
            if (Restart.Text == "По умолчанию") restart = "default";
            else restart = Restart.Text;
            try
            {
                Message message = new Message();
                message.Add(IdPack, Name.Text, start, tests, time, restart);
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("UpdateTestIntoPack", Data.ServiceSel, JsonConvert.SerializeObject(message)));
                if (response.args[0].Equals("ok")) MessageBox.Show("Тест успешно изменен");
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь в поддержку"); }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
