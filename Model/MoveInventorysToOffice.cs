using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    internal class MoveInventorysToOffice
    {
        static InventoryContext context = new InventoryContext();
        public static void moveInventorysToOffice(int newOffice)
        {
            //Insperation https://stackoverflow.com/a/16051132 2022-01-15
            List<Inventory> result = context.Inventory.OfType<Inventory>().Where(x => x.Id == Program.memOffice).ToList();
            
            foreach (var temp in result)
            { 
                temp.Id = newOffice;   
            }
        }
    }
}
