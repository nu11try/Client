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
        public string ReadConfigFile()
        {
            string config = "";
            using (StreamReader sr = new StreamReader(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + configName, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    config = sr.ReadLine();
                }
                sr.Close();
            }
            return config;
        }
    }
}
