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
    /// Логика взаимодействия для FormShowCheckList.xaml
    /// </summary>
    /// 

    public class comment
    {
        
        public string Number { get; set; }
        public string Step { get; set; }

        public string Color { get; set; }
    }

    public partial class FormShowCheckList : Window
    {
        ServerConnect server = new ServerConnect();
        string response;
        string[] comments;
        string IDTest = "";
        List<comment> list;
        public FormShowCheckList(string TAG)
        {
            IDTest = TAG;
            InitializeComponent();
            GetCheckList();
        }
        private void GetCheckList()
        {
            try
            {
                response = server.SendMsg("getCheckList", "ai", IDTest);
                comments = response.Split('\n');
                list = new List<comment>();
                int qq = 1;
                int q = 0;
                foreach (var i in comments)
                {
                    string w = i.Substring(0, 1);
                    comment comment = new comment();
                    comment.Number = i.Substring(0, 1);
                    comment.Step = i.Substring(1).Trim();
                    if (comment.Number.Contains("-"))
                    {
                        comment.Number = q + "." + qq;
                        qq++;
                    }
                    else {
                        qq = 1;
                        q++;
                    }
                    if (comment.Step.Contains("Отсутствуют комментарии к шагу"))
                    {
                        comment.Color = "red";
                    }
                    else {
                        comment.Color = "white";
                    }
                    list.Add(comment);
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }

            DataContext = this;
            CommentsList.ItemsSource = list;
        }
    }
}
