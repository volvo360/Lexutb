using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    public class SetupDefaultOffice
    {
        static InventoryContext context = new InventoryContext();

        public static void setupDefaultOffice()
        {
            context.ValidOffice.Add(new ValidOffice("Helsingborg", "Sverige", "SEK"));
            context.ValidOffice.Add(new ValidOffice("London", "England", "GBP"));
            context.ValidOffice.Add(new ValidOffice("New York", "Sverige", "USD"));

            context.Category.Add(new Category("computer"));
            context.Category.Add(new Category("mobilephone"));
            context.Category.Add(new Category("printer"));

            context.SaveChanges();

            var tempCat = context.Category.FirstOrDefault(category => category.Type == "computer");
            context.CategoryText.Add(new CategoryText(tempCat.Id, "sv", "Datorer", "Bärbar", "Bärbardator"));

            var CategoryProp = new CategoryProp(tempCat.Id, "notebook", "true");
            
            context.CategoryProp.Add(CategoryProp);
            context.SaveChanges();
            context.CategoryPropText.Add(new CategoryPropText(CategoryProp.Id, "sv", "Bärbar", "Bärbardator"));

            tempCat = context.Category.FirstOrDefault(category => category.Type == "mobilephone");
            context.CategoryText.Add(new CategoryText(tempCat.Id, "sv", "Mobiltelefon", "Dubbla SIM-kort", "Stöd för dubbla SIM-kort"));

            CategoryProp = new CategoryProp(tempCat.Id, "DualSim", "true");

            context.CategoryProp.Add(CategoryProp);
            context.SaveChanges();
            context.CategoryPropText.Add(new CategoryPropText(CategoryProp.Id, "sv", "Dubbla SIM-kort", "Stöd för dubbla SIM-kort"));

            tempCat = context.Category.FirstOrDefault(category => category.Type == "printer");
            context.CategoryText.Add(new CategoryText(tempCat.Id, "sv", "Skrivare", "Stöd för PIN-kod", "SStöd för PIN-kod"));
            CategoryProp = new CategoryProp(tempCat.Id, "PrintPin", "true");

            context.CategoryProp.Add(CategoryProp);
            context.SaveChanges();
            context.CategoryPropText.Add(new CategoryPropText(CategoryProp.Id, "sv", "Stöd för PIN-kod", "Stöd för PIN-kod"));

            context.SaveChanges();

            var temp = context.ValidOffice.FirstOrDefault(office => office.Id == 1);
            var temp2 = context.Category.FirstOrDefault(Category => Category.Type == "computer");

            List<InventoryExtraData> extraData = new List<InventoryExtraData>();

            foreach (var t in temp2.CategoryProps)
            {
                extraData.Add(new InventoryExtraData(t.Id, t.Value));
            }

            context.Inventory.Add(new Inventory(temp2, "COMP-101", DateTime.Today.AddMonths(-30), "HP", "Notebook", temp, 15500, "SEK", extraData));
            
            temp = context.ValidOffice.FirstOrDefault(office => office.Id == 3);
            temp2 = context.Category.FirstOrDefault(Category => Category.Type == "mobilephone");

            extraData = new List<InventoryExtraData>();

            foreach (var t in temp2.CategoryProps)
            {
                extraData.Add(new InventoryExtraData(t.Id, t.Value));
            }

            context.Inventory.Add(new Inventory(temp2, "MOB-101", DateTime.Today.AddMonths(-28), "Apple", "Iphone X", temp, 1435, "USD", extraData));

            temp = context.ValidOffice.FirstOrDefault(office => office.Id == 2);
            temp2 = context.Category.FirstOrDefault(Category => Category.Type == "printer");

            extraData = new List<InventoryExtraData>();

            foreach (var t in temp2.CategoryProps)
            {
                extraData.Add(new InventoryExtraData(t.Id, t.Value));
            }

            context.Inventory.Add(new Inventory(temp2, "PRIN-101", DateTime.Today.AddMonths(-1), "HP", "Laserjet 6L", temp, 347, "EUR", extraData));

            context.SaveChanges();
        }
    }
}
