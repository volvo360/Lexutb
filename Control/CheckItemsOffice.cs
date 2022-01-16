using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class CheckItemsOffice
    {
        public static bool checkItemsOffice()
        {
            var temp = Program.inventories.Select(x => x.Office == Program.memOffice).ToList().Count();

            if (temp > 0)
            {
                return false;
            }
            else
            { 
                return true;
            }
        }
    }
}
