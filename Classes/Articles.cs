using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{   
    public class Articles
    {
        //SKU = Stock Keeping Unit
        public string articleSKU { get; set; }
        public string articleCategory { get; set; }
        public string articleName { get; set; }
        public float articlePrice { get; set; }
        public int articleVATcategory { get; set; }
        public int articleStock { get; set; }
        public float articlePurchasePrice { get; set; }

        public Articles()
        {
        }

        public Articles(string SKU, string Category, string Name, float Price)
        {
            this.articleSKU = SKU;
            this.articleCategory = Category;
            this.articleName = Name;
            this.articlePrice = Price;
        }

        public Articles(string SKU, string Category, string Name, float Price, int vATcategory) : this(SKU, Category, Name, Price)
        {
            articleVATcategory = vATcategory;
        }

        public Articles(string SKU, string Category, string Name, float Price, int vATcategory, int stock, float purchasePrice) : this(SKU, Category, Name, Price, vATcategory)
        {
            this.articleStock = stock;
            this.articlePurchasePrice = purchasePrice;
        }
    }
}
