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
        public ChartValues<CustomerVm> Customers { get; set; }
        public static SeriesCollection SeriesCollection { get; private set; }
        ServerConnect server = new ServerConnect();
        public static Dictionary<string, Brush> hiddenTests;
        public static Dictionary<string, IChartValues> valuesTests;
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
                   i / 3 * 80, (i * 20) - i / 3 * 60, 0, 0);
                if (checkBox.Name == Data.ServiceSel)
                    checkBox.IsChecked = true;
                projects.Children.Add(checkBox);
                TextBlock textBox = new TextBlock();
                textBox.Text = project.args[i];

                textBox.Margin = new Thickness(
                    i / 3 * 80 + 20, (i * 20) - i / 3 * 60, 20, 20);
                textBox.Foreground = Brushes.White;
                projects.Children.Add(textBox);
            }
            Message message = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetStends", Data.ServiceSel));
            for (int i = 0; i < message.args.Count; i++)
            {
                StendSelected.Items.Add(message.args[i]);
            }
            StendSelected.SelectedIndex = 0;
            createCharts();
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

        public void createCharts()
        {
            Message mess = new Message();
            mess.Add(after.SelectedDate.ToString());
            mess.Add(before.SelectedDate.ToString());
            mess.Add(StendSelected.SelectedItem.ToString());
            for (int i = 0; i < projects.Children.Count; i += 2)
            {
                CheckBox checkBox = (CheckBox)projects.Children[i];
                if (checkBox.IsChecked == true)
                    mess.Add(checkBox.Name);
            }

            Message response = JsonConvert.DeserializeObject<Message>(server.SendMsg("GetCharts", Data.ServiceSel, JsonConvert.SerializeObject(mess)));
            List<string> tests = new List<string>();
            List<List<string>> results = new List<List<string>>();
            List<List<string>> versions = new List<List<string>>();
            List<string> version = new List<string>();
            response.args.ForEach(elem =>
            {
                Message test = JsonConvert.DeserializeObject<Message>(elem);
                if (tests.Contains(test.args[6]))
                {
                    results[tests.IndexOf(test.args[6])].Add(test.args[1]);
                    versions[tests.IndexOf(test.args[6])].Add(test.args[4]);
                }
                else
                {
                    tests.Add(test.args[6]);
                    results.Add(new List<string>());
                    results[tests.IndexOf(test.args[6])].Add(test.args[1]);
                    versions.Add(new List<string>());
                    versions[tests.IndexOf(test.args[6])].Add(test.args[4]);
                }
                if (version.Contains(test.args[4]))
                {

                }
                else
                {
                    version.Add(test.args[4]);
                }
            });
            for (int i = 0; i < versions.Count; i++)
            {
                if (versions[i].Count != version.Count)
                {
                    for (int j = 0; j < version.Count; j++)
                    {
                        if (!versions[i].Contains(version[j]))
                        {
                            versions[i].Insert(j, version[j]);
                            results[i].Insert(j, "");
                        }
                    }
                }
            }
            SeriesCollection.Clear();
            string temp = "";
            for (int i = 0; i < version.Count; i++)
            {
                version[i] = version[i].Split('|')[0];
                if (version[i] == temp)
                {
                    version[i] = "";
                }
                else
                {
                    temp = version[i];
                }
            }
            for (int i = 0; i < tests.Count; i++)
            {
                LineSeries lineSeries = new LineSeries()
                {
                    Title = tests[i],
                    Fill = Brushes.Transparent
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

            Labels.Labels = version;
            //let create a mapper so LiveCharts know how to plot our CustomerViewModel class
            var customerVmMapper = Mappers.Xy<CustomerVm>()
            .X((value, index) => index) // lets use the position of the item as X
            .Y(value => value.Value); //and PurchasedItems property as Y

            //lets save the mapper globally
            Charting.For<CustomerVm>(customerVmMapper);
            DataContext = this;
        }
        private void SelectStend(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            createCharts();
        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }

}
