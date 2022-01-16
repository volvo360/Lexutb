using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class SaveEditInventorys
    {
        

        public static void saveEditInventory(string oldInvId)
        {
            var editInventory = Program.inventories.FirstOrDefault(x => x.InventoryId == oldInvId);

            if (editInventory == null)
            {
                return;
            }

            editInventory.InventoryId = Program.memInvId;
            editInventory.PurchaseDate = Program.memPurchaseDate;
            editInventory.Brand = Program.memBrand;
            editInventory.Model = Program.memModel;
            editInventory.PurchasePrice = Program.memPurchasePrice;
            editInventory.Currency = Program.memCurrency;
            editInventory.Office = Program.memOffice;
            editInventory.Country = Program.memCountry;

            if (editInventory.GetType().Name == "Computer")
            {
                editInventory.setValue("Notebook", Program.memNotebook);
            }
            else if (editInventory.GetType().Name == "MobilePhone")
            {
                editInventory.setValue("DualSim", Program.memNotebook);
            }
            else if (editInventory.GetType().Name == "Printer")
            {
                editInventory.setValue("PrintPin", Program.memNotebook);
            }

            FileManagment.saveRegister();
        }
    }
}
