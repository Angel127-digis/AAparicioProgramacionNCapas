using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Calculadora" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Calculadora.svc or Calculadora.svc.cs at the Solution Explorer and start debugging.
    public class Calculadora : ICalculadora
    {
        public void DoWork()
        {
        }
       
        public double suma(int numeroUno, int numeroDos)
        {
            return numeroUno + numeroDos;
        }
       
        public double resta(int numeroUno, int numeroDos)
        {
            return numeroUno - numeroDos;
        }
       
        public double multiplicacion(int numeroUno, int numeroDos)
        {
            return numeroUno * numeroDos;
        }
       
        public double division(int numeroUno, int numeroDos)
        {
            return numeroUno / numeroDos;
        }

        
    }
}
