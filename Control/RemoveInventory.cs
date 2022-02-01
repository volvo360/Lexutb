using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    internal class ControlRemoveInventory
    {
        static InventoryContext context = new InventoryContext();
        public static void removeArticle(int inputInventoryId)
        {
            context.Inventory.Remove(new Inventory(inputInventoryId));
            context.SaveChanges();          
        }
    }
}
