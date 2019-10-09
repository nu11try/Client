using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoardClient
{
    public class PacksWithTest
    {
        public bool Select { get; set; }
        // ID-шник теста
        public string ID { get; set; }
        // Первончальное имя теста
        public string Name { get; set; }
        // Новое (измененное) имя
        public string NewName { get; set; }
        // Ответственный за выполнение
        public string Count { get; set; }
        // Результат
        public string Result { get; set; }
        public string RestartCount { get; set; }
        public string Time { get; set; }
        public string IP { get; set; }
        public string Status { get; set; }        
    }
}
