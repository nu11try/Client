using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace DashBoardClient
{
    static class DataIP
    {
        public static int Port { get; set; }
        public static string IP { get; set; }
    }
    class Server
    {
        //const int port = 8888;
        //const string ip = "172.31.197.89";
        //const string ip = "172.17.42.40";
        //const string ip = "172.31.197.232";
        //const string ip = "172.17.42.32";
        // const string ip = "127.0.0.1";
        //const string ip = "172.31.191.200";
        static TcpListener listener;
        public static MainWindow window;
        static public void ServerMain()
        {
            try
            {
                //   Data.IP = "172.17.42.32";
                DataIP.IP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                DataIP.Port = 8890;

                listener = new TcpListener(IPAddress.Parse(DataIP.IP), DataIP.Port);
                listener.Start();
               

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Action action1 = () =>
                    {
                        ClientObject clientObject = new ClientObject(client);
                        ConnectClient(clientObject);
                    };
                    window.Dispatcher.Invoke(action1);
                    //создаем новый поток для обслуживания нового клиента 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
        static async void ConnectClient(ClientObject clientObject)
        {
            await Task.Run(() => clientObject.Process());
        }
    }
}
