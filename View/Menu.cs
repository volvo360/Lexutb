using System;
using System.Collections.Generic;

namespace Checkpoint_2
{
    class DisplayMenu
    {
       //Display menu for the user
       public static void displayMenu()
        {
            List<string> Menu = new List<String>();

            Menu.Add("Addera artikel"); //1
            Menu.Add("Editera artikel"); //2
            Menu.Add("Sök"); //3
            Menu.Add("Visa alla artiklar"); //4
            Menu.Add("Raddera artikel"); //5
            Menu.Add("Visa alla varugrupper"); //6
            Menu.Add("Addera varugrupp"); //7
            Menu.Add("Editera varugrupp"); //8
            Menu.Add("Editera moms för varugrupp"); //9
            Menu.Add("Raddera varugrupp"); //10
            Menu.Add("Avsluta programmet"); //11

            bool runMenu = true;

            while (runMenu)
            {
                Console.ResetColor();

                int i = 1;

                foreach (var item in Menu)
                {
                    Console.WriteLine($"{i} {item}");

                    i++;
                }

                Console.Write("\nVälj menyalternativ : ");

                int menuOption = 0;

                string inputChoiceMenu = Console.ReadLine().Trim().ToLower();  

                if (int.TryParse(inputChoiceMenu, out menuOption))
                {
                    if (menuOption >= 1 && (menuOption < Menu.Count + 1))
                    {
                        switch (menuOption)
                        {
                            case 1:
                                AddArticle.addProduct();
                                Console.Clear();
                                break;

                            case 2:
                                EditArticle.editArticle();
                                Console.Clear();
                                break;

                            case 3:
                                Console.Clear();
                                Search.search();
                                Console.Clear();
                                break;

                            case 4:
                                Console.Clear();
                                ShowArticles.showArticles();
                                Console.Clear();
                                break;

                            case 5:
                                ViewRemoveProduct.removeArticle();
                                Console.Clear();
                                break;

                            case 6:
                                Console.Clear();
                                ShowCategorys.showCategorys();
                                Console.ReadKey();
                                Console.Clear();
                                break;

                            case 7:
                                Console.Clear();
                                AddCattegory.addCategory();
                                Console.Clear();
                                break;
                                
                            case 8:
                                Console.Clear();
                                EditCategory.editCategory();
                                Console.Clear();
                                break;

                            case 9:
                                Console.Clear();
                                EditCategory.editVATCategory();
                                Console.Clear();
                                break;

                            case 10:
                                ViewRemoveCategory.removeCategory();
                                Console.Clear();
                                break;

                            case 11:
                                Console.Clear();
                                runMenu = false;
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Inget giltigt alternativ! Tryck på enter för att starta om.");
                                Console.ReadKey();
                                Console.ResetColor();
                                Console.Clear();
                                break;
                        }
                    }                    
                }
                else
                {
                    if (inputChoiceMenu == "q")
                    {
                        Console.Clear();
                        runMenu = false;
                        break;
                    }

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write("Ange ett giltigt meny alternativ! Tryck på en tangent för att starta om.");
                    Console.ReadKey();
                    Console.ResetColor();
                    Console.Clear();
                }
            }
        }
    }
}