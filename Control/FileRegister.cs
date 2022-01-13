using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class FileRegister
    {
        public static void readRegister()
        {
            if (!File.Exists("CategorysRegister.txt"))
            {
                return;
            }

            string json = File.ReadAllText("CategorysRegister.txt");
            Program.categorys = JsonConvert.DeserializeObject<List<Categorys>>(json);

            if (!File.Exists("ArticlesRegister.txt"))
            {
                return;
            }

            json = File.ReadAllText("ArticlesRegister.txt");
            Program.articles = JsonConvert.DeserializeObject<List<Articles>>(json);
        }

        public static async void saveRegister()
        {
            string json = JsonConvert.SerializeObject(Program.categorys);

            await File.WriteAllTextAsync("CategorysRegister.txt", json);

            json = JsonConvert.SerializeObject(Program.articles);

            await File.WriteAllTextAsync("ArticlesRegister.txt", json);
        }

    }
}
