using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class ValidCurrency
    {
        public static List<string> validCurrency = new List<string>() { "SEK", "EUR", "USD", "GBP" };
        public static Dictionary<string, string> validCurrencyDict = new Dictionary<string, string> () { {"€", "EUR"}, { "$", "USD" }, { "£", "GBP" } };
    }
}
