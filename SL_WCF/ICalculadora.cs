using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Services;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalculadora" in both code and config file together.
    [ServiceContract]
    public interface ICalculadora
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        double suma(int numeroUno, int numeroDos);

        [OperationContract]
        double resta(int numeroUno, int numeroDos);

        [OperationContract]
        double multiplicacion(int numeroUno, int numeroDos);

        [OperationContract]
        double division(int numeroUno, int numeroDos);
    }

}
