using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace PL
{
    public class LeerExcel
    {
        public static void Leer()
        {
            String ruta = @"C:\Users\digis\Documents\Miiguel Angel Aparicio Nava\AAparicioProgramacionNCapas\PL_Web\CargaMasiva\RegistrosExcel-1103202515411194.xlsx";

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            if (System.IO.File.Exists(ruta))
            {
                using (var package = new ExcelPackage(new System.IO.FileInfo(ruta)))
                {
                    var worsheet = package.Workbook.Worksheets[0];

                    for (int row = 2; row <= worsheet.Dimension.Rows; row++)
                    {
                        for (int col = 1; col <= worsheet.Dimension.Columns; col++)
                        {
                            Console.Write(worsheet.Cells[row, col].Text + "  ");
                        }
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("El archivo no existe");
            }
        }
    }
}
