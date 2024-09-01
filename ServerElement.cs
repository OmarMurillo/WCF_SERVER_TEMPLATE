using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_SERVER.intercepters;

namespace WCF_SERVER
{
    public class ServerElement : InterceptingElement
    {
        protected override ChannelMessageInterceptor CreateMessageInterceptor()
        {
            return new ServerInterceptor();
        }
    }
}
