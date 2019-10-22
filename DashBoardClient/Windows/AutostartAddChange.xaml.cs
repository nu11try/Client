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
<<<<<<< HEAD
            response = server.SendMsg("getAutostartInfo", Data.ServiceSel, ID);
            packsList = response.Split('╡');
            if (packsList[0] == "error")
=======
            Message message = new Message();
            Id = ID;
            message.Add(Id);
            string request = JsonConvert.SerializeObject(message);
            response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetAutostartInfo", Data.ProjectName, request));
            if (response.args[0] == "error")
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
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

<<<<<<< HEAD
            response = server.SendMsg("getPacksForList", Data.ServiceSel);
            packsList = response.Split('╡');
            if (packsList[0] == "no_packs")
=======
            Message message = new Message();
            string request = JsonConvert.SerializeObject(message);
            response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetPacksForList", Data.ProjectName, request));
            if (response.args[0] == "no_packs")
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
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
<<<<<<< HEAD
                if (checkTranslateType.IsChecked == true) paramAut += "±" + "regular" + "±";
                else paramAut += "±" + "one" + "±";
                for (int i = 0; i < weekDay.SelectedItems.Count; i++) buf += ((TextBlock)weekDay.SelectedItems[i]).Text + "╟";                
                paramAut += buf + "±";
                buf = "";
                for (int i = 0; i < packName.SelectedItems.Count; i++) buf += packName.SelectedItems[i] + "╟";
                paramAut += buf + "±";
                buf = "";
                paramAut += hourSelected.Text + "±" + minuteSelected.Text;

                if (server.SendMsg("addAutostart", Data.ServiceSel, paramAut) == "OK")
=======
                string weekDaysS = JsonConvert.SerializeObject(weekDays);
                Message packs = new Message();
                for (int i = 0; i < packName.SelectedItems.Count; i++)
>>>>>>> ba6800c8e2c9604ad79dacf32926ff8a23d5b28d
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
