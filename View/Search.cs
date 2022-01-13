using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class Search
    {
        private static bool masterBreak = true;

        public static void search()
        {
            while (masterBreak)
            {
                Console.Clear();

                Console.Write("Vill du söka bland (A)rtiklar/(v)arugrupper (q = avbryt)? ");

                string searchCondition = Console.ReadLine().Trim().ToLower();

                if (searchCondition == "q")
                {
                    return;
                }

                else if (searchCondition == "" || searchCondition == "a")
                {
                    searchArticleName();

                    if (!masterBreak)
                    {
                        return;
                    }
                }

                else if (searchCondition == "v")
                {
                    searchCategoryName();

                    if (!masterBreak)
                    {
                        return;
                    }
                }
            }

            Console.WriteLine();

            while (true)
            { 
                Console.Write("Vill du söka efter ett annat ord (j/N)? ");

                string run = Console.ReadLine().Trim().ToLower();

                if (run == "n" || run == "")
                {
                    return;
                }
                else if (run == "j")
                {
                    break;
                }
            }
        }
        public static void searchArticleName()
        {
            while (true)
            {
                float totalSaleValue = 0;

                string oldProductSKU = null;

                Console.Clear();

                Console.Write("Ange sökord för att söka bland artiklarna (q = avbryt) : ");
                String searchWord = Console.ReadLine().ToLower();

                if (searchWord == "q")
                {
                    masterBreak = false;

                    return;
                }

                else if (searchWord.Length > 0)
                {
                    /*var res = CheckArticleSKU.checkArticleSKU(searchWord);

                    if (res == false)
                    {
                        continue;
                    }*/

                    var results = Program.articles.Where(str => str.articleName.ToLower().Contains(searchWord.ToLower()));

                    if (results == null) 
                    {
                        Console.WriteLine("Sökordet går ej att finna! Vänligen tryck på en tangent!");
                        Console.ReadLine();

                        continue;
                    }

                    //LINQ for joining our categorys with coresponding article
                    var query = from cat in Program.categorys
                                join prod in results on cat.categoryId equals prod.articleCategory

                                select new { prod, cat };

                    //Lamda expression for sorting category name and then 
                    var result = query.OrderBy(x => x.cat.categoryName).ThenBy(x => x.prod.articleSKU);

                    Console.Clear();

                    Console.WriteLine("Artikelnummer".PadRight(15) + "Varugrupp".PadRight(30) + "Produkt".PadRight(25) + "Lagerantal".PadRight(15) + "Inköppspris".PadRight(15) + "Försäljnings pris".PadRight(20) + "Moms".PadRight(10) + "Pris ex. Moms".PadRight(15) + "Vinst/artikel".PadRight(15) + "Försäljningsvärde" + "\n");

                    foreach (var product in result)
                    {
                        if (oldProductSKU != product.prod.articleSKU.ToString())
                        {
                            if (product.prod.articleVATcategory > 0)
                            {
                                string articleValue = null;

                                if (product.prod.articleStock > 0)
                                {
                                    float articleValueSum = (product.prod.articleStock * product.prod.articlePrice);
                                    articleValue = articleValueSum.ToString("#.00") + " " + "kr";

                                    totalSaleValue += articleValueSum;
                                }

                                if (product.prod.articlePurchasePrice == 0 || product.prod.articlePurchasePrice == null)
                                {
                                    Console.Write($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}");

                                    string[] productName = product.prod.articleName.Split(' ');

                                    int i = 0;

                                    int last = productName.Count();

                                    foreach (string name in productName)
                                    {
                                        if (name.ToLower().Contains(searchWord))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.Write($"{name}");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.Write($"{name}");
                                        }
                                        if (i < last)
                                        {
                                            Console.Write(' ');
                                            i++;
                                        }
                                    }

                                    Console.Write($"{"".PadRight((25 - product.prod.articleName.Length))}");

                                    Console.WriteLine($"{"".PadRight(15)}{"".ToString().PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{"".PadRight(15)}");
                                }
                                else
                                {
                                    Console.Write($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}");

                                    string[] productName = product.prod.articleName.Split(' ');

                                    int i = 0;

                                    int last = productName.Count();

                                    foreach (string name in productName)
                                    {
                                        if (name.ToLower().Contains(searchWord))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.Write($"{name}");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.Write($"{name}");
                                        }
                                        if (i < last)
                                        { 
                                            Console.Write(' ');
                                            i++;
                                        }
                                    }

                                    Console.Write($"{"".PadRight((25 - product.prod.articleName.Length))}");
                                    Console.WriteLine($"{product.prod.articleStock.ToString().PadRight(15)}{product.prod.articlePurchasePrice.ToString("#.00").PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory]) - product.prod.articlePurchasePrice).ToString("#.00") + " kr").PadRight(15)}{articleValue.PadRight(15)}");
                                }
                            }
                            else
                            {
                                string articleValue = null;

                                if (product.prod.articleStock > 0)
                                {
                                    float articleValueSum = (product.prod.articleStock * product.prod.articlePrice);
                                    articleValue = articleValueSum.ToString("#.00") + " " + "kr";

                                    totalSaleValue += articleValueSum;
                                }

                                if (product.prod.articlePurchasePrice == 0 || product.prod.articlePurchasePrice == null)
                                {
                                    Console.Write($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}");

                                    string[] productName = product.prod.articleName.Split(' ');

                                    int i = 0;

                                    int last = productName.Count();

                                    foreach (string name in productName)
                                    {
                                        if (name.ToLower().Contains(searchWord))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.Write($"{name}");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.Write($"{name}");
                                        }
                                        if (i < last)
                                        {
                                            Console.Write(' ');
                                            i++;
                                        }
                                    }

                                    Console.Write($"{"".PadRight((25 - product.prod.articleName.Length))}");

                                    Console.WriteLine($"{"".PadRight(15)}{"".ToString().PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{"".ToString().PadRight(10)}{"".PadRight(15)}{"".PadRight(15)}");
                                }
                                else
                                {
                                    Console.Write($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}");

                                    string[] productName = product.prod.articleName.Split(' ');

                                    int i = 0;

                                    int last = productName.Count();

                                    foreach (string name in productName)
                                    {
                                        if (name.ToLower().Contains(searchWord))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.Write($"{name}");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.Write($"{name}");
                                        }
                                        if (i < last)
                                        {
                                            Console.Write(' ');
                                            i++;
                                        }
                                    }

                                    Console.Write($"{"".PadRight((25 - product.prod.articleName.Length))}");
                                    Console.WriteLine($"{product.prod.articleStock.ToString().PadRight(15)}{product.prod.articlePurchasePrice.ToString("#.00").PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory]) - product.prod.articlePurchasePrice).ToString("#.00") + " kr").PadRight(15)}{articleValue.PadRight(15)}");
                                }
                            }
                            oldProductSKU = product.prod.articleSKU.ToString();
                        }
                    }
                    //Lamda expressions to find stock value
                    var sum = result.Sum(x => x.prod.articlePrice);

                    Console.WriteLine($"\nAlla artiklars samlade värde än {sum.ToString("#.00")} kr\n");

                    if (totalSaleValue > 0)
                    {
                        Console.WriteLine($"Föräljningsvärdet för alla sökresultat är {totalSaleValue.ToString("#.00")} kr");
                    }

                    Console.Write("\nTryck på en tangent för att komma vidare!");
                    Console.ReadKey();

                    break;
                }
            }
        }

        public static void searchCategoryName()
        {
            while (true)
            {
                Console.Clear();

                Console.Write("Ange sökord för att söka bland varugrupperna (q = avbryt) : ");

                String searchWord = Console.ReadLine().ToLower();

                float totalSearchValue = 0;

                if (searchWord == "q")
                {
                    masterBreak = false;

                    return;
                }

                else if (searchWord.Length > 0)
                {
                    var res = Program.categorys.Where(str => str.categoryName.ToLower().Contains(searchWord.ToLower()));

                    if (res == null)
                    {
                        Console.WriteLine("Sökordet går ej att finna! Vänligen tryck på en tangent!");
                        Console.Read();
                        continue;
                    }
  
                    var result = res.OrderBy(x => x.categoryName).ThenBy(x => x.categoryId);

                    Console.Clear();

                    Console.WriteLine("Kategori id".PadRight(15) + "Kategori".PadRight(20) + "Momssats".PadRight(10)+"Kategrori värde".PadRight(20)+"Antal artiklar".PadRight(25)+"Försäljningsvärde" + "\n");

                    foreach (var categoryData in result)
                    {
                        float sumCategory = Program.articles.Sum(x => x.articlePrice);

                        totalSearchValue += sumCategory;

                        if (categoryData.VATcategory > 0)
                        {
                            //Lamda expressions to find stock value
                            var tempValue = Program.articles.Where(x => x.articleStock > 0 && x.articleCategory == categoryData.categoryId).Sum(x => x.articleStock * x.articlePrice);
                            var tempStock = Program.articles.Where(x => x.articleStock > 0 && x.articleCategory == categoryData.categoryId).Sum(x => x.articleStock);

                            if (tempValue > 0)
                            {
                                Console.Write($"{categoryData.categoryId.ToString().PadRight(15)}");

                                string[] categoryName = categoryData.categoryName.Split(' ');

                                int i = 0;

                                int last = categoryName.Count();

                                foreach (string name in categoryName)
                                {
                                    if (name.ToLower().Contains(searchWord))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write($"{name}");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.Write($"{name}");
                                    }
                                    if (i < last)
                                    {
                                        Console.Write(' ');
                                        i++;
                                    }
                                }

                                Console.Write($"{"".PadRight((20 - categoryData.categoryName.Length))}");

                                Console.WriteLine($"{((Program.vatData[categoryData.VATcategory] * 100).ToString() + " %").PadRight(10)}{(sumCategory.ToString("#.00") + " kr").ToString().PadRight(20)}{tempStock.ToString().PadRight(25)}{tempValue.ToString("#.00") + " kr"}");
                            }
                            else
                            {
                                Console.Write($"{categoryData.categoryId.ToString().PadRight(15)}");

                                string[] categoryName = categoryData.categoryName.Split(' ');

                                int i = 0;

                                int last = categoryName.Count();

                                foreach (string name in categoryName)
                                {
                                    if (name.ToLower().Contains(searchWord))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write($"{name}");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.Write($"{name}");
                                    }
                                    if (i < last)
                                    {
                                        Console.Write(' ');
                                        i++;
                                    }
                                }

                                Console.Write($"{"".PadRight((20 - categoryData.categoryName.Length))}");

                                Console.WriteLine($"{((Program.vatData[categoryData.VATcategory] * 100).ToString() + " %").PadRight(10)}{(sumCategory.ToString("#.00") + " kr").ToString().PadRight(20)}");
                            }

                        }
                        else
                        {
                            Console.WriteLine($"{categoryData.categoryId.ToString().PadRight(15)}{categoryData.categoryName.PadRight(20)}{"".PadRight(10)}{(sumCategory + " kr").ToString().PadRight(10)}");
                        }
                    }

                    Console.WriteLine($"\nTotalt försäljningsvärde i den sökta varugruppen {totalSearchValue.ToString("#.00")} kr");

                    Console.Write("\nTryck på en tangent för att komma vidare!");
                    Console.ReadKey();

                    break;
                }
            }
        }
    }
}
