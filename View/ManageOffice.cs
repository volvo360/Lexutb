using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class ManageOffice
    {
        private static bool masterbreak = true;

        public static int idOffice = -1;

        public static string Currency;

        public static int Office;

        public static string OfficeName;

        public static string Country;

        private static void addOffice()
        {
            while (true)
            {
                Console.Write($"Ange namnet på det nya kontoret (q = avbrytt): ");

                string inputOffice = Console.ReadLine().Trim().ToLower();

                if (inputOffice == "q")
                {
                    masterbreak = false;
                    break;
                }

                else if (inputOffice.Length > 0)
                {
                    if (Program.validOffices.FirstOrDefault(x => x.Office == CommonFunction.FirstLetterToUpper(inputOffice)) == null)
                    {
                        OfficeName = CommonFunction.FirstLetterToUpper(inputOffice);

                        Office = Program.validOffices.Count();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nDu försöker att editera till ett redan existerande kontor! " +
                            "Vänligen försök på nytt genom att trycka på en tangent!");
                        Console.ResetColor();
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private static void showCurrency()
        {
            while (true)
            {
                if (Program.memCurrency != null)
                {
                    Console.Write($"Ange valutan på inköpspriset ({Program.memCurrency}) : ");
                }
                else
                {
                    Console.Write($"Ange valutan på inköpspriset : ");
                }
                string inputCurency = Console.ReadLine().Trim();

                if (inputCurency.ToLower() == "q")
                {
                    masterbreak = false;
                    return;
                }

                else if (inputCurency == "" && Program.memCurrency != null)
                {
                    Currency = Program.memCurrency;
                    break;
                }

                else if (inputCurency.Length >= 1)
                {
                    if (inputCurency.Length > 1)
                    {
                        inputCurency = inputCurency.ToUpper();
                    }

                    if (ValidCurrency.validCurrency.Contains(inputCurency))
                    {
                        Currency = Program.memCurrency = inputCurency;
                        break;
                    }
                    else if (ValidCurrency.validCurrencyDict.ContainsKey(inputCurency))
                    {
                        Currency = Program.memCurrency = ValidCurrency.validCurrencyDict[inputCurency];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ange en giltig valuta! Giltiga valutor är : \n");

                        foreach (var temp in ValidCurrency.validCurrency)
                        {
                            Console.WriteLine(temp);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ange en giltig valuta! Giltiga valutor är : \n");

                    foreach (var temp in ValidCurrency.validCurrency)
                    {
                        Console.WriteLine(temp);
                    }
                }
            }
        }

        private static void editOffice()
        {
            while (true)
            {
                if (Program.memOffice >= 0)
                {
                    var temp = Program.validOffices.ElementAt(Program.memOffice);

                    Console.Write($"Ange kontor att editera ({temp.Office}) q = avbrytt : ");
                }
                else
                {
                    Console.Write($"Ange kontor att editera q = avbrytt : ");
                }
                var inputOffice = Console.ReadLine().Trim();

                int.TryParse(inputOffice, out idOffice);

                if (inputOffice.ToLower() == "q")
                {
                    masterbreak = false;
                    return;
                }

                else if (inputOffice == "" && Program.memOffice >= 0)
                {
                    Office = idOffice;
                    break;
                }

                else if (idOffice >= 0 && inputOffice.Length > 0)
                {
                    --idOffice;

                    try
                    {
                        var temp = Program.validOffices.ElementAt(idOffice);
                        if (temp != null)
                        {
                            Program.memOffice = idOffice;
                            Office = temp.OfficeId;
                            OfficeName = temp.Office;
                            Currency = Program.memCurrency = temp.Currency;
                            Country = Program.memCountry = temp.Country;

                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Giltiga kontor är :\n");

                        int i = 1;

                        foreach (var office in Program.validOffices)
                        {
                            Console.WriteLine($"{i} {office.Office}");
                            i++;
                        }
                    }
                }

                else if (inputOffice.Length > 0)
                {
                    OfficeName = CommonFunction.FirstLetterToUpper(inputOffice.ToLower());

                    if (Program.validOffices.FirstOrDefault(x => x.Office == OfficeName) != null)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Giltiga kontor är :\n");

                        int i = 1;

                        foreach (var office in Program.validOffices)
                        {
                            Console.WriteLine($"{i} {office.Office}");
                            i++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Giltiga kontor är :\n");

                    int i = 1;

                    foreach (var office in Program.validOffices)
                    {
                        Console.WriteLine($"{i} {office.Office}");
                        i++;
                    }
                }
            }

            while (true)
            {
                Console.Write($"Ange nytt namn på kontoret ({OfficeName}) q = avbrytt: ");

                string inputOffice = Console.ReadLine().Trim().ToLower();

                if (inputOffice == "q")
                {
                    masterbreak = false;
                    break;
                }

                else if (inputOffice.Length > 0)
                {
                    if (Program.validOffices.FirstOrDefault(x => x.Office == CommonFunction.FirstLetterToUpper(inputOffice)) != null)
                    {
                        OfficeName =CommonFunction.FirstLetterToUpper(inputOffice);
                    }
                    else
                    { 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nDu försöker att editera till ett redan existerande kontor! " +
                            "Vänligen försök på nytt genom att trycka på en tangent!");
                        Console.ResetColor();
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void showValidateEmptyOffice()
        {
            if (CheckItemsOffice.checkItemsOffice())
            {
                Program.validOffices.RemoveAt(Program.memOffice);
                return;
            }
            else
            {
                int oldOffice = Program.memOffice;

                while (true)
                { 
                    while (true)
                    {
                         Console.Write($"Kontoret är inte tomt på inventarier, vilket kontor ska dessa flyttas till q = avbrytt ? ");
                    
                        var inputOffice = Console.ReadLine().Trim();

                        int.TryParse(inputOffice, out idOffice);

                        if (inputOffice.ToLower() == "q")
                        {
                            masterbreak = false;
                            return;
                        }

                        else if (inputOffice == "" && Program.memOffice >= 0)
                        {
                            Office = idOffice;
                            break;
                        }

                        else if (idOffice >= 0 && inputOffice.Length > 0)
                        {
                            --idOffice;

                            try
                            {
                                var temp = Program.validOffices.ElementAt(idOffice);
                                if (temp != null)
                                {
                                    Program.memOffice = idOffice;
                                    Office = temp.OfficeId;
                                    OfficeName = temp.Office;
                                    Currency = Program.memCurrency = temp.Currency;
                                    Country = Program.memCountry = temp.Country;



                                    break;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Giltiga kontor är :\n");

                                int i = 1;

                                foreach (var office in Program.validOffices)
                                {
                                    Console.WriteLine($"{i} {office.Office}");
                                    i++;
                                }
                            }
                        }

                        else if (inputOffice.Length > 0)
                        {
                            OfficeName = CommonFunction.FirstLetterToUpper(inputOffice.ToLower());

                            if (Program.validOffices.FirstOrDefault(x => x.Office == OfficeName) != null)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Giltiga kontor är :\n");

                                int i = 1;

                                foreach (var office in Program.validOffices)
                                {
                                    Console.WriteLine($"{i} {office.Office}");
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Giltiga kontor är :\n");

                            int i = 1;

                            foreach (var office in Program.validOffices)
                            {
                                Console.WriteLine($"{i} {office.Office}");
                                i++;
                            }
                        }

                    }

                    Console.Write($"Vill du flytta inventarierna till {OfficeName}-kontoret (j/N/q = avbrytt)? ");

                    string inputConfirmOffice = Console.ReadLine().Trim().ToLower();

                    if (inputConfirmOffice == "" || inputConfirmOffice == "n")
                    {
                        continue;
                    }
                    else if (inputConfirmOffice == "q")
                    {
                        masterbreak = false;
                    }
                    else if (inputConfirmOffice == "j")
                    {
                        break;
                    }
                }

                int newOffice = Program.memOffice;

                Program.memOffice = oldOffice;

                MoveInventorysToOffice.moveInventorysToOffice(newOffice);

                Program.validOffices.RemoveAt(Program.memOffice);
            }

            FileManagment.saveRegister();
        }

        public static void deleteOffice()
        {
            while (true)
            { 
                while (true)
                {
                    

                    if (Program.memOffice >= 0)
                    {
                        var temp = Program.validOffices.ElementAt(Program.memOffice);

                        Console.Write($"Vilket kontor vill du radera ({temp.Office}) q = avbrytt : ");
                    }
                    else
                    {
                        Console.Write($"Vilket kontor vill du radera q = avbrytt : ");
                    }
                    var inputOffice = Console.ReadLine().Trim();

                    int.TryParse(inputOffice, out idOffice);

                    if (inputOffice.ToLower() == "q")
                    {
                        masterbreak = false;
                        return;
                    }

                    else if (inputOffice == "" && Program.memOffice >= 0)
                    {
                        Office = idOffice;
                        break;
                    }

                    else if (idOffice >= 0 && inputOffice.Length > 0)
                    {
                        --idOffice;

                        try
                        {
                            var temp = Program.validOffices.ElementAt(idOffice);
                            if (temp != null)
                            {
                                Program.memOffice = idOffice;
                                Office = temp.OfficeId;
                                OfficeName = temp.Office;
                                Currency = Program.memCurrency = temp.Currency;
                                Country = Program.memCountry = temp.Country;

                                break;
                            }
                        }

                        catch
                        {
                            Console.WriteLine("Giltiga kontor är :\n");

                            int i = 1;

                            foreach (var office in Program.validOffices)
                            {
                                Console.WriteLine($"{i} {office.Office}");
                                i++;
                            }
                        }
                    }

                    else if (inputOffice.Length > 0)
                    {
                        OfficeName = CommonFunction.FirstLetterToUpper(inputOffice.ToLower());

                        if (Program.validOffices.FirstOrDefault(x => x.Office == OfficeName) != null)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Giltiga kontor är :\n");

                            int i = 1;

                            foreach (var office in Program.validOffices)
                            {
                                Console.WriteLine($"{i} {office.Office}");
                                i++;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Giltiga kontor är :\n");

                        int i = 1;

                        foreach (var office in Program.validOffices)
                        {
                            Console.WriteLine($"{i} {office.Office}");
                            i++;
                        }
                    }
                }

                while (true)
                {
                    var temp = Program.validOffices.ElementAt(Program.memOffice);

                    Console.Write($"Vill du verkligen raddera kontoret \"{temp.Office}\" (j/N/q = avbryt)? ");

                    string inputChoiceDelete = Console.ReadLine().Trim().ToLower();

                    if (inputChoiceDelete == "" || inputChoiceDelete == "n")
                    {
                        break;
                    }
                    else if (inputChoiceDelete == "q")
                    {
                        return;
                    }
                    else if (inputChoiceDelete == "j")
                    {
                        showValidateEmptyOffice();
                        return;
                    }
                }
            }
        }

        private static void showCountry()
        {
            while (true)
            {
                if (Program.memCountry != null)
                {
                    Console.Write($"Ange land ({Program.memCountry}) : ");
                }
                else
                {
                    Console.Write($"Ange land : ");
                }
                var inputCountry = Console.ReadLine().Trim();

                if (inputCountry.ToLower() == "q")
                {
                    masterbreak = false;
                    return;
                }

                else if (inputCountry == "" && Program.memCountry != null)
                {
                    Country = Program.memCountry;
                    break;
                }

                else if (inputCountry.Length > 0)
                {
                    Country = Program.memCountry = CommonFunction.FirstLetterToUpper(inputCountry.ToLower());
                    break;
                }
                else
                {
                    Console.WriteLine("Ange ett giltigt land!");
                }
            }
        }

        public static void editOfficeMenu()
        {
            Console.Clear();

            if (!masterbreak)
            {
                return;
            }

            editOffice();

            if (!masterbreak)
            {
                return;
            }

            showCountry();

            if (!masterbreak)
            {
                return;
            }

            showCurrency();

            if (!masterbreak)
            {
                return;
            }

            SaveEditOffice.saveEditOffice();

            return;
        }

        private static void showAddOffice()
        {
            Console.Clear();

            if (!masterbreak)
            {
                return;
            }

            addOffice();

            if (!masterbreak)
            {
                return;
            }

            showCountry();

            if (!masterbreak)
            {
                return;
            }

            showCurrency();

            if (!masterbreak)
            {
                return;
            }

            Program.validOffices.Add(new ValidOffice(Office, OfficeName, Country, Currency));

            FileManagment.saveRegister();
        }


        public static void manageOffice()
        {
            List<string> Menu = new List<String>();

            Menu.Add("Editera kontor"); //1
            Menu.Add("Addera kontor"); //2
            Menu.Add("Raddera kontor"); //3
            Menu.Add("Avsluta"); //4

            bool runMenu = true;

            while (runMenu)
            {
                masterbreak=true;

                Console.ResetColor();

                int i = 1;

                foreach (var item in Menu)
                {
                    Console.WriteLine($"{i} {item}");

                    i++;
                }

                Console.Write("\nVälj menyalternativ : ");

                int menuOption = 0;

                string inputChoiceMenu = Console.ReadLine().Trim().ToLower();

                if (int.TryParse(inputChoiceMenu, out menuOption))
                {
                    if (menuOption >= 1 && (menuOption < Menu.Count + 1))
                    {
                        switch (menuOption)
                        {
                            case 1:
                                editOfficeMenu();
                                Console.Clear();
                                break;

                            case 2:
                                showAddOffice();
                                Console.Clear();
                                break;

                            case 3:
                                Console.Clear();
                                deleteOffice();
                                Console.Clear();
                                break;
                            case 4:
                                Console.Clear();

                                runMenu = false;
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Inget giltigt alternativ! Tryck på enter för att starta om.");
                                Console.ReadKey();
                                Console.ResetColor();
                                Console.Clear();
                                break;
                        }
                    }
                }
                else
                {
                    if (inputChoiceMenu == "q")
                    {
                        Console.Clear();
                        runMenu = false;
                        break;
                    }

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write("Ange ett giltigt meny alternativ! Tryck på en tangent för att starta om.");
                    Console.ReadKey();
                    Console.ResetColor();
                    Console.Clear();
                }
            }

        }

    }
}
