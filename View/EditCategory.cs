using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{ 
    public class EditCategory : ShowCategoryVAT
    {
        private static string oldCategoryId;
        public static string newCategoryId;
        private static string inputEditCategory;
        private static string newCategoryName;
        public static int newCategoryVAT;
        private static bool combineCategorys = false;

        private static bool masterBreak = true;

        private static Dictionary<string, object> fieldsDict = new Dictionary<string, object>();

        //Tried to split the functions into smaller sub functions, but it did not facilitate reading the code.
        //Worth a try anyway!

        public static void editVATCategory()
        {
            bool updateProductVAT = false;
            string inputEditVATgroupCategory;

            int existingVATgroup = 0;

            while (true)
            {
                while (true)
                {
                    if (AddCattegory.masterCategoryId != null)
                    {
                        Console.Write($"Vilken varugrupp vill du ändra momssatsen för alla produkter? " +
                            $"({AddCattegory.masterCategoryId}) (q = avbryt)?");
                    }
                    else
                    {
                        Console.Write($"Vilken varugrupp vill du ändra momssatsen för alla produkter? (q = avbryt)? ");
                    }

                    inputEditVATgroupCategory = Console.ReadLine().Trim();

                    if (inputEditVATgroupCategory.ToLower() == "q")
                    {
                        masterBreak = false;
                        return;
                    }

                    if (AddCattegory.masterCategoryId != null)
                    {
                        if (inputEditVATgroupCategory == "")
                        {
                            inputEditVATgroupCategory = AddCattegory.masterCategoryId;
                        }
                    }

                    var fields = typeof(MastersettingsCategory).GetFields();

                    if (fields != null)
                    {
                        foreach (FieldInfo field in fields)
                        {
                            fieldsDict[field.Name] = field.GetValue(field.Name);
                        }
                    }

                    // I don't understand why this dosen't work, if checking mastersettings then the field exist! 

                    if (typeof(MastersettingsCategory).GetType().GetProperty("format") != null)
                    {
                        inputEditVATgroupCategory = CheckSyntax.check("Categorys", oldCategoryId).fixedString;
                    }
                    else if (fieldsDict.ContainsKey("format"))
                    {
                        inputEditVATgroupCategory = CheckSyntax.check("Categorys", oldCategoryId).fixedString;
                    }
                    
                    else if (MastersettingsCategory.forceUpercase)
                    {
                        inputEditVATgroupCategory = inputEditVATgroupCategory.ToUpper();
                    }

                    if (inputEditVATgroupCategory == null || inputEditVATgroupCategory.Length == 0)
                    {
                        Console.Write("Ingen kategori angiven, vill du se alla kategorier (J/n/q) ? ");

                        string inputShowCategorys = Console.ReadLine().Trim().ToLower();


                        if (inputShowCategorys == "" || inputShowCategorys == "j")
                        {
                            ShowCategorys.showCategorys();

                            continue;
                        }
                        else if (inputShowCategorys == "q")
                        {
                            return;
                        }
                        else if (inputShowCategorys == "n")
                        {
                            break;
                        }
                    }

                    else if (CheckCategoryId.checkCategoryId(inputEditVATgroupCategory, false))
                    {
                        //Lambda expression find data in our categoryregister
                        var reponse = Program.categorys.Find(r => r.categoryId == inputEditVATgroupCategory);

                        while (true)
                        {
                            existingVATgroup = reponse.VATcategory;

                            Console.Write($"Vill du editera standardmomsen för \"{reponse.categoryName}\" (J/n/q) ");
                            string inputVerifyGroup = Console.ReadLine().Trim();

                            if (inputVerifyGroup == "" || inputVerifyGroup == "j")
                            {
                                if (reponse.VATcategory > 0)
                                {
                                    while (true)
                                    {
                                        Console.Write($"Nuvarande momssats är " +
                                            $"{Program.vatData[reponse.VATcategory] * 100}%, vill du ändra den (j/N/q)? ");

                                        string inputChangeVATcategory = Console.ReadLine().Trim().ToLower();

                                        if (inputChangeVATcategory == "" || inputChangeVATcategory == "n")
                                        {
                                            break;
                                        }
                                        else if (inputChangeVATcategory == "q")
                                        {
                                            return;
                                        }
                                        else if (inputChangeVATcategory == "j")
                                        {
                                            var response2 = Program.articles.Count(r => r.articleCategory == inputEditVATgroupCategory);


                                            if (response2 > 0)
                                            {
                                                showVAT(existingVATgroup, true, true);
                                            }
                                            else
                                            {
                                                showVAT(existingVATgroup, false, true);
                                            }
                                            
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    showVAT(existingVATgroup, true, true);

                                    break;
                                }
                            }
                            else if (inputVerifyGroup == "q")
                            {
                                return;
                            }
                            else if (inputVerifyGroup == "n")
                            {
                                break;
                            }

                            break;
                        }

                        break;
                    }
                    else
                    {
                        while(true)
                        { 
                            Console.Write("Varugruppen existerar inte, vill se alla varugrupper (J/n/q)? ");
                            string showCategorys = Console.ReadLine().Trim().ToLower();

                            if (showCategorys == "" || showCategorys == "j")
                            {
                                ShowCategorys.showCategorys(false);
                            }
                            else if (showCategorys == "q")
                            {
                                return;
                            }
                            else if (showCategorys == "n")
                            {
                                break;
                            }
                        }
                    }
                }

                var reponse2 = Program.categorys.Find(r => r.categoryId == inputEditVATgroupCategory);

                FileRegister.saveRegister();

                reponse2.VATcategory = newCategoryVAT;

                while (true)
                {
                    Console.Write("Vill du ändra momsen på fler varugrupper (j/N)? ");
                    string updateMoreCategorys = Console.ReadLine().Trim().ToLower();

                    if (updateMoreCategorys == "q")
                    {
                        return;
                    }

                    else if (updateMoreCategorys == "" || updateMoreCategorys == "n")
                    {
                        return;
                    }
                    else if (updateMoreCategorys == "n")
                    {
                        break;
                    }
                }
            }
        }

        private static void showEditCategory()
        {
            //if (!CheckCategoryId.checkCategoryId(newCategoryId, false))
            {
                var result = Program.categorys.Find(r => r.categoryId == oldCategoryId);

                while (true)
                {
                    Console.Write($"Ange det nya kategori namnet ({result.categoryName}) (q = avbryt) : ");

                    newCategoryName = Console.ReadLine().Trim();

                    if (newCategoryName == "")
                    {
                        newCategoryName = result.categoryName;
                        break;
                    }
                    else if (newCategoryName.Length > 0)
                    {
                        break;
                    }
                    else if (newCategoryName.ToLower() == "q")
                    {
                        masterBreak = false;
                        return;
                    }
                }

                EditCategory.showVAT(result.VATcategory, false);
           
                saveChanges();
            }
        }

        private static void showSelectCategory()
        {
            bool masterBreak2 = true;

            while (masterBreak2)
            {
                if (AddCattegory.masterCategoryId != null)
                {
                    Console.Write($"Ange id på varugruppen du vill editera ({AddCattegory.masterCategoryId})" +
                        $" (q = avbryt)? ");
                }
                else
                {
                    Console.Write($"Ange id på varugruppen du vill editera (q = avbryt)? ");
                }

                oldCategoryId = Console.ReadLine().Trim().ToLower();

                if (oldCategoryId == "q")
                {
                    masterBreak = false;
                    return;
                }

                if (AddCattegory.masterCategoryId != null)
                {
                    if (oldCategoryId == "")
                    {
                        oldCategoryId = AddCattegory.masterCategoryId;
                    }
                }

                var fields = typeof(MastersettingsCategory).GetFields();

                if (fields != null)
                {
                    foreach (FieldInfo field in fields)
                    {
                        fieldsDict[field.Name] = field.GetValue(field.Name);
                    }
                }

                // I don't understand why this dosen't work, if checking mastersettings then the field exist! 

                if (typeof(MastersettingsCategory).GetType().GetProperty("format") != null)
                {
                    oldCategoryId = CheckSyntax.check("Categorys", oldCategoryId).fixedString;
                }
                else if (fieldsDict.ContainsKey("format"))
                {
                    oldCategoryId = CheckSyntax.check("Categorys", oldCategoryId).fixedString;
                }
                else if (MastersettingsCategory.forceUpercase)
                {
                    oldCategoryId = oldCategoryId.ToUpper();
                }

                if (oldCategoryId == null || oldCategoryId.Length == 0)
                {
                    Console.Write("Ingen kategori angiven, vill du se alla kategorier (J/n/q) ? ");

                    string inputShowCategorys = Console.ReadLine().Trim().ToLower();

                    if (inputShowCategorys == "" || inputShowCategorys == "j")
                    {
                        ShowCategorys.showCategorys();

                        continue;
                    }
                    else if (inputShowCategorys == "q")
                    {
                        masterBreak = false;
                        return;
                    }
                    else if (inputShowCategorys == "n")
                    {
                        break;
                    }
                }

                else if (CheckCategoryId.checkCategoryId(oldCategoryId, false))
                {
                    //Do nothing for the moment, alright for the moment
                }

                else
                {
                    while (true)
                    {
                        Console.Write("Kategori id finns inte, vill du se alla kategorier (J/n/q) ? ");

                        string inputShowCategorys = Console.ReadLine().Trim().ToLower();

                        if (inputShowCategorys == "" || inputShowCategorys == "j")
                        {
                            ShowCategorys.showCategorys();

                            break;
                        }
                        else if (inputShowCategorys == "q")
                        {
                            masterBreak = false;
                            return;
                        }
                        else if (inputShowCategorys == "n")
                        {
                            break;
                        }
                    }
                    continue;
                }

                if (CheckCategoryId.checkCategoryId(oldCategoryId, false))
                {
                    while (masterBreak)
                    {
                        if (typeof(MastersettingsCategory).GetType().GetProperty("format") != null)
                        {
                            oldCategoryId = CheckSyntax.check("Categorys", oldCategoryId).fixedString;
                        }
                        else if (fieldsDict.ContainsKey("format"))
                        {
                            oldCategoryId = CheckSyntax.check("Categorys", oldCategoryId).fixedString;
                        }
                        else if (MastersettingsCategory.forceUpercase)
                        {
                            oldCategoryId = oldCategoryId.ToUpper();
                        }
                       
                        var result = Program.categorys.Find(r => r.categoryId == oldCategoryId);

                        Console.Write($"Vill du editera \"{result.categoryName}\" (J/n/q)? ");
                        inputEditCategory = Console.ReadLine().Trim();

                        if (inputEditCategory.ToLower() == "q")
                        {
                            masterBreak = false;
                            return;
                        }
                        else if (inputEditCategory.ToLower() == "n")
                        {
                            break;
                        }
                        else if (inputEditCategory.ToLower() == "j" || inputEditCategory == "")
                        {
                            Console.Write($"Ange nytt kategori id ({result.categoryId}) (q = avbryt) : ");

                            inputEditCategory = Console.ReadLine().Trim();

                            if (inputEditCategory.ToLower() == "q")
                            {
                                masterBreak = false;
                                return;
                            }
                            else if (inputEditCategory == "")
                            {
                                newCategoryId = result.categoryId;

                                showEditCategory();

                                return;
                            }
                            else if (inputEditCategory.Length > 0)
                            {
                                newCategoryId = inputEditCategory;

                                if (typeof(MastersettingsCategory).GetType().GetProperty("format") != null)
                                {
                                    newCategoryId = CheckSyntax.check("Categorys", newCategoryId).fixedString;
                                }
                                else if (fieldsDict.ContainsKey("format"))
                                {
                                    newCategoryId = CheckSyntax.check("Categorys", newCategoryId).fixedString;
                                }
                                else if (MastersettingsCategory.forceUpercase)
                                {
                                    newCategoryId = newCategoryId.ToUpper();
                                }

                                if (CheckCategoryId.checkCategoryId(newCategoryId, false))
                                {
                                    Console.Write("Kategori id finns redan, vill du slå samman till en kategori för "+
                                        "produkterna (j/N/q)? ");
                                    string inputCombineCategorys = Console.ReadLine().Trim();

                                    if (inputCombineCategorys.ToLower() == "q")
                                    {
                                        masterBreak = false;
                                        return;
                                    }
                                    else if (inputCombineCategorys == "" || inputCombineCategorys.ToLower() == "n")
                                    {
                                        masterBreak2 = false;
                                        continue;
                                    }
                                    if (inputCombineCategorys.ToLower() == "j")
                                    {
                                        if (typeof(MastersettingsCategory).GetType().GetProperty("format") != null)
                                        {
                                            oldCategoryId = CheckSyntax.check("Categorys", oldCategoryId).fixedString;
                                        }
                                        else if(MastersettingsCategory.forceUpercase)
                                        {
                                            newCategoryId = newCategoryId.ToUpper();
                                        }

                                        combineCategorys = true;

                                        break;
                                    }
                                    
                                }
                                showEditCategory();

                                return;
                                break;
                            }
                            break;
                        }
                    }
                    //Console.WriteLine();
                }
                else
                {
                    while (true)
                    {
                        Console.Write("Kategori id finns inte, vill du se alla kategorier (J/n/q) ? ");

                        string inputShowCategorys = Console.ReadLine().Trim().ToLower();

                        if (inputShowCategorys == "" || inputShowCategorys == "j")
                        {
                            ShowCategorys.showCategorys();
                            break;
                        }
                        else if (inputShowCategorys == "n")
                        {
                            break;
                        }
                        else if (inputShowCategorys == "q")
                        {
                            masterBreak = false;
                            return;
                        }
                    }
                    continue;
                }
            }
        }

        private static void saveChanges()
        {
            ChangeCategoryProperties.changeCategeryProperties(oldCategoryId, newCategoryId, newCategoryName, combineCategorys);

            FileRegister.saveRegister();
        }

        //Tried to split the function into smaller sub functions, but it did not facilitate reading the code.
        //Worth a try anyway!
        public static void editCategory()
        {
            while (true)
            {
                showSelectCategory();

                if (!masterBreak)
                {
                    return;
                }

                saveChanges();

                Console.Write("Vill du editera fler varugrupper (J/n/q) ? ");
                string inputChoice = Console.ReadLine().Trim().ToLower();

                if (inputChoice == "" || inputChoice == "j")
                {
                    Console.Clear();
                    continue;
                }
                else if (inputChoice == "n")
                { 
                    return ;
                }
                else if (inputChoice == "q")
                {
                    return ;
                }

            }
            Console.WriteLine();
        }
    }
}
