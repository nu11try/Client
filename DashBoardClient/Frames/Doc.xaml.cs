using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        Message response;
        BackgroundWorker bw;
        public Doc()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                DocListView.ItemsSource = DocList;
            };
            
        }

        private void UpdateList()
        {
            DocList = new List<DocClass>();
            try
            {
                response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetDocument", Data.ServiceSel));                                
                if (response.args[0] == "no_doc") return;                

                for (var i = 0; i < response.args.Count; i += 3)
                {
                    DocClass doc = new DocClass();                   
                    doc.ID = response.args[i];
                    if (response.args[i + 1].Contains(".doc"))
                    {
                        doc.Pim = response.args[i + 1].Split('/').Last().Replace(".doc", "");
                    }
                    if (response.args[i + 1].Contains(".docx"))
                    {
                        doc.Pim = response.args[i + 1].Split('/').Last().Replace(".docx", "");
                    }
                    doc.Date = response.args[i + 2];

                    DocList.Add(doc);
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Обратитесь к поддержке!");
            }

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
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                DocListView.ItemsSource = DocList;
            };
        }

        private void OpenKPDoc(object sender, RoutedEventArgs e)
        {
            OpenKPList kPList = new OpenKPList((sender as Button).Tag.ToString());
            kPList.ShowDialog();
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                UpdateList();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                wait.Opacity = 0;
                DocListView.ItemsSource = DocList;
            };
        }
    }
}
