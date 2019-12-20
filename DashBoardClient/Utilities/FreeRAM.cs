using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoardClient
{
    class FreeRAM
    {
        public static void Free()
        {
            GC.Collect();
            GC.GetTotalMemory(true);
        }
    }
}
