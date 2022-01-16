using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class Printer : Inventory
    {
        public static bool PrintPin { get; set; }

        public Printer()
        {
        }

        public Printer(string inventoryId, DateTime purchaseDate, string brand, string model, float purchasePrice, string currency, int office, string country) : base(inventoryId, purchaseDate, brand, model, purchasePrice, currency, office, country)
        {
        }

        public Printer(string inventoryId, DateTime purchaseDate, string brand, string model, float purchasePrice, string currency, int office, string country, bool printPin) : base(inventoryId, purchaseDate, brand, model, purchasePrice, currency, office, country)
        {
            PrintPin = printPin;
        }

        public override string showExtra()
        {
            if (PrintPin)
            {
                return "Stöd för PIN vid utskrift";
            }
            else
            {
                return "";
            }
        }

        public override void setValue(string variable, object value)
        {
            if (variable == "PrintPin")
            {
                PrintPin = (bool)value;
            }

            return;
        }
    }
}
