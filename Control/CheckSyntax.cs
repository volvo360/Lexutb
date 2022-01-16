using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_1
{
    internal class CheckSyntax
    {
        
        private static int minCountDivider = 0;

        private static string inputString;

        private static char syntaxDivider = '-';

        public static bool ClassExist(string className)
        {
            Type type = Type.GetType(String.Format("{0}.{1}", "Mini_project_1", className));
            if (type != null)
            {
                return true;
            }
            return false;
        }

        public static void convertFieldsToDictonary()
        {
            if (Program.fields != null)
            {
                foreach (FieldInfo field in Program.fields)
                {
                    Program.fieldsDict[field.Name] = field.GetValue(field.Name);
                }
            }
        }

        public static (bool correct, string fixedString) checkFormat()
        {
            int i = 0;

            string[] tempInputstring;

            if (Program.fieldsDict.ContainsKey("syntaxDivider"))
            {
                tempInputstring = inputString.Split((char)Program.fieldsDict["syntaxDivider"]);
                syntaxDivider = (char)Program.fieldsDict["syntaxDivider"];
            }
            else if (typeof(MasterSettings).GetType().GetProperty("defaultSyntaxDivider") != null)
            {
                tempInputstring = inputString.Split(MasterSettings.defaultSyntaxDivider);
                syntaxDivider = MasterSettings.defaultSyntaxDivider;
            }
            else
            {
                tempInputstring = inputString.Split('-');
                syntaxDivider = '-';
            }

            List<string> stringToValiddate = tempInputstring.ToList();

            minCountDivider = ((List<string>)Program.fieldsDict["format"]).Count() - 1;

            if (!checkSyntaxDivider())
            {
                return (false, "");
            }

            foreach (var MasterValidate in ((List<string>)Program.fieldsDict["format"]))
            {
                bool minNumber = false;

                var tempValidate = MasterValidate.Split('-');

                foreach (string validate in tempValidate)
                {
                    int length = 0;

                    if (validate.ToLower() == "letter")
                    {
                        if (validate.All(Char.IsLetter))
                        {
                            if (validate.All(c => char.IsUpper(c)))
                            {
                                tempInputstring[i] = tempInputstring[i].ToUpper();
                            }
                        }
                    }
                    else if (int.TryParse(validate, out length))
                    {
                        if (!minNumber)
                        {
                            minNumber = true;

                            if (tempInputstring[i].Length >= length)
                            {
                                //Do nothing, it's allright... Min length is fullfield
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Textdelen uppfyller inte kraven på en minsta längd om {length}.");
                                Console.ResetColor();

                                return (false, "");
                            }
                        }
                        else
                        {
                            if (minNumber)
                            {
                                minNumber = true;

                                if (tempInputstring[i].Length <= length)
                                {
                                    //Do nothing, it's allright... Max length is fullfield
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Textdelen överstriger krav på antal tecken {length}.");
                                    Console.ResetColor();

                                    return (false, "");
                                }
                            }
                        }
                    }
                    else if (validate.ToLower() == "int")
                    {
                        if (MasterValidate.Contains("-"))
                        {
                            var temp = MasterValidate.Split('-');

                            foreach (var val in temp)
                            {
                                length = 0;

                                if (val.ToLower() == "int")
                                {
                                    //Do nothing, only formating data
                                }
                                else if (int.TryParse(val, out length))
                                {
                                    if (!minNumber)
                                    {
                                        minNumber = true;

                                        int checkValue = 0;

                                        if (int.TryParse(tempInputstring[i], out length))
                                        {
                                            if (int.TryParse(val, out checkValue))
                                            {

                                                if (length >= checkValue)
                                                {
                                                    //Min value was fulfield
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine($"Siffer deln är mindre än det tillåtna värdet {checkValue}.");
                                                    Console.ResetColor();

                                                    return (false, "");
                                                }
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine($"Sifferdeln innehåller andra tecken än bara siffror.");
                                                Console.ResetColor();
                                                return (false, "");
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Kunde inte hämta ut värdet för minst/max värde från inställningarna.");
                                            Console.ResetColor();

                                            return (false, "");
                                        }
                                    }
                                    else
                                    {
                                        if (minNumber)
                                        {
                                            int checkValue = 0;

                                            if (int.TryParse(tempInputstring[i], out length))
                                            {
                                                if (int.TryParse(val, out checkValue))
                                                {

                                                    if (length < checkValue)
                                                    {
                                                        //Max value was fulfield
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine($"Siffer deln är större än det tillåtna värdet {checkValue}.");
                                                        Console.ResetColor();

                                                        return (false, "");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine($"Sifferdeln innehåller andra tecken än bara siffror.");
                                                    Console.ResetColor();

                                                    return (false, "");
                                                }
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine($"Kunde inte hämta ut värdet för minst/max värde från inställningarna.");
                                                Console.ResetColor();

                                                return (false, "");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (validate == "alphanumeric ")
                    {
                        if (validate.All(Char.IsLetterOrDigit))
                        {
                            //Do nothing, it's allright
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"En del av den inmattade strängen innehåller inte blandat bokstäver och siffror som ställs som krav!");
                            Console.ResetColor();
                            return (false, "");
                        }
                    }
                    else if (validate.Contains("#"))
                    {
                        int stringLength = validate.Count(f => f == '#');

                        if (stringLength > 0)
                        {
                            while (tempInputstring[i].Length < stringLength)
                            {
                                tempInputstring[i] = "0" + tempInputstring[i];
                            }
                        }
                    }
                }

                i++;
            }

            return (true, String.Join(MasterSettings.defaultSyntaxDivider, tempInputstring));
        }

        public static bool checkSyntaxDivider()
        {
            int count = inputString.Count(x => x == (char)Program.fieldsDict["syntaxDivider"]);

            if (inputString.Contains((char)Program.fieldsDict["syntaxDivider"]) && count == minCountDivider)
            {
                return true;
            }
            else if ((bool)Program.fieldsDict["foreceHyphen"] == false && count == 0)
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;

                if (count < minCountDivider)
                {
                    Console.WriteLine($"Antalet {Program.fieldsDict["syntaxDivider"]} är för få! Minsta antalet ska vara {count}.\n");
                }
                else
                {
                    Console.WriteLine($"Antalet {Program.fieldsDict["syntaxDivider"]} är för många! Max antalet är {count}.\n");
                }

                Console.ResetColor();
                return false;
            }
        }

        public static bool checkForceHyphen()
        {
            int count = inputString.Count(x => x == '-');

            if ((bool)Program.fieldsDict["foreceHyphen"])
            {
                if (inputString.Contains('-') && (int)Program.fieldsDict["minHyphen"] >= count && (int)Program.fieldsDict["minHyphen"] <= count)
                {
                    if (inputString.Contains('-') && (int)Program.fieldsDict["minHyphen"] >= count && (int)Program.fieldsDict["minHyphen"] <= count)
                    {
                        return true;
                    }
                    else if (inputString.Contains('-'))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        if ((int)Program.fieldsDict["minHyphen"] < count)
                        {
                            Console.WriteLine($"Antalet bindestreck är för få! Minsta antalet ska vara {Program.fieldsDict["minHyphen"]}\n");
                        }
                        else
                        {
                            Console.WriteLine($"Antalet bindestreck är för många! Max antalet är {(int)Program.fieldsDict["maxHyphen"]}.\n");
                        }

                        Console.ResetColor();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"Det finns krav på att det ska förekomma ett bindesstreck i inmatningen!\n");

                    Console.ResetColor();

                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public static bool checkMinLengthLeftSide()
        {
            int maxArray = 0;

            int count = inputString.Count(x => x == '-');

            if (count == 0)
            {
                if (inputString.Length < (int)Program.fieldsDict["minLengthLeftSide"])
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"Den inmattade texten är för kort, minsta antal tecken är {(int)Program.fieldsDict["minLengthLeftSide"]}.\n");

                    Console.ResetColor();
                    return false;
                }

                return true;
            }

            if (count > (int)Program.fieldsDict["maxHyphen"])
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"Antalet bindestreck är för många! Max antalet är {(int)Program.fieldsDict["maxHyphen"]}.\n");

                Console.ResetColor();
                return false;
            }

            if ((int)Program.fieldsDict["minHyphen"] == (int)Program.fieldsDict["maxHyphen"])
            {
                maxArray = (int)Program.fieldsDict["minHyphen"] + 1;
            }
            else
            {
                maxArray = (int)Program.fieldsDict["maxHyphen"] + 1;
            }

            if ((bool)Program.fieldsDict["foreceHyphen"])
            {
                string[] temp = new string[maxArray];

                int tempValue = 0;

                temp = inputString.Split('-');

                if (temp[0].Length < (int)Program.fieldsDict["minLengthLeftSide"])
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"Antalet minsta tecken på vänster sidan är för få! Minsta antalet ska vara {Program.fieldsDict["minLengthLeftSide"]}\n");

                    Console.ResetColor();

                    return false;
                }
                else if (temp[1].All(Char.IsDigit))
                {
                    if (Program.fieldsDict["minValueRightSide"] != null)
                    {
                        int.TryParse(temp[1], out tempValue);

                        if ((int)Program.fieldsDict["minValueRightSide"] <= tempValue && (int)Program.fieldsDict["maxValueRightSide"] > tempValue)
                        {
                            return true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine($"Inmatningen på höger sida efter bindesstrecket ska enbart bestå av siffror mellan {(int)Program.fieldsDict["minValueRightSide"]} och {(int)Program.fieldsDict["maxValueRightSide"]}\n");

                            Console.ResetColor();
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (inputString.Length >= (int)Program.fieldsDict["minLengthLeftSide"])
                {
                    return true;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"Något gick fel vid validering av den inmatade texten, verifiera att du uppfyller grundkraven och försök på nytt!\n");

            Console.ResetColor();

            return false;
        }


        public static (bool correct, string fixedString) check(string className = "MastersettingsInventory", string InputString = null)
        {
            inputString = InputString;

            if (className == null || inputString == null)
            {
                return (false, "");
            }

            if (className == "MastersettingsInventory")
            {
                if (ClassExist(className) && Program.fieldsDictArticle.Count == 0)
                {
                    Program.fields = typeof(MastersettingsInventory).GetFields();
                }
            }

            else
            {
                return (false, "");
            }

            if (className == "MastersettingsInventory")
            {
                if (ClassExist(className) && Program.fieldsDictArticle.Count == 0)
                {
                    convertFieldsToDictonary();
                    Program.fieldsDictArticle = Program.fieldsDict;
                }
                else
                {
                    Program.fieldsDict = Program.fieldsDictArticle;
                }
            }
           
            if (Program.fields == null)
            {
                return (false, "");
            }

            if (Program.fieldsDict.ContainsKey("syntaxDivider"))
            {
                syntaxDivider = (char)Program.fieldsDict["syntaxDivider"];
            }
            else if (typeof(MasterSettings).GetType().GetProperty("defaultSyntaxDivider") != null)
            {
                syntaxDivider = MasterSettings.defaultSyntaxDivider;
            }
            else
            {
                syntaxDivider = '-';
            }

            //If we should use our more general condition syntax of format

            if (Program.fieldsDict.ContainsKey("format"))
            {
                var temp = checkFormat();

                if (temp.correct)
                {
                    return (true, temp.fixedString);
                }
                else
                {
                    return (false, "");
                }
            }

            foreach (var field in Program.fields)
            {
                switch (field.Name)
                {
                    case "foreceHyphen":

                        if (inputString.Count(x => (x == syntaxDivider)) > 0)
                        {
                            if (!checkForceHyphen())
                            {
                                return (false, "");
                            }
                            break;
                        }

                        else
                            break;
                    case "minLengthLeftSide":
                        if (!checkMinLengthLeftSide())
                        {
                            return (false, "");
                        }
                        break;
                    default:
                        continue;
                }

            }

            if ((bool)Program.fieldsDict["forceUpercase"])
            {
                InputString = InputString.ToUpper();
            }
            return (true, InputString);
        }
    }
}
