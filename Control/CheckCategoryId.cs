using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    internal class CheckCategoryId
    {
        public static bool checkCategoryId(string searchId, bool existing = true)
        {
            if (searchId.Length == 0)
            {
                return false;
            }

            if (typeof(MastersettingsArticle).GetType().GetProperty("format") != null)
            {
                searchId = CheckSyntax.check("Categorys", searchId).fixedString;
            }
            else if (MastersettingsCategory.forceUpercase)
            {
                searchId = searchId.ToUpper();
            }

            string[] temp_array = new string[0];

            if (CheckSyntax.check("Categorys", searchId).correct)
            { 
                //Lambda expression to see if categoryId already exists in our categoryregister
                var reponse = Program.categorys.Find(r => r.categoryId == searchId);

                if (reponse != null)
                {
                    if (existing)
                    { 
                        Console.WriteLine($"Kategorin id finns redan, kategorins namn är \"{reponse.categoryName}\"");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (!existing)
                    {
                        return false;
                    }
                    return true;
                }              
            }
            else
            {
                return false ;
            }
        }
    }
}