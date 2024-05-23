using Microsoft.AspNetCore.Components.Web.Virtualization;
using NCrontab;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Aztec.Internal;
using static NCrontab.CrontabSchedule;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QRCodeGenerator.CronTab_Tools
{
    public sealed partial class CrontabSchedule
    {
        [Required]
        [RegularExpression(@"^[.*\s]+$", ErrorMessage = "Invalid format. Only dots, spaces, and asterisks are allowed.")]
        public readonly CrontabField? _seconds;

        [RegularExpression(@"^[a-zA-Z0-9\s-]+$", ErrorMessage = "Invalid format. Only letters, digits, spaces, and hyphens are allowed.")]
        public readonly CrontabField _minutes;

        [RegularExpression(@"^[a-zA-Z0-9\s-*]+$", ErrorMessage = "Invalid format. Only letters, digits, spaces, hyphens, and asterisks are allowed.")]
        public readonly CrontabField _hours;

        [RegularExpression(@"^[a-zA-Z0-9\s-*]+$", ErrorMessage = "Invalid format. Only letters, digits, spaces, hyphens, and asterisks are allowed.")]
        public readonly CrontabField _days;

        [RegularExpression(@"^[a-zA-Z0-9\s-*]+$", ErrorMessage = "Invalid format. Only letters, digits, spaces, hyphens, and asterisks are allowed.")]
        public readonly CrontabField _months;

        [RegularExpression(@"^[a-zA-Z0-9\s-*]+$", ErrorMessage = "Invalid format. Only letters, digits, spaces, hyphens, and asterisks are allowed.")]
        public readonly CrontabField _daysOfWeek;
        public static string MyWriteUp { get; set; } = null!;
        public static string AwaitTime { get; set; } = null!;
        public static string NextAwaitTime { get; set; } = null!;
        public static string MinuteValue { get; set; }
        public static string HourValue { get; set; }
        public static string ContainMinuteValue { get; set; }
        public static int SingleMinute { get; set; } = 0;
        public static int HourCountX { get; set;} = 0;
        public static int LastIndexOfMinute { get; set;} = 0;
        public static bool ContantComma { get; set;} = false;
        public static int HourCount { get; set;} = 0;
        public static List<string> OutputAllResult { get; set; } = new();
        public static List<string> OutputAllResultDuplicate { get; set; } = new();

        static readonly CrontabField SecondZero = CrontabField.Seconds("0");
        public CrontabSchedule(CrontabField seconds, CrontabField minutes, CrontabField hours, CrontabField days, CrontabField months, CrontabField daysOfWeek)
        {
            _seconds = seconds;
            _minutes = minutes;
            _hours = hours;
            _days = days;
            _months = months;
            _daysOfWeek = daysOfWeek;
            //PrintOutText(seconds, minutes, hours, days, months, daysOfWeek);
        }


        public sealed partial class ParseOptions
        {
            public bool IncludingSeconds { get; set; }
        }

        public static CrontabSchedule Parse(string expression, ParseOptions? options) =>
            TryParse(expression, options);

        public static CrontabSchedule TryParse(string expression, ParseOptions? options)
        {

            var tokens = expression.Split(StringSeparatorStock.Space, StringSplitOptions.RemoveEmptyEntries);


            var includingSeconds = options is { IncludingSeconds: true };
            var expectedTokenCount = includingSeconds ? 6 : 5;

          
            //Assign an array of 6 items to the CrontabField Class
            var fields = new CrontabField[6];

            //Here it checks if the ***** is upto 6 if not it will start with minute instead of secound first
            var offset = includingSeconds ? 0 : 1;
            //Here is were the main logic for ****** work is
            for (var i = 0; i < tokens.Length; i++)
            {
                var kind = (CrontabFieldKind)i + offset;
                var field = CrontabField.Parse(kind, tokens[i]);
                //12
                if (field == null)
                    return null!;
                fields[i + offset] = field; // non-null by mutual exclusivity!
                //continue loop for all the items in this fields *****
            }

            return new CrontabSchedule(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);
        }
        private static void PassLoop(List<string> output,char texx ,string hour)
        {
            //Implement this logic this logic 4 - 6 for hour and minute
            if (texx == '*' && hour == "minute")
            {
                OutputAllResult.Add(FileFormDicAndListItems.minuteMap[0]);
            }

            if (texx == ',' && (hour == "minute" || hour == "hour"))
            {
                NextAwaitTime = texx.ToString();
                if (hour == "minute")
                {
                    //past every not alt to be here
                    if (output.Contains(ContainMinuteValue) && ContainMinuteValue == null) { 
                        OutputAllResult.Add(FileFormDicAndListItems.minuteMap[3]);
                        HourCountX = OutputAllResult.Count;
                        } 
                }
                if (hour == "hour")
                {
                    if (!OutputAllResult.Contains(FileFormDicAndListItems.minuteMap[5]))
                    {
                        OutputAllResult.Add(FileFormDicAndListItems.minuteMap[5]);
                    }
                    HourCountX = OutputAllResult.Count;

                }
            }
            if (NextAwaitTime == "," && char.IsDigit(texx))
            {
                if (hour == "hour")
                {
                    OutputAllResult.Add($", ");
                    OutputAllResult.Add($" {texx} ");
                    HourCountX = OutputAllResult.Count;
                }
                else
                {
                    ContantComma = true;
                    OutputAllResult.Add($" {texx} ");
                }
            }
            if (texx == '-' && hour == "hour")
            {
                OutputAllResult.Add(FileFormDicAndListItems.minuteMap[6]);
                var Rex =  OutputAllResult.IndexOf(FileFormDicAndListItems.minuteMap[6]);
                if (Rex != -1)
                {
                    OutputAllResult.IndexOf(FileFormDicAndListItems.minuteMap[6], Rex - 2);
                }
            }
            //check here it inputed the number first before adding other.
            //if (char.IsDigit(texx) && hour == "minute")
            //{
            //    OutputAllResult.Add(texx.ToString());
            //    HourCount = OutputAllResult.LastIndexOf(texx.ToString());
            //}

            if (texx == '/' && (hour == "minute" || hour == "hour"))
            {
                if (hour == "minute")
                {
                    NextAwaitTime = "/";
                    OutputAllResult.Clear();
                }
                if (hour == "hour"){ NextAwaitTime = "/";}

                //if (hour == "hour")
                //{ //Check code properly
                //    var last = OutputAllResult.Count;
                //    OutputAllResult.RemoveRange(LastIndexOfMinute, last);
                //}
            }
            if (NextAwaitTime == "/" && (hour == "minute" || hour == "hour") && char.IsDigit(texx))
            {
                if (hour == "minute")
                {
                    NextAwaitTime = "";
                    ContainMinuteValue = FileFormDicAndListItems.ValuesHour(texx)[1];
                    OutputAllResult.Add(ContainMinuteValue);
                    HourCount = OutputAllResult.IndexOf(FileFormDicAndListItems.ValuesHour(texx)[1]);//check here
                }
                if (hour == "hour")
                {
                    NextAwaitTime = "";
                    OutputAllResult.Add(FileFormDicAndListItems.ValuesHour(texx)[0]);
                    //check here properly
                    //OutputAllResult.RemoveRange(HourCount, 2); Check this code.
                }
            }


        }

        private static void ForTimeStamp(List<string> output, string[] time, string TimeInterval)
        {
            if (time.Length > 1)
            {

                for (var i = 0; i < time.Length; i++)
                {
                    var tex = time[i];
                    if (char.IsDigit(tex, 0))
                    {
                        for (var j = 0; j < time[i].Length; j++)
                        {
                            var texx = time[i][j];
                            PassLoop(output, texx, TimeInterval);
                        }
                    }
                    else
                    {
                        for (var j = 0; j < time[i].Length; j++)
                        {

                            var texx = time[i][j];
                            PassLoop(output, texx, TimeInterval);

                         }
                    }
                }
            }
            else
            {
                foreach (var field in time)
                {
                    //if (field.Contains("*") && (TimeInterval == "hour"))
                    //{
                    //    output.Add($" ");
                    //}
                    //check here
                    if (field.Contains("*") && (TimeInterval == "minute"))
                    {
                        OutputAllResult.Add(FileFormDicAndListItems.minuteMap[0]);
                    }
                    if (char.IsDigit(field, 0) && (TimeInterval == "minute"))
                    {
                        SingleMinute += 1;
                        MinuteValue = field;
                        OutputAllResult.Add(FileFormDicAndListItems.minuteMap[1] + $" {field} ");
                    }
                    if (field.Contains("*") && (TimeInterval == "minute"))
                    {
                        OutputAllResult.Add(" ");
                    }
                    if (char.IsDigit(field, 0) && (TimeInterval == "hour") && SingleMinute > 0)
                    {
                        OutputAllResult.Clear();
                        OutputAllResult.Add( $"At {field} : {MinuteValue} ");
                        var Newnumbers = (field.Length == 1) ? $"0{field}": $"{field}";
                        var Newnumbers2 = (MinuteValue.Length == 1) ? $"0{MinuteValue}": $"{MinuteValue}";
                        OutputAllResult.Add($"At {Newnumbers} : {Newnumbers2} ");
                    }
                    else if(char.IsDigit(field, 0) && (TimeInterval == "hour") && SingleMinute == 0)
                    {

                        OutputAllResult.Add(FileFormDicAndListItems.minuteMap[5] + $"{field}");
                    }

                    //if (char.IsDigit(field, 0) && TimeInterval == "hour")
                    //{
                    //    if (AwaitTime == null)
                    //    {
                    //        output.Add($"past hour {NextAwaitTime}");
                    //        output.Insert(1, $" At every minute and Place the really Time in the bracket(\"minute\")");
                    //    }
                    //    else
                    //    {
                    //        NextAwaitTime = field;
                    //        //Check if you can use the replace keyword here...
                    //        output.RemoveAt(0);
                    //        output.Insert(0, $"At {NextAwaitTime}:{AwaitTime} ");
                    //    }
                    //}
                    //else if (char.IsDigit(field, 0) && (NextAwaitTime == null))
                    //{
                    //    AwaitTime = field;
                    //    output.Add($"At {TimeInterval} {field}");

                    //}
                }
            }

        }
        
        public static string PrintOutText(CrontabSchedule crontab)
        {

            OutputAllResultDuplicate.Clear();
            OutputAllResult.Clear();
            //List<string> output1 = new List<string>();
            List<string> output2 = new List<string>();
            //List<string> output3 = new List<string>(); 
            //List<string> output4 = new List<string>();
            //List<string> output5 = new List<string>();
            string[] _seconds = (crontab._seconds == null || crontab._seconds._crontabFields == null)
                ? null!
                : crontab._seconds._crontabFields;

            var _minutes = crontab._minutes._crontabFields;
            var _hours = crontab._hours._crontabFields;
            var _days = crontab._days._crontabFields;
            var _daysOfWeek = crontab._daysOfWeek._crontabFields;
            var _months = crontab._months._crontabFields;
            //if(_seconds == null)
            //{
            //    output1.Clear();
            //}

            //if ()
            //{
            //    output.Add($"At minute {tex}");

            //}

            ForTimeStamp(output2, _minutes, "minute");
            LastIndexOfMinute = OutputAllResult.Count;
            if (HourCountX > 0)
            {
                var andPositioning = OutputAllResult.IndexOf(FileFormDicAndListItems.minuteMap[3]);
                if (andPositioning != -1)
                {
                    OutputAllResult[andPositioning] = " ";

                }
                //OutputAllResult.RemoveAt(HourCountX);
                OutputAllResult.Insert(HourCountX - 1, FileFormDicAndListItems.minuteMap[3]);
            }
            ForTimeStamp(output2, _hours, "hour");
            if(ContantComma == true || HourCountX > 0)
            {
                OutputAllResult.Insert(HourCountX - 1, $" and ");
            }

            //ForTimeStamp(output3, _hours, "hour");

            //ForTimeStampDaysMonth(output4, _months, "month");

            //ForTimeStampDaysMonth(output5, _daysOfWeek, "daysOfWeek");



            //ForTimeStampDaysMonth(output, _days, "day");
            for (int i = 0; i < OutputAllResult.Count; i++)
            {
                var number = 0;
                if (OutputAllResult[i] == FileFormDicAndListItems.minuteMap[0] && number == 0)
                {
                    number++;
                    OutputAllResultDuplicate.Add(OutputAllResult[i]);

                }else if (OutputAllResult[i] != FileFormDicAndListItems.minuteMap[0] || number == 0)
                {
                    OutputAllResultDuplicate.Add(OutputAllResult[i]);
                }
            }
            foreach (var item in OutputAllResultDuplicate)
            {
                MyWriteUp += item;
            }



            return MyWriteUp;
        }

        //Work on this code for Days and Month make sure you implement the man day address
        private static void ForTimeStampDaysMonth(List<string> output, string[] time, string TimeInterval)
        {

            if (time.Length > 1)
            {
                for (var i = 0; i < time.Length; i++)
                {
                    var tex = time[i];
                    if (char.IsDigit(tex, 0))
                    {
                        for (var j = 0; j < time[i].Length; j++)
                        {
                            var texx = time[i][j];
                            PassLoop(output, texx, TimeInterval);
                        }
                    }
                    //else if (char.IsDigit(tex, 0) && tex.Length == 1)
                    //{
                    //    var intNumber = Convert.ToInt32(tex);
                    //    var day = CrontabFieldImpl.Day._names[intNumber];
                    //    output.Add($"on day-of-month {day}");
                    //}
                    else
                    {
                        for (var j = 0; j < time[i].Length; j++)
                        {

                            var texx = time[i][j];
                            PassLoop(output, texx, TimeInterval);

                        }
                    }
                }
            }
            else
            {
                foreach (var field in time)
                {
                    if (field.Contains("*"))
                    {
                        OutputAllResult.Add(FileFormDicAndListItems.minuteMap[0]);
                    }
                    if (char.IsDigit(field, 0) )
                    {
                        var intNumber = Convert.ToInt32(field) - 1;
                        var day = (TimeInterval == "day") ? CrontabFieldImpl.Day._names[intNumber] : CrontabFieldImpl.Month._names[intNumber];
                        //var day = CrontabFieldImpl.Day._names[intNumber];
                       var inWork = (TimeInterval == "month") ? "in": "on";
                        output.Add($"{inWork} {day}");

                    }
                }
            }

        }

    }
}