using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class MoveInventorysToOffice
    {
        public static void moveInventorysToOffice(int newOffice)
        {
            //Insperation https://stackoverflow.com/a/16051132 2022-01-15
            List<Inventory> result = Program.inventories.OfType<Inventory>().Where(x => x.Office == Program.memOffice).ToList();
            
            foreach (var temp in result)
            { 
                temp.Office = newOffice;   
            }
        }
    }
}
