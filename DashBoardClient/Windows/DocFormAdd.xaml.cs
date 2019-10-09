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
    public partial class DocFormAdd : Window
    {
        ServerConnect server = new ServerConnect();
        List<string> response = new List<string>();
        string[] docAr = new string[] { };
        string action = "add";
        string ID = "";
        public DocFormAdd()
        {
            InitializeComponent();
        }

        public DocFormAdd(string id)
        {
            InitializeComponent();            
            GetDocInfo(id);
            action = "update";
            ID = id;
        }

        private void GetDocInfo(string id)
        {             
            // 0 - pim         
            // 1 - date
            response.Add(server.SendMsg("getDocInfo", "ai", id));
            docAr = response[0].Split('╡');
            if (response[0] == "error") MessageBox.Show("Ошибка! Обратитесь к поддержке");
            else
            {
                docAr = docAr[0].Split('±');

                PimLink.Text = docAr[0];
                DateBlock.Text = docAr[1];
            }
        }

        private void SendDoc(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PimLink.Text == "" || DateBlock.Text == "")
                {
                    MessageBox.Show("Не все данные выбраны!");
                    return;
                }
                else
                {
                    string paramDoc = PimLink.Text + "±" + DateBlock.Text;
                    if (action == "add")
                    {
                        if (server.SendMsg("addDoc", "ai", paramDoc) == "OK")
                        {
                            MessageBox.Show("Поздравляем! Документ добавлен!");
                            PimLink.Text = "";
                            DateBlock.Text = "";
                        }
                    }
                    else if (action == "update")
                    {
                        paramDoc += "±" + ID;
                        if (server.SendMsg("updateDoc", "ai", paramDoc) == "OK") MessageBox.Show("Поздравляем! Документ обновлен !");
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
