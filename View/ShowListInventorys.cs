using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class ShowListInventorys
    {
        public static void showListInventorys(List<Inventory> sortedListCategory, bool showReadKey = true)
        {
            Console.WriteLine("Inköppsdatum".PadRight(15) + "Inventarie id".PadRight(15) + "Kategori".PadRight(15) + "Tillverkare".PadRight(15) + "Modell".PadRight(15) + "Placering".PadRight(15) + "Land".PadRight(15) + "Inköpps pris".PadRight(15) + "Inköpps valuta".PadRight(15) + "Pris i SEK".PadRight(15) + "Extra".PadRight(20)+"\n");

            foreach (var temp in sortedListCategory)
            {
                Console.ResetColor();

                var resultOffice = Program.validOffices.ElementAt(temp.Office);

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
                }
                else
                {
                    localPrice = "";
                }

                if (temp.GetType().Name.ToLower() == "computer")
                {
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
                        Console.Write(temp.InventoryId.ToString().PadRight(15) + "Datorer".PadRight(15));
                    }

                    else
                    {
                        Console.Write(temp.PurchaseDate.ToShortDateString().ToString().PadRight(15));
                        Console.Write(temp.InventoryId.ToString().PadRight(15) + "Datorer".PadRight(15));
                    }
                }

                else if (temp.GetType().Name.ToLower() == "mobilephone")
                {
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
                        Console.Write(temp.InventoryId.ToString().PadRight(15) + "Mobiltelefoner".PadRight(15));
                    }

                    else
                    {
                        Console.Write(temp.PurchaseDate.ToShortDateString().ToString().PadRight(15));
                        Console.Write(temp.InventoryId.ToString().PadRight(15) + "Mobiltelefoner".PadRight(15));
                    }
                }

                else if (temp.GetType().Name.ToLower() == "printer")
                {
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
                        Console.Write(temp.InventoryId.ToString().PadRight(15) + "Skrivare".PadRight(15));
                    }

                    else
                    {
                        Console.Write(temp.PurchaseDate.ToShortDateString().ToString().PadRight(15));
                        Console.Write(temp.InventoryId.ToString().PadRight(15) + "Skrivare".PadRight(15));
                    }
                }

                

                Console.Write(temp.Brand.ToString().PadRight(15) + temp.Model.ToString().PadRight(15) 
                    + resultOffice.Office.ToString().PadRight(15) + resultOffice.Country.ToString().PadRight(15) + 
                    temp.PurchasePrice.ToString().PadRight(15) + resultOffice.Currency.ToString().PadRight(15) + localPrice.PadRight(15));
            
                Console.WriteLine(temp.showExtra());
            }

            Console.ResetColor();

            if (showReadKey)
            { 
                Console.WriteLine("\nTryck på en tangent för att komma vidare...");

                Console.ReadKey();
            }
        }
    }
}
