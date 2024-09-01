using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WCF_SERVER.intercepters;

namespace WCF_SERVER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8000/windspeed/");
            using(ServiceHost host = new ServiceHost(typeof(Service), baseAddress)) { 
                CustomBinding binding = CreateCustomBinding();

                ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(IService), binding, "");

                host.Description.Behaviors.Add(new CustomServiceBehavior());
               // endpoint.EndpointBehaviors.Add(new UrnToEndpointBehavior());

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior()
                {
                    HttpGetEnabled = true,

                };
                host.Description.Behaviors.Add(smb);

                host.Open();
                Console.WriteLine("Service is running ...");
                Console.ReadLine();

            }
        }

        public static CustomBinding CreateCustomBinding()
        {
            CustomBinding binding = new CustomBinding();

            ReliableSessionBindingElement reliableMessaging = new ReliableSessionBindingElement()
            {
                Ordered = true,
                ReliableMessagingVersion = ReliableMessagingVersion.WSReliableMessaging11,
                InactivityTimeout = TimeSpan.FromMinutes(10),
            };

            MtomMessageEncodingBindingElement mtomMessage = new MtomMessageEncodingBindingElement()
            {
                MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap11, AddressingVersion.WSAddressingAugust2004),
                WriteEncoding = Encoding.UTF8,
            };

            HttpTransportBindingElement httpTransport = new HttpTransportBindingElement()
            {
               ManualAddressing = false,
               MaxBufferSize = 65536,
               MaxReceivedMessageSize = 65536
            };

            var interceptorBinding = new InterceptingBindingElement(new ServerInterceptor());

            binding.Elements.Add(reliableMessaging);
            binding.Elements.Add(mtomMessage);
            binding.Elements.Add(interceptorBinding);
            binding.Elements.Add(httpTransport);
            return binding;
        }
    }
}
