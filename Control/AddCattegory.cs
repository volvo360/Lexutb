using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    internal class AddCattegory
    {
        public static string masterCategoryId = null;

        public static void addCategory()
        {
            bool masterRun = true;

            while (masterRun)
            {
                Console.Write("Vilket id vill du ange på den nya kategorin (q = avbryt)? ");

                string inputCategoryId = Console.ReadLine();

                if (inputCategoryId.ToLower() == "q")
                {
                    MastersettingsArticle.foreceBreak = false;

                    Console.Clear();

                    return;
                }

                if (!CheckSyntax.check("Categorys", inputCategoryId).correct)
                {
                    Console.WriteLine("Tryck på en tangent för att göra ett nytt försök! ");
                    Console.ReadKey();
                    continue;
                }

                if (MastersettingsArticle.forceUpercase)
                {
                    inputCategoryId.ToUpper();
                }

                bool subRun = true;

                while (subRun)
                {
                    Console.Write("Ange kategori namn (q = avbryt): ");

                    string inputCategoryName = Console.ReadLine().Trim();

                    if (inputCategoryName.ToLower() == "q")
                    {
                        return;
                    }

                    else if (inputCategoryName.Length > 0)
                    {
                        if (MastersettingsArticle.forceUpercase)
                        {
                            inputCategoryId = inputCategoryId.ToUpper();
                        }

                        while(true)
                        {
                            Console.WriteLine("Ange önskad standard grupp för moms för artiklar i kategorin:");

                            EditCategory.showVAT(Mastersettings.defaultVATclass, false);

                            break;
                        }

                        Program.categorys.Add(new Categorys(inputCategoryId, inputCategoryName, EditCategory.newCategoryVAT));

                        FileRegister.saveRegister();

                        masterCategoryId = inputCategoryId; 

                        bool addMoreCategory = true;

                        while (addMoreCategory)
                        {
                            Console.Write("Vill du addera ytterligare varugrupper (J/n)? ");

                            string moreInput = Console.ReadLine().Trim().ToLower();

                            if (moreInput == "n")
                            {
                                addMoreCategory = false;
                                subRun = false;
                                masterRun = false;

                                break;
                            }
                            else if (moreInput == "j" || moreInput == "")
                            {
                                Console.Clear();

                                break;
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Kategori namn får ej vara tomt!!!");
                    }
                }
            }
        }
    }
}
