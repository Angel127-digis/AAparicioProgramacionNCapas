using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {
                    result.Objects = new List<object>();

                    var listaEstado = context.EstadoGetAll().ToList();

                    if (listaEstado.Count() > 0)
                    {
                        foreach (var itemEstado in listaEstado)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = itemEstado.IdEstado;
                            estado.Nombre = itemEstado.Nombre;
                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
