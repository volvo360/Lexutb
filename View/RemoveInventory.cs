using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class RemoveInventory
    {
        public static void removeInventory()
        {
            while (true)
            {
                Console.Clear();

                Console.Write("Ange inventarie id (q = avbryt) : ");

                string inputInventoryToRemove = Console.ReadLine().Trim().ToLower();

                if (inputInventoryToRemove == "q")
                {
                    return;
                }
                else if (inputInventoryToRemove == "" || inputInventoryToRemove.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Den inmatade strängen får ej vara tom!!!\n");

                    Console.ResetColor();

                    Console.Write("Tryck på en tangent för att börja om!");
                    Console.ReadKey();

                    continue;

                }
                var temp = CheckSyntax.check("MastersettingsInventory", inputInventoryToRemove);

                if (temp.correct)
                {
                    var result = Program.inventories.Where(x => x.InventoryId == temp.fixedString).ToList();

                    if (result.Count >0)
                    {
                        Console.WriteLine();

                        ShowListInventorys.showListInventorys(result, false);

                        Console.Write("\nVill du verkligen radera denna inventarie (j/N)? ");

                        string inputDeleate = Console.ReadLine().Trim().ToLower();

                        if (inputDeleate == "" || inputDeleate == "n")
                        {
                            continue;
                        }
                        else if (inputDeleate == "j")
                        {
                            ControlRemoveInventory.removeArticle(temp.fixedString);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Den inmatade inventarie id förekommer inte!!!\n");

                        Console.ResetColor();

                        Console.Write("Tryck på en tangent för att börja om!");
                        Console.ReadKey();
                    }
                }

            }
        }
    }
}
