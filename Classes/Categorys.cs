using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class Categorys
    {
        public string categoryId { get; set; }
        public string categoryName { get; set; }

        public int VATcategory { get; set; }

        public Categorys(string id, string Category)
        {
            categoryId = id;
            categoryName = Category;
        }

        public Categorys()
        {

        }

        public Categorys(string categoryId, string categoryName, int vATcategory) : this(categoryId, categoryName)
        {
            VATcategory = vATcategory;
        }
    }
}
