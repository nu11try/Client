using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DashBoardClient
{
    class ServerConnect
    {
        const int port = 8888;
        const string address = "172.31.197.232";
        //const string address = "127.0.0.1";

        /// <summary>
        /// Функциия для запуска запроса на коннект к серверу
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="service">Сервис</param>
        /// <returns></returns>
        public string SendMsg(string msg, string service)
        {
            return ConnectServer(msg, service, "");
        }

        public string SendMsg(string msg, string service, string param)
        {
            return ConnectServer(msg, service, param);
        }

        public string Auth(string login, string password)
        {
            return ConnectServer("auth@" + login + "±" + password, "", "");
        }

        /// <summary>
        /// Функция для подключения к серверу
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        private string ConnectServer(string msg, string service, string param)
        {
            TcpClient client = null;
            StringBuilder builder = new StringBuilder();
            string response = "";
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                // преобразуем сообщение в массив байтов
                byte[] data = new byte[] { };
                if (param.Length == 0) data = Encoding.Unicode.GetBytes(msg + "@" + service);
                else data = Encoding.Unicode.GetBytes(msg + "@" + service + "@" + param);
               
                // отправка сообщения
                stream.Write(data, 0, data.Length);

                // получаем ответ
                data = new byte[5042]; // буфер для получаемых данных
                
                int bytes = 0;
                              
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                response = builder.ToString();

                builder.Clear();                
                stream.Close();
                client.Close();
                
                //MessageBox.Show(response);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return response;
        }
    }
}
