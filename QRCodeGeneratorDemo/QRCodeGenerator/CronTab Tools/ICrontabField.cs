using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerator.CronTab_Tools
{
    public interface ICrontabField
    {
        int GetFirst();
        int Next(int start);
        bool Contains(int value);
    }
}
