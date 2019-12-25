using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoardClient
{
    public class AddedTests
    {        
        // ID-шник теста
        public string ID { get; set; }
        // Первончальное имя теста
        public string Name { get; set; }
        // Новое (измененное) имя
        public string NewName { get; set; }
        // Ответственный за выполнение
        public string Author { get; set; }
        // кп
        public string Kp { get; set; }
        public string Sort { get; set; }
    }
}
