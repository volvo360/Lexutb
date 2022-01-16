using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    public static class EditInventory
    {
        private static string oldInvId = null;

        public static void editInputInventoryID()
        {
            while (true)
            {
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
                    List<Inventory> exists = Program.inventories.Where(x => x.InventoryId == check.fixedString).ToList();
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
                    var editInventory = Program.inventories.FirstOrDefault(x => x.InventoryId == temp.fixedString);

                    if (editInventory != null)
                    {
                        if (editInventory.GetType().Name == "Computer")
                        {
                           Type myType = Type.GetType("Mini_project_1.Computer");

                            if (myType != null)
                            {
                                Program.fieldsDictInv = AddInventory.GetProperties<Computer, Inventory>();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else  if (editInventory.GetType().Name == "MobilePhone")
                        {
                            Type myType = Type.GetType("Mini_project_1.MobilePhone");

                            if (myType != null)
                            {
                                Program.fieldsDictInv = AddInventory.GetProperties <MobilePhone, Inventory>();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (editInventory.GetType().Name == "Printer")
                        {
                            Type myType = Type.GetType("Mini_project_1.Printer");

                            if (myType != null)
                            {
                                Program.fieldsDictInv = AddInventory.GetProperties<MobilePhone, Inventory>();
                            }
                            else
                            {
                                return;
                            }
                        }

                        //Set some variables used by the user when editing data.

                        oldInvId = editInventory.InventoryId;

                        Program.memInvId = editInventory.InventoryId;
                        Program.memPurchaseDate = editInventory.PurchaseDate;
                        Program.memBrand = editInventory.Brand;
                        Program.memModel = editInventory.Model;
                        Program.memPurchasePrice = editInventory.PurchasePrice;
                        Program.memPurchaseDate = editInventory.PurchaseDate;
                        Program.memCurrency = editInventory.Currency;
                        Program.memOffice = editInventory.Office;
                        Program.memCountry = editInventory.Country;

                        foreach (KeyValuePair<string, object> kvp in Program.fieldsDictInv)
                        {
                            switch (kvp.Key)
                            {
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
                                case "Notebook":
                                    AddInventory.showNotebook();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                case "DualSim":
                                    AddInventory.showDualSIM();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                case "PrintPin":
                                    AddInventory.showPrintPin();

                                    if (!AddInventory.masterbreak)
                                    {
                                        return;
                                    }

                                    break;
                                default:

                                    break;
                            }
                        }

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
