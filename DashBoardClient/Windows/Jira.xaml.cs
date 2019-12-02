﻿using Newtonsoft.Json;
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

    public class Errors
    {
        public string link { get; set; }
        public string name { get; set; }
        public string type { get; set; }

        public string data { get; set; }
        public string status { get; set; }
        public string executor { get; set; }
    }
    public partial class Jira : Window
    {
        string id;
        readonly ServerConnect server = new ServerConnect();
        public Jira(string TAG)
        {
            id = TAG;
            InitializeComponent();
            Update();
            
        }
        private void Update()
        {
            
            JiraList.Items.Clear();
            Message args = new Message();
            args.Add(id);
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetErrors", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            for (int i = 0; i < res.args.Count; i += 6)
            {
                Errors error = new Errors();
                error.name = res.args[i];
                error.link = res.args[i + 1];
                error.type = res.args[i + 2];
                error.data = res.args[i + 3];
                error.executor = res.args[i + 4];
                error.status = res.args[i + 5];
                JiraList.Items.Add(error);
            }
        }
        private void AddBug(object sender, RoutedEventArgs e)
        {
            AddBug AddBug = new AddBug(id);
            AddBug.ShowDialog();
            Update();
        }
        private void DeleteBug(Object sender, RoutedEventArgs e)
        {
            string link = (sender as Button).Tag.ToString();
            Message args = new Message();
            args.Add(id, link);
            Message res = JsonConvert.DeserializeObject<Message>(server.SendMsg("DeleteBug", Data.ServiceSel, JsonConvert.SerializeObject(args)));
            Update();
        }
    }
}