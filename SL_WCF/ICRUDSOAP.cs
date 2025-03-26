using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICRUDSOAP" in both code and config file together.
    [ServiceContract]
    public interface ICRUDSOAP
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        ResultSOAP Add(ML.Usuario usuario);
        
        [OperationContract]
        ResultSOAP Delete(int idUsuario);

        [OperationContract]
        ResultSOAP Update(ML.Usuario usuario);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Usuario))]
        ResultSOAP GetAll(ML.Usuario usuario);
    }
}
