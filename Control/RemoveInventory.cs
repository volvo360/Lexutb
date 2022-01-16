using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class ControlRemoveInventory
    {
        public static void removeArticle(string inputInventoryId)
        {
            Program.inventories.RemoveAt(Program.inventories.FindIndex(x => x.InventoryId == inputInventoryId));

            FileManagment.saveRegister();
        }
    }
}
