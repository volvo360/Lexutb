using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class ChangeCategoryProperties
    {
        public static void changeCategeryProperties(string oldCategoryId = null, string newCategoryId = null, string newCategoryName = null, bool combineCategorys = false)
        {
            if (oldCategoryId == null || oldCategoryId.Length == 0 || newCategoryId == null || newCategoryId.Length == 0 || newCategoryName == null || newCategoryName.Length == 0)
            {
                return;
            }

            var result = Program.categorys.Find(x => x.categoryId == oldCategoryId);

            if (result != null)
            {
                if (!CheckCategoryId.checkCategoryId(newCategoryId, false))
                {
                    result.categoryId = newCategoryId;
                }

                result.categoryName = newCategoryName;

                if (combineCategorys)
                {
                    var results = Program.articles.Where(x => x.articleCategory == oldCategoryId);

                    foreach (var product in results)
                        product.articleCategory = newCategoryId;

                    Program.categorys.RemoveAt(Program.categorys.FindIndex(x => x.categoryId == oldCategoryId));
                }
            }
        }

        public static void saveChangeDefaultVATgroup(string categoryId = null, bool updateProductVAT = false)
        {
            if (categoryId != null)
            {
                try {
                    var res = Program.categorys.Find(x => x.categoryId == categoryId);

                    if (res != null)
                    {
                        res.VATcategory = EditCategory.newCategoryVAT;

                        if (updateProductVAT)
                        {
                            try
                            {
                                var results = Program.articles.Where(x => x.articleCategory == categoryId);

                                foreach (var product in results)
                                {
                                    product.articleVATcategory = EditCategory.newCategoryVAT;
                                }
                            }
                            catch (Exception)
                            {
                                return;
                            }
                        }
                    }
                    FileRegister.saveRegister();
                }

                catch 
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}
