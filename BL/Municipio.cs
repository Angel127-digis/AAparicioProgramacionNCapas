using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {
                    result.Objects = new List<object>();
                    var listaMunicipios = context.MunicipioGetByIdEstado(IdEstado).ToList();

                    if (listaMunicipios.Count() > 0)
                    {
                        foreach (var itemMunicipio in listaMunicipios)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = itemMunicipio.IdMunicipio;
                            municipio.Nombre = itemMunicipio.Nombre;
                            result.Objects.Add(municipio);
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
        //    public static ML.Result GetByIdEstado(int idEstado)
        //    {
        //        ML.Result result = new ML.Result();

        //        try
        //        {
        //            using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
        //            {

        //                var listaMunicipios = context.MunicipioGetByIdEstado(idEstado).SingleOrDefault();

        //                if (listaMunicipios != null)
        //                {
        //                    var row = listaMunicipios;

        //                    ML.Usuario usuario = new ML.Usuario();
        //                    usuario.Direccion = new ML.Direccion();
        //                    usuario.Direccion.Colonia = new ML.Colonia();
        //                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();


        //                    if (row != null)
        //                    {
        //                        usuario.Direccion.Colonia.Municipio.IdMunicipio = row.IdMunicipio;
        //                        usuario.Direccion.Colonia.Municipio.Nombre = row.Nombre;
        //                    }
        //                    else
        //                    {
        //                        usuario.Direccion.Colonia.Municipio.IdMunicipio = 0;
        //                    }

        //                    result.Object = usuario;

        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "No hay registros";
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result.Correct = false ;
        //            result.ErrorMessage = ex.Message;
        //            result.Ex = ex;
        //        }
        //        return result;
        //    }
        //}
    }
}
