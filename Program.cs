using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Checkpoint_1_OOP
{
    internal  class Program
    {
        public static bool syncFile = false;

        public static List<Product> products_OOP = new List<Product>();

        public static Dictionary<string, string> products = new Dictionary<string, string>();

        public static void Main(string[] args)
        {
            Product.buildVatDictonary();

            Console.WriteLine("Välkommen till programmet för poduktregister!\n");

            Views.showMenu();

        }
    }
}
