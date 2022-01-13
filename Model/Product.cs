using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;

namespace Checkpoint_1_OOP
{
    class Product
    {
        public static List<Product> products_OOP = new List<Product>();

        //sku = stock keeping unit (artikelnummer)

        public string sku { get; set; }
        public string articletName { get; set; }
        public string articleDescription { get; set; }

        public float price { get; set; }

        public int vatClass { get; set; }

        public static Dictionary<int, float> vatData = new Dictionary<int, float>();

        public static void buildVatDictonary()
        {
            vatData.Add(1, (float)0.25);
            vatData.Add(2, (float)0.12);
            vatData.Add(3, (float)0.06);
            vatData.Add(4, 0);
        }

        public Product(string sku2, string articletName2, string articleDescription2, float price2, int vatClass2)
        {
            //buildVatDictonary();

            sku = sku2;
            articletName = articletName2;
            articleDescription = articleDescription2;

            price = price2;

            vatClass = vatClass2;
        }

        public static void addProducts()
        {
            bool run = true;

            if (Program.products.Count() == 0)
            {
                if (File.Exists("ArticleRegister.txt"))
                {
                    string json = File.ReadAllText("ArticleRegister.txt");

                    products_OOP = JsonConvert.DeserializeObject<List<Product>>(json);
                }
            }

            Console.WriteLine("Skriv in produkter. Avsluta med att skriva 'exit'\n");

            while (run)
            {
                Console.Write("Ange artikel id: ");

                string temp_sku = Console.ReadLine().Trim();

                string[] temp_array = new string[0];

                if (temp_sku.ToLower() == "exit")
                {
                    break;
                }

                if (validateSKU.verifySKU(temp_sku))
                {
                    bool run2 = true;

                    if (validateSKU.foreceUpercase)
                    {
                        temp_sku = temp_sku.ToUpper();
                    }

                    while (run2)
                    {
                        Console.Write("Ange produktens namn : ");

                        string articletName = Console.ReadLine().Trim();

                        if (articletName.ToLower() == "exit")
                        {
                            run = false;

                            break;
                        }

                        if (articletName.Length > 0)
                        {
                            bool run3 = true;

                            while (run3)
                            {
                                Console.Write("Ange produktens beskrivning : ");

                                string articleDescription = Console.ReadLine().Trim();

                                if (articleDescription.ToLower() == "exit")
                                {
                                    run = false;

                                    break;
                                }

                                if (articleDescription.Length > 0)
                                {
                                    bool run4 = true;

                                    while (run4)
                                    {
                                        Console.Write("Ange produktens pris : ");

                                        string productPrice = Console.ReadLine().Trim();

                                        if (productPrice.ToLower() == "exit")
                                        {
                                            run = false;

                                            break;
                                        }

                                        float price = float.Parse(productPrice, CultureInfo.InvariantCulture);

                                        if (price > 0)
                                        {
                                            bool run5 = true;

                                            while (run5)
                                            {
                                                Console.WriteLine("Ange produktens momskategori, välj av nedasnstående alternativ : \n");

                                                Console.WriteLine("Momskategori\tMomssats \n");

                                                foreach (KeyValuePair<int, float> vatTemp in vatData)
                                                {
                                                    Console.WriteLine($"{vatTemp.Key.ToString()}\t{(100 * vatTemp.Value).ToString()}%");
                                                }

                                                string vatCategory = Console.ReadLine().Trim();

                                                if (vatCategory.ToLower() == "exit")
                                                {
                                                    run = false;

                                                    break;
                                                }

                                                if (vatCategory.All(char.IsDigit))
                                                {
                                                    int tempValue = int.Parse(vatCategory);

                                                    if (vatData.ContainsKey(tempValue))
                                                    {
                                                        Program.syncFile = true;

                                                        run4 = false;

                                                        Product tempProdouct = new Product(temp_sku, articletName, articleDescription, price, tempValue); ;

                                                        products_OOP.Add(tempProdouct);

                                                        run5 = false;

                                                        break;
                                                    }
                                                }
                                            }
                                        }

                                        Program.syncFile = true;

                                        run3 = false;

                                        break;
                                    }
                                }

                                run2 = false;

                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void editProduct()
        {
            string artId = null;

            while (artId != "exit")
            {
                Console.Write("Vilken artikelnummer vill du ändra produktnamn på (exit för att avbrytta)? ");

                artId = Console.ReadLine().Trim().ToLower();

                if (artId == "exit")
                {
                    Views.editMultipleSKU = false;
                    break;
                }

                if (validateSKU.foreceUpercase)
                {
                    artId = artId.ToUpper();
                }

                //Lambda expression to see if sku already exists in our register
                var reponse = products_OOP.Find(r => r.sku == artId);

                if (reponse != null)
                {
                    Console.Write("Vill du verkligem editera produkten \"" + reponse.articletName + "\" (J/N) ");

                    string run = Console.ReadLine().Trim().ToLower();

                    if (run == "j" || run == "ja")
                    {
                        bool sub_run = true;

                        while (sub_run)
                        {
                            Console.Write($"Artikelnummer : {reponse.sku} (tryck på enter för att vara oförändrat) : ");

                            var temp_input = Console.ReadLine().Trim();

                            if (temp_input.Length == 0)
                            {
                                //do nothing
                                break;
                            }

                            else
                            {
                                if (validateSKU.verifySKU(temp_input))
                                {
                                    if (validateSKU.foreceUpercase)
                                    {
                                        temp_input = temp_input.ToUpper();
                                    }

                                    reponse.sku = temp_input;

                                    Program.syncFile = true;

                                    break;
                                }
                            }
                        }

                        while (sub_run)
                        {
                            Console.Write($"Produkt : {reponse.articletName} (tryck på enter för att vara oförändrat) : ");

                            var temp_input = Console.ReadLine().Trim();

                            if (temp_input.Length == 0)
                            {
                                //do nothing
                                break;
                            }

                            else
                            {
                                reponse.articletName = temp_input;

                                Program.syncFile = true;

                                break;
                            }
                        }

                        while (sub_run)
                        {
                            Console.Write($"Beskrivning : {reponse.articleDescription} (tryck på enter för att vara oförändrat) : ");

                            var temp_input = Console.ReadLine().Trim();

                            if (temp_input.Length == 0)
                            {
                                //do nothing
                                break;
                            }

                            else
                            {
                                reponse.articleDescription = temp_input;

                                Program.syncFile = true;
                                break;
                            }
                        }

                        while (sub_run)
                        {
                            Console.Write($"Pris : {reponse.price} (tryck på enter för att vara oförändrat) : ");

                            var temp_input = Console.ReadLine().Trim();

                            if (temp_input.Length == 0)
                            {
                                //do nothing
                                break;
                            }

                            else
                            {
                                float price = float.Parse(temp_input, CultureInfo.InvariantCulture);

                                if (price > 0)
                                {
                                    reponse.articleDescription = temp_input;

                                    Program.syncFile = true;
                                    break;
                                }
                            }
                        }

                        while (sub_run)
                        {
                            Console.WriteLine("Ange produktens momskategori, välj av nedasnstående alternativ : \n");
                            
                            Console.WriteLine("Momskategori\tMomssats \n");

                            foreach (KeyValuePair<int, float> vatTemp in vatData)
                            {
                                if (vatTemp.Key == reponse.vatClass)
                                {
                                    Console.WriteLine($"* {vatTemp.Key.ToString()}\t{(100 * vatTemp.Value).ToString()}%");
                                }

                                else
                                {
                                    Console.WriteLine($"{vatTemp.Key.ToString()}\t{(100 * vatTemp.Value).ToString()}%");
                                }
                            }

                            string vatCategory = Console.ReadLine().Trim();

                            if (vatCategory.ToLower() == "exit")
                            {
                                artId = "exit";

                                Views.editMultipleSKU = false;

                                break;
                            }

                            if (vatCategory == "" || vatCategory == null)
                            {
                                artId = "exit";

                                break;
                            }

                            if (vatCategory.All(char.IsDigit))
                            {
                                int tempValue = int.Parse(vatCategory);

                                if (vatData.ContainsKey(tempValue))
                                {
                                    reponse.vatClass = tempValue;

                                    artId = "exit";

                                    break;
                                }
                            }
                        }
                    }
                    else if (run == "n" || run == "nej")
                    {
                        artId = "exit";
                    }
                }
                else
                {
                    Console.WriteLine("Artikelnumret existerar inte!!!!\n");
                }
            }
        }

        public static void deleteProduct()
        {
            bool tempRun = true;

            while (tempRun)
            {
                Console.Write("Ange vilken artikel nummer du vill raddera (exit avbrytter): ");

                string removeProduct = Console.ReadLine().Trim().ToUpper();

                if (removeProduct.ToLower() == "exit")
                {
                    return;
                }

                var reponse = Product.products_OOP.Find(r => r.sku == removeProduct);

                if (reponse != null)
                {
                    Console.Write($"Är du säker på att du vill raddera produkten \"{reponse.articletName}\" (J/N) ? ");
                    
                    string res = Console.ReadLine();

                    if (res.ToLower() == "j" || res.ToLower() == "ja")
                    {
                        //Lambda expression to find which id the article has in our global list to later be able to delete it.
                        int id = products_OOP.FindIndex(x => x.sku == removeProduct);
                        
                        //Remove artivle from our list of articles
                        products_OOP.RemoveAt(id);

                        Program.syncFile = true;

                        tempRun = false;
                    }
                }
                else
                {
                    Console.WriteLine("Inte giltigt artikelnummer!");

                    Console.ReadLine();
                }
            }
        }

        public static void exitProgram()
        {
            if (Program.syncFile)
            {
                while (true)
                { 
                    Console.Write("Du har ej sparat ändringarna i regristret till filen, vil du göra detta? (j/n) ");

                    string input = Console.ReadLine();

                    if (input.ToLower() == "j" || input.ToLower() == "ja")
                    {
                        Product.saveRegister();
                        break;
                    }
                    else if (input.ToLower() == "n" || input.ToLower() == "nej")
                    {
                        //Do nothing
                        break;
                    }
                }
            }
            else
            {
                // End function, we shouldn't save our registry
            }

            Console.Clear();
        }
        
        public static void readProducts()
        {
            if (!File.Exists("ArticleRegister.txt"))
            {
                return;
            }

            string json = File.ReadAllText("ArticleRegister.txt");
            products_OOP = JsonConvert.DeserializeObject<List<Product>>(json);

            Views.showProducts();

            Console.WriteLine("\nTryck på enter för att fortsätta!");

            Console.ReadLine();
        }

        public static async void saveRegister()
        {
            string json = JsonConvert.SerializeObject(products_OOP);

            await File.WriteAllTextAsync("ArticleRegister.txt", json);

            Console.WriteLine("Filen har sparats lokalt! Vänligen tryck på enter!");

            Program.syncFile = false;

            Console.ReadLine();
        }
    }
}
