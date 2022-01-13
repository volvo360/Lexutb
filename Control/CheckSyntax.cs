using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    internal class CheckSyntax
    {
        private static int minCountDivider = 0;

        private static string inputString;
        
        private static FieldInfo[] fields;

        private static Dictionary<string, object> fieldsDictArticle = new Dictionary<string, object>();

        private static Dictionary<string, object> fieldsDictCategory = new Dictionary<string, object>();

        private static Dictionary<string, object> fieldsDict = new Dictionary<string, object>();

        private static char syntaxDivider = '-';

        private static bool ClassExist(string className)
        {
            Type type = Type.GetType(String.Format("{0}.{1}", "Checkpoint_2", className));
            if (type != null)
            {
                return true;
            }
            return false;
        }

        private static void convertFieldsToDictonary()
        {
            if (fields != null)
            {
                foreach (FieldInfo field in fields)
                {
                    fieldsDict[field.Name] = field.GetValue(field.Name);
                }
            }
        }

        private static (bool correct, string fixedString) checkFormat()
        {
            int i = 0;

            string[] tempInputstring;

            if (fieldsDict.ContainsKey("syntaxDivider"))
            {
                tempInputstring = inputString.Split((char)fieldsDict["syntaxDivider"]);
                syntaxDivider = (char)fieldsDict["syntaxDivider"];
            }
            else if (typeof(Mastersettings).GetType().GetProperty("defaultSyntaxDivider") != null)
            {
                tempInputstring = inputString.Split(Mastersettings.defaultSyntaxDivider);
                syntaxDivider = Mastersettings.defaultSyntaxDivider;
            }
            else
            {
                tempInputstring = inputString.Split('-');
                syntaxDivider = '-';
            }
            
            List<string> stringToValiddate = tempInputstring.ToList(); 

            minCountDivider = ((List<string>)fieldsDict["format"]).Count()-1;

            if (!checkSyntaxDivider())
            { 
                return (false, "");
            }

            foreach (var MasterValidate in ((List<string>)fieldsDict["format"]))
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

            return (true, String.Join(syntaxDivider, tempInputstring));
        }

        private static bool checkSyntaxDivider()
        {
            int count = inputString.Count(x => x == (char)fieldsDict["syntaxDivider"]);

            if (inputString.Contains((char)fieldsDict["syntaxDivider"]) && count == minCountDivider)
            {
                return true;
            }
            else if ((bool)fieldsDict["foreceHyphen"] == false && count == 0)
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;

                if (count < minCountDivider)
                {
                    Console.WriteLine($"Antalet {fieldsDict["syntaxDivider"]} är för få! Minsta antalet ska vara {count}.\n");
                }
                else
                {
                    Console.WriteLine($"Antalet {fieldsDict["syntaxDivider"]} är för många! Max antalet är {count}.\n");
                }

                Console.ResetColor();
                return false;
            }
        }

        private static bool checkForceHyphen()
        {
            int count = inputString.Count(x => x == '-');

            if ((bool)fieldsDict["foreceHyphen"])
            {
                if (inputString.Contains('-') && (int)fieldsDict["minHyphen"] >= count && (int)fieldsDict["minHyphen"] <= count)
                {
                    if (inputString.Contains('-') && (int)fieldsDict["minHyphen"] >= count && (int)fieldsDict["minHyphen"] <= count)
                    {
                        return true;
                    }
                    else if (inputString.Contains('-'))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        if ((int)fieldsDict["minHyphen"] < count)
                        {
                            Console.WriteLine($"Antalet bindestreck är för få! Minsta antalet ska vara {fieldsDict["minHyphen"]}\n");
                        }
                        else
                        {
                            Console.WriteLine($"Antalet bindestreck är för många! Max antalet är {(int)fieldsDict["maxHyphen"]}.\n");
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

        private static bool checkMinLengthLeftSide()
        {
            int maxArray = 0;

            int count = inputString.Count(x => x == '-');

            if (count == 0)
            {
                if (inputString.Length < (int)fieldsDict["minLengthLeftSide"])
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"Den inmattade texten är för kort, minsta antal tecken är {(int)fieldsDict["minLengthLeftSide"]}.\n");

                    Console.ResetColor();
                    return false;
                }

                return true;
            }

            if (count > (int)fieldsDict["maxHyphen"])
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"Antalet bindestreck är för många! Max antalet är {(int)fieldsDict["maxHyphen"]}.\n");
                
                Console.ResetColor();
                return false;
            }

            if ((int)fieldsDict["minHyphen"] == (int)fieldsDict["maxHyphen"])
            {
                maxArray = (int)fieldsDict["minHyphen"] + 1;
            }
            else
            {
                maxArray = (int)fieldsDict["maxHyphen"] + 1;
            }

            if ((bool)fieldsDict["foreceHyphen"])
            {
                string[] temp = new string[maxArray];

                int tempValue = 0;

                temp = inputString.Split('-');

                if (temp[0].Length < (int)fieldsDict["minLengthLeftSide"])
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"Antalet minsta tecken på vänster sidan är för få! Minsta antalet ska vara {fieldsDict["minLengthLeftSide"]}\n");
                
                    Console.ResetColor();

                    return false;
                }
                else if (temp[1].All(Char.IsDigit))
                {
                    if (fieldsDict["minValueRightSide"] != null)
                    {
                        int.TryParse(temp[1], out tempValue);

                        if ((int)fieldsDict["minValueRightSide"] <= tempValue && (int)fieldsDict["maxValueRightSide"] > tempValue)
                        { 
                            return true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine($"Inmatningen på höger sida efter bindesstrecket ska enbart bestå av siffror mellan {(int)fieldsDict["minValueRightSide"]} och {(int)fieldsDict["maxValueRightSide"]}\n");

                            Console.ResetColor();
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (inputString.Length >= (int)fieldsDict["minLengthLeftSide"])
                {
                    return true;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"Något gick fel vid validering av den inmatade texten, verifiera att du uppfyller grundkraven och försök på nytt!\n");

            Console.ResetColor();

            return false;
        }
        

        public static (bool correct, string fixedString) check(string className = null, string  InputString = null)
        {
            inputString = InputString;

            if (className == null || inputString == null)
            {
                return (false , "");
            }

            if (className == "Articles")
            {
                if (ClassExist(className) && fieldsDictArticle.Count == 0)
                {
                    fields = typeof(MastersettingsArticle).GetFields();
                }
            }
            else if(className == "Categorys")
            {
                if (ClassExist(className) && fieldsDictCategory.Count == 0)
                {
                    fields = typeof(MastersettingsCategory).GetFields();
                }
            }
             
            else
            {
                return (false , "");
            }

            if (className == "Articles")
            {
                if (ClassExist(className) && fieldsDictArticle.Count == 0)
                {
                    convertFieldsToDictonary();
                    fieldsDictArticle = fieldsDict;
                }
                else
                {
                    fieldsDict = fieldsDictArticle;
                }
            }
            else if (className == "Categorys")
            {
                if (ClassExist(className) && fieldsDictCategory.Count == 0)
                {
                    convertFieldsToDictonary();
                    fieldsDictArticle = fieldsDict;
                }
                else
                {
                    fieldsDict = fieldsDictArticle;
                }
            }

            if (fields == null)
            { 
                return (false , "");
            }

            if (fieldsDict.ContainsKey("syntaxDivider"))
            {
                syntaxDivider = (char)fieldsDict["syntaxDivider"];
            }
            else if (typeof(Mastersettings).GetType().GetProperty("defaultSyntaxDivider") != null)
            {
                syntaxDivider = Mastersettings.defaultSyntaxDivider;
            }
            else
            {
                syntaxDivider = '-';
            }

            //If we should use our more general condition syntax of format

            if (fieldsDict.ContainsKey("format"))
            {
                var temp = checkFormat();

                if (temp.correct)
                {
                    return (true , temp.fixedString);
                }
                else
                {
                    return (false , "");
                }
            }

            foreach (var field in fields)
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
                            return (false , "");
                        }
                        break;
                    default:
                        continue;
                }

            }

            if ((bool)fieldsDict["forceUpercase"])
            {
                InputString = InputString.ToUpper();
            }
            return (true , InputString);
        }
    }
}
