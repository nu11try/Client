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
    /// Логика взаимодействия для Doc.xaml
    /// </summary>
    public partial class Doc : Page
    {
        readonly ServerConnect server = new ServerConnect();
        public List<DocClass> DocList { get; set; }
        string response = "";
        string[] docList;
        public Doc()
        {
            InitializeComponent();
            UpdateList();           
        }

        private void UpdateList()
        {
            DocList = new List<DocClass>();
            try
            {
                response = server.SendMsg("getDoc", "ai");
                docList = response.Split('╡');
                if (docList[0] == "no_doc") return;                

                for (var i = 0; i < docList.Length - 1; i++)
                {
                    DocClass doc = new DocClass();
                    string[] docsForList = docList[i].Split('±');                    
                    doc.ID = docsForList[0];
                    doc.Pim = docsForList[1];
                    doc.Date = docsForList[2];

                    DocList.Add(doc);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }
            DataContext = this;
            DocListView.ItemsSource = DocList;
        }

        public class DocClass
        {
            public string ID { get; set; }            
            public string Pim { get; set; }
            public string Date { get; set; }
        }

        private void AddDoc(object sender, RoutedEventArgs e)
        {
            DocFormAdd docFormAdd = new DocFormAdd();
            docFormAdd.ShowDialog();
            UpdateList();
        }

        private void UpdateDoc(object sender, RoutedEventArgs e)
        {
            DocFormAdd docFormAdd = new DocFormAdd((sender as Button).Tag.ToString());
            docFormAdd.ShowDialog();
            UpdateList();
        }

        private void OpenKPDoc(object sender, RoutedEventArgs e)
        {
            OpenKPList kPList = new OpenKPList((sender as Button).Tag.ToString());
            kPList.ShowDialog();
            UpdateList();
        }
    }
}