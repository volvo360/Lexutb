using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class ShowCategorys
    {
        private static bool masterBreak = true;

        private static float totalCategorySum = 0;

        private static void showCategorysById()
        {
            var list = Program.categorys.OrderBy(x => x.categoryId);

            Console.WriteLine("Kategorier sorterade på kategori id!\n");
            Console.WriteLine("Kategori id".PadRight(15) + "Kategori".PadRight(20) + "Momssats".PadRight(10) + "Antal artiklar".PadRight(20) + "Artiklarnas försäljningsvärde".PadRight(25) + "\n");

            foreach (var categoryData in list)
            {
                if (categoryData.VATcategory > 0)
                {
                    var tempValue = Program.articles.Where(x => x.articleStock > 0 && x.articleCategory == categoryData.categoryId).Sum(x => x.articleStock * x.articlePrice);
                    var tempStock = Program.articles.Where(x => x.articleStock > 0 && x.articleCategory == categoryData.categoryId).Sum(x => x.articleStock);

                    totalCategorySum += tempValue;

                    if (tempValue > 0)
                    {
                        Console.WriteLine($"{categoryData.categoryId.ToString().PadRight(15)}{categoryData.categoryName.PadRight(20)}{((Program.vatData[categoryData.VATcategory] * 100).ToString() + " %").PadRight(10)}{tempStock.ToString().PadRight(20)}{tempValue.ToString("#.00") + " kr"}");
                    }
                    else
                    {
                        Console.WriteLine($"{categoryData.categoryId.ToString().PadRight(15)}{categoryData.categoryName.PadRight(20)}{((Program.vatData[categoryData.VATcategory] * 100).ToString() + " %").PadRight(10)}{tempStock.ToString().PadRight(20)}{""}");
                    }
                }
                else
                {
                    Console.WriteLine($"{categoryData.categoryId.ToString().PadRight(15)}{categoryData.categoryName.PadRight(20)}");
                }
            }

            if (totalCategorySum > 0)
            {
                Console.WriteLine($"\nTotalt försäljningsvärde för alla kategori är {totalCategorySum.ToString("#.00")} kr");
            }

            Console.WriteLine();
        }

        private static void showCategorysByName()
        {
            var list = Program.categorys.OrderBy(x => x.categoryName);

            Console.WriteLine("Kategorier sorterade på namn id!\n");
            Console.WriteLine("Kategori id".PadRight(15) + "Kategori".PadRight(20) + "Momssats".PadRight(10) + "Antal artiklar".PadRight(20) + "Artiklarnas försäljningsvärde".PadRight(25) + "\n");

            foreach (var categoryData in list)
            {
                if (categoryData.VATcategory > 0)
                {
                    var tempValue = Program.articles.Where(x => x.articleStock > 0 && x.articleCategory == categoryData.categoryId).Sum(x => x.articleStock * x.articlePrice);
                    var tempStock = Program.articles.Where(x => x.articleStock > 0 && x.articleCategory == categoryData.categoryId).Sum(x => x.articleStock);

                    totalCategorySum += tempValue;

                    //Console.WriteLine($"{categoryData.categoryId.ToString().PadRight(15)}{categoryData.categoryName.PadRight(20)}{((Program.vatData[categoryData.VATcategory] * 100).ToString() + " %").PadRight(10)}");
                    if (tempValue > 0)
                    {
                        Console.WriteLine($"{categoryData.categoryId.ToString().PadRight(15)}{categoryData.categoryName.PadRight(20)}{((Program.vatData[categoryData.VATcategory] * 100).ToString() + " %").PadRight(10)}{tempStock.ToString().PadRight(20)}{tempValue.ToString("#.00") + " kr"}");
                    }
                    else
                    {
                        Console.WriteLine($"{categoryData.categoryId.ToString().PadRight(15)}{categoryData.categoryName.PadRight(20)}{((Program.vatData[categoryData.VATcategory] * 100).ToString() + " %").PadRight(10)}{tempStock.ToString().PadRight(20)}{""}");
                    }
                }
                else
                {
                    Console.WriteLine($"{categoryData.categoryId.ToString().PadRight(15)}{categoryData.categoryName.PadRight(20)}");
                }
            }

            if (totalCategorySum > 0)
            {
                Console.WriteLine($"\nTotalt försäljningsvärde för alla kategori är {totalCategorySum.ToString("#.00")} kr");
            }

            Console.WriteLine();
        }

        public static void showCategorys(bool displayLine = true)
        {
            if (Program.categorys.Count > 0)
            {
                while (masterBreak)
                { 
                    Console.Write("Hur vill du sortera kategorierna (I)D/(n)amn? ");

                    string sortBy2 = Console.ReadLine().Trim();

                    Console.WriteLine();

                    if (sortBy2 == "" || sortBy2.ToLower() == "id" || sortBy2.ToLower() == "i")
                    {
                        showCategorysById();

                        break;
                    }
                    else if (sortBy2.ToLower() == "namn" || sortBy2.ToLower() == "n")
                    {
                        showCategorysByName();

                        break;
                    }
                }
                if (displayLine)
                { 
                    Console.Write("Tryck på en tangent för att komma vidare!");
                }
            }
            else
            {
                Console.WriteLine("Det finns inga kategori registrerade än!");
            }
        }
    }
}
