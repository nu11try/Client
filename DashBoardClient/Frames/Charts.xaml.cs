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
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Newtonsoft.Json;

namespace DashBoardClient
{
    /// <summary>
    /// Логика взаимодействия для Charts.xaml
    /// </summary>
    public partial class Charts : Page
    {
        public class MyDate
        {
            public MyDate(string start, string end) {
                if(start == "")
                {
                    start = "09.12.2010";
                }
                if (end == "")
                {
                    end = "01.12.2100";
                }
                dayStart = Int32.Parse(start.Split('.')[0]);
                mounthStart = Int32.Parse(start.Split('.')[1]);
                yearStart = Int32.Parse(start.Split('.')[2].Split(' ')[0]);

                dayEnd = Int32.Parse(end.Split('.')[0]);
                mounthEnd = Int32.Parse(end.Split('.')[1]);
                yearEnd = Int32.Parse(end.Split('.')[2].Split(' ')[0]);
            }
            public Boolean Compare(string date)
            {
                
                int day = Int32.Parse(date.Split(' ')[0]);
                string mounthS = date.Split(' ')[1];
                int mounth = 0;
                if (mounthS == "января")
                    mounth = 1;
                if (mounthS == "февраля")
                    mounth = 2;
                if (mounthS == "марта")
                    mounth = 3;
                if (mounthS == "апреля")
                    mounth = 4;
                if (mounthS == "мая")
                    mounth = 5;
                if (mounthS == "июня")
                    mounth = 6;
                if (mounthS == "июля")
                    mounth = 7;
                if (mounthS == "августа")
                    mounth = 8;
                if (mounthS == "сентября")
                    mounth = 9;
                if (mounthS == "октября")
                    mounth = 10;
                if (mounthS == "ноября")
                    mounth = 11;
                if (mounthS == "декабря")
                    mounth = 12;
                int year = Int32.Parse(date.Split(' ')[2]);
                if (yearStart > year)
                    if (mounthStart > mounth)
                        if (dayStart > day)
                            return false;
                if (yearEnd < year)
                    if (mounthEnd < mounth)
                        if (dayEnd < day)
                            return false;
                return true;
            }
            public int dayStart { get; set; }
            public int mounthStart { get; set; }
            public int yearStart { get; set; }

