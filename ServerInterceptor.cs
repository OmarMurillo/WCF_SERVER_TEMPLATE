using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WCF_SERVER.intercepters;

namespace WCF_SERVER
{
    public class ServerInterceptor : ChannelMessageInterceptor
    {
        int messagesSinceLastReport = 0;
        readonly int reportPeriod = 5;

        public ServerInterceptor() { }

        public override void OnReceive(ref Message msg)
        {
            msg.Headers.To = new Uri("urn:SAP:");
            Console.WriteLine(msg.ToString());
            Console.WriteLine(msg.Headers.To);
            msg.Headers.To = new Uri("http://localhost:8000/windspeed/");

            return;
            if (msg.Headers.To.ToString() == "urn:SAP:")
            {
                Console.WriteLine(reportPeriod + " x2 wind speed reports have been received.");
                return;
            }

            // Drop incoming Message if the Message does not have the special header
            msg = null;
        }

        public override ChannelMessageInterceptor Clone()
        {
            return new ServerInterceptor();
        }
    }
}
