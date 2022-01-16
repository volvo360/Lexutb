using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class FileManagment
    {
        public static void readRegister()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            string json;


            if (!File.Exists("OfficeRegister.txt"))
            {
                SetupDefaultOffice.setupDefaultOffice();

                saveRegister();
            }
            else
            {
                json = File.ReadAllText("OfficeRegister.txt");
                Program.validOffices = JsonConvert.DeserializeObject<List<ValidOffice>>(json, settings);
            }

            if (!File.Exists("InventoryRegister.txt"))
            {
                return;
            }

            json = File.ReadAllText("InventoryRegister.txt");
            Program.inventories = JsonConvert.DeserializeObject<List<Inventory>>(json, settings);
        }

        public static async void saveRegister()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            string json = json = JsonConvert.SerializeObject(Program.inventories, settings);

            await File.WriteAllTextAsync("InventoryRegister.txt", json);

            json = json = JsonConvert.SerializeObject(Program.validOffices, settings);

            await File.WriteAllTextAsync("OfficeRegister.txt", json);
        }
    }
}
