using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class SetupDefaultOffice
    {
        public static void setupDefaultOffice()
        {
            Program.validOffices.Add(new ValidOffice(0, "Berlin", "Tyskland", "EUR"));
            Program.validOffices.Add(new ValidOffice(1, "Helsingborg", "Sverige", "SEK"));
            Program.validOffices.Add(new ValidOffice(2, "London", "England", "GBP"));
            Program.validOffices.Add(new ValidOffice(3, "New Yourk", "Sverige", "USD"));
        }
    }
}
