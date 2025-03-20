using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ResultSQL
    {
        public int Correctos { get; set; }
        public int Incorrectos { get; set; }
        public int RegistrosInsertadosMal { get; set; }
        public List<object> Resultados { get; set; }
    }
}
