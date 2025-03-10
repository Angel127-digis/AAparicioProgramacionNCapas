using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {
                    var registrosRol = (from rol in context.Rols
                                        select rol).ToList();
                    if (registrosRol != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var rolIten in registrosRol)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = rolIten.IdRol;
                            rol.Nombre = rolIten.Nombre;

                            result.Objects.Add(rol);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
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
