using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{
    public class MasterSettings
    {
        public const char defaultSyntaxDivider = '-';

        public const int depreciationPeriod = 3;
    }
    public static class MastersettingsInventory
    {
        public const char syntaxDivider = '-';

        //Enter how you want the syntax of id to be, if you want capital letters, enter LETTER- # where # is the
        //minimum number of letters that must be present, can also be combined with a maximum length of letter - # - #.
        //You can also specify that an ID should consist of letters-min / max by entering int-200-500 as an example.
        //You can also force a combination of letters and numbers in a "subpart" by entering alphanumeric in your id syntax.

        public static List<string> format = new List<string> { "LETTER-3", "int-100-1000" };

        //Below are used if format is not set!!!

        public const bool foreceHyphen = true;

        public const int minHyphen = 1;

        public const int maxHyphen = 1;

        public const bool onlyLettersLeftSide = true;

        public const bool onlyNumberRightSide = true;

        public const int minLengthLeftSide = 3;

        public const int minValueRightSide = 200;

        public const int maxValueRightSide = 500;

        public const bool forceUpercase = true;

        public static bool foreceBreak = false;
    }
}
