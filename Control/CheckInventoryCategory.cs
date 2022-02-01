using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    internal class CheckInventoryCategory
    {
        static InventoryContext context = new InventoryContext();
        public static (bool status, int id, string name) checkInventoryCategory(object input)
        {
            if (input.GetType().Name == "int")
            {
                var temp = context.Category.Include(x => x.CategoryTexts).FirstOrDefault(category => category.Id == Int32.Parse((string)input));

                if (temp != null)
                    return (true, temp.Id, "");
                else
                    return (false, 0, "");
            }
            else
            {
                var temp = context.Category.Include(x => x.CategoryTexts).FirstOrDefault(category => category.CategoryTexts.Name == input.ToString().ToLower());

                if (temp != null)
                    return (true, temp.Id, temp.CategoryTexts.Name);
                else
                    return (false, 0, "");
            }
        }
    }
}
