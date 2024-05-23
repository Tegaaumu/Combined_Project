using QRCodeGenerator.CronTab_Tools;

namespace QRCodeGenerator.CrobTab_Tool.Model
{
    public class CrobTabInput
    {
        public string InputString { get; set; } = null!;
        public string Output { get; set; } = null!;
        public string[] ArraystringBroken { get; set; } = null!;
        public string ArraystringBrokenRR { get; set; } = null!;

        public readonly char[] Separators = { ' ' };
        public string OutputValue { get; set; } = null!;

        public CrontabSchedule? _crontab;

        public bool _isSixPart;
    }
}
