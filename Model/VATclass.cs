using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class VATclass
    {
        public static void buildVatDictonary()
        {
            Program.vatData.Add(1, (float)0.25);
            Program.vatData.Add(2, (float)0.12);
            Program.vatData.Add(3, (float)0.06);
            Program.vatData.Add(4, 0);
        }
    }
}
