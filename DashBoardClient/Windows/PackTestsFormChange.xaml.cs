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
        string response = "";

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
<<<<<<< HEAD
                message.Add(ids.args[0], ids.args[1]);
                request = JsonConvert.SerializeObject(message);
                response = server.SendMsg("GetTestPerform", Data.ServiceSel, request);
                resMes = JsonConvert.DeserializeObject<Message>(response);

                response = server.SendMsg("getTestPerform", Data.ServiceSel, TAG);
=======
                // Запрос на сервер для получения списка тестов и информации по текущему тесту
                response = server.SendMsg("getTestPerform", Data.ProjectName, TAG);
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
                // Общее развертывание ответа
                perform = response.Split('╡');
                // Получение информации по конкретному тесту
                testInfo = perform[0].Split('±');
                // Набор всех тестов без текущего
                testList = perform[1].Split('▲');

                // Заполняем поле с имененм
                Name.Text = testInfo[0];
                // Заполняем выпадающий список тестами (выполнение)
                Start.Items.Add("Первым");
                for (int i = 0; i < testList.Length - 1; i++) Start.Items.Add(testList[i]);
                Start.Text = testInfo[1];
                // -------------------------
                // Заполняем выпадающий список тестами (зависимость)
                TestsSelect.Items.Add("Нет");
                for (int i = 0; i < testList.Length - 1; i++) TestsSelect.Items.Add(testList[i]);
                // Выделяем элементы, если они имеются
                if (testInfo[4] != "not")
                {
                    string[] testsName = testInfo[4].Split('▲');
                    for (int i = 0; i < testsName.Length - 1; i++) TestsSelect.SelectedItems.Add(testsName[i]);
                }
                else TestsSelect.SelectedIndex = 0;
                // -------------------------
                // Заполняется поле с временем выполнения теста
                if (testInfo[2] == "default") Time.SelectedIndex = 0;
                else
                {
                    Time.SelectedIndex = 1;
                    TimeChange.Text = testInfo[2];
                }
                // -------------------------
                // Заполняется поле с количеством рестартов
                if (testInfo[3] == "default") Restart.SelectedIndex = 0;
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
                string param = IdPack + "±" + start + "±" + tests + "±" + time + "±" + restart;
<<<<<<< HEAD
                if (server.SendMsg("updatePackTestPerform", Data.ServiceSel, param) == "OK") MessageBox.Show("Поздравляем! Данные обновлены!");                
=======
                if (server.SendMsg("updatePackTestPerform", Data.ProjectName, param) == "OK") MessageBox.Show("Поздравляем! Данные обновлены!");                
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь в поддержку"); }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
