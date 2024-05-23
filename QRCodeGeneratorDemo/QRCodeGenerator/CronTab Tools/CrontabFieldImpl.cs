using NCrontab;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZXing.Aztec.Internal;

namespace QRCodeGenerator.CronTab_Tools
{
    sealed partial class CrontabFieldImpl
    {
        public static readonly CrontabFieldImpl Second = new CrontabFieldImpl(CrontabFieldKind.Second, 0, 59, null!);
        public static readonly CrontabFieldImpl Minute = new CrontabFieldImpl(CrontabFieldKind.Minute, 0, 59, null!);
        public static readonly CrontabFieldImpl Hour = new CrontabFieldImpl(CrontabFieldKind.Hour, 0, 23, null!);
        public static readonly CrontabFieldImpl Day = new CrontabFieldImpl(CrontabFieldKind.Day, 1, 31, null!);


        public static readonly CrontabFieldImpl Month = new CrontabFieldImpl(CrontabFieldKind.Month, 1, 12, new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
        public static readonly CrontabFieldImpl DayOfWeek = new CrontabFieldImpl(CrontabFieldKind.DayOfWeek, 0, 6, new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });

        //For ++ (under //5)
        static readonly CrontabFieldImpl[] FieldByKind = { Second, Minute, Hour, Day, Month, DayOfWeek };



        static readonly CompareInfo Comparer = CultureInfo.InvariantCulture.CompareInfo;


        public static CrontabFieldImpl FromKind(CrontabFieldKind kind)
        {
            //check is this is a kind of time...
            if (!Enum.IsDefined(typeof(CrontabFieldKind), kind))
            {
                var kinds = string.Join(", ", Enum.GetNames(typeof(CrontabFieldKind)));
                throw new ArgumentException($"Invalid crontab field kind. Valid values are {kinds}.", nameof(kind));
            }
            //check here very well ++
            return FieldByKind[(int)kind];
        }

        CrontabFieldImpl(CrontabFieldKind kind, int minValue, int maxValue, string[] names)
        {
            Debug.Assert(Enum.IsDefined(typeof(CrontabFieldKind), kind));
            Debug.Assert(minValue >= 0);
            Debug.Assert(maxValue >= minValue);
            Debug.Assert(names == null || names.Length == (maxValue - minValue + 1));

            Kind = kind;
            MinValue = minValue;
            MaxValue = maxValue;
            _names = names!;
        }


        public CrontabFieldKind Kind { get; private set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public string[] _names;
        public int ValueCount => MaxValue - MinValue + 1;

        public CrontabField StoreNewValues(string str)
        {

            if (str.Length == 0)
                return null!;

            //
            // Next, look for a list of values (e.g. 1,2,3).
            //

            return InternalParse(str);

        }
        CrontabField StoreArrayValue(string str, string[] resultArray)
        {
             //var substrings = str.Split(StringSeparatorStock.Comma);

            string[] substrings = Regex.Split(str, @"(?<=[, /])");
            //string[] substrings = Regex.Split(str, @"(?<=[,.\\s])");

            var token = substrings.GetEnumerator();

            resultArray = new string[substrings.Length];

            // Parse the string and get an array of CrontabField instances
            string[] fields = ParseFields(token, resultArray);

            // Create a new CrontabSchedule using the parsed fields
            CrontabField schedule = new CrontabField(fields);


            return schedule;
        }

        //note i modifield this string[] to an object[]
        private static string[] ParseFields(IEnumerator token, string[] resultArray)
        {
            // Your logic to split and parse the string into CrontabField instances
            // For demonstration, let's assume you have already done this.
            int index = 0;

            while (token.MoveNext())
            {
                resultArray[index] = token.Current.ToString()!; // Store the current substring
                index++;
            }
            // Return an array of CrontabField instances
            return resultArray; // Replace with actual parsed fields
        }
        CrontabField InternalParse(string str)
        {
            string[] resultArray = Array.Empty<string>();


            CrontabField schedule = StoreArrayValue(str, resultArray);

            return schedule;

            //if (str.IndexOf(',') > 0)
            //{
            //    //T result = default(T);

            //    //check the use of this code
            //    var substrings = str.Split(StringSeparatorStock.Comma);

            //    using var token = ((IEnumerable<string>)str.Split(StringSeparatorStock.Comma)).GetEnumerator();

            //    resultArray = new string[substrings.Length];

            //    // Parse the string and get an array of CrontabField instances
            //    CrontabField[] fields = ParseFields((IEnumerable)token, resultArray);

            //    // Create a new CrontabSchedule using the parsed fields
            //    CrontabSchedule schedule = new CrontabSchedule(fields);


            //    return schedule;
            //}

            //int? every = null;

            ////
            //// Look for stepping first (e.g. */2 = every 2nd).
            ////

            ////check meaning of this statement
            //if (str.IndexOf('/') is var slashIndex and > 0)
            //{
            //    var substrings = str.Split(StringSeparatorStock.Comma);

            //    using var token = ((IEnumerable<string>)str.Split(StringSeparatorStock.Comma)).GetEnumerator();

            //    resultArray = new string[substrings.Length];

            //    // Parse the string and get an array of CrontabField instances
            //    CrontabField[] fields = ParseFields((IEnumerable)token, resultArray);

            //    // Create a new CrontabSchedule using the parsed fields
            //    CrontabSchedule schedule = new CrontabSchedule(fields);


            //    return schedule;
            //}

            ////
            //// Next, look for wildcard (*).
            ////

            //if (str.Length == 1 && str[0] == '*')
            //{
            //    string[] resultArray = new string[] { str };
            //    return resultArray;
            //}

            ////
            //// Next, look for a range of values (e.g. 2-10).
            ////

            //if (str.IndexOf('-') is var dashIndex and > 0)
            //{
            //    //13 for items that contains -
            //    var first = ParseValue(str.Substring(0, dashIndex));
            //    var last = ParseValue(str.Substring(dashIndex + 1));
            //    //16
            //    return acc(first, last, every ?? 1, success, errorSelector);
            //}


            //var value = ParseValue(str);

            //return null;

        }

        //int ParseValue(string str)
        //{
        //    if (str.Length == 0)
        //        throw new CrontabException("A crontab field value cannot be empty.");
        //    //15
        //    if (str[0] is >= '0' and <= '9')
        //        return int.Parse(str, CultureInfo.InvariantCulture);

        //    if (_names == null)
        //    {
        //        throw new CrontabException($"'{str}' is not a valid [{Kind}] crontab field value. It must be a numeric value between {MinValue} and {MaxValue} (all inclusive).");
        //    }

        //    for (var i = 0; i < _names.Length; i++)
        //    {
        //        if (Comparer.IsPrefix(_names[i], str, CompareOptions.IgnoreCase))
        //            return i + MinValue;
        //    }

        //    var names = string.Join(", ", _names);
        //    throw new CrontabException($"'{str}' is not a known value name. Use one of the following: {names}.");
        //}

    }
}
