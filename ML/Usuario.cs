using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public String UserName { get; set; }

        public String Nombre { get; set; }

        public String ApellidoPaterno { get; set; }

        public String ApellidoMaterno { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }
        
        public String FechaNacimiento { get; set; }
        
        public String Sexo { get; set; }

        public String Telefono { get; set; }

        public String Celular { get; set; }
        public bool Estatus { get; set; }

        public String CURP { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
        public byte[] Imagen { get; set; }
        public List<object> Usuarios { get; set; }
    }
}
