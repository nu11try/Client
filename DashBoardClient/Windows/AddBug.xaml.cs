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

    public partial class AddBug : Window
    {
        string id;
        public AddBug(string tag)
        {
            id = tag;
            InitializeComponent();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Send(object sender, RoutedEventArgs e)
        {
            Message args = new Message();
            args.Add(Id.Text, id);
            ServerConnect server = new ServerConnect();
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("AddBug", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            this.Close();
        }
    }
}
