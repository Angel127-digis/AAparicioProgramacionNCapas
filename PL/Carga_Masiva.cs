using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Carga_Masiva
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();

            String ruta = @"C:\Users\digis\Documents\Miiguel Angel Aparicio Nava\NuevosRegistros.txt";
            try
            {
                StreamReader streamReader = new StreamReader(ruta);
                String fila = "";

                streamReader.ReadLine();

                while ((fila = streamReader.ReadLine()) != null)
                {
                    String[] registros = fila.Split('|');

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();

                    usuario.Nombre = registros[0];
                    usuario.ApellidoPaterno = registros[1];
                    usuario.ApellidoMaterno = registros[2];
                    usuario.Telefono = registros[3];
                    usuario.Email = registros[4];
                    usuario.Password = registros[5];
                    usuario.FechaNacimiento = registros[6];
                    usuario.Sexo = registros[7];
                    usuario.Celular = registros[8];
                    usuario.Estatus = Convert.ToBoolean(Convert.ToInt32(registros[9]));
                    usuario.CURP = registros[10];
                    usuario.Rol.IdRol = Convert.ToInt32(registros[11]);
                    usuario.UserName = registros[12];
                    usuario.Imagen = null;

                    BL.Usuario.Add(usuario);
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
