﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace Mini_project_2
{
    internal class Program
    {
        public static string defaultType = "computer";

        public static string memInvId = null;

        public static DateTime memPurchaseDate;

        public static string memBrand = null;

        public static string memModel = null;

        public static float memPurchasePrice = 0;

        public static string memCurrency = "SEK";

        public static int memOffice = -1;

        public static string memCountry = null;

        public static string oldCategory = null;

        public static bool memNotebook = true;

        public static bool memDualSIM = true;

        public static bool memPrintPin = false;

        public static FieldInfo[] fields;

        public static List<ValidOffice> ValidOffice = new List<ValidOffice>();

        public static Dictionary<string, object> fieldsDictArticle = new Dictionary<string, object>();

        public static Dictionary<string, object> fieldsDict = new Dictionary<string, object>();
        public static Dictionary<string, object> fieldsDictInv = new Dictionary<string, object>();

        public static List<Inventory> inventories = new List<Inventory>();

        static void Main(string[] args)
        {
            InventoryContext context = new InventoryContext();

            context.Database.EnsureCreated();

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            if (!context.ValidOffice.Any())
            {
                SetupDefaultOffice.setupDefaultOffice();
            }

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            ExchangeRates.getExchangeRates();

            //FileManagment.readRegister();

            Console.WriteLine("Välkommen till vårt inventarieregister!\n");

            Menu.displayMenu();
         }
    }
}
