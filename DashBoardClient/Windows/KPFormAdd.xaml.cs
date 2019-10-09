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
    public partial class KPFormAdd : Window
    {
        ServerConnect server = new ServerConnect();
        List<string> response = new List<string>();
        string[] docAr = new string[] { };
        string action = "add";
        string ID = "";
        string IDKP = "";

        public KPFormAdd(string id, string idKP, string actionType)
        {
            InitializeComponent();
            GetKPInfo(id, idKP);
            if (actionType == "update") action = "update";
            ID = id;
            IDKP = idKP;
        }

        private void GetKPInfo(string id, string idKP)
        {             
            // 0 - name         
            // 1 - date
            response.Add(server.SendMsg("getKPInfo", "ai", id + "±" + idKP));
            docAr = response[0].Split('╡');
            if (response[0] == "error" && action == "update") { MessageBox.Show("Ошибка! Обратитесь к поддержке"); return; }
            else
            {
                if (response[0] != "error")
                {
                    docAr = docAr[0].Split('±');

                    NameKP.Text = docAr[1];
                    DateBlock.Text = docAr[2];
                }
                else { return; }
            }
        }

        private void SendDoc(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameKP.Text == "" || DateBlock.Text == "")
                {
                    MessageBox.Show("Не все данные выбраны!");
                    return;
                }
                else
                {
                    // Последний элемент - это STEP ASCC
                    string paramKP = NameKP.Text + "±" + DateBlock.Text + "±" + Data.NameUser + "±" + ID + "±" + "0";
                    if (action == "add")
                    {
                        if (server.SendMsg("addKP", "ai", paramKP) == "OK")
                        {
                            MessageBox.Show("Поздравляем! КП добавлен!");
                            NameKP.Text = "";
                            DateBlock.Text = "";
                        }
                    }
                    else if (action == "update")
                    {
                        paramKP = IDKP + "±" + NameKP.Text + "±" + DateBlock.Text + "±" + Data.NameUser + "±" + ID;
                        if (server.SendMsg("updateKP", "ai", paramKP) == "OK") MessageBox.Show("Поздравляем! КП обновлен !");
                    }
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
