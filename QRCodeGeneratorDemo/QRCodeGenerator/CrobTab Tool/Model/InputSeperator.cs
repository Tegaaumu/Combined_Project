namespace QRCodeGenerator.CrobTab_Tool.Model
{
    public static class InputSeperator
    {
        public static string[] ValuesToArray(string values)
        {
            string[] answer = values.Split(" ", 5);
            return answer;
        }
    }
}
