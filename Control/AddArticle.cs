using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class AddArticle
    {
        private static string productSKU = null;

        private static float productPrice = 0;

        private static string productName = null;

        private static float purchePrice = 0;

        private static float minSalePrice = 0;

        private static int stocknumber = 0;

        private static Dictionary<string, object> fieldsDict = new Dictionary<string, object>();

        public static void addProduct()
        {
            bool continuousRun = true;

            bool subRun = true;

            while (continuousRun)
            {
                Console.Clear();

                //Add a category if no one exists

                if (Program.categorys.Count == 0)
                {
                    Console.Write("Det finns inga kategori än skapade i tjänsten, vill du skapa en ny sådan (J/n/q) ? ");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "j" || input == "")
                    {
                        Console.Clear();

                        AddCattegory.addCategory();

                        MastersettingsArticle.foreceBreak = false;

                        continuousRun = false;
                    }
                }

                while (subRun)
                {
                    if (AddCattegory.masterCategoryId != null)
                    {
                        Console.Write($"Ange id på vilken kategori produkten ska kopplas till ({AddCattegory.masterCategoryId}) (q = avbryt)? ");
                    }    
                    else
                    {
                        Console.Write("Ange id på vilken kategori produkten ska kopplas till (q = avbryt)? ");
                    }
                    
                    string categoryId = Console.ReadLine().Trim();

                    if (categoryId.Length >= 0)
                    {
                        if (categoryId.ToLower() == "q")
                        {
                            subRun = false;
                            continuousRun = false;
                            break;
                        }

                        if (categoryId == "")
                        {
                            categoryId = AddCattegory.masterCategoryId;
                        }
                        else if (categoryId == "n")
                        {
                            break;
                        }

                        if (typeof(MastersettingsArticle).GetType().GetProperty("format") != null)
                        {
                            categoryId = CheckSyntax.check("Category", categoryId).fixedString;
                        }
                        else if (MastersettingsArticle.forceUpercase)
                        {
                            categoryId = categoryId.ToUpper();
                        }

                        if (Program.categorys.Any(cus => cus.categoryId == categoryId))
                        {
                            while (true)
                            {
                                Console.Write("Ange artikelnummer : (q = avbryt) ");
                                productSKU = Console.ReadLine().Trim();

                                if (productSKU.ToLower() == "q")
                                {
                                    subRun = false;
                                    continuousRun = false;
                                    return;
                                }

                                //Quick fix for problem below

                                var fields = typeof(MastersettingsArticle).GetFields();

                                if (fields != null)
                                {
                                    foreach (FieldInfo field in fields)
                                    {
                                        fieldsDict[field.Name] = field.GetValue(field.Name);
                                    }
                                }

                                // I don't understand why this dosen't work, if checking mastersettings then the field exist! 

                                if (typeof(MastersettingsArticle).GetType().GetProperty("format") != null)
                                {
                                    productSKU = CheckSyntax.check("Articles", productSKU).fixedString;
                                }
                                else if (fieldsDict.ContainsKey("format"))
                                {
                                    productSKU = CheckSyntax.check("Articles", productSKU).fixedString;
                                }

                                else if (MastersettingsArticle.forceUpercase)
                                {
                                    productSKU = productSKU.ToUpper();
                                }

                                if (!CheckSyntax.check("Articles", productSKU).correct)
                                {
                                    Console.WriteLine("Tryck på någon tangent för att försöka på nytt!");
                                    Console.ReadKey();  
                                    continue;
                                }
                                else if (!CheckArticleSKU.checkArticleSKU(productSKU))
                                {
                                    Console.WriteLine("Tryck på någon tangent för att försöka på nytt!");
                                    Console.ReadKey();
                                    //Wrong syntax of SKU
                                    continue;
                                }
                            
                                break;
                            }

                            while (true)
                            {
                                Console.Write("Ange artikelns namn : (q = avbryt) ");
                                productName = Console.ReadLine().Trim();

                                if (productName.ToLower() == "q")
                                {
                                    return;
                                }
                                else if (productName.Length == 0)
                                {
                                    Console.WriteLine("Artikelnamnet får ej vara blankt!");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    AddCattegory.masterCategoryId = categoryId;
                                    break;

                                }
                            }

                            while (true)
                            {
                                Console.Write("Ange artikelns inköpspris : (q = avbryt) ");
                                string inputProducPrice = Console.ReadLine().Trim();

                                if (inputProducPrice.ToLower() == "q")
                                {
                                    return;
                                }
                                else if (inputProducPrice.Length == 0)
                                {
                                    Console.WriteLine("Priset får ej vara tomt");
                                }
                                else
                                {
                                    if (float.TryParse(inputProducPrice, out purchePrice))
                                    {
                                        if (purchePrice > 0)
                                        {
                                            while (true)
                                            {
                                                var result = Program.articles.Find(x => x.articleCategory == categoryId);

                                                int vatGroup = Mastersettings.defaultVATclass;

                                                if (result != null)
                                                {
                                                    vatGroup = result.articleVATcategory;
                                                }

                                                EditCategory.showVAT(vatGroup, false);

                                                minSalePrice = purchePrice*(1+Program.vatData[EditCategory.newCategoryVAT]);

                                                break;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ange ett giltigt pris!");
                                    }
                                }
                                break;
                            }

                            bool subRunPrice = true;

                            while (subRunPrice)
                            {
                                Console.Write($"Ange artikelns pris (får ej vara mindre än {minSalePrice}): (q = avbryt) ");
                                string inputProducPrice = Console.ReadLine().Trim();

                                if (inputProducPrice.ToLower() == "q")
                                {
                                    return;
                                }
                                else if (inputProducPrice.Length == 0)
                                {
                                    Console.WriteLine("Priset får ej vara tomt");
                                }
                                else
                                {
                                    if (float.TryParse(inputProducPrice, out productPrice))
                                    {
                                        if (productPrice > minSalePrice)
                                        {
                                            subRunPrice = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ange ett giltigt pris och som uppfyller minkravet på priset!");
                                    }
                                }
                            }

                            bool subRunStock = true;
                            while (subRunStock)
                            {
                                Console.Write($"Ange lager antal: (q = avbryt) ");
                                string inputStockNumber = Console.ReadLine().Trim();

                                if (inputStockNumber.ToLower() == "q")
                                {
                                    return;
                                }
                                else if (inputStockNumber.Length < 0)
                                {
                                    Console.WriteLine("Lagerantalet får ej vara tomt");
                                }
                                else
                                {
                                    stocknumber = int.Parse(inputStockNumber.Trim());

                                    if (stocknumber >= 0)
                                    {
                                        subRunStock = false;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ange ett giltigt lager antal!");

                                    }
                                }
                            }

                            Program.articles.Add(new Articles(productSKU, categoryId, productName, productPrice, EditCategory.newCategoryVAT, stocknumber, purchePrice));
                            FileRegister.saveRegister();

                            while (true)
                            {
                                Console.Write("Vill du addera yterligare en artikel (J/n)? ");

                                string inputChoice = Console.ReadLine();

                                if (inputChoice.ToLower() == "j" || inputChoice == "")
                                {
                                    Console.Clear();
                                    break;
                                }
                                else if (inputChoice.ToLower() == "n")
                                {
                                    subRun = false;
                                    continuousRun = false;
                                    break;
                                }
                            }

                            if (Mastersettings.renderProductsAfterAdd)
                            {
                                Console.Write("Vill du se alla artiklar (J/n)? ");

                                string inputChoice = Console.ReadLine();

                                if (inputChoice.ToLower() == "j" || inputChoice == "")
                                {
                                    Console.Clear();
                                    ShowArticles.showArticles();
                                    //Console.ReadKey(); 
                                    break;
                                }
                                else if (inputChoice.ToLower() == "n")
                                {
                                    subRun = false;
                                    continuousRun = false;
                                    return;
                                }
                            }

                            break;
                        }
                        else
                        {
                            while (true)
                            { 
                                Console.Write("Kategorin finns inte, vill du se alla kategorier (J/n/q)? ");

                                string inputError = Console.ReadLine();

                                if (inputError.ToLower() == "j" || inputError == "")
                                {
                                    ShowCategorys.showCategorys();
                                    Console.ReadKey();
                                    break;
                                }
                                else if (inputError.ToLower() == "n" )
                                {
                                    break;
                                }
                                else if (inputError.ToLower() == "q")
                                {
                                    return;
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
