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
    /// Логика взаимодействия для FormShowCheckList.xaml
    /// </summary>
    /// 

    public class Сomment
    {        
        public string step { get; set; }
        public string comment { get; set; }

        public string Color { get; set; }
    }

    public class Comments
    {
        public Comments()
        {
            step = new List<string>();
            comment = new List<string>();
        }
        public List<string> step { get; set; }
        public List<string> comment { get; set; }
    }

    public partial class FormShowCheckList : Window
    {
        ServerConnect server = new ServerConnect();

        Message response;
        Message message = new Message();
        string request;
        
        string[] comments;
        string IDTest = "";
        List<Сomment> list;
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
                message.Add(IDTest);
                request = JsonConvert.SerializeObject(message);
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetCommnents", Data.ServiceSel, request));     
                Comments comments = JsonConvert.DeserializeObject<Comments>(response.args[0]);       
                list = new List<Сomment>();

                int cNum = 1;
                int mNum = 0;
                for (int i = 0; i < comments.step.Count; i++)
                {
                    Сomment comment = new Сomment();
                    comment.step = comments.step[i];

                    comment.comment = comments.comment[i];
                    if (comment.step.Contains("-"))
                    {
                        comment.step = mNum + "." + cNum;
                        cNum++;
                    }
                    else
                    {
                        cNum = 1;
                        mNum = Int32.Parse(comments.step[i]);
                    }
                    list.Add(comment);
                    if (comment.comment.Contains("Отсутствуют комментарии к шагу")) comment.Color = "red";
                    else comment.Color = "white";
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }

            DataContext = this;
            CommentsList.ItemsSource = list;
        }
    }
}
