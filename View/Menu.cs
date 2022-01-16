using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class Menu
    {
        //Display menu for the user
        public static void displayMenu()
        {
            List<string> Menu = new List<String>();

            Menu.Add("Addera artikel"); //1
            Menu.Add("Sök artikel"); //2
            Menu.Add("Presentera alla inventarier"); //3
            Menu.Add("Editera inventarie"); //4
            Menu.Add("Raddera inventarie"); //5
            Menu.Add("Hantera kontor"); //5
            Menu.Add("Avsluta programmet"); //6

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
                                AddInventory.addArticle();
                                Console.Clear();
                                break;

                            case 2:
                                SearchInventory.searchInventory();
                                Console.Clear();
                                break;

                            case 3:
                                Console.Clear();
                                ListInventorys.listInventorys();
                                Console.Clear();
                                break;
                            case 4:
                                Console.Clear();
                                EditInventory.editInventory();
                                Console.Clear();
                                break;

                            case 5:
                                Console.Clear();
                                RemoveInventory.removeInventory();
                                Console.Clear();
                                break;
                            case 6:
                                Console.Clear();
                                ManageOffice.manageOffice();
                                Console.Clear();
                                break;
                            case 7:
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
