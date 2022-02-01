using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    public class ListInventorys
    {
        static InventoryContext context = new InventoryContext();

        static public void listInventorys()
        {
            List <string> options = new List<string>();

            options.Add( "(K)ategorier");
            options.Add( "(i)nköppsdatum");
            options.Add( "(p)lacering");
            options.Add( "(l)and");
            options.Add( "(in)ventarieid");
            options.Add( "(t)illverkare");
            options.Add( "(m)odell");

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Hur vill du presentera inventarierna, vänlig välja av nedanstående sätt (q = avbryt):\n");

                foreach (var option in options)
                {
                    Console.WriteLine(option);
                }

                Console.Write("\nDitt val : ");

                var inputChoice = Console.ReadLine().Trim().ToLower();

                Console.Clear();

                if (inputChoice == "q")
                {
                    return;
                }

                else if (inputChoice == "" || inputChoice == "k")
                {
                    var sortedListCategory = context.Inventory.Include(x => x.Category).Include(x => x.Office).Include(x => x.Category.CategoryTexts).Include(x => x.Category.CategoryTexts).Include(x => x.Category.CategoryProps).Include(x => x.Category.CategoryProps).ThenInclude(x => x.CategoryPropTexts).Include(x => x.ExtraData).OrderBy(x => x.Category.Name).ThenBy(x => x.InventoryId).ToList();

                    ShowListInventorys.showListInventorys(sortedListCategory);
                }
                else if ( inputChoice == "i" || inputChoice == "inköpsdatum")
                {
                    var sortedListCategory = context.Inventory.Include(x => x.Category).Include(x => x.Office).Include(x => x.Category.CategoryTexts).Include(x => x.Category.CategoryProps).ThenInclude(x => x.CategoryPropTexts).Include(x => x.ExtraData).OrderBy(x => x.PurchaseDate).ThenBy(x => x.GetType().Name).ThenBy(x => x.Id).ToList();

                    ShowListInventorys.showListInventorys(sortedListCategory);
                }
                else if (inputChoice == "p" || inputChoice == "placering")
                {
                    var sortedListCategory = context.Inventory.Include(x => x.Category).Include(x => x.Office).Include(x => x.Category.CategoryTexts).Include(x => x.Category.CategoryProps).ThenInclude(x => x.CategoryPropTexts).Include(x => x.ExtraData).OrderBy(x => x.Office).ThenBy(x => x.Office.Country).ThenBy(x => x.PurchaseDate).ToList();
                    ShowListInventorys.showListInventorys(sortedListCategory);
                }
                else if (inputChoice == "in" || inputChoice == "inventarieid")
                {
                    var sortedListCategory = context.Inventory.Include(x => x.Category).Include(x => x.Office).Include(x => x.Category.CategoryTexts).Include(x => x.Category.CategoryProps).ThenInclude(x => x.CategoryPropTexts).Include(x => x.ExtraData).OrderBy(x => x.InventoryId).ThenBy(x => x.PurchaseDate).ToList();

                    ShowListInventorys.showListInventorys(sortedListCategory);
                }
                else if (inputChoice == "t" || inputChoice == "tillverkare")
                {
                    var sortedListCategory = context.Inventory.Include(x => x.Category).Include(x => x.Office).Include(x => x.Category.CategoryTexts).Include(x => x.Category.CategoryProps).ThenInclude(x => x.CategoryPropTexts).Include(x => x.ExtraData).OrderBy(x => x.Brand).ThenBy(x => x.Model).ThenBy(x => x.PurchaseDate).ToList();
                    ShowListInventorys.showListInventorys(sortedListCategory);
                }
                else if (inputChoice == "m" || inputChoice == "modell")
                {
                    var sortedListCategory = context.Inventory.Include(x => x.Category).Include(x => x.Office).Include(x => x.Category.CategoryTexts).Include(x => x.Category.CategoryProps).ThenInclude(x => x.CategoryPropTexts).Include(x => x.ExtraData).OrderBy(x => x.Model).ThenBy(x => x.Brand).ThenBy(x => x.PurchaseDate).ToList();
                    ShowListInventorys.showListInventorys(sortedListCategory);
                }
            }
        }
    }
}
