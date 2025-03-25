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

        [Required(ErrorMessage = "UserName Obligatorio")]
        [RegularExpression(@"[a-z A-Z]", ErrorMessage ="Nombre solo acepta letras")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Nombre Obligatorio")]
        [RegularExpression(@"[a-z A-Z]", ErrorMessage = "Apellido Paterno solo acepta letras")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "Apellido Paterno Obligatorio")]
        [RegularExpression(@"[a-z A-Z]", ErrorMessage = "Apellido Paterno solo acepta letras")]
        public String ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Apellido Materno Obligatorio")]
        [RegularExpression(@"[a-z A-Z]", ErrorMessage = "Apellido Materno solo acepta letras")]
        public String ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Email obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "EL email no es valido")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Contraseña Obligatorio")]
        [RegularExpression(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$", ErrorMessage = "La contraseña es incorrecta")]
        public String Password { get; set; }
        
        [Required(ErrorMessage = "Fecha de nacimiento Obligatoria")]
        public String FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Sexo obligatorio")]
        [RegularExpression(@"[a-z A-Z]", ErrorMessage = "Sexo solo acepta letras")]
        public String Sexo { get; set; }

        [Required(ErrorMessage = "Telefono Obligatorio")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Telefono solo acepta numeros")]
        public String Telefono { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Celular solo acepta numeros")]
        public String Celular { get; set; }
        public bool Estatus { get; set; }

        [Required(ErrorMessage = "CURP Obligatorio")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "La CURP es invalida")]
        public String CURP { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
        public byte[] Imagen { get; set; }
        public String ImagenBase64 { get; set; }
        public List<object> Usuarios { get; set; }
    }
}
