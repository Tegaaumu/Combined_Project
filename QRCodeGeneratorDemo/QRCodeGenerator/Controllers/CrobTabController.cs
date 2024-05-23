using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCodeGenerator.CrobTab_Tool.Model;
using QRCodeGenerator.CronTab_Tools;

namespace QRCodeGenerator.Controllers
{
    public class CrobTabController : Controller
    {
        // GET: CrobTabController
        public IActionResult Index()
        {
            CrobTabInput crobTabInput = new CrobTabInput();

            return View(crobTabInput);
        }

        [HttpPost]
        public IActionResult Index(CrobTabOutput InputSting)
        {

            CrobTabInput crobTabInput = new CrobTabInput()
            {
                InputString = InputSting.InputString
            };

            if (crobTabInput.InputString == null)
            {
                ModelState.AddModelError(" ", "This string can't be empty");
            }

            var expression = crobTabInput.InputString!.Trim();

            if (expression.Length >= 5 || expression.Length == 6)
            {

                crobTabInput._isSixPart = expression.Split(crobTabInput.Separators, StringSplitOptions.RemoveEmptyEntries).Length == 6;
                crobTabInput._crontab = CrontabSchedule.Parse(expression, new CrontabSchedule.ParseOptions { IncludingSeconds = crobTabInput._isSixPart });
                //After you have get your value successfully. Create a new method that will break down each of the Array and write a valid if statement to check if they are working properly

                crobTabInput.OutputValue = CrontabSchedule.PrintOutText(crobTabInput._crontab);

                return View(crobTabInput);
            }

            return View(InputSting);
        }

        //public IActionResult Index(CrobTabOutput InputSting)
        //{
        //    string Tega = "";
        //    CrobTabInput crobTabInput = new CrobTabInput()
        //    {
        //        InputString = InputSting.InputString
        //    };
        //    var rexArray = InputSeperator.ValuesToArray(crobTabInput.InputString);

        //    RaedingEachValues.CheckEachValue(rexArray);

        //    CrobTabValues.DoSomething(RaedingEachValues.FoundItemsList, out Tega);

        //    crobTabInput.ArraystringBroken = rexArray;
        //    crobTabInput.ArraystringBrokenRR = Tega;

        //    return View(crobTabInput);
        //}
    }
}
