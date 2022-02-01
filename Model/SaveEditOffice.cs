using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    internal class SaveEditOffice
    {
        static InventoryContext context = new InventoryContext();

        public static void saveEditOffice()
        {
            try
            {
                var temp = context.ValidOffice.FirstOrDefault(office => office.Id == ManageOffice.idOffice);

                temp.Currency = ManageOffice.Currency;

                temp.Office = ManageOffice.OfficeName;

                temp.Currency = ManageOffice.Country;

                context.SaveChanges(); 

            }

            catch
            {
                return;

            }
        }
    }
}
