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
        public string Number { get; set; }
        public string Step { get; set; }

        public string Color { get; set; }
    }

    public partial class FormShowCheckList : Window
    {
        ServerConnect server = new ServerConnect();

        Message response;
        Message message;
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
                //response = server.SendMsg("getCheckList", Data.ServiceSel, IDTest);
                

                message.Add(IDTest);
                request = JsonConvert.SerializeObject(message);
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetCheckList", Data.ServiceSel, request));

                comments = response.args[0].Split('\n');

                list = new List<Сomment>();
                int qq = 1;
                int q = 0;

                int cNum = 1;
                int mNum = 0;
                foreach (var i in comments)
                {
                    Сomment comment = new Сomment();
                    comment.Number = i.Split('¾')[0];

                    comment.Step = i.Split('¾')[1].Trim();
                    if (comment.Number.Contains("-"))
                    {
                        comment.Number = mNum + "." + cNum;
                        cNum++;
                    }
                    else
                    {
                        cNum = 1;
                        mNum++;
                    }
                    if (comment.Step.Contains("Отсутствуют комментарии к шагу")) comment.Color = "red";                    
                    else comment.Color = "white";                    
                    list.Add(comment);
                }
            }
            catch { MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!"); }

            DataContext = this;
            CommentsList.ItemsSource = list;
        }
    }
}
