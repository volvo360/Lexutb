using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    internal class SaveEditInventorys
    {
        static InventoryContext context = new InventoryContext();

        public static void saveEditInventory(string oldInvId)
        {
            var editInventory = context.Inventory.Include(x => x.Category.CategoryProps).FirstOrDefault(x => x.InventoryId == oldInvId);

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

            editInventory.ExtraData = AddInventory.extraData;
            
            editInventory.Office = context.ValidOffice.FirstOrDefault(office => office.Id == Program.memOffice);
            
            context.SaveChanges();
}
    }
}
