using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkpoint_1_OOP
{
    internal class Views
    {
        public static bool editMultipleSKU = true;

        static public void showProductInfo()
        {
            while (true)
            {
                Console.Write("Ange artikelnummer för produkten du vill ha mer info om  (exit för att avbryta) : ");

                string searchSKU = Console.ReadLine();

                if (searchSKU.ToLower() == "exit")
                {
                    return;
                }

                if (validateSKU.foreceUpercase)
                { 
                    searchSKU = searchSKU.ToUpper();
                }

                //Lambda expression to see if sku already exists in our register
                var reponse = Product.products_OOP.Find(r => r.sku == searchSKU);

                if (reponse != null)
                {
                    Console.WriteLine($"Artikel nummer : {reponse.sku}\n");
                    Console.WriteLine($"Artikel : {reponse.articletName}\n");
                    Console.WriteLine($"Beskrivning : {reponse.articleDescription}\n");
                    Console.WriteLine($"Pris : {reponse.price}\n");
                    Console.WriteLine($"Moms : {Product.vatData[reponse.vatClass]*100}%");

                    break;
                }

                else
                { 
                    Console.WriteLine("Artikelnumret finns inte i registret!\n");
                }


            }
        }

        static public void showProducts()
        {
            if (Product.products_OOP.Count() > 0)
            {
                //Lambda expressions for sorting our articles based on article numbers.
                var sortedKey = Product.products_OOP.OrderBy(x => x.sku);

                Console.WriteLine("Produkterna sorterade på artikelnummer:\n");

                Console.WriteLine("Artikelnummer:\tProdukt");

                foreach (var kvp in sortedKey)
                {
                    Console.WriteLine($"{kvp.sku}\t{kvp.articletName}");
                }

                //Lambda expression to sort our articles based on article.
                var sortedProduct = Product.products_OOP.OrderBy(x => x.articletName);

                Console.WriteLine("\nProdukterna sorterade på produkt:\n");

                Console.WriteLine("Artikelnummer:\tProdukt");

                foreach (var  kvp in sortedProduct)
                {
                    Console.WriteLine($"{kvp.sku}\t{kvp.articletName}");
                }
}
            else
            {
                Console.WriteLine("Det finns inga produkter i programmet!");
            }
        }

        public static void showMenu()
        {
            int value;

            bool menuRun = true;

            List<string> menuoption = new List<string>();

            menuoption.Add("Läs in produkterna från fil");
            menuoption.Add("Spara proukterna till fil");
            menuoption.Add("Addera produkt till registret");
            menuoption.Add("Redigera produkt i registret");
            menuoption.Add("Undersök produkt");
            menuoption.Add("Presentera alla produkter");
            menuoption.Add("Raddera produkt i registret");
            menuoption.Add("Avsluta programmet");

            while (menuRun)
            {
                for (int i = 1; i <= menuoption.Count(); i++)
                {
                    int k = i - 1;

                    Console.WriteLine($"{i} {menuoption[k]}");
                }

                Console.Write("\nVälj ett altermativ i menyn : ");

                string option = Console.ReadLine();

                if (int.TryParse(option, out value) && value >= 1 && value <= menuoption.Count())
                {
                    //Console.WriteLine($"Du valde option {menuoption[--value]}");
                    Console.Clear();

                    switch (value)
                    {
                        case 1:
                            Product.readProducts();

                            Console.Clear();

                            break;

                        case 2:
                            Product.saveRegister();

                            Console.Clear();

                            break;

                        case 3:
                            Product.addProducts();
                            
                            Console.Clear();

                            break;

                        case 4:
                            while (Views.editMultipleSKU)
                            { 
                                Product.editProduct();

                                Console.Clear();
                            }

                            Views.editMultipleSKU = true;

                            break;

                        case 5:
                            showProductInfo();

                            Console.WriteLine("\nTryck på enter!");

                            Console.ReadLine();

                            Console.Clear();

                            break;


                        case 6:
                            showProducts();
                            
                            Console.WriteLine("\nTryck på enter!");
                            
                            Console.ReadLine();
                            
                            Console.Clear();

                            break;

                        case 7:
                            Product.deleteProduct();
                            
                            Console.Clear();

                            break;

                        case 8:
                            Product.exitProgram();
                            
                            menuRun = false;

                            break;

                        default:
                            break;
                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("\nVänligen Ange ett giltigt meny alternativ!\n");
                    Console.ResetColor();
                    
                    Console.WriteLine("Tryck på enter för att starta om!");
                    Console.ReadLine();

                    Console.Clear();
                }
            }
        }
    }
}