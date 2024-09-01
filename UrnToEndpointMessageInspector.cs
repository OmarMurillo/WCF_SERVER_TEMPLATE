using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCF_SERVER
{
    public class UrnToEndpointMessageInspector: IClientMessageInspector, IDispatchMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Console.WriteLine("B_REQUEST", request.ToString());
            return null;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext) {
            var headers = request.Headers;
            Console.WriteLine(channel.SessionId);
            Console.WriteLine(instanceContext);
            Console.WriteLine(request.ToString());

            if(headers.To != null && headers.To.AbsoluteUri.StartsWith("urn:"))
            {
                var newEndpointAddress = new EndpointAddress("http://localhost:52681/Service").Uri;
                request.Headers.To = newEndpointAddress;    
            }
            return null;
        }
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("A_REPLY", reply.ToString());
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("B_REPLY", reply.ToString());
        }
    }
}
