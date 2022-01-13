using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class ModelSearchCategory
    {
        public static bool removeCategory(string inputCategoryId)
        {
            if (MastersettingsArticle.forceUpercase)
            {
                inputCategoryId = inputCategoryId.ToUpper();
            }

            if (CheckCategoryId.checkCategoryId(inputCategoryId, false))
            {
                bool removeConnectedArticles = false;

                if (Program.articles.Count(x => x.articleCategory == inputCategoryId) > 0)
                {
                    removeConnectedArticles = true;
                }

                while (removeConnectedArticles)
                { 
                    Console.Write("Vill du även radera kopplade produkter till kategorin (J/n/q) ");
                    string removeProductsCategory = Console.ReadLine().Trim().ToLower();

                    if (removeProductsCategory == "q")
                    {
                        return false;
                    }
                    else if (removeProductsCategory == "j" || removeProductsCategory == "")
                    {
                        Program.articles.RemoveAll(x => x.articleSKU == inputCategoryId);
                        break;
                    }
                    else if (removeProductsCategory == "n")
                    {
                        break;
                    }
                }
                try
                {
                    Program.categorys.RemoveAt(Program.categorys.FindIndex(x => x.categoryId == inputCategoryId));
                    FileRegister.saveRegister();
                }

                catch {
                    //Do nothing as the category don't exists
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
