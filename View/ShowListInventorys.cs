using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    internal class ShowListInventorys
    {
        static InventoryContext context = new InventoryContext();

        public static void showExtra(Inventory extraData)
        {
            if (extraData.Category.CategoryProps == null || extraData.ExtraData == null)
            {
                return;
            }

            int i = 0;

            foreach (var extra in extraData.ExtraData)
            {
                if (extra.Value == "true")
                {
                    //Console.Write(extraData.Category.CategoryTexts.ExtraText.ToString());
                    Console.Write(extraData.Category.CategoryProps[i].CategoryPropTexts.ExtraText.ToString());
                }
                else if (extra.Value == "false")
                {
                    //Don't display anything
                }
                else
                {
                    Console.Write(extraData.Category.CategoryTexts.ExtraText.ToString()+" : "+extra.Value);
                }
                i++;
            }   
        }

        public static void showListInventorys(List<Inventory> sortedListCategory, bool showReadKey = true)
        {
            Console.WriteLine("Id".PadRight(5)+"Inköppsdatum".PadRight(15) + "Inventarie id".PadRight(15) + "Kategori".PadRight(15) + "Tillverkare".PadRight(15) + "Modell".PadRight(15) + "Placering".PadRight(15) + "Land".PadRight(15) + "Inköpps pris".PadRight(15) + "Inköpps valuta".PadRight(15) + "Pris i SEK".PadRight(15) + "Extra".PadRight(20)+"\n");

            float totalPrice = 0f;

            int totalDays = 0;

            foreach (var temp in sortedListCategory)
            {
                Console.ResetColor();

                var resultOffice = context.ValidOffice.FirstOrDefault(x => x.Id == temp.Office.Id);

                if (resultOffice == null)
                {
                    continue;
                }

                float exchangeRate = 0;

                string localPrice = "";

                if (resultOffice.Currency == "USD")
                {
                    exchangeRate = 1 / (float)ExchangeRates.GetData["data"]["USD"];
                }
                else if (resultOffice.Currency == "GBP")
                {
                    exchangeRate = 1 / (float)ExchangeRates.GetData["data"]["GBP"];
                }
                else if (resultOffice.Currency == "EUR")
                {
                    exchangeRate = 1 / (float)ExchangeRates.GetData["data"]["EUR"];
                }

                if (exchangeRate > 0)
                {
                    localPrice = (temp.PurchasePrice * exchangeRate).ToString("#.00");

                    totalPrice += float.Parse(localPrice);
                }
                else
                {
                    localPrice = "";
                    totalPrice += temp.PurchasePrice;
                }

                totalDays += (DateTime.Now - temp.PurchaseDate).Days;

                Console.Write(temp.Id.ToString().PadRight(5));

                var sub = DateTime.Now.Year - temp.PurchaseDate.Year;

                if (sub >= MasterSettings.depreciationPeriod)
                {
                    var currentDate = DateTime.Now;

                    var dateSpan = (currentDate.AddYears(-MasterSettings.depreciationPeriod) - temp.PurchaseDate).Days;

                    var dateDiff = CommonFunction.GetMonthsBetween(DateTime.Now, temp.PurchaseDate);

                    if ((MasterSettings.depreciationPeriod * 12 - dateDiff) <= 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if ((MasterSettings.depreciationPeriod * 12 - dateDiff) <= 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(temp.PurchaseDate.ToShortDateString().ToString().PadRight(15));
                    Console.Write(temp.InventoryId.ToString().PadRight(15) + temp.Category.CategoryTexts.Name.PadRight(15));
                }

                else
                {
                    Console.Write(temp.PurchaseDate.ToShortDateString().ToString().PadRight(15));
                    Console.Write(temp.InventoryId.ToString().PadRight(15) + temp.Category.CategoryTexts.Name.PadRight(15));
                }

                Console.Write(temp.Brand.ToString().PadRight(15) + temp.Model.ToString().PadRight(15) 
                    + resultOffice.Office.ToString().PadRight(15) + resultOffice.Country.ToString().PadRight(15) + 
                    temp.PurchasePrice.ToString().PadRight(15) + resultOffice.Currency.ToString().PadRight(15) + localPrice.PadRight(15));
            
                showExtra(temp);

                Console.WriteLine();
            }

            Console.ResetColor();

            Console.WriteLine();

            Console.Write($"I urvalet har vi {sortedListCategory.Count} artiklar, ");
            Console.Write($"med ett inköpsvärde på {totalPrice.ToString("#.00")} kr ");
            Console.WriteLine($"och ett medelinköpsvärde på {(totalPrice/ sortedListCategory.Count).ToString("#.00")} kr. Samt att medelåldern är ca {(totalDays/30/sortedListCategory.Count).ToString("#")} månader.");

            if (showReadKey)
            { 
                Console.WriteLine("\nTryck på en tangent för att komma vidare...");

                Console.ReadKey();
            }
        }
    }
}
