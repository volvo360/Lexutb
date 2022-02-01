using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    public class ValidOffice
    {
        public int Id { get; set; }

        public string Office { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }

        public List <Inventory> Data { get; set; }
        
        public ValidOffice()
        {
        }

        public ValidOffice(string office, string country, string currency)
        {
            Office = office;
            Country = country;
            Currency = currency;
        }
    }
}
