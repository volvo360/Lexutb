using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class SaveEditOffice
    {
        public static void saveEditOffice()
        {
            try
            {
                var temp = Program.validOffices.ElementAt(ManageOffice.idOffice);

                temp.Currency = ManageOffice.Currency;

                temp.Office = ManageOffice.OfficeName;

                temp.Currency = ManageOffice.Country;

                FileManagment.saveRegister();
            }

            catch
            {
                return;

            }
        }
    }
}
