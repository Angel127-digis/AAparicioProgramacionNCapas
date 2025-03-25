using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public String Calle { get; set; }
        public String NumeroInterior { get; set; }
        public String NumeroExterior { get; set; }
        
        public ML.Colonia Colonia { get; set; }
        public List<object> Direcciones { get; set; }
    }
}
