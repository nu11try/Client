using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace DashBoardClient
{
    class ServerConnect
    {
        const int port = 8888;
        //const string address = "172.17.42.40";
        const string address = "172.31.197.232";

        private Request request = new Request();
        string bufJSON;
        string nameText;
        Random rnd = new Random();

        /// <summary>
        /// Функциия для запуска запроса на коннект к серверу
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="service">Сервис</param>
        /// <returns></returns>
        public string SendMsg(string msg, string service)
        {
            request.Add(msg, service, "");
            bufJSON = JsonConvert.SerializeObject(request);
            nameText = "\\" + rnd.Next() + ".txt";
            File.Delete(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + nameText, bufJSON);
            return ConnectServer(bufJSON);
        }

        public string SendMsg(string msg, string service, string param)
        {
            request.Add(msg, service, param);
            bufJSON = JsonConvert.SerializeObject(request);
            nameText = "\\" + rnd.Next() + ".txt";
            File.Delete(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + nameText, bufJSON);
            return ConnectServer(bufJSON);
        }

        private string ConnectServer(string json)
        {
            TcpClient client = null;
            StringBuilder builder = new StringBuilder();
            string response = "";
            try
            {
                /*
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                // преобразуем сообщение в массив байтов
                byte[] data = new byte[] { };
                data = Encoding.Unicode.GetBytes(json);
               
                // отправка сообщения
                stream.Write(data, 0, data.Length);

                // получаем ответ
                data = new byte[9999999]; // буфер для получаемых данных
                
                int bytes = 0;
                              
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                response = builder.ToString();

                builder.Clear();                
                stream.Close();
                client.Close();*/

                //MessageBox.Show(response);
                //MessageBox.Show(json);

                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                byte[] data = File.ReadAllBytes(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);
                File.Delete(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);

                int bufferSize = 1024;
                byte[] dataLength = BitConverter.GetBytes(data.Length);
                stream.Write(dataLength, 0, 4);
                int bytesSent = 0;
                int bytesLeft = data.Length;
                while (bytesLeft > 0)
                {
                    int curDataSize = Math.Min(bufferSize, bytesLeft);
                    stream.Write(data, bytesSent, curDataSize);
                    bytesSent += curDataSize;
                    bytesLeft -= curDataSize;
                }
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + nameText, data);
                string param = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + nameText).Replace("\n", " ");
                File.Delete(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);

                nameText = "\\" + rnd.Next() + ".txt";
                byte[] fileSizeBytes = new byte[4];
                int bytes = stream.Read(fileSizeBytes, 0, 4);
                int dataLengthResponse = BitConverter.ToInt32(fileSizeBytes, 0);
                bytesLeft = dataLengthResponse;
                data = new byte[dataLengthResponse];
                int bytesRead = 0;
                while (bytesLeft > 0)
                {
                    int curDataSize = Math.Min(bufferSize, bytesLeft);
                    if (client.Available < curDataSize)
                        curDataSize = client.Available; //This saved me
                    bytes = stream.Read(data, bytesRead, curDataSize);
                    bytesRead += curDataSize;
                    bytesLeft -= curDataSize;
                }
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + nameText, data);
                param = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + nameText).Replace("\n", " ");
                File.Delete(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);
                response = param;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            request = new Request();
            bufJSON = "";
            return response;
        }
    }

    public class Request
    {
        public Request()
        {
            args = new List<string>();
        }

        public void Add(params string[] tmp)
        {
            for (int i = 0; i < tmp.Length; i++)
            {
                args.Add(tmp[i]);
            }
        }
        public List<string> args { get; set; }
    }
}
