using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Checkpoint_2
{
    public static class TypeUtilities
    {
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }
    }

    public class Program
    {
        public static List<Categorys> categorys = new List<Categorys>();
        public static List<Articles> articles = new List<Articles>();

        public static Dictionary<int, float> vatData = new Dictionary<int, float>();

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            VATclass.buildVatDictonary();

            FileRegister.readRegister();

            Console.WriteLine("Välkommen till vårt registerprogram för butik!\n");

            DisplayMenu.displayMenu();

            return;
        }
    }
}
