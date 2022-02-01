using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    internal class ManageInventoyryCategory
    {
        private static int oldCategoryId = 0;

        private static string oldCategory = null;

        private static string newCategoryName = null;

        static InventoryContext context = new InventoryContext();

        static bool masterBreak = false;

        private static int editProp = 0;

        private static List<CategoryProp> newCategoryProp = new List<CategoryProp>();

        private static List<CategoryPropText> newCategoryPropText = new List<CategoryPropText>();

        private static void editInventoryCategoryName()
        {
            while (true)
            {
                Console.Write("Ange nytt namn på inventariekatogorin (" + oldCategory + ") (q = avbryt): ");

                var inputString = Console.ReadLine().Trim();

                if (inputString.ToLower() == "q")
                {
                    masterBreak = true;
                    break;
                }
                else if (inputString == "")
                {
                    newCategoryName = oldCategory;
                    break;
                }
                else if (inputString.Length > 0)
                {
                    var temp = CheckInventoryCategory.checkInventoryCategory(inputString);

                    if (!temp.status)
                    {
                        newCategoryName = inputString;

                        var temp2 = context.Category.Include(x => x.CategoryTexts).FirstOrDefault(x => x.CategoryTexts.CategoryId == oldCategoryId);
                        oldCategory = temp2.CategoryTexts.Name = CommonFunction.FirstLetterToUpper(inputString);
                        //context.SaveChanges();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Kategorinamnet finns redan, vill du flytta alla inventarier till denna kategori (j/N/q = avbryt)? ");
                    }
                }
            }
        }

        private static void editProperties(int inputExtraParameter)
        {
            if (inputExtraParameter > 0)
            {
                var temp = context.Category.Include(x => x.CategoryTexts).Include(z => z.CategoryProps).ThenInclude(x => x.CategoryPropTexts).FirstOrDefault(x => x.CategoryTexts.Id == inputExtraParameter);

                while(true)
                { 
                    Console.Write("Vill du (e)ditera alternativet eller (r)adera det (E/t/q = avbrytt) ? ");

                    string inputChoice = Console.ReadLine().Trim().ToLower();

                    if (inputChoice == "r" || inputChoice == "radera")
                    {
                        var temp2 = context.Category.Include(x => x.CategoryTexts).Include(z => z.CategoryProps).ThenInclude(x => x.CategoryPropTexts).Where(x => x.CategoryProps[0].Id == oldCategoryId);

                        Console.WriteLine("Vill du verkligen radera \"" + temp.CategoryTexts.ExtraText + "\" över tillgängliga frågor (j/N/q = avbrytt) ? ");
                    
                        string inputConfirmDelete = Console.ReadLine().Trim().ToLower();

                        if (inputConfirmDelete == "n" || inputConfirmDelete == "")
                        {
                            break;
                        }
                        else if (inputConfirmDelete == "j" || inputConfirmDelete == "ja")
                        {
                            context.CategoryProp.Remove(new CategoryProp(new Category() { Id = (int)temp.CategoryTexts.CategoryId}));
                            break;
                        }
                    
                    }
                    else if (inputChoice == "" || inputChoice == "e" || inputChoice == "editera")
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.Write($"Ange fråga ({temp.CategoryTexts.ExtraText}) : ");

                    string inputQuestionText = Console.ReadLine().Trim();

                    if (inputQuestionText == "q")
                    {
                        masterBreak = true;

                        return;
                    }
                    else if (inputQuestionText == "")
                    {
                        inputQuestionText = temp.CategoryTexts.ExtraText;
                        break;
                    }

                    else if (inputQuestionText.Length > 0)
                    {
                        var temp2 = context.Category.Include(x => x.CategoryTexts).Include(z => z.CategoryProps).FirstOrDefault(x => x.CategoryTexts.ExtraInsertText.ToLower() == inputQuestionText.ToLower() && x.Id == oldCategoryId && x.CategoryTexts.Lang == "sv");

                        if (temp2 == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("Du försöker att ange ett alternativ som redan existerar");

                            Console.ResetColor();
                            Console.WriteLine();

                            continue;
                        }

                        temp2.CategoryTexts.ExtraInsertText = CommonFunction.FirstLetterToUpper(inputQuestionText);

                        break;
                    }
                }
                
                while(true)
                {
                    if (temp.CategoryProps[0].Value == "true" || temp.CategoryProps[0].Value == "false")
                    {
                        Console.Write("Det är en Ja eller nej fråga (J/n/q = avbryt) ? ");

                        string inputBool = Console.ReadLine().Trim().ToLower();

                        if (inputBool == "q")
                        {
                            masterBreak = true;
                        }
                        else if (inputBool == "" || inputBool == "j")
                        {
                            Console.Write("Önskat förval (");

                            if (temp.CategoryProps[0].Value == "true")
                            {
                                Console.Write("J/n)");
                            }
                            else
                            {
                                Console.Write("j/N)");
                            }

                            Console.Write("? ");

                            string inputDefaultBool = Console.ReadLine().Trim().ToLower();

                            if (inputDefaultBool == "")
                            {
                                inputDefaultBool = temp.CategoryProps[0].Value;
                            }
                            else if (inputDefaultBool == "j")
                            {
                                inputDefaultBool = "true";
                            }
                            else if (inputDefaultBool == "n")
                            {
                                inputDefaultBool = "false";
                            }
                            else if (inputDefaultBool == "q")
                            {
                                masterBreak=false;
                                return;
                            }
                            temp.CategoryProps[0].Value = inputDefaultBool;

                            break;

                        }
                        else
                        {
                            Console.Write("Inmatningssträng vald. Vänligen bekräfta valet j/N/q = avbryt?");

                            string inputConfirmInputString = Console.ReadLine().Trim().ToLower();

                            if (inputConfirmInputString == "" || inputConfirmInputString == "n" || inputConfirmInputString == "nej")
                            {
                                
                                break;
                            }
                            else if (inputConfirmInputString == "q")
                            {
                                masterBreak=true;
                            }
                            else if (inputConfirmInputString == "j" || inputConfirmInputString == "ja")
                            {
                                temp.CategoryProps[0].Value = "";
                            }
                        }
                    }
                    else
                    {
                        Console.Write("Förvalet är inmatningssträng, vill du ändra till ja/nej alternativ (j/N/q = avbryt) ? ");

                        string inputConfirmInputString = Console.ReadLine().Trim().ToLower();

                        if (inputConfirmInputString == "q")
                        {
                            masterBreak =true;
                            return;
                        }
                        else if (inputConfirmInputString == "" || inputConfirmInputString == "n" || inputConfirmInputString == "nej")
                        {
                            break ;
                        }
                        else if (inputConfirmInputString == "j" || inputConfirmInputString == "ja")
                        {
                            Console.Write("Önskat förval (J/n) ? ");

                            string inputDefaultBool = Console.ReadLine().Trim().ToLower();

                            if (inputDefaultBool == "j" || inputDefaultBool == "")
                            {
                                inputDefaultBool = "true";
                            }
                            else if (inputDefaultBool == "n")
                            {
                                inputDefaultBool = "false";
                            }
                            else if (inputDefaultBool == "q")
                            {
                                masterBreak = false;
                                return;
                            }
                            temp.CategoryProps[0].Value = inputDefaultBool;
                            break;
                        }
                    }
                }
            }
        }

        private static void addNewInventoryPropertie()
        {
            string inputQuestionText;
            string propertieText;
            string inputBool;

            while (true)
            {
                Console.Write($"Ange intern egenskap (engelsk term)  (q = avbryt): ");

                propertieText = Console.ReadLine().Trim().ToLower();

                if (propertieText == "q")
                {
                    return;
                }
                else if (propertieText.Length > 0)
                {
                    propertieText = propertieText.Replace(' ', '_');

                    break;
                }
            }

            while (true)
            { 
                Console.Write($"Ange fråga : ");

                inputQuestionText = Console.ReadLine().Trim();

                if (inputQuestionText == "q")
                {
                    masterBreak = true;

                    return;
                }
                else if (inputQuestionText.Length > 0)
                {
                    var temp2 = context.Category.Include(x => x.CategoryTexts).Include(z => z.CategoryProps).
                        ThenInclude(z => z.CategoryPropTexts).FirstOrDefault(x => x.CategoryProps[0].CategoryPropTexts.ExtraInsertText.ToLower() == inputQuestionText.ToLower()
                        && x.Id == oldCategoryId && x.CategoryTexts.Lang == "sv");

                    if (temp2 == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Du försöker att ange ett alternativ som redan existerar");

                        Console.ResetColor();
                        Console.WriteLine();

                        continue;
                    }

                    inputQuestionText = CommonFunction.FirstLetterToUpper(inputQuestionText);

                    break;
                }
            }

            while (true)
            {
                Console.Write("Det är en Ja eller nej fråga (J/n/q = avbryt) ? ");

                inputBool = Console.ReadLine().Trim().ToLower();

                if (inputBool == "q")
                {
                    masterBreak = true;
                }
                else if (inputBool == "" || inputBool == "j")
                {
                    Console.Write("Önskat förval (J/n) ? ");

                    inputBool = Console.ReadLine().Trim().ToLower();

                    if (inputBool == "")
                    {
                        inputBool = "true";
                    }
                    else if (inputBool == "j")
                    {
                        inputBool = "true";
                    }
                    else if (inputBool == "n")
                    {
                        inputBool = "false";
                    }
                    else if (inputBool == "q")
                    {
                        masterBreak = false;
                        return;
                    }

                    if (inputBool == "true" || inputBool == "false")
                    {
                        while (true)
                        {
                            Console.Write("Text som ska presenteras under samamnställning (q = avbryt) :");

                            string inputTextSumary = Console.ReadLine().Trim();

                            if (inputTextSumary == " q")
                            {
                                masterBreak = false;
                                return;
                            }
                            else if (inputTextSumary.Length > 0)
                            {
                                CategoryPropText tempCategoryPropText = new CategoryPropText("sv", inputQuestionText, inputTextSumary);

                                newCategoryPropText.Add(tempCategoryPropText);
                            }
                        }
                    }

                    break;
                }
                else if (inputBool == "n")
                {
                    inputBool = "";
                }
            }

            CategoryProp tempMewCategoryProp = new CategoryProp(oldCategoryId, propertieText, inputBool);

            newCategoryProp.Add(tempMewCategoryProp);
        }

        private static void editInventoryProperties()
        {
            var temp = context.Category.Include(x => x.CategoryTexts).Include(z => z.CategoryProps).ThenInclude(z => z.CategoryPropTexts).Where(x => x.Id == oldCategoryId);

            int i = 0;

            List<int> validList = new List<int>();

            Console.WriteLine("\nId".PadRight(5)+"Fråga (ev. alternativ)");

            foreach (var t in temp)
            {
                //foreach (var x in t)
                {
                    if (t.CategoryProps[i].Value == "true")
                    {
                        Console.Write(t.CategoryTexts.Id.ToString().PadRight(5)+t.CategoryProps[i].CategoryPropTexts.ExtraInsertText + " " + "J/n" + "\n");
                    }
                    else if (t.CategoryProps[i].Value == "false")
                    {
                        Console.Write(t.CategoryTexts.Id.ToString().PadRight(5) + " "+ t.CategoryProps[i].CategoryPropTexts.ExtraInsertText + " " + "j/N" + "\n");
                    }
                    else
                    {
                        Console.Write(t.CategoryTexts.Id.ToString().PadRight(5) + " " + t.CategoryProps[i].CategoryPropTexts.ExtraInsertText + " " + " (text)" + "\n");
                    }

                    validList.Add(t.CategoryTexts.Id);
                }
            }

            Console.WriteLine();
            
            while (true)
            {
                Console.Write("Vilken extra information för kategorin vill du editera eller (a)ddera (f= färdig med editering / q = avbryt)? ");

                string inputEditExtraParameter = Console.ReadLine().Trim();
                int inputExtraParameter = 0;

                if (inputEditExtraParameter == "q")
                {
                    masterBreak = true;
                    return;
                }
                if (inputEditExtraParameter == "f")
                {
                    return;
                }
                else if (inputEditExtraParameter == "a" || inputEditExtraParameter == "addera")
                {
                    addNewInventoryPropertie();
                    break;
                }
                else if (int.TryParse(inputEditExtraParameter, out inputExtraParameter))
                {
                    if (validList.Contains(inputExtraParameter))
                    {
                        editProperties(inputExtraParameter);
                    }
                }
            }
        }

        private static void editInvenyotyCategory()
        {
            editProp = 0;

            while (true)
            {
                if (oldCategory == null)
                {
                    Console.Write("Vilken inventariegrupp vill du editera (q = avbryt)? ");
                }
                else
                {
                    Console.Write("Vilken inventariegrupp vill du editera (" + oldCategory + ") (q = avbryt)? ");
                }

                string inputEditCategory = Console.ReadLine().Trim();

                if (inputEditCategory == "" && oldCategory != null)
                {
                    inputEditCategory = oldCategory;
                    break;
                }
                else if (inputEditCategory.ToLower() == "q")
                {
                    return;
                }
                else if (inputEditCategory.Length > 0)
                {
                    var t = CheckInventoryCategory.checkInventoryCategory(inputEditCategory);

                    if (t.status)
                    {
                        oldCategoryId = t.id;

                        oldCategory = t.name;

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Du angav en ej giltig kategori, vänligen ange någon av nedanstående : \n");

                        var temp = context.Category.Include(x => x.CategoryTexts).OrderBy(x => x.CategoryTexts);

                        foreach (var t_sub in temp)
                        {
                            Console.WriteLine(t_sub.Id+". "+t_sub.CategoryTexts.Name);
                        }
                        Console.WriteLine();
                    }
                }
            }

            if (!masterBreak)
            {
                editInventoryCategoryName();
            }
            else
            {
                return;
            }

            if (!masterBreak)
            {
                editInventoryProperties();
            }
            else
            {
                context.ChangeTracker.Clear();
                return;
            }

            if (!masterBreak)
            {
                context.ChangeTracker.Clear();
                Console.WriteLine("Inga ändringar har sparatas, tryck på en tangent för att komma åter till editeringsmenyn!");
            }
            else
            {
                if (newCategoryProp.Count > 0)
                {
                    int i = 0;

                    foreach (var temp in newCategoryProp)
                    {
                        context.CategoryProp.Add(temp);

                        context.SaveChanges();

                        var temp2 = newCategoryPropText[i];

                        temp2.CategoryPropId = temp.Id;

                        context.CategoryPropText.Add(temp2);
                        context.SaveChanges();
                    }
                }

                context.SaveChanges();
                Console.WriteLine("Ändringarna har sparatas, tryck på en tangent för att komma åter till editeringsmenyn!");
            }

            Console.ReadKey();
        }

        private static void addInvenyotyCategory()
        {
            Console.WriteLine("Funktionen ej inplmenterad än! Tryck på en tangent!");
            Console.ReadKey();
        }

        private static void deleteInvenyotyCategory()
        {
            Console.WriteLine("Funktionen ej inplmenterad än! Tryck på en tangent!");
            Console.ReadKey();
        }

        public static void manageInventoyryCategory()
        {
            List<CategoryProp> newCategoryProp = new List<CategoryProp>();

            List<CategoryPropText> newCategoryPropText = new List<CategoryPropText>();

            List<string> Menu = new List<String>();

            Console.WriteLine("Hantera inventariekategorier\n");

            Menu.Add("Editera inventariekategori"); //1
            Menu.Add("Addera inventariekategori"); //2
            Menu.Add("Raddera inventariekategori"); //3
            Menu.Add("Avsluta editering av inventariekategorier"); //4

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
                                Console.Clear();
                                editInvenyotyCategory();
                                Console.Clear();
                                break;

                            case 2:
                                Console.Clear();
                                addInvenyotyCategory();
                                Console.Clear();
                                break;

                            case 3:
                                Console.Clear();
                                deleteInvenyotyCategory();
                                Console.Clear();
                                break;
                            case 4:
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
