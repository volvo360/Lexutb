using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    public class Inventory
    {
        public int Id { get; set; }

        public ValidOffice Office { get; set; }

        public Category Category { get; set; }

        public string InventoryId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
        
        public float PurchasePrice { get; set; }

        public string Currency { get; set; }

        public string Extra { get; set; }

        public List<InventoryExtraData> ExtraData { get; set; }

        public Inventory()
        {

        }

        /*public Inventory( DateTime purchaseDate, string brand, string model, float purchasePrice, 
            string currency, ValidOffice office)
        {
            PurchaseDate = purchaseDate;

            Brand = brand;

            Model = model;

            PurchasePrice = purchasePrice;

            Currency = currency;

            Office = office;
        }*/

        public Inventory(int id)
        {
            Id = id;
        }

        public Inventory(Category category, string inventoryId, DateTime purchaseDate, string brand, string model, ValidOffice office, float purchasePrice, string currency, List<InventoryExtraData> extraData)
        {
            Category = category;
            InventoryId = inventoryId;
            PurchaseDate = purchaseDate;
            Brand = brand;
            Model = model;
            Office = office;
            PurchasePrice = purchasePrice;
            Currency = currency;
            ExtraData = extraData;
        }
    }

    public class InventoryExtraData
    {
        public InventoryExtraData()
        {
        }

        public InventoryExtraData(int categoryProp, string value)
        {
            CategoryPropId = categoryProp;
            Value = value;
        }

        public InventoryExtraData(int inventoryId, int categoryProp, string value)
        {
            InventoryId = inventoryId;
            CategoryPropId = categoryProp;
            Value = value;
        }

        public int Id { get; set; }

        public int? InventoryId { get; set; }

        public int CategoryPropId { get; set; }

        public string Value { get; set; }
    }
}
