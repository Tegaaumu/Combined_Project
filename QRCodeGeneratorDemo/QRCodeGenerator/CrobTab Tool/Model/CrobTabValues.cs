using System.Text.RegularExpressions;

namespace QRCodeGenerator.CrobTab_Tool.Model
{
    public static class CrobTabValues
    {
        public static void DoSomething(Dictionary<int, string> list, out string Text)
        {
            Text = "";
            foreach (var item in list.Values)
            {
                switch (item)
                {
                    case "/":
                        Text += $"The days fix\n..";
                        // Add confirmation logic for "sample"
                        break;
                    case ",":
                        Text += $" and \n....";
                        // Add confirmation logic for "test"
                        break;
                    case "-":
                        Text += $" to \n...";
                        // Add confirmation logic for "example"
                        break;
                    case "*":
                        Text += $"Every \n.....<br>";
                        // Add confirmation logic for "example"
                        break;
                    default:
                        Console.WriteLine($"No confirmation logic for {item}.");
                        break;
                }
            }
        }


    }
}
