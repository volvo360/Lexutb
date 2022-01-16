using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace Mini_project_1
{
    internal class AddInventory
    {
        public static string categoryType;

        public static string InventoryId = null;

        public static DateTime PurchaseDate;

        public static string Brand;

        public static string Model;

        public static float PurchasePrice;

        public static string Currency;

        public static int Office;

        public static string Country;

        public static bool Notebook;

        public static bool DualSim = true;

        public static bool PrintPin = true;

        public static bool masterbreak = true;

        //https://stackoverflow.com/a/28444291 2022-01-13

       
       
        public static void showInputInventoryID()
        {
            while (true)
            {
                if (Program.memInvId == null)
                {
                    try
                    {
                        //Insperation https://stackoverflow.com/a/16051132 2022-01-15
                        var xItemsOnly = Program.inventories.OfType<Inventory>().OrderByDescending(s => s.InventoryId).First();

                        Program.memInvId = xItemsOnly.InventoryId;
                    }
                    catch (Exception ex)
                    {
                        //Do nothing.....
                    }
                }

                if (Program.memInvId != null)
                { 
                    var temp = Program.memInvId.Split("-");

                    if (temp.Length > 0)
                    {
                        int newId;

                        if (int.TryParse(temp[1], out newId))
                        {
                            Program.memInvId = temp[0] + "-" + (++newId);
                        }
                    }
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
                    masterbreak = false;

                    return;
                }

                if (inputInventoryId == "")
                {
                    inputInventoryId = Program.memInvId;
                }

                var check = CheckSyntax.check("MastersettingsInventory", inputInventoryId);

                if (check.correct)
                {

                    List <Inventory> exists = Program.inventories.Where(x => x.InventoryId == check.fixedString).ToList();
                    if (exists.Count > 0)
                    {
                        Console.WriteLine("Tyvärr används redan det inventari id av :");

                        ShowListInventorys.showListInventorys(exists);
                    }
                    else
                    {
                        Program.memInvId = InventoryId = check.fixedString;
                        break;
                    }
                }
            }
        }

        public static void showPurchaseDate()
        {
            while (true)
            {
                if (Program.memPurchaseDate != default(DateTime))
                {
                    Console.Write($"Ange inköpsdatum  ({Program.memPurchaseDate.ToShortDateString()}) : ");
                }
                else
                {
                    Console.Write($"Ange inköpsdatum  ({DateTime.Now.ToShortDateString()}) : ");
                }

                
                var inputPurchaseDate = Console.ReadLine().Trim();

                DateTime dDate;

                if (inputPurchaseDate.ToLower() == "q")
                {
                    masterbreak = false;

                    return;
                }

                if (inputPurchaseDate == "" && (Program.memPurchaseDate == default(DateTime)))
                {
                    PurchaseDate = dDate = Program.memPurchaseDate = DateTime.Now;
                    break;
                }
                else if (inputPurchaseDate == "")
                {
                    PurchaseDate = dDate = Program.memPurchaseDate;
                    break;
                }

                else if (DateTime.TryParse(inputPurchaseDate, out dDate))
                {
                    if (CommonFunction.GetDifferenceInYears(dDate, DateTime.Today) >= MasterSettings.depreciationPeriod)
                    {
                        Console.WriteLine("Du försöker att addera en produkt som är utgallrad enligt gällande regler!!!");
                    }
                    else
                    {
                        Program.memPurchaseDate = PurchaseDate = dDate;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt datum format!");
                }
            }
        }
        public static void showBrand()
        {
            while (true)
            {
                if (Program.memBrand != null)
                {
                    Console.Write($"Ange tillverkaren av inventarien ({Program.memBrand}) : ");
                }
                else
                {
                    Console.Write($"Ange tillverkaren av inventarien : ");
                }
                var inputBrand = Console.ReadLine().Trim();

                if (inputBrand.ToLower() == "q")
                {
                    masterbreak = false;
                    return;
                }

                else if (inputBrand == "" && Program.memBrand != null)
                {
                    Brand = Program.memBrand;
                    break;
                }

                else if (inputBrand.Length > 1)
                {
                    Brand = Program.memBrand = inputBrand;
                    break;
                }
                else
                {
                    Console.WriteLine("Ange en giltig tillverkare!");
                }
            }
        }
        public static void showModel()
        {
            while (true)
            {
                if (Program.memModel != null)
                {
                    Console.Write($"Ange modell av inventarien ({Program.memModel}) : ");
                }
                else
                {
                    Console.Write($"Ange modell av inventarien : ");
                }
                var inputModel = Console.ReadLine().Trim();

                if (inputModel.ToLower() == "q")
                {
                    masterbreak = false;
                    return;
                }

                else if (inputModel == "" && Program.memModel != null)
                {
                    Model = Program.memModel;
                    break;
                }

                else if (inputModel.Length > 1)
                {
                    Model = Program.memModel = inputModel;
                    break;
                }
                else
                {
                    Console.WriteLine("Ange en giltig modell!");
                }
            }
        }

        public static void showOffice()
        {
            while (true)
            {
                if (Program.memOffice >= 0)
                {
                    var temp = Program.validOffices.ElementAt(Program.memOffice);

                    Console.Write($"Välj kontor ({temp.Office}) : ");
                }
                else
                {
                    Console.Write($"Välj kontor : ");
                }
                var inputOffice = Console.ReadLine().Trim();

                if (inputOffice.ToLower() == "q")
                {
                    masterbreak = false;
                    return;
                }

                else if (inputOffice == "" && Program.memOffice >= 0)
                {
                    break;

                }
                else if (inputOffice.Length > 0)
                {
                    int choice = 0;

                    if (int.TryParse(inputOffice, out choice))
                    {
                        --choice;

                        try
                        {
                            var temp = Program.validOffices.ElementAt(choice);
                            if (temp != null)
                            {
                                Program.memOffice = choice;
                                Office = temp.OfficeId;
                                Currency = Program.memCurrency = temp.Currency;
                                Country = Program.memCountry = temp.Country;

                                break;
                            }
                        }

                        catch
                        {
                            Console.WriteLine("Giltiga kontor är :\n");

                            int i = 1;

                            foreach (var office in Program.validOffices)
                            {
                                Console.WriteLine($"{i} {office.Office}");
                                i++;
                            }
                        }
                    }
                    else
                    {
                        var res = Program.validOffices.FirstOrDefault(i => i.Office == CommonFunction.FirstLetterToUpper(inputOffice));

                        if (res != null)
                        {
                            Program.memOffice = Program.validOffices.FindIndex(i => i.Office == CommonFunction.FirstLetterToUpper(inputOffice));

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Giltiga kontor är :\n");

                            int i = 1;

                            foreach (var office in Program.validOffices)
                            {
                                Console.WriteLine($"{i} {office.Office}");
                                i++;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Giltiga kontor är :\n");

                    int i = 1;

                    foreach (var office in Program.validOffices)
                    {
                        Console.WriteLine($"{i} {office.Office}");
                        i++;
                    }
                }
            }
        }

        public static void showPurchasePrice()
        {
            while (true)
            {
                if (Program.memPurchasePrice > 0)
                {
                    Console.Write($"Ange inköpspriset på inventarien ({Program.memPurchasePrice}) : ");
                }
                else
                {
                    Console.Write($"Ange inköppspriset på inventarien : ");
                }
                var inputPrice = Console.ReadLine().Trim();

                if (inputPrice.ToLower() == "q")
                {
                    masterbreak = false;
                    return;
                }

                else if (inputPrice == "" && Program.memPurchasePrice > 0)
                {
                    PurchasePrice = Program.memPurchasePrice;
                    break;
                }

                else if (float.TryParse(inputPrice, out PurchasePrice))
                {
                    if (PurchasePrice > 0)
                    {
                        Program.memPurchasePrice = PurchasePrice;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ange ett giltigt inköppspris"); 
                    }
                }
                else
                {
                    Console.WriteLine("Ange ett giltigt inköppspris"); 
                }
            }
        }

        public static void showNotebook()
        {
            while (true)
            { 
                if (Program.memNotebook)
                {
                    Console.Write($"Datorn är bärbar (J/n/q = avbrytt) : ");
                }
                else 
                {
                    Console.Write($"Datorn är bärbar (j/N/q = avbrytt) ");
                }

                string inputChoiceNotebook = Console.ReadLine().Trim().ToLower();

                if (inputChoiceNotebook == "q")
                {
                    masterbreak = false;
                    return;
                }
                else if (inputChoiceNotebook == "")
                {
                    Notebook = Program.memNotebook;
                    break;
                }
                else if (inputChoiceNotebook == "j")
                {
                    Notebook = Program.memNotebook = true;
                    break;
                }
                else if (inputChoiceNotebook == "n")
                {
                    Notebook = Program.memNotebook = false;
                    break;
                }
                else
                { 
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Vänligen svara j/n/q ¨på frågan!\n");

                    Console.ResetColor();

                    Console.Write("Tryck på en tangent för att börja om!");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        public static void showDualSIM()
        {
            while (true)
            {
                if (Program.memNotebook)
                {
                    Console.Write($"Mobilen har stöd för dubbla SIM kort (J/n/q = avbrytt) : ");
                }
                else
                {
                    Console.Write($"Mobilen har stöd för dubbla SIM kort (j/N/q = avbrytt) : ");
                }

                string inputChoiceDualSIM= Console.ReadLine().Trim().ToLower();

                if (inputChoiceDualSIM == "q")
                {
                    masterbreak = false;
                    return;
                }
                else if (inputChoiceDualSIM == "")
                {
                    Notebook = Program.memDualSIM;
                    break;
                }
                else if (inputChoiceDualSIM == "j")
                {
                    Notebook = Program.memDualSIM = true;
                    break ;
                }
                else if (inputChoiceDualSIM == "n")
                {
                    Notebook = Program.memDualSIM = false;
                    break ;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Vänligen svara j/n/q ¨på frågan!\n");

                    Console.ResetColor();

                    Console.Write("Tryck på en tangent för att börja om!");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        public static void showPrintPin()
        {
            while (true)
            {
                if (Program.memNotebook)
                {
                    Console.Write($"Skrivaren har stöd för PIN kod vid utskrift (J/n/q = avbrytt) : ");
                }
                else
                {
                    Console.Write($"Skrivaren har stöd för PIN kod vid utskrift (j/N/q = avbrytt) : ");
                }

                string inputPrintPin = Console.ReadLine().Trim().ToLower();

                if (inputPrintPin == "q")
                { 
                    masterbreak = false;
                    return;
                }
                else if (inputPrintPin == "")
                {
                    Notebook = Program.memPrintPin;
                    break;
                }
                else if (inputPrintPin == "j")
                {
                    Notebook = Program.memPrintPin = true;
                    break;
                }
                else if (inputPrintPin == "n")
                {
                    Notebook = Program.memPrintPin = false;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Vänligen svara j/n/q ¨på frågan!\n");

                    Console.ResetColor();

                    Console.Write("Tryck på en tangent för att börja om!");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        //https://stackoverflow.com/a/52646457 Last comment 2022-01-14

        public static Dictionary<string, object> GetProperties<Derived, Base>()
        {
            var onlyInterestedInTypes = new[] { typeof(Derived).Name, typeof(Base).Name };

            return Assembly
                .GetAssembly(typeof(Derived))
                .GetTypes()
                .Where(x => onlyInterestedInTypes.Contains(x.Name))
                .OrderBy(x => x.IsSubclassOf(typeof(Base)))
                .SelectMany(x => x.GetProperties())
                .GroupBy(x => x.Name)
                .Select(x => x.First())
                .ToDictionary(x => x.Name, x => (object)x.Name);
        }

        public static void showAddArticle(string categoryType)
        {
            if (categoryType == null)
            {
                return;
            }

            if (categoryType.ToLower() == "computer")
            {
                Type myType = Type.GetType("Mini_project_1.Computer");

                if (myType != null)
                {
                    Program.fieldsDictInv = GetProperties<Computer, Inventory>();
                }
                else
                {
                    return ;
                }
            }
            else if (categoryType.ToLower() == "mobilephone")
            {
                Type myType = Type.GetType("Mini_project_1.MobilePhone");

                if (myType != null)
                {
                    Program.fieldsDictInv = GetProperties<MobilePhone, Inventory>();
                }
                else
                {
                    return;
                }
            }
            else if (categoryType.ToLower() == "printer")
            {
                Type myType = Type.GetType("Mini_project_1.Printer");

                if (myType != null)
                {
                    Program.fieldsDictInv = GetProperties<Printer, Inventory>();
                }
                else
                {
                    return;
                }
            }

            foreach (KeyValuePair<string, object> kvp in Program.fieldsDictInv)
            {
                switch (kvp.Key)
                {
                    case "InventoryId":
                        showInputInventoryID();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    
                    case "PurchaseDate":
                        showPurchaseDate();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    case "Brand":
                        showBrand();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    case "Model":
                        showModel();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    case "PurchasePrice":
                        showPurchasePrice();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;

                    case "Currency":
                        //showCurrency();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    case "Office":
                        showOffice();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    case "Country":
                        //showCountry();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    case "Notebook":
                        showNotebook();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    case "DualSim":
                        showDualSIM();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;
                    case "PrintPin":
                        showPrintPin();

                        if (!masterbreak)
                        {
                            return;
                        }

                        break;

                    default:
                        
                        break;

                }
            }

            if (categoryType.ToLower() == "computer")
            {
                Program.inventories.Add(new Computer(InventoryId, PurchaseDate, Brand, Model, PurchasePrice, Currency, Office, Country, Notebook));

            }
            else if (categoryType.ToLower() == "mobilephone")
            {
                Program.inventories.Add(new MobilePhone(InventoryId, PurchaseDate, Brand, Model, PurchasePrice, Currency, Office, Country, DualSim));

            }
            else if (categoryType.ToLower() == "printer")
            {
                Program.inventories.Add(new Printer(InventoryId, PurchaseDate, Brand, Model, PurchasePrice, Currency, Office, Country, PrintPin));
            }

            FileManagment.saveRegister();
        }

        private static void resetMemParameters()
        {
            //Reset some variables used by the user when entering data.

            Program.memInvId = null;
            Program.memPurchaseDate = default(DateTime);
            Program.memBrand = null;
            Program.memModel = null;
            Program.memOffice = -1;
            Program.memPurchasePrice = 0;
            Program.memNotebook = true;
            Program.memDualSIM = true;
            Program.memPrintPin = false;
            /*Program.memCurrency = "SEK";
            Program.memOffice = null;
            Program.memCountry = null;*/
    }

        public static void addArticle()
        {
            masterbreak = true;

            Dictionary<string, string> inventoryCategorys = new Dictionary<string, string>(){{"dator", "Computer" },
                { "mobiltelefon", "MobilePhone"}, { "skrivare", "Printer"}};

            categoryType = null;
            while (true)
            {
                if (!masterbreak)
                {
                    return;
                }

                while (true)
                {
                    Console.Clear();

                    var dictKey = inventoryCategorys.FirstOrDefault(x => x.Value.ToLower() == Program.defaultType.ToLower()).Key;

                    if (Program.defaultType != null && dictKey != null)
                    {

                        Console.Write("Vilken typ av artikel vill du addera (");

                        Console.Write(dictKey);

                        Console.Write(" / q = avbryt) ");
                    }

                    else
                    {
                        Console.Write("Vilken typ av kategori vill du addera (q = avbryt)? ");
                    }

                    categoryType = Console.ReadLine().Trim().ToLower();

                    int dictonaryKey = 0;

                    if (categoryType == "")
                    {
                        categoryType = Program.defaultType;
                        break;
                    }
                    else if (inventoryCategorys.ContainsKey(categoryType))
                    {
                        categoryType = Program.defaultType = inventoryCategorys[categoryType];
                        resetMemParameters();
                        break;
                    }
                    else if (int.TryParse(categoryType, out dictonaryKey))
                    {
                        --dictonaryKey;

                        try
                        {
                            categoryType = Program.defaultType = inventoryCategorys.ElementAt(dictonaryKey).Value;
                            resetMemParameters();
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Du angav en siffra som inte har någon koppling till en kategori av inventarier!");
                            Console.Write("Tryck på en tangent för att försöka på nyttt!");

                            Console.ReadKey();
                        }

                    }

                    else if (categoryType == "q")
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Välj en av nedanstående typer av arktiklar du vill registera :");

                        int i = 1;

                        foreach (KeyValuePair<string, string> kvp in inventoryCategorys)
                        {
                            Type myType = Type.GetType("Mini_project_1." + kvp.Value);

                            if (myType != null)
                            {
                                Console.WriteLine($"{i}" + " " + kvp.Key);

                                i++;
                            }

                        }

                        Console.Write("Tryck på en tangent för att försöka på nyttt!");

                        Console.ReadKey();
                    }
                }

                showAddArticle(categoryType);

                if (!masterbreak)
                {
                    return;
                }

                while (true)
                {
                    Console.Write("Vill du addera fler inventarier (J/n)? ");

                    var continueRun = Console.ReadLine().Trim().ToLower();

                    if (continueRun == "" || continueRun == "j")
                    {
                        break;
                    }
                    else if (continueRun == "n")
                    {
                        return;
                    }
                }
            }
        }
    }
}
