using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class Inventory
    {
        public string InventoryId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
        
        public int Office { get; set; }
        
        public float PurchasePrice { get; set; }

        public string Currency { get; set; }

        public string Country { get; set; }

        public Inventory()
        {

        }

        public Inventory(string inventoryId, DateTime purchaseDate, string brand, string model, float purchasePrice, 
            string currency, int office, string country)
        {
            InventoryId = inventoryId;

            PurchaseDate = purchaseDate;

            Brand = brand;

            Model = model;

            PurchasePrice = purchasePrice;

            Currency = currency;

            Office = office;

            Country = country;
        }

        public virtual string showExtra()
        {
            return "";
        }

        public virtual void setValue(string variable, object value)
        {
            return ;
        }
    }
}