            public int dayEnd { get; set; }
            public int mounthEnd { get; set; }
            public int yearEnd { get; set; }
        }
        public ChartValues<CustomerVm> Customers { get; set; }
        public static SeriesCollection SeriesCollection { get; private set; }
        ServerConnect server = new ServerConnect();
        public static Dictionary<string, Brush> hiddenTests;
        public static Dictionary<string, IChartValues> valuesTests;
        BackgroundWorker bw;
        List<string> tests = new List<string>();
        List<List<string>> results = new List<List<string>>();
        List<List<string>> dates = new List<List<string>>();
        List<string> date = new List<string>();
        string a, b, stend;
        Message mess = new Message();
        public Charts()
        {
            InitializeComponent();
            hiddenTests = new Dictionary<string, Brush>();
            valuesTests = new Dictionary<string, IChartValues>();
            SeriesCollection = new SeriesCollection();
            Message services = JsonConvert.DeserializeObject<Message>(Data.ServiceName);
            Message project = JsonConvert.DeserializeObject<Message>(Data.ProjectName);
            for (var i = 0; project.args.Count > i; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Name = services.args[i];
                checkBox.Margin = new Thickness(
                   i / 3 * 140, (i * 20) - i / 3 * 60, 0, 0);
                if (checkBox.Name == Data.ServiceSel)
                    checkBox.IsChecked = true;
                projects.Children.Add(checkBox);
                TextBlock textBox = new TextBlock();
                textBox.Text = project.args[i];

                textBox.Margin = new Thickness(
                    i / 3 * 140 + 20, (i * 20) - i / 3 * 60, 20, 20);
                textBox.Foreground = Brushes.White;
                projects.Children.Add(textBox);
            }
            Message message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetStends", Data.ServiceSel));
            int flag = -1;
            for (int i = 0; i < message.args.Count; i++)
            {
                StendSelected.Items.Add(message.args[i]);
                if (message.args[i] == Data.StendSel)
                    flag = i;
            }

            StendSelected.SelectedIndex = flag;
            a = after.SelectedDate.ToString();
            b = before.SelectedDate.ToString();
            mess = new Message();
            mess.Add(StendSelected.SelectedValue.ToString());
            for (int i = 0; i < projects.Children.Count; i += 2)
            {
                CheckBox checkBox = (CheckBox)projects.Children[i];
                if (checkBox.IsChecked == true)
                    mess.Add(checkBox.Name);
            }
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                СreateCharts();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {

                for (int i = 0; i < tests.Count; i++)
                {
                    LineSeries lineSeries = new LineSeries()
                    {
                        Title = tests[i],
                        Fill = Brushes.Transparent,
                        PointGeometry = null
                    };
                    lineSeries.Values = new ChartValues<CustomerVm>();
                    for (int j = 0; j < results[i].Count; j++)
                    {
                        CustomerVm vm = new CustomerVm
                        {
                            Value = results[i][j] == "" ? double.NaN : double.Parse(results[i][j]),
                            Show = results[i][j] == "" ? "" : tests[i] + ": " + results[i][j] + "c"
                        };
                        lineSeries.Values.Add(vm);

                    }
                    SeriesCollection.Add(lineSeries);
                }
                LineSeries lineSeries1 = new LineSeries();
                lineSeries1.Stroke = Brushes.Transparent;
                lineSeries1.Title = "";
                lineSeries1.Values = new ChartValues<CustomerVm>();
                SeriesCollection.Add(lineSeries1);
                Labels.Labels = date;
                //let create a mapper so LiveCharts know how to plot our CustomerViewModel class
                var customerVmMapper = Mappers.Xy<CustomerVm>()
                .X((value, index) => index) // lets use the position of the item as X
                .Y(value => value.Value); //and PurchasedItems property as Y

                //lets save the mapper globally
                Charting.For<CustomerVm>(customerVmMapper);
                DataContext = this;
                wait.Opacity = 0;

            };
        }

        internal static void Visible(string text)
        {
            int flag = -1;
            for (int i = 0; i < SeriesCollection.Count; i++)
            {
                if (SeriesCollection[i].Title == text && !hiddenTests.ContainsKey(text))
                {
                    LineSeries lineSeries = (LineSeries)SeriesCollection[i];
                    hiddenTests.Add(lineSeries.Title, lineSeries.Stroke);
                    valuesTests.Add(lineSeries.Title, lineSeries.Values);
                    flag = i;
                    break;
                }
            }
            if (flag != -1)
            {
                LineSeries lineSeries = (LineSeries)SeriesCollection[flag];
                lineSeries.Stroke = Brushes.Transparent;
                lineSeries.Values = new ChartValues<CustomerVm>();
            }
            else
            {
                for (int i = 0; i < SeriesCollection.Count; i++)
                {
                    if (text == SeriesCollection[i].Title)
                    {
                        LineSeries lineSeries = (LineSeries)SeriesCollection[i];
                        lineSeries.Values = valuesTests[text];
                        lineSeries.Stroke = hiddenTests[text];
                        hiddenTests.Remove(text);
                        valuesTests.Remove(text);
                        break;
                    }
                }
            }
        }

