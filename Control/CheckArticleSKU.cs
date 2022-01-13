using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    internal class CheckArticleSKU
    {
        public static bool checkArticleSKU(string searchSKU, bool existing = true)
        {
            if (MastersettingsArticle.forceUpercase)
            {
                searchSKU = searchSKU.ToUpper();
            }
            
            //Lambda expression to see if categoryId already exists in our categoryregister
            var reponse = Program.articles.Find(r => r.articleSKU == searchSKU);

            if (reponse != null)
            {
                if (existing)
                {
                    Console.WriteLine($"Artikel id finns redan, artikelns namn är \"{reponse.articleName}\"");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (existing)
                {
                    return true;
                }

                return true;
            }
        }
    }
}