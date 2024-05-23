using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerator.CronTab_Tools
{
    public static class FileFormDicAndListItems
    {
        public static Dictionary<int, string> minuteMap = new Dictionary<int, string>
        {
            { 0, " At every minute " },
            { 1, " At every " },
            { 2, " past every " },
            { 3, " past every minute " },
            { 4, " and every minute from " },
            { 5, " past every hour " },
            { 6, " through " },
            { -1, "" }
            // Add more mappings as needed
        };

        public static Dictionary<int, string> ValuesHour(char number)
        {
            Dictionary<int, string> hourMap = new Dictionary<int, string>()
            {
                //For /
                { 0, $" past every {number} hour" },
                { 1, $" At every {number} minute" },
                //for
                { 2, $" past every hour and {number} " }
            };

            return hourMap;
        }

        public static Dictionary<int, string> DifferentTimeReading = new Dictionary<int, string>
        {
            { 0, "and every minute from" },
            { 1, "and 1 past the hour" },
            // Add more mappings as needed
            { -1, "" }
        };

    }
}