        public void СreateCharts()
        {
           
             tests = new List<string>();
             results = new List<List<string>>();
             dates = new List<List<string>>();
             date = new List<string>();
            MyDate myDate = new MyDate(a,b);
            

            Message response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetCharts", Data.ServiceSel, JsonConvert.SerializeObject(mess)));
            
            try
            {
                response.args.ForEach(elem =>
                {
                    Message test = JsonConvert.DeserializeObject<Message>(elem);
                    if (myDate.Compare(test.args[4]))
                    {
                        if (tests.Contains(test.args[6]))
                        {
                            results[tests.IndexOf(test.args[6])].Add(test.args[1]);
                            dates[tests.IndexOf(test.args[6])].Add(test.args[4]);
                        }
                        else
                        {
                            tests.Add(test.args[6]);
                            results.Add(new List<string>());
                            results[tests.IndexOf(test.args[6])].Add(test.args[1]);
                            dates.Add(new List<string>());
                            dates[tests.IndexOf(test.args[6])].Add(test.args[4]);
                        }
                        if (date.Contains(test.args[4]))
                        {

                        }
                        else
                        {
                            date.Add(test.args[4]);
                        }
                    }
                });
            }
            catch { }
            for (int i = 0; i < dates.Count; i++)
            {
                if (dates[i].Count != date.Count)
                {
                    for (int j = 0; j < date.Count; j++)
                    {
                        if (!dates[i].Contains(date[j]))
                        {
                            dates[i].Insert(j, date[j]);
                            if (j!=0 && j != results[i].Count && results[i][j - 1] != "" && results[i][j] != "")
                            {
                                results[i].Insert(j, results[i][j-1]);
                            }
                            else
                            {
                                results[i].Insert(j, "");
                            }
                        }
                    }
                }
            }
            string temp = "";
            for (int i = 0; i < date.Count; i++)
            {
                date[i] = date[i].Split('|')[0];
                if (date[i] == temp)
                {
                    date[i] = "";
                }
                else
                {
                    temp = date[i];
                }
            }
        }
        private void SelectStend(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            wait.Opacity = 1;
            print.IsEnabled = false;
            a = after.SelectedDate.ToString();
            b = before.SelectedDate.ToString();
            mess = new Message();
            mess.Add(StendSelected.SelectedValue.ToString());
            for (int i = 0; i < projects.Children.Count; i += 2)
            {
                CheckBox checkBox = (CheckBox)projects.Children[i];
                if (checkBox.IsChecked == true)
                    mess.Add(checkBox.Name);
            }
            bw = new BackgroundWorker();
            bw.DoWork += (obj, ea) => {
                СreateCharts();
            };
            bw.RunWorkerAsync();
            bw.RunWorkerCompleted += (obj, ea) => {
                SeriesCollection.Clear();
                for (int i = 0; i < tests.Count; i++)
                {
                    LineSeries lineSeries = new LineSeries()
                    {
                        Title = tests[i],
                        Fill = Brushes.Transparent,
                        PointGeometry = null
                    };
                    lineSeries.Values = new ChartValues<CustomerVm>();
                    for (int j = 0; j < results[i].Count; j++)
                    {
                        CustomerVm vm = new CustomerVm
                        {
                            Value = results[i][j] == "" ? double.NaN : double.Parse(results[i][j]),
                            Show = results[i][j] == "" ? "" : tests[i] + ": " + results[i][j] + "c"
                        };
                        lineSeries.Values.Add(vm);

                    }
                    SeriesCollection.Add(lineSeries);
                }
                LineSeries lineSeries1 = new LineSeries();
                lineSeries1.Stroke = Brushes.Transparent;
                lineSeries1.Title = "";
                lineSeries1.Values = new ChartValues<CustomerVm>();
                SeriesCollection.Add(lineSeries1);
                Labels.Labels = date;
                //let create a mapper so LiveCharts know how to plot our CustomerViewModel class
                var customerVmMapper = Mappers.Xy<CustomerVm>()
                .X((value, index) => index) // lets use the position of the item as X
                .Y(value => value.Value); //and PurchasedItems property as Y

                //lets save the mapper globally
                Charting.For<CustomerVm>(customerVmMapper);
                DataContext = this;
                wait.Opacity = 0;
                print.IsEnabled = true;
            };
            InitializeComponent();
        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }

}
