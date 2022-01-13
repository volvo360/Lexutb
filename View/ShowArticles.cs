using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{ 
    public static class ShowArticles
    {  
        public static void showArticles() 
        {
            float totalSaleValue = 0;

            //LINQ for joining categoryid with products productCategory

            var query = from cat in Program.categorys
                        join prod in Program.articles
                on cat.categoryId equals prod.articleCategory into joined
                        from prod in joined.DefaultIfEmpty()
                        select new { prod, cat };
            while (true)
            {
                Console.Write("Hur vill du sortera artiklarna ((P)RIS/(v)arugrupp/(a)rtikel/q = quit)? ");

                string inpupSortBy = Console.ReadLine().Trim().ToLower();

                if (inpupSortBy == "q")
                {
                    return;
                }

                else if (inpupSortBy == "" || inpupSortBy == "pris" || inpupSortBy == "p")
                {
                    var result = query.Where(x => x.prod != null).OrderBy(x => x.prod.articlePrice).ThenBy(x => x.cat.categoryId);
                        
                    Console.Clear();

                    string oldProductSKU = null;

                    Console.WriteLine("Artikelnummer".PadRight(15) + "Varugrupp".PadRight(30) + "Produkt".PadRight(25) + "Lagerantal".PadRight(15) + "Inköppspris".PadRight(15) + "Försäljnings pris".PadRight(20) + "Moms".PadRight(10) + "Pris ex. Moms".PadRight(15) + "Vinst/artikel".PadRight(15) + "Försäljningsvärde" + "\n");

                    totalSaleValue = 0;

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

                                if (product.prod.articlePurchasePrice == 0)
                                {
                                    Console.WriteLine($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}{product.prod.articleName.ToString().PadRight(25)}{"".PadRight(15)}{"".ToString().PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{"".PadRight(15)}");
                                }
                                else
                                {
                                    Console.WriteLine($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}{product.prod.articleName.ToString().PadRight(25)}{product.prod.articleStock.ToString().PadRight(15)}{product.prod.articlePurchasePrice.ToString("#.00").PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory]) - product.prod.articlePurchasePrice).ToString("#.00") + " kr").PadRight(15)}{articleValue.PadRight(15)}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}{product.prod.articleName.ToString().PadRight(25)}{product.prod.articlePrice.ToString("#.00").PadRight(10)}");
                            }
                            oldProductSKU = product.prod.articleSKU.ToString();
                        }
                    }

                    var sum = Program.articles.Sum(x => x.articlePrice);
                    Console.WriteLine($"\nAlla artiklars samlade värde ex moms är {sum.ToString("#.00")} kr");
                    var sumVat = Program.articles.Where(x => x.articleVATcategory > 0).Sum(x => (1 + Program.vatData[x.articleVATcategory]) * x.articlePrice);
                    var subSumVat = Program.articles.Where(x => x.articleVATcategory == 0).Sum(x => x.articlePrice);
                    //var sumVat = Program.products.Sum(x => (1 + Program.vatData[x.VATcategory]) * x.price);

                    if (subSumVat > 0)
                    {
                        Console.WriteLine($"Alla artiklars samlade värde inkl moms (det finns produkter utan angiven momskategori) är {(sumVat + subSumVat).ToString("#.00")} kr\n");
                    }
                    else
                    {
                        Console.WriteLine($"Alla artiklars samlade värde inkl moms är {(sumVat + subSumVat).ToString("#.00")} kr\n");
                    }

                    if (totalSaleValue > 0)
                    {
                        Console.WriteLine($"Totalt försäljningsvärde ink. moms { totalSaleValue.ToString("#.00") } kr\n");
                    }

                    Console.Write("Tryck på en tangent för att kommma vidare!");
                    Console.ReadKey();

                    break;
                }

                else if (inpupSortBy == "varugrupp" || inpupSortBy == "v")
                {
                    var result = query.Where(x => x.prod != null).OrderBy(x => x.cat.categoryName).ThenBy(x => x.prod.articleSKU);

                    Console.Clear();

                    Console.WriteLine("Artikelnummer".PadRight(15) + "Varugrupp".PadRight(30) + "Produkt".PadRight(25) + "Lagerantal".PadRight(15) + "Inköppspris".PadRight(15) + "Försäljnings pris".PadRight(20) + "Moms".PadRight(10) + "Pris ex. Moms".PadRight(15) + "Vinst/artikel".PadRight(15) + "Försäljningsvärde" + "\n");

                    foreach (var product in result)
                    {
                        string articleValue = null;

                        if (product.prod.articleStock > 0)
                        {
                            float articleValueSum = (product.prod.articleStock * product.prod.articlePrice);
                            articleValue = articleValueSum.ToString() + " " + "kr";

                            totalSaleValue += articleValueSum;

                            articleValue = (product.prod.articleStock * product.prod.articlePrice).ToString("#.00") + " " + "kr";
                        }

                        if (product.prod.articlePurchasePrice == 0)
                        {
                            Console.WriteLine($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}{product.prod.articleName.ToString().PadRight(25)}{"".PadRight(15)}{"".ToString().PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{"".PadRight(15)}{articleValue.PadRight(15)}");
                        }
                        else
                        {
                            Console.WriteLine($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}{product.prod.articleName.ToString().PadRight(25)}{product.prod.articleStock.ToString().PadRight(15)}{product.prod.articlePurchasePrice.ToString("#.00").PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory]) - product.prod.articlePurchasePrice).ToString("#.00") + " kr").PadRight(15)}{articleValue.PadRight(15)}");
                        }
                    }

                    var sum = Program.articles.Sum(x => x.articlePrice);
                    Console.WriteLine($"\nAlla artiklars samlade värde ex moms är {sum.ToString("#.00")} kr");
                        
                    var sumVat = Program.articles.Where(x => x.articleVATcategory > 0).Sum(x => (1 + Program.vatData[x.articleVATcategory]) * x.articlePrice);
                    var subSumVat = Program.articles.Where(x => x.articleVATcategory == 0).Sum(x => x.articlePrice);
                        
                    if (subSumVat > 0)
                    {
                        Console.WriteLine($"Alla artiklars samlade värde inkl moms (det finns produkter utan angiven momskategori) är {(sumVat + subSumVat).ToString("#.00")} kr\n");
                    }
                    else
                    {
                        Console.WriteLine($"Alla artiklars samlade värde inkl moms är {(sumVat + subSumVat).ToString("#.00")} kr\n");
                    }

                    if (totalSaleValue > 0)
                    {
                        Console.WriteLine($"Totalt försäljningsvärde ink. moms { totalSaleValue.ToString("#.00") } kr\n");
                    }

                    Console.Write("Tryck på en tangent för att kommma vidare!");
                    Console.ReadKey();
                    break;
                }

                else if (inpupSortBy == "artikel" || inpupSortBy == "a")
                {
                    var result = query.Where(x => x.prod != null).OrderBy(x => x.prod.articleName).ThenBy(x => x.cat.categoryName);

                    Console.Clear();

                    Console.WriteLine("Artikelnummer".PadRight(15) + "Varugrupp".PadRight(30) + "Produkt".PadRight(25) + "Lagerantal".PadRight(15) + "Inköppspris".PadRight(15) + "Försäljnings pris".PadRight(20) + "Moms".PadRight(10) + "Pris ex. Moms".PadRight(15) + "Vinst/artikel".PadRight(15) + "Försäljningsvärde" + "\n");

                    foreach (var product in result)
                    {
                        string articleValue = null;

                        if (product.prod.articleStock > 0)
                        {
                            float articleValueSum = (product.prod.articleStock * product.prod.articlePrice);
                            articleValue = articleValueSum.ToString("#.00") + " " + "kr";

                            totalSaleValue += articleValueSum;

                            articleValue = (product.prod.articleStock * product.prod.articlePrice).ToString("#.00") + " " + "kr";
                        }

                        if (product.prod.articlePurchasePrice == 0)
                        {
                            Console.WriteLine($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}{product.prod.articleName.ToString().PadRight(25)}{"".PadRight(15)}{"".ToString().PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{"".PadRight(15)}{articleValue.PadRight(15)}");
                        }
                        else
                        {
                            Console.WriteLine($"{product.prod.articleSKU.ToString().PadRight(15)}{(product.prod.articleCategory.ToString() + " " + product.cat.categoryName).PadRight(30)}{product.prod.articleName.ToString().PadRight(25)}{product.prod.articleStock.ToString().PadRight(15)}{product.prod.articlePurchasePrice.ToString("#.00").PadRight(15)}{product.prod.articlePrice.ToString("#.00").PadRight(20)}{((Program.vatData[product.prod.articleVATcategory] * 100).ToString() + " %").PadRight(10)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory])).ToString("#.00") + " kr").PadRight(15)}{((product.prod.articlePrice / (1 + Program.vatData[product.prod.articleVATcategory]) - product.prod.articlePurchasePrice).ToString("#.00") + " kr").PadRight(15)}{articleValue.PadRight(15)}");
                        }
                    }

                    var sum = Program.articles.Sum(x => x.articlePrice);
                    Console.WriteLine($"\nAlla artiklars samlade värde ex moms är {sum.ToString("#.00")} kr");
                        
                    var sumVat = Program.articles.Where(x => x.articleVATcategory > 0).Sum(x => (1 + Program.vatData[x.articleVATcategory]) * x.articlePrice);
                    var subSumVat = Program.articles.Where(x => x.articleVATcategory == 0).Sum(x => x.articlePrice);
                        
                    if (subSumVat > 0)
                    {
                        Console.WriteLine($"Alla artiklars samlade värde inkl moms (det finns produkter utan angiven momskategori) är {(sumVat + subSumVat).ToString("#.00")} kr\n");
                    }
                    else
                    {
                        Console.WriteLine($"Alla artiklars samlade värde inkl moms är {(sumVat + subSumVat).ToString("#.00")} kr\n");
                    }

                    if (totalSaleValue > 0)
                    {
                        Console.WriteLine($"Totalt försäljningsvärde ink. moms { totalSaleValue.ToString("#.00") } kr\n");
                    }

                    Console.Write("Tryck på en tangent för att kommma vidare!");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}