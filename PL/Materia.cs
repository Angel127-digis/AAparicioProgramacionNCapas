using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class Materia
    {

        public static void Add() //logica para pedir la informacion
        {
            ML.Materia materia = new ML.Materia();

            Console.WriteLine("Ingrese el ID de la materia");
            materia.Id = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre de la materia");
            materia.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese los creditos de la materia");
            materia.Creditos = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Ingrese la calificacion de la materia");
            materia.Calificacion = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Ingrese la evaluacion de la materia");
            materia.Evaluacion = Console.ReadLine();


            BL.Materia.Add(materia);
        }

        public static void update() //logica para actualizar informacion
        {
            ML.Materia materia = new ML.Materia();

            Console.WriteLine("Ingrese el ID de la materia");
            materia.Id = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre de la materia");
            materia.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese los creditos de la materia");
            materia.Creditos = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Ingrese la calificacion de la materia");
            materia.Calificacion = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Ingrese la evaluacion de la materia");
            materia.Evaluacion = Console.ReadLine();


            BL.Materia.update(materia);
        }
        public static void delete() //logica para actualizar informacion
        {
            

            Console.WriteLine("Ingrese el ID de la materia");
            int idMateria = Convert.ToInt32(Console.ReadLine());

            BL.Materia.delete(idMateria);
        }
    }
}
