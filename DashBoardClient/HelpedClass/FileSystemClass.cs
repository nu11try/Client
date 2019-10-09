using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoardClient.HelpedClass
{
    public class FileSystemClass
    {
        string configName = "\\config.cfg";
        public void WriteConfigFile(string msg)
        {
            using (FileStream fstream = new FileStream(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + configName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(msg);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }
        /// <summary>
        /// Login
        /// Password
        /// User
        /// Token
        /// Security
        /// </summary>
        /// <returns></returns>
        public List<string> ReadConfigFile()
        {
            List<string> response = new List<string>();
            using (StreamReader sr = new StreamReader(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + configName, Encoding.Default))
            {
                string buf;
                string[] bufArray;
                while (!sr.EndOfStream)
                {
                    buf = sr.ReadLine();
                    bufArray = buf.Split(';');
                    for (int i = 0; i < bufArray.Length; i++) response.Add(bufArray[i].Split(':')[1]);
                }
                sr.Close();
            }
            return response;
        }
    }
}
