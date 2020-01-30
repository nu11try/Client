using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace DashBoardClient
{
    
    class Controller
    {
        private string query = "";
        private static Message res = new Message();
        private StartTests startTests = new StartTests();
        public static MainWindow window;
        public string transformation(string param)
        {
            Message mess = JsonConvert.DeserializeObject<Message>(param);
            if (mess.args[0].Equals("Push"))
            {
                mess.args.RemoveAt(0);
                window.Push(JsonConvert.DeserializeObject<Message>(mess.args[0]));
            }
            if (mess.args[0].Equals("TestsNow"))
            {
                mess.args.RemoveAt(0);
                window.TestsNow(JsonConvert.DeserializeObject<Message>(mess.args[0]));
            }
            return "true";
        }
    }
}
