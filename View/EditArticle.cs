using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class EditArticle
    {
        private static string newSkU;
        private static string newCategoryId;
        private static string newProductName;
        private static float newProductPrice;



        private static bool masterBreak = true;

        private static bool subRunEditArticle = true;

        private static Categorys resultsCategory = new Categorys();

        private static Articles results = new Articles();

        private static Dictionary<string, object> fieldsDict = new Dictionary<string, object>();

        private static string productSKU = null;

        private static float productPrice = 0;

        private static string productName = null;

        private static float purchePrice = 0;

        private static float minSalePrice = 0;

        private static int stocknumber = 0;

        private static void showSelectArticleToEdit()
        {
            bool masterEdit = false;

            while (true)
            {
                masterEdit = false;

               Categorys resultsCategory = new Categorys();

                Console.Clear();

                Console.Write("Vilken produkt vill du editera (q = avbryt)? ");
                string editProductSKU = Console.ReadLine().Trim().ToLower();

                if (editProductSKU == "q")
                {
                    masterBreak = false;
                    return;
                }
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
                    editProductSKU = CheckSyntax.check("Articles", editProductSKU).fixedString;
                }
                else if (fieldsDict.ContainsKey("format"))
                {
                    editProductSKU = CheckSyntax.check("Articles", editProductSKU).fixedString;
                }

                else if (MastersettingsArticle.forceUpercase)
                {
                    editProductSKU = editProductSKU.ToUpper();
                }

                var res = Program.articles.Find(x => x.articleSKU == editProductSKU);

                if (res == null)
                {
                    while (true)
                    { 
                        Console.Write("Artikeln finns inte, vill du titta på alla produkter (J/n/q) ");

                        string showProducts = Console.ReadLine().Trim().ToLower();

                        if (showProducts == "j" || showProducts == "")
                        {
                            Console.WriteLine();
                            ShowArticles.showArticles();
                            masterEdit = true;
                            break;
                        }
                        else if (showProducts == "n")
                        {
                            masterEdit = true;
                            break;
                        }
                        else if (showProducts == "q")
                        {
                            return;
                        }
                    }
                }

                if (masterEdit)
                {
                    continue;
                }

                while (true)
                {
                    Console.Write($"Vill du verkligen editera produkten \"{res.articleName}\" (J/n/q)? ");
                    string inputChoiceEditArticle = Console.ReadLine().Trim().ToLower();

                    if (inputChoiceEditArticle == "" || inputChoiceEditArticle == "j")
                    {
                        break;
                    }
                    else if (inputChoiceEditArticle == "n")
                    {
                        masterEdit = true;
                        break;
                    }
                    else if (inputChoiceEditArticle == "q")
                    {
                        return;
                    }

                }

                if (masterEdit)
                {
                    continue;
                }

                if (CheckArticleSKU.checkArticleSKU(editProductSKU, false))
                {
                    results = Program.articles.Find(str => str.articleSKU == editProductSKU);

                    if (results != null)
                    {
                        while (true)
                        {
                            Console.Write($"Ange önskat artikelnummer ({editProductSKU}) (q = avbryt) : ");

                            String inputSKU = Console.ReadLine().Trim();

                            newSkU = inputSKU;

                            if (MastersettingsArticle.forceUpercase)
                            {
                                newSkU = newSkU.ToUpper();
                            }
                            

                            if (inputSKU.ToLower() == "q")
                            {
                                masterBreak = false;
                                return;
                            }

                            else if (inputSKU == "")
                            {
                                newSkU = editProductSKU;

                                results = Program.articles.Find(str => str.articleSKU == newSkU);

                                subRunEditArticle = false;
                                break;
                            }
                            else
                            {
                                newSkU = inputSKU;

                                if (MastersettingsArticle.forceUpercase)
                                {
                                    newSkU = inputSKU.ToUpper();
                                }

                                if (newSkU == editProductSKU)
                                {
                                    subRunEditArticle = false;
                                    break;
                                }
                                else if (!CheckArticleSKU.checkArticleSKU(newSkU))
                                {
                                    //Product SKU already in use, do nothing!
                                    subRunEditArticle = false;
                                }
                                else
                                {
                                    if (MastersettingsArticle.forceUpercase)
                                    {
                                        editProductSKU = editProductSKU.ToUpper();
                                    }

                                    results = Program.articles.Find(str => str.articleSKU == editProductSKU);
                                    subRunEditArticle = false;

                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Write("Artikeln finns inte, vill du titta på alla produkter (J/n/q) ");

                            string showProducts = Console.ReadLine().Trim().ToLower();

                            if (showProducts == "j" || showProducts == "")
                            {
                                Console.WriteLine();
                                ShowArticles.showArticles();
                                break;
                            }
                            else if (showProducts == "n")
                            {
                                break;
                            }
                            else if (showProducts == "q")
                            {
                                return;
                            }
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Ange ett giltigt formaterat artikelnummer!\n");
                }
            }
        }

        private static void showSelectCategoryForProduct()
        {
            while(true)
            {
                if (results.articleCategory != null)
                {
                    resultsCategory = Program.categorys.Find(str => str.categoryId == results.articleCategory);
                    
                    Console.Write($"Ange önskad kategori artikeln ska tillhöra ({results.articleCategory } {resultsCategory.categoryName}) (q = avbryt) : ");
                }
                else
                {
                    Console.Write($"Ange önskad kategori artikeln ska tillhöra (q = avbryt) : ");
                }

                String inputCategorySKU = Console.ReadLine().Trim();
                newCategoryId = inputCategorySKU;

                if (inputCategorySKU == "")
                {
                    newCategoryId = results.articleCategory;
                }

                if (inputCategorySKU.ToLower() == "q")
                {
                    masterBreak = false;
                    return;
                }

                else if (inputCategorySKU == "" && results.articleCategory.Length > 0)
                {
                    if (MastersettingsCategory.forceUpercase)
                    {
                        inputCategorySKU = inputCategorySKU.ToUpper();
                    }
                    break;
                }
                else
                {
                    newSkU = inputCategorySKU;

                    if (MastersettingsCategory.forceUpercase)
                    {
                        inputCategorySKU = inputCategorySKU.ToUpper();
                    }

                    if (CheckCategoryId.checkCategoryId(newSkU, false))
                    {
                        break;
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Write("Vill du se alla kategori som finns (J/n/q)? ");

                            string inputChoiceCategory = Console.ReadLine().Trim().ToLower();

                            if (inputChoiceCategory == "" || inputChoiceCategory == "j")
                            {
                                ShowCategorys.showCategorys(false);
                                break;
                            }
                            else if (inputChoiceCategory == "q")
                            {
                                masterBreak = false;
                                return;
                            }
                            else if (inputChoiceCategory == "n")
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static void showEditArticleName()
        {
            while (true)
            {
                Console.Write($"Ange artikelns namn ({results.articleName}) (q = avbryt) : ");
                newProductName = Console.ReadLine().Trim();

                if (newProductName == "")
                {
                    newProductName = results.articleName;
                }

                var resultRroductName = Program.articles.Find(str => str.articleName.ToLower() == newProductName.ToLower());

                if (newProductName.ToLower() == "q")
                {
                    masterBreak = false;
                    return;
                }

                else if (newProductName == "")
                {
                    newProductName = results.articleName;
                    break;
                }
                else if (newProductName.Length > 0)
                {
                    break;

                }
            }
        }

        private static void showEditArticlePrice()
        {
            while (true)
            {
                //Console.Write($"Ange produktens pris ({results.articlePurchasePrice} kr) (q = avbryt) : ");
                Console.Write($"Ange artikelns inköpspris ({results.articlePurchasePrice}): (q = avbryt) ");
                string inputProducPrice = Console.ReadLine().Trim();

                if (inputProducPrice.ToLower() == "q")
                {
                    return;
                }
                
                else
                {
                    if (inputProducPrice == "")
                    {
                        inputProducPrice = results.articlePurchasePrice.ToString();
                    }

                    if (float.TryParse(inputProducPrice, out purchePrice))
                    {
                        if (purchePrice > 0)
                        {
                            while (true)
                            {
                                var result = Program.articles.Find(x => x.articleCategory == newCategoryId);

                                int vatGroup = Mastersettings.defaultVATclass;

                                if (result != null)
                                {
                                    vatGroup = result.articleVATcategory;
                                }

                                EditCategory.showVAT(vatGroup, false);

                                minSalePrice = purchePrice * (1 + Program.vatData[EditCategory.newCategoryVAT]);

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
                Console.Write($"Ange artikelns pris (får ej vara mindre än {minSalePrice}, nuvarande pris är {results.articlePrice}): (q = avbryt) ");
                string inputProducPrice = Console.ReadLine().Trim();

                if (inputProducPrice.ToLower() == "q")
                {
                    return;
                }
                else if (inputProducPrice.Length == 0)
                {
                    inputProducPrice = results.articlePrice.ToString();
                }
                
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

            bool subRunStock = true;
            while (subRunStock)
            {
                Console.Write($"Ange lager antal ({results.articleStock}): (q = avbryt) ");
                string inputStockNumber = Console.ReadLine().Trim();

                if (inputStockNumber.ToLower() == "q")
                {
                    return;
                }
                else if (inputStockNumber == "")
                {
                    inputStockNumber = results.articleStock.ToString();
                }
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

        private static void saveEditToRegister()
        {
            results.articleSKU = newSkU;
            results.articleCategory = newCategoryId;
            results.articleName = newProductName;
            results.articlePrice = productPrice;
            results.articleVATcategory = EditCategory.newCategoryVAT;
            results.articlePurchasePrice = purchePrice;
            results.articleStock = stocknumber;

            FileRegister.saveRegister();
        }

        public static void editArticle()
        {
            while (true)
            {
                subRunEditArticle = true;

                while (subRunEditArticle)
                {
                    showSelectArticleToEdit();
                    if (!masterBreak)
                    {
                        return;
                    }
                }

                if (!masterBreak)
                {
                    return;
                }

                showSelectCategoryForProduct();

                if (!masterBreak)
                {
                    return;
                }

                showEditArticleName();

                if (!masterBreak)
                {
                    return;
                }

                showEditArticlePrice();

                if (!masterBreak)
                {
                    return;
                }
                
                saveEditToRegister();

                Console.Write("Vill du editera fler artiklar (j/N)? ");

                string inputEditMoreArticles = Console.ReadLine().Trim().ToLower();

                if (inputEditMoreArticles == "" || inputEditMoreArticles == "n")
                {
                    return;
                }
                else if (inputEditMoreArticles == "j")
                {
                    Console.Clear();
                    continue;
                }
                else if (inputEditMoreArticles == "q")
                {
                    return;
                }
            }
        }
    }
}
