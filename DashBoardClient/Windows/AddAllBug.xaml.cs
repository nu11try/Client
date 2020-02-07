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

    public partial class AddAllBug : Window
    {
        string id;
       readonly ServerConnect server = new ServerConnect();
        public AddAllBug(string tag)
        {
            id = tag;
            InitializeComponent();
            GetTests();
        }

        private void GetTests()
        {

            Message args = new Message();
            args.Add(id);
            
            if (id != "-")
            {
                Message res1 = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetTestsWithBugs", Data.ServiceSel, JsonConvert.SerializeObject(args)));
                Id.Text = id;
                Id.IsEnabled = false;
                for (int i = 0; i < res1.args.Count; i++)
                {
                    Tests.Items.Add(res1.args[i]);
                    Tests.SelectedItems.Add(res1.args[i]);
                }
            }
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetAllTests", Data.ServiceSel));
            for (int i = 0; i < res.args.Count; i++)
            {
                if(!Tests.Items.Contains(res.args[i]))
                Tests.Items.Add(res.args[i]);
            }
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Send(object sender, RoutedEventArgs e)
        {
            if (Id.IsEnabled == false)
            {
                Message args = new Message();
                args.Add(Id.Text);
                for (int i = 0; i < Tests.SelectedItems.Count; i++)
                    args.Add(Tests.SelectedItems[i].ToString()) ;
                Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("ChangeBug", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            }
            else
            {
                Message args = new Message();
                args.Add(Id.Text);
                for (int i = 0; i < Tests.SelectedItems.Count; i++)
                    args.Add(Tests.SelectedItems[i].ToString());
                Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("AddBug", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            }
            this.Close();
        }
    }
}
