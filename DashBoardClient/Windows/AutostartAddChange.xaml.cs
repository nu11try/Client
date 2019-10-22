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
        string response = "";
        string[] packsList;
        string[] docAr = new string[] { };

        public AutostartAddChange()
        {
            InitializeComponent();
            Init();
        }

        public AutostartAddChange(string ID)
        {
            InitializeComponent();
            Init();
            response = server.SendMsg("getAutostartInfo", Data.ServiceSel, ID);
            packsList = response.Split('╡');
            if (packsList[0] == "error")
            {
                MessageBox.Show("Произошла ошибка получения данных автотеста");
                return;
            }

            for (var i = 0; i < packsList.Length - 1; i++) packName.Items.Add(packsList[i].Split('±')[4]);
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

            response = server.SendMsg("getPacksForList", Data.ServiceSel);
            packsList = response.Split('╡');
            if (packsList[0] == "no_packs")
            {
                MessageBox.Show("Нет доступных на добавление наборов");
                return;
            }

            for (var i = 0; i < packsList.Length - 1; i++) packName.Items.Add(packsList[i].Split('±')[0]);
        }

        private void SendDoc(object sender, RoutedEventArgs e)
        {
            try
            {
                string buf = "";
                string paramAut = NameAut.Text;
                if (NameAut.Text == "" || weekDay.SelectedItems.Count == 0 || packName.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Не все данные указаны");
                    return;
                }
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
                {
                    NameAut.Text = "";
                    hourSelected.SelectedIndex = 0;
                    minuteSelected.SelectedIndex = 0;                    
                    MessageBox.Show("Поздравляем! Автостарт добавлен!");
                }
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
