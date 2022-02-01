using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    internal class CheckItemsOffice
    {
        static InventoryContext context = new InventoryContext();

        public static bool checkItemsOffice()
        {
            var temp = context.Inventory.Select(x => x.Id == Program.memOffice).ToList().Count();

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
