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
    /// Логика взаимодействия для TestFormAdd.xaml
    /// </summary>
    public partial class AutostartAddChange : Window
    {
        ServerConnect server = new ServerConnect();
        Message response;
        string[] docAr = new string[] { };
        string Id = "";
        public AutostartAddChange()
        {
            InitializeComponent();
            Init();
        }

        public AutostartAddChange(string ID)
        {
            InitializeComponent();
            Init();
            Message message = new Message();
            Id = ID;
            message.Add(Id);
            string request = JsonConvert.SerializeObject(message);
            response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetAutostartInfo", Data.ProjectName, request));
            if (response.args[0] == "error")
            {
                MessageBox.Show("Произошла ошибка получения данных автотеста");
                return;
            }

            for (var i = 0; i < response.args.Count; i+= 7) {
                Message packs = JsonConvert.DeserializeObject<Message>(response.args[i + 4]);
                 packs.args.ForEach(elem => {
                     if (!packName.Items.Contains(elem))
                         packName.Items.Add(elem);
                     });
            }
        }

        private void Init()
        {
            for (int i = 0; i < 24; i++)
            {
                if (i > 9) hourSelected.Items.Add(i.ToString());
                else hourSelected.Items.Add("0" + i.ToString());
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9) minuteSelected.Items.Add(i.ToString());
                else minuteSelected.Items.Add("0" + i.ToString());
            }

            hourSelected.SelectedIndex = 0;
            minuteSelected.SelectedIndex = 0;


            Message message = new Message();
            string request = JsonConvert.SerializeObject(message);
            response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ProjectName, request));
            if (response.args[0] == "no_packs")
            {
                MessageBox.Show("Нет доступных на добавление наборов");
                return;
            }

            for (var i = 0; i < response.args.Count; i+=8)
            {
                packName.Items.Add(response.args[i]);
            }
        }

        private void SendDoc(object sender, RoutedEventArgs e)
        {
            try
            {

                if (NameAut.Text == "" || weekDay.SelectedItems.Count == 0 || packName.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Не все данные указаны");
                    return;
                }     
                Message message = new Message();
                Message weekDays = new Message();
                for (int i = 0; i < weekDay.SelectedItems.Count; i++) {
                    weekDays.Add(((TextBlock)weekDay.SelectedItems[i]).Text);
                }

                string weekDaysS = JsonConvert.SerializeObject(weekDays);
                Message packs = new Message();
                for (int i = 0; i < packName.SelectedItems.Count; i++)
                {
                    packs.Add((packName.SelectedItems[i]) + "");
                }
                string packsS = JsonConvert.SerializeObject(packs);
                message.Add(NameAut.Text, checkTranslateType.IsChecked == true? "regular":"one", weekDaysS, packsS, hourSelected.Text, minuteSelected.Text);
                string request = JsonConvert.SerializeObject(message);
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("AddAutostart", Data.ProjectName, request));
                NameAut.Text = "";
                hourSelected.SelectedIndex = 0;
                minuteSelected.SelectedIndex = 0;
                MessageBox.Show("Поздравляем! Автостарт добавлен!");
            }
            catch
            {
                MessageBox.Show("Произошел сбой! Обратитесь в поддержку!");
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReplaceType(object sender, RoutedEventArgs e)
        {
            CheckBox chBox = (CheckBox)sender;
            if (chBox.IsChecked == true) chBox.Content = "Регулярный";
            else chBox.Content = "Единоразовый";
        }
    }
}
