using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    public static class EditInventory
    {
        private static string oldInvId = null;

        private static string oldCategory = null;

        static InventoryContext context = new InventoryContext();

        public static void editArticle()
        {
            AddInventory.masterbreak = true;

            //Dictionary<string, string> inventoryCategorys = new Dictionary<string, string>(){{"dator", "Computer" },
            //{ "mobiltelefon", "MobilePhone"}, { "skrivare", "Printer"}};

            Dictionary<string, string> inventoryCategorys = new Dictionary<string, string>();

            var temp = context.Category.Include(z => z.CategoryTexts).ToList();

            int i = 0; ;

            foreach (var item in temp)
            {
                inventoryCategorys[item.CategoryTexts.Name.ToString()] = item.Type.ToString();
                
                i++;
            }

            AddInventory.categoryType = null;
            if (!AddInventory.masterbreak)
            {
                return;
            }

            while (true)
            {
                var dictKey = inventoryCategorys.FirstOrDefault(x => x.Value.ToLower() == Program.defaultType.ToLower()).Key;

                if (Program.defaultType != null && dictKey != null)
                {

                    Console.Write("Ange kategori för produkten (");

                    Console.Write(dictKey);

                    Console.Write(" / q = avbryt) ");
                }

                else
                {
                    Console.Write("Ange kategori för produkten (q = avbryt)? ");
                }

                AddInventory.categoryType = Console.ReadLine().Trim().ToLower();

                int dictonaryKey = 0;

                if (AddInventory.categoryType == "")
                {
                    AddInventory.categoryType = Program.defaultType;
                    break;
                }
                else if (int.TryParse(AddInventory.categoryType, out dictonaryKey))
                {
                    var temp2 = context.Category.FirstOrDefault(category => category.Id == dictonaryKey);

                    if (temp2 == null)
                    {
                        Console.WriteLine("Du angav en siffra som inte har någon koppling till en kategori av inventarier!\n");

                        i = 1;

                        foreach (KeyValuePair<string, string> kvp in inventoryCategorys)
                        {
                            Console.WriteLine($"{i}" + " " + kvp.Key);

                            i++;
                        }

                        Console.Write("\nTryck på en tangent för att försöka på nyttt!");

                        Console.ReadKey();

                        continue;
                    }

                    AddInventory.categoryType = Program.defaultType = temp2.Type;
                    break;
                }

                else if (AddInventory.categoryType == "q")
                {
                    return;
                }
                else
                {
                    var temp3 = context.Category.FirstOrDefault(x => x.Name.ToLower() == AddInventory.categoryType.ToLower());

                    if (temp3 == null)
                    {
                        temp3 = context.Category.FirstOrDefault(x => x.Type.ToLower() == AddInventory.categoryType.ToLower());
                    }

                    if (temp3 != null)
                    {
                        AddInventory.categoryType = Program.defaultType = temp3.Type;
                        break;
                    }

                    Console.WriteLine("Välj en av nedanstående typer av arktiklar du vill registera :");

                    i = 1;

                    foreach (KeyValuePair<string, string> kvp in inventoryCategorys)
                    {
                        Console.WriteLine($"{i}" + " " + kvp.Key);

                        i++;
                    }

                    Console.Write("Tryck på en tangent för att försöka på nyttt!");

                    Console.ReadKey();
                }
            }
        }

        public static void editInputInventoryID()
        {
            while (true)
            {
                if (oldCategory.ToLower() != AddInventory.categoryType.ToLower())
                { 
                    var xItemsOnly = context.Inventory.Include(x => x.Category).Where(x => x.Category.Type == AddInventory.categoryType).OrderByDescending(s => s.Id).First();

                    var temp = xItemsOnly.InventoryId.Split('-');

                    if (temp[1].All(char.IsDigit))
                    {
                        int t = 0;

                        int.TryParse(temp[1], out t);

                        temp[1] = (++t).ToString();

                        xItemsOnly.InventoryId = temp[0] + "-" + temp[1];
                }

                    Program.memInvId = xItemsOnly.InventoryId;
                }

                if (Program.memInvId != null)
                {
                    Console.Write($"Ange inventarinummer i formatet ABC-(# mellan 100-1000) ({Program.memInvId}) : ");
                }
                else
                {
                    Console.Write("Ange inventarinummer i formatet ABC-(# mellan 100-1000) : ");
                }

                var inputInventoryId = Console.ReadLine().Trim();

                if (inputInventoryId.ToLower() == "q")
                {
                    AddInventory.masterbreak = false;

                    return;
                }

                if (inputInventoryId == "")
                {
                    inputInventoryId = Program.memInvId;
                }

                var check = CheckSyntax.check("MastersettingsInventory", inputInventoryId);

                if (check.correct)
                {
                    var exists = context.Inventory.Where(x => x.InventoryId == check.fixedString).ToList();
                    if (exists.Count > 0 && Program.memInvId != inputInventoryId)
                    {
                        Console.WriteLine("Tyvärr används redan det inventari id av :");

                        ShowListInventorys.showListInventorys(exists);
                    }
                    else
                    {
                        Program.memInvId = inputInventoryId = check.fixedString; ;
                        break;
                    }
                }
            }
        }
        
        public static void editInventory()
        {
            while (true)
            { 
                Console.Clear();

                Console.Write("Vilken artikel vill du editera (q = avbrytt)? ");

                string inventoryToEdit = Console.ReadLine().Trim();

                if (inventoryToEdit.ToLower() == "q")
                {
                    break;
                }

                else if (inventoryToEdit.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Den inmatade strängen får ej vara tom!!!\n");

                    Console.ResetColor();

                    Console.Write("Tryck på en tangent för att börja om!");
                    Console.ReadKey();

                    continue;
                }

                var temp = CheckSyntax.check("MastersettingsInventory", inventoryToEdit);

                if (!temp.correct)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Den inmatade strängen uppfyller inte min kravet på ABC-(# mellan 100-1000)!!!\n");

                    Console.ResetColor();

                    Console.Write("Tryck på en tangent för att börja om!");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    var editInventory = context.Inventory.Include(x => x.Category).Include(x => x.Office).FirstOrDefault(x => x.InventoryId == temp.fixedString);

                    if (editInventory != null)
                    {
                        //Set some variables used by the user when editing data.

                        oldInvId = editInventory.InventoryId;

                        Program.memInvId = editInventory.InventoryId;
                        Program.memPurchaseDate = editInventory.PurchaseDate;
                        Program.memBrand = editInventory.Brand;
                        Program.memModel = editInventory.Model;
                        Program.memPurchasePrice = editInventory.PurchasePrice;
                        Program.memPurchaseDate = editInventory.PurchaseDate;
                        Program.memCurrency = editInventory.Currency;
                        Program.memOffice = editInventory.Office.Id;
                        Program.oldCategory = oldCategory =  editInventory.Category.Type.ToString();

                        var tem = new Inventory();

                        var temp2 = tem.GetType().GetProperties()
                                        .Select(field => field.Name)
                                        .ToList();

                        foreach (var variable in temp2)
                        {
                            switch (variable)
                            {
                                case "Category":
                                    editArticle();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;

                                case "InventoryId":
                                    editInputInventoryID();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;

                                case "PurchaseDate":
                                    AddInventory.showPurchaseDate();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                case "Brand":
                                    AddInventory.showBrand();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                case "Model":
                                    AddInventory.showModel();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                case "PurchasePrice":
                                    AddInventory.showPurchasePrice();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;

                                case "Currency":
                                    //AddArticle.showCurrency();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                case "Office":
                                    AddInventory.showOffice();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                case "Country":
                                    //AddArticle.showCountry();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                default:

                                    break;
                            }
                        }

                        AddInventory.showExtraParameters(editInventory.ExtraData);

                        SaveEditInventorys.saveEditInventory(oldInvId);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Angivet inventarie id finns inte!\n");

                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
