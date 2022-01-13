using System;
using System.Collections.Generic;

namespace Checkpoint_2
{
    public class ShowCategoryVAT
    {
        public static void showVAT(int defaultVAT = Mastersettings.defaultVATclass, bool updateProducts = true, bool forceUpdateCategory= false)
        {
            bool updateProductVAT = false;

            Console.WriteLine("Välj önskad momsgrupp nedan :\n");

            int i = 1;

            float defDispalyValueVAT = 0;

            foreach (KeyValuePair<int, float> vat in Program.vatData)
            {
                if (vat.Key == defaultVAT)
                {
                    defDispalyValueVAT = vat.Value * 100;
                                       
                    Console.WriteLine($"{vat.Key} {vat.Value * 100}%");
                }
                else
                {
                    Console.WriteLine($"{vat.Key} {vat.Value * 100}%");
                }
                i++;
            }

            Console.WriteLine();
            Console.Write($"Välj momsgrupp ({defDispalyValueVAT}%) : ");

            while (true)
            {
                string inputVATclass = Console.ReadLine().Trim();

                int tempCategoryVAT;

                if (inputVATclass == "")
                {
                    EditCategory.newCategoryVAT = Mastersettings.defaultVATclass;
                    break;
                }
                else if (int.TryParse(inputVATclass, out tempCategoryVAT))
                {

                    if (tempCategoryVAT > 0 && tempCategoryVAT <= Program.vatData.Count)
                    {
                        EditCategory.newCategoryVAT = tempCategoryVAT;
                        break;
                    }
                    else
                    {
                        Console.Write("Välj en giltig momskategori : ");
                    }
                }
            }

            while (true && updateProducts)
            {
                Console.Write("Vill du även uppdatera alla produkters momskategori (J/n/q)? ");

                string inputUpdateProductVAT = Console.ReadLine().Trim().ToLower();

                if (inputUpdateProductVAT == "q")
                {
                    return;
                }
                else if (inputUpdateProductVAT == "" || inputUpdateProductVAT == "j")
                {
                    updateProductVAT = true;
                    break;
                }
                else if (inputUpdateProductVAT == "n")
                {
                    updateProductVAT = false;
                    break;
                }
            }

            if (forceUpdateCategory)
            { 
                ChangeCategoryProperties.saveChangeDefaultVATgroup(EditCategory.newCategoryId, updateProductVAT);
            
            }
        }
    }
}