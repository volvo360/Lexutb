using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class MobilePhone : Inventory
    {
        public static bool DualSim { get; set; }

        public MobilePhone()
        {
        }

        public MobilePhone(string inventoryId, DateTime purchaseDate, string brand, string model, float purchasePrice, string currency, int office, string country) : base(inventoryId, purchaseDate, brand, model, purchasePrice, currency, office, country)
        {
        }

        public MobilePhone(string inventoryId, DateTime purchaseDate, string brand, string model, float purchasePrice, string currency, int office, string country, bool dualSim) : base(inventoryId, purchaseDate, brand, model, purchasePrice, currency, office, country)
        {
            DualSim = dualSim;
        }

        public override string showExtra()
        {
            if (DualSim)
            {
                return "Stöd för dubbla SIM";
            }
            else
            {
                return "";
            }
        }

        public override void setValue(string variable, object value)
        {
            if (variable == "DualSim")
            {
                DualSim = (bool)value;
            }

            return;
        }
    }
}
