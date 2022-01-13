using System;
using System.Linq;

namespace Checkpoint_1_OOP
{
    class validateSKU
    {
        //Default parameters

        static int minLengthLeftSide = 3;

        static int minValueRightSide = 200;

        static int maxValueRightSide = 500;

        public static bool foreceUpercase = true;

        public static bool verifySKU(string temp_string)
        {
            string[] temp_array = new string[0];

            if (temp_string.IndexOf("-") > 0 && temp_string.Length >= 3)
            {
                int count = temp_string.Count(f => f == '-');

                if (count == 1)
                {
                    temp_array = temp_string.Split('-');

                    if (temp_array.Length == 2)
                    {
                        if (temp_array[0].All(char.IsLetter) && temp_array[0].Trim().Length >= minLengthLeftSide)
                        {
                            if (foreceUpercase)
                            {
                                temp_array[0] = temp_array[0].Trim().ToUpper();
                            }

                            if (temp_array[1].All(char.IsDigit))
                            {
                                int tempValue = int.Parse(temp_array[1]);

                                if (tempValue >= minValueRightSide && tempValue <= maxValueRightSide)
                                {
                                    string searchSKU = null;

                                    if (foreceUpercase)
                                    {
                                        searchSKU = (temp_array[0] + "-" + temp_array[1]).ToUpper();
                                    }
                                    else
                                    {
                                        searchSKU = (temp_array[0] + "-" + temp_array[1]);
                                    }

                                    //Lambda expression to see if sku already exists in our register
                                    var reponse = Product.products_OOP.Find(r => r.sku == searchSKU);

                                    if (reponse != null)
                                    {
                                        Console.WriteLine($"Artikelnumret finns redan, produkten är \"{reponse.articletName}\"");
                                        return false;
                                    }
                                    if (Program.products.ContainsKey(temp_string))
                                    {
                                        Console.WriteLine($"Artikelnumret finns redan, produkten är \"{Program.products[temp_array[0] + "-" + temp_array[1]]}\"");
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
                                    
                                    Console.WriteLine($"Högra delen av artikelnumret ska befinna sig mellan {minValueRightSide}-{maxValueRightSide}");
                                    
                                    Console.ResetColor();
                                }
                            }
                            return false;
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            if (temp_array[1].All(char.IsDigit))
                            {
                                int tempValue = int.Parse(temp_array[1]);

                                if (tempValue >= minValueRightSide && tempValue <= maxValueRightSide)
                                {
                                    Console.WriteLine($"Ett artikelnummers första del får bara innehålla vanliga bokstäver, samt ska ha en minsta längd på {minLengthLeftSide}");
                                }
                                else
                                {
                                    Console.WriteLine("För att uppfylla kraven för ett giltigt artikelnummmer ska denna följa syntax abcd-234");
                                    Console.ResetColor();
                                    return false;
                                }

                                Console.ResetColor();

                                return false;
                            }
                        }

                        Console.ResetColor();
                        
                        return false;
                    }

                    return false;
                }

                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Ett artikelnummer får bara innehålla ett - !!!!");

                Console.ResetColor();

                return false;
            }
        }
    }
}