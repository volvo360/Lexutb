using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class ViewRemoveCategory
    {
        public static void removeCategory()
        { 
            bool runRemoveProduct = true;

            while (runRemoveProduct)
            {
                Console.Clear();

                string menuOption = null;

                while (true)
                    { 
                    Console.Write("Ange kategorinummer på kategorin du vill raddera (q = avbryt)? ");

                    menuOption = Console.ReadLine().Trim().ToLower();

                    if (menuOption == "q")
                    {
                        return;
                    }

                    if (!CheckSyntax.check("Categorys", menuOption).correct)
                    {
                        Console.Write("Du har inte angivet rätt syntax för ett kategori id! Tryck på en tangent för att göra ett nytt försök!");
                        Console.ReadKey();
                        Console.WriteLine();
                    }
                    else
                    {
                        break;
                    }
                }

                if (typeof(Mastersettings).GetType().GetProperty("syntax") != null)
                {
                    menuOption = CheckSyntax.check("Categorys", menuOption).fixedString;
                }

                else if (MastersettingsArticle.forceUpercase)
                {
                    menuOption = menuOption.ToUpper();
                }

                var res = Program.categorys.Find(x => x.categoryId == menuOption);

                while (true)
                {
                    Console.Write($"Vill du verkligen radera kategorin \"{res.categoryName}\" (J/n/q) ");
                    string inputSelectChoice = Console.ReadLine().Trim().ToLower();

                    if (inputSelectChoice == "" || inputSelectChoice == "j")
                    {
                        runRemoveProduct = false;
                        break;
                    }
                    else if (inputSelectChoice == "q")
                    {
                        return;
                    }
                    else if (inputSelectChoice == "n")
                    {
                        break;
                    }
                }

                if (ModelSearchCategory.removeCategory(menuOption))
                {
                    Console.WriteLine("kategorin är raderad! Tryck på enter!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("kategorin finns inte! Tryck på enter!");
                    Console.ReadKey();
                }
            }
        }
    }
}
