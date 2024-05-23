using NCrontab;
using System.Collections;

namespace QRCodeGenerator.CronTab_Tools
{
    public sealed partial class CrontabField
    {
            readonly BitArray? _bits;
            /* readonly */
            int _minValueSet;
            /* readonly */
            int _maxValueSet;
        public string[] _crontabFields = new string[] { };
        readonly CrontabFieldImpl _impl = null!;


        public CrontabField(string[] crontabFields)
        {
            _crontabFields = crontabFields;
        }

        public static CrontabField Parse(CrontabFieldKind kind, string expression) =>
            TryParse(kind, expression);

        ////When using the type T you must place <T> at the front of the method been declared.
        public static CrontabField TryParse(CrontabFieldKind kind, string expression)
        {

            var field = new CrontabField(CrontabFieldImpl.FromKind(kind));

            //Think of the next ogival step to take from here.....


            var error = field._impl.StoreNewValues(expression);

            return error;

        }

        CrontabField(CrontabFieldImpl impl)
        {
            _impl = impl ?? throw new ArgumentNullException(nameof(impl));
            //This is were the values for the number of date, minute, hour, and days are stored.....
            _bits = new BitArray(impl.ValueCount);
            _minValueSet = int.MaxValue;
            _maxValueSet = -1;
        }

        public static CrontabField Seconds(string expression) =>
            Parse(CrontabFieldKind.Second, expression);

        /// <summary>
        /// Parses a crontab field expression representing minutes.
        /// </summary>

        public static CrontabField Minutes(string expression) =>
            Parse(CrontabFieldKind.Minute, expression);

        /// <summary>
        /// Parses a crontab field expression representing hours.
        /// </summary>

        public static CrontabField Hours(string expression) =>
            Parse(CrontabFieldKind.Hour, expression);

        /// <summary>
        /// Parses a crontab field expression representing days in any given month.
        /// </summary>

        public static CrontabField Days(string expression) =>
            Parse(CrontabFieldKind.Day, expression);

        /// <summary>
        /// Parses a crontab field expression representing months.
        /// </summary>

        public static CrontabField Months(string expression) =>
            Parse(CrontabFieldKind.Month, expression);

        /// <summary>
        /// Parses a crontab field expression representing days of a week.
        /// </summary>

        public static CrontabField DaysOfWeek(string expression) =>
            Parse(CrontabFieldKind.DayOfWeek, expression);
    }
}
