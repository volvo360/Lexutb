using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class ValidOffice
    {
        public int OfficeId { get; set; }

        public string Office { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        
        public ValidOffice()
        {
        }

        public ValidOffice(int officeId, string office, string country, string currency)
        {
            OfficeId = officeId;
            Office = office;
            Country = country;
            Currency = currency;
        }
    }
}
