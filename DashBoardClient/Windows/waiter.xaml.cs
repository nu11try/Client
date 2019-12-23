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
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Media.Animation;

namespace DashBoardClient
{
    /// <summary>
    /// Логика взаимодействия для waiter.xaml
    /// </summary>
    public partial class Waiter : Window
    {
        public Waiter()
        {
            InitializeComponent();

        }

        public static Thread ShowWaiter()
        {
            Thread thread = new Thread(new ThreadStart(StartForm));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join(400);
            return thread;
        }

        private static void StartForm()
        {
            try
            {
                //Waiter sp = new Waiter();
                
                //sp.ShowDialog();
            }
            catch { }
        }
        public static void AbortWaiter(Thread thread)
        {
            thread.Abort();
        }
    }


}
