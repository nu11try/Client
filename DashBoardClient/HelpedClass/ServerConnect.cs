﻿using System;
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
        string address = Data.IPServer;

        private Request request = new Request();
        string bufJSON;

        string nameText = "";
        byte[] data;
        string param;

        /// <summary>
        /// Функциия для запуска запроса на коннект к серверу
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="service">Сервис</param>
        /// <returns></returns>
        public string SendMsg(string msg, string service)
        {
            while (true)
            {
                try
                {
                    request.Add(msg, service, "");
                    bufJSON = JsonConvert.SerializeObject(request);
                    nameText = "\\" + DateTime.Now.ToString("ddMMyyyyhhmmssfff");
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + nameText, bufJSON);
                    break;
                }
                catch
                {
                    Task.Delay(1000);
                }
            }
            return ConnectServer(bufJSON, nameText);
        }

        public string SendMsg(string msg, string service, string param)
        {
            while (true)
            {
                try
                {
                    request.Add(msg, service, param);
                    bufJSON = JsonConvert.SerializeObject(request);
                    nameText = "\\" + DateTime.Now.ToString("ddMMyyyyhhmmfffss");
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + nameText, bufJSON);
                    break;
                }
                catch
                {
                    Task.Delay(1000);
                }
            }
            return ConnectServer(bufJSON, nameText);
        }

        private string ConnectServer(string json, string nameText)
        {
            TcpClient client = null;
            StringBuilder builder = new StringBuilder();
            string response = "";
            try
            {
                if (Data.IPServer == null)
                {
                    try
                    {
                        Data.IPServer = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\server.conf");
                        if (Data.IPServer.Equals("") || Data.IPServer == null)
                        {
                            Settings settings = new Settings();
                            settings.ShowDialog();
                        }
                    }
                    catch 
                    {
                        Settings settings = new Settings();
                        settings.ShowDialog();
                    }                    
                }
                try
                {
                    client = new TcpClient(Data.IPServer, port);
                }
                catch {
                    MessageBox.Show("Невозможно подключиться к серверу! Смените IP, если это не даст результата, напишите в поддержку!");
                    Settings settings = new Settings();
                    settings.ShowDialog();
                    Data.IPServer = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\server.conf");
                    client = new TcpClient(Data.IPServer, port);
                }
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    try
                    {
                        data = File.ReadAllBytes(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);
                        File.Delete(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);
                        break;
                    }
                    catch
                    {
                        Task.Delay(1000);
                    }
                }                

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
                nameText = "\\" + DateTime.Now.ToString("MMddyyyyhhmmssfff");

                while (true)
                {
                    try
                    {
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + nameText, data);
                        param = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + nameText).Replace("\n", " ");
                        File.Delete(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);
                        break;
                    }
                    catch
                    {
                        Task.Delay(1000);
                    }
                }


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

                nameText = "\\" + DateTime.Now.ToString("ddyyyyhhMMmmssfff");

                while (true)
                {
                    try
                    {
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + nameText, data);
                        param = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + nameText).Replace("\n", " ");
                        File.Delete(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + nameText);
                        break;
                    }
                    catch
                    {
                        Task.Delay(1000);
                    }
                }
                response = param;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
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
