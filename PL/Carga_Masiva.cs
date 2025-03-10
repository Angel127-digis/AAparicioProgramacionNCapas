using System;
using System.Collections.Generic;
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
                while ((fila = streamReader.ReadLine()) != null)
                {
                    Console.WriteLine(fila);
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
