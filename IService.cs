using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_SERVER
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract(IsOneWay = true)]
        void ReportWindSpeed(int speed);  
    }
}
