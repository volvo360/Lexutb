using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class RemoveArticle
    {
        public static void removeArticle(string inputSKU)
        {
            Program.articles.RemoveAt(Program.articles.FindIndex(x => x.articleSKU == inputSKU));

            FileRegister.saveRegister();
        }
    }
}
