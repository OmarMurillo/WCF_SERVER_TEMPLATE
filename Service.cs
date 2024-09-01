using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_SERVER
{
    public class Service : IService
    {
        public void ReportWindSpeed(int speed)
        {
            if (speed >= 64)
            {
                Console.WriteLine("Dangerous wind detected! Reported speed (" + speed + ") is greater than 64 kph.");
            }
        }
    }
}
