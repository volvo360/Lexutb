using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class Computer : Inventory
    {
        public static bool Notebook { get; set; }

        public Computer()
        {
        }

        public Computer(string inventoryId, DateTime purchaseDate, string brand, string model, float purchasePrice, string currency, int office, string country) : base(inventoryId, purchaseDate, brand, model, purchasePrice, currency, office, country)
        {
        }

        public Computer(string inventoryId, DateTime purchaseDate, string brand, string model, float purchasePrice, string currency, int office, string country, bool notebook = true) : base(inventoryId, purchaseDate, brand, model, purchasePrice, currency, office, country)
        {
            Notebook = notebook;
        }

        public override string showExtra()
        {
            if (Notebook)
            {
                return "Bärbar dator";
            }
            else
            {
                return "";
            }
        }

        public override void setValue(string variable, object value)
        {
            if (variable == "Notebook")
            {
                Notebook = (bool)value;
            }

            return;
        }
    }
}
