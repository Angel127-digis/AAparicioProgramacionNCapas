using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {
                    result.Objects = new List<object>();
                    var listaColonia = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();

                    if (listaColonia.Count() > 0)
                    {
                        foreach (var itemColonia in listaColonia)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = itemColonia.IdColonia;
                            colonia.Nombre = itemColonia.Nombre;
                            result.Objects.Add(colonia);
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
