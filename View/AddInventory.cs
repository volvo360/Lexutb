using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Mini_project_2
{
    public class AddInventory
    {
        static InventoryContext context = new InventoryContext();

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

        public static List<InventoryExtraData> extraData = new List<InventoryExtraData>();

       
        public static void showInputInventoryID()
        {
            while (true)
            {
                if (Program.memInvId == null)
                {
                    try
                    {
                        var xItemsOnly = context.Inventory.Include(x => x.Category).Where(x => x.Category.Type == categoryType).OrderByDescending(s => s.Id).First();

                        var temp = xItemsOnly.InventoryId.Split('-');

                        if (temp[1].All(char.IsDigit)) 
                        {
                            int t = 0;

                            int.TryParse(temp[1], out t);

                            temp[1] = (++t).ToString();
                        }

                        Program.memInvId = temp[0] + "-" + temp[1];
                    }
                    catch (Exception ex)
                    {
                        //Do nothing.....
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

                    List <Inventory> exists = context.Inventory.Where(x => x.InventoryId == check.fixedString).ToList();
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
                    var temp = context.ValidOffice.FirstOrDefault(x => x.Id == Program.memOffice);

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
                        var temp = context.ValidOffice.FirstOrDefault(x => x.Id == choice);
                        if (temp != null)
                        {
                            Program.memOffice = choice;
                            Office = temp.Id;
                            Currency = Program.memCurrency = temp.Currency;
                            Country = Program.memCountry = temp.Country;

                            Console.Write("Du har valt \""+temp.Office+"\", bekräfta genom att trycka på enter, annars q för nytt val ");

                            string inputValidateOffice = Console.ReadLine().Trim().ToLower();

                            if (inputValidateOffice == "")
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Giltiga kontor är :\n");

                            foreach (var office in context.ValidOffice.OrderBy(x => x.Office))
                            {
                                Console.WriteLine($"{office.Id} {office.Office}");
                            }
                        }
                    }
                    else
                    {
                        var res = context.ValidOffice.FirstOrDefault(i => i.Office == CommonFunction.FirstLetterToUpper(inputOffice));

                        if (res != null)
                        {
                            Program.memOffice = context.ValidOffice.FirstOrDefault(i => i.Office == CommonFunction.FirstLetterToUpper(inputOffice)).Id;
                            Office = res.Id;
                            Currency = Program.memCurrency = res.Currency;
                            Country = Program.memCountry = res.Country;

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Giltiga kontor är :\n");

                            foreach (var office in context.ValidOffice.OrderBy(x => x.Office))
                            {
                                Console.WriteLine($"{office.Id} {office.Office}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Giltiga kontor är :\n");

                    foreach (var office in context.ValidOffice.OrderBy(x => x.Office))
                    {
                        Console.WriteLine($"{office.Id} {office.Office}");
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
                    Console.Write($"Ange inköpspriset på inventarien ({Program.memPurchasePrice}) ({Currency}) : ");
                }
                else
                {
                    Console.Write($"Ange inköppspriset på inventarien ({Currency}): ");
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

        public static void showExtraParameters(List<InventoryExtraData> defaultData = null)
        {
            var insertCategory = context.Category.Include(x => x.CategoryProps).ThenInclude(x => x.CategoryPropTexts).Include(x => x.CategoryTexts).FirstOrDefault(category => category.Type == categoryType);

            var defaultChoice = "N";

            String[] questionText = null;

            if (insertCategory == null)
            {
                return;
            }

            int i = 0;

            foreach (var extra in insertCategory.CategoryProps)
            {
                while (true)
                {
                    if (defaultData != null)
                    {
                        var temp = defaultData.FirstOrDefault(x => x.CategoryPropId == insertCategory.CategoryProps[i].Id);

                        if (insertCategory.CategoryProps[i].Value == "true" || insertCategory.CategoryProps[i].Value == "false")
                        {
                            Console.Write(extra.CategoryPropTexts.ExtraInsertText + " ");

                            if (temp != null)
                            {
                                if (temp.Value == "true")
                                {
                                    defaultChoice = "j";
                                    Console.Write("(J/n/q = avbryt) ? ");
                                }
                                else if (insertCategory.CategoryProps[i].Value == "false")
                                {
                                    Console.Write("j/N/q = avbryt) ? ");
                                }

                                else
                                {
                                    defaultChoice = temp.Value;
                                    Console.Write(questionText[i] + "(" + temp.Value + ")" + " : ");
                                }
                            }
                        }
                    }
                    else if (insertCategory.CategoryProps[i].Value == "true" || insertCategory.CategoryProps[i].Value == "false")
                    {
                        Console.Write(extra.CategoryPropTexts.ExtraInsertText + " ");

                        if (insertCategory.CategoryProps[i].Value == "true")
                        {
                            defaultChoice = "j";
                            Console.Write("(J/n/q = avbryt) ? ");
                        }
                        else if (insertCategory.CategoryProps[i].Value == "false")
                        {
                            Console.Write("j/N/q = avbryt) ? ");
                        }
                    }
                    else
                    {
                        Console.Write(questionText[i] + " : ");
                    }

                    var inputString = Console.ReadLine().Trim();

                    if (inputString.ToLower() == "q")
                    {
                        masterbreak = false;
                        return;
                    }
                    else if (inputString == "")
                    {
                        if (defaultChoice.ToLower() == "j")
                        {
                            inputString = "true";
                        }
                        else if (defaultChoice.ToLower() == "n")
                        {
                            inputString = "false";
                        }
                        
                        extraData.Add(new InventoryExtraData(insertCategory.CategoryProps[i].Id, inputString));
                        i++;
                        
                        break;
                    }
                    else if (inputString.Length > 0)
                    {
                        if (defaultChoice.ToLower() == "j")
                        {
                            inputString = "true";
                        }
                        else if (defaultChoice.ToLower() == "n")
                        {
                            inputString = "false";
                        }
                        else
                        {
                            continue;
                        }
                        
                        extraData.Add(new InventoryExtraData(insertCategory.CategoryProps[i].Id, inputString));
                        i++;
                        
                        break;
                    }
                }
            }
        }

        public static void showAddArticle(string categoryType)
        {
            if (categoryType == null)
            {
                return;
            }

            var tem = new Inventory();

            var temp = tem.GetType().GetProperties()
                            .Select(field => field.Name)
                            .ToList();

            foreach (var variable in temp)
            {
                switch (variable)
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
                    
                    default:
                        
                        break;

                }
            }



            var Office2 = context.ValidOffice.FirstOrDefault(x => x.Id == Office);

            var insertCategory = context.Category.FirstOrDefault(category => category.Type == categoryType);

            if (insertCategory != null)
            {
                showExtraParameters();
            }

            var temp2 = new Inventory(insertCategory, InventoryId, PurchaseDate, Brand, Model, Office2, PurchasePrice, Currency, extraData);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            context.Inventory.Add(temp2);

            context.SaveChanges();
        }

        public static void resetMemParameters()
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

            Dictionary<string, string> inventoryCategorys = new Dictionary<string, string>();

            var temp = context.Category.Include(z => z.CategoryTexts).ToList();

            int i = 0; ;

            foreach (var item in temp)
            {
                //foreach (var item in cat)
                {
                    //inventoryCategorys[] = item.CategoryTexts[0].Name.ToString();
                    inventoryCategorys[item.CategoryTexts.Name.ToString()] = item.Type.ToString();
                }
                i++;
                
            }

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
                        Console.Write("Vilken typ av atikel vill du addera (q = avbryt)? ");
                    }

                    categoryType = Console.ReadLine().Trim().ToLower();

                    int dictonaryKey = 0;

                    if (categoryType == "")
                    {
                        categoryType = Program.defaultType;
                        break;
                    }
                    else if (int.TryParse(categoryType, out dictonaryKey))
                    {
                        var temp2 = context.Category.FirstOrDefault(category => category.Id == dictonaryKey);

                        i = 1;

                        if (temp2 == null)
                        {
                            Console.WriteLine("Du angav en siffra som inte har någon koppling till en kategori av inventarier!\n");

                            

                            foreach (KeyValuePair<string, string> kvp in inventoryCategorys)
                            {
                                Console.WriteLine($"{i}" + " " + kvp.Key);

                                i++;
                            }

                            Console.Write("\nTryck på en tangent för att försöka på nyttt!");

                            Console.ReadKey();

                            continue;
                        }

                        categoryType = Program.defaultType = temp2.Type;
                        resetMemParameters();
                        break;
                    }

                    else if (categoryType == "q")
                    {
                        return;
                    }
                    else
                    {
                        var temp3 = context.Category.FirstOrDefault(x => x.Name.ToLower() == categoryType.ToLower());

                        if (temp3 == null)
                        {
                            temp3 = context.Category.FirstOrDefault(x => x.Type.ToLower() == categoryType.ToLower());
                        }

                        if (temp3 != null)
                        {
                            categoryType = Program.defaultType = temp3.Type;
                            resetMemParameters();
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
