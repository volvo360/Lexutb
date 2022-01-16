using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class SearchInventory
    {
        public static void searchInventory()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Sökning kommer att ske på inventarie id, märke, modell, kontor och land!\n");

                Console.Write("Ange sökord (q = avbryt) : ");

                string inputSearchString = Console.ReadLine().Trim().ToLower();

                if (inputSearchString == "q")
                {
                    return;
                }
                else if (inputSearchString == "" || inputSearchString.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Den inmatade strängen får ej vara tom!!!\n");

                    Console.ResetColor();

                    Console.Write("Tryck på en tangent för att börja om!");
                    Console.ReadKey();

                    continue;

                }
                else
                {
                    var searchId = Program.inventories.FindAll(x => x.InventoryId.ToLower().Contains(inputSearchString)).ToList();

                    var searchBrand = Program.inventories.FindAll(x => x.Brand.ToLower().Contains(inputSearchString)).ToList();

                    var searchModel = Program.inventories.FindAll(x => x.Model.ToLower().Contains(inputSearchString)).ToList();

                    var tempSearchOffice = Program.validOffices.FirstOrDefault(x => (x.Office.ToLower().Contains(inputSearchString) || x.Country.ToLower().Contains(inputSearchString)));

                    var searchOffice = Program.inventories.Where(x => x.Office == tempSearchOffice.OfficeId).ToList();

                    var searchCountry = Program.inventories.FindAll(x => x.Country.ToLower().Contains(inputSearchString)).ToList();

                    var result = searchId;

                    result.AddRange(searchBrand);
                    result.AddRange(searchModel);
                    result.AddRange(searchOffice);
                    result.AddRange(searchCountry);

                    result = result.OrderBy(x => x.PurchaseDate).ThenBy(x => x.GetType().Name.ToString()).ThenBy(x => x.InventoryId).ToList();

                    if (result.Count > 0)
                    {
                        ShowListInventorys.showListInventorys(result);
                    }
                    else
                    {
                        Console.WriteLine("\nTyvärr inget sökresultat, prova med färre ord eller sök ord!\n");
                        Console.Write("Tryck på en tangent för att börja om!");
                        Console.ReadKey();
                    }
                }
                    
            }
        }
    }
}
