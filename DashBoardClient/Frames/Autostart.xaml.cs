﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashBoardClient
{
    /// <summary>
    /// Логика взаимодействия для Autostart.xaml
    /// </summary>
    public partial class Autostart : Page
    {
        readonly ServerConnect server = new ServerConnect();
        public List<AutoClass> AutoList { get; set; }
        string response = "";
        string[] autoList;
        public Autostart()
        {
            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            AutoList = new List<AutoClass>();
            try
            {
                response = server.SendMsg("getAutostart", "ai");
                autoList = response.Split('╡');
                if (autoList[0] == "error") return;

                for (var i = 0; i < autoList.Length - 1; i++)
                {
                    AutoClass auto = new AutoClass();
                    string[] docsForList = autoList[i].Split('±');
                    auto.ID = docsForList[0];
                    auto.Name = docsForList[1];
                    if (docsForList[5] == "regular") auto.Type = "Регулярно";
                    else if (docsForList[5] == "one") auto.Type = "Единоразово";
                    auto.Pack = docsForList[4].Replace('\n', ' ');
                    auto.Time = docsForList[3];
                    auto.Day = docsForList[2].Replace('\n', ' ');
                    auto.Status = docsForList[6];

                    AutoList.Add(auto);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
            DataContext = this;
            AutoListView.ItemsSource = AutoList;
        }

        public class AutoClass
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Pack { get; set; }
            public string Time { get; set; }
            public string Day { get; set; }
            public string Status { get; set; }
        }
        
        private void AddAutostart(object sender, RoutedEventArgs e)
        {
            //AutostartAdd autoAdd = new AutostartAdd((sender as Button).Tag.ToString());
            AutostartAddChange autoAdd = new AutostartAddChange();
            autoAdd.ShowDialog();
            UpdateList();            
        }
        private void chgAutostart(object sender, RoutedEventArgs e)
        {
            AutostartAddChange autoChg = new AutostartAddChange((sender as Button).Tag.ToString());            
            autoChg.ShowDialog();
            UpdateList();
        }
    }
}