using ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class Usuario
    {
        public static void Add() //logica para pedir la informacion
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el UserName");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el correo electronico");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Ingrese la fecha de nacimineto");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Ingrese el sexo");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Ingrese el numero de telefono");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Ingrese el numero celular");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Ingrese el estatus");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Ingrese la CURP");
            usuario.CURP = Console.ReadLine();
            //Console.WriteLine("Ingrese la imagen");
            //usuario.Imagen = Encoding.UTF8.GetBytes(Console.ReadLine());

            ML.Result result = BL.Usuario.AddLINQ(usuario);
            if (result.Correct)
            {
                Console.WriteLine("Registro agregado correctamente");
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }
        }

        public static void Update() //logica para actualizar informacion
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el Id del usuario");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese el UserName");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el correo electronico");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Ingrese la fecha de nacimineto");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Ingrese el sexo");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Ingrese el numero de telefono");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Ingrese el numero celular");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Ingrese el estatus");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Ingrese la CURP");
            usuario.CURP = Console.ReadLine();
            //Console.WriteLine("Ingrese la imagen");
            //usuario.Imagen = Encoding.UTF8.GetBytes(Console.ReadLine());

            ML.Result result = BL.Usuario.UpdateLINQ(usuario);
            if (result.Correct)
            {
                Console.WriteLine("Registro actualizado correctamente");
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }
        }
        public static void Delete() //logica para actualizar informacion
        {
            int idUsuario = 0;
            Console.WriteLine("Ingrese el Id del usuario");
            idUsuario = Convert.ToInt32(Console.ReadLine());
            ML.Result result = BL.Usuario.DeleteLINQ(idUsuario);
            if (result.Correct)
            {
                Console.WriteLine("Registro elimindado correctamente");
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }

            //BL.Usuario.Delete(idusuario);
        }
        //public static void Show()
        //{
        //    int numeroDeControl = 0;
        //    Console.WriteLine("¿Desea ver todos los registor o solo uno? T=Todos, U=Uno");
        //    String respuesta = Console.ReadLine();
        //    bool todosRegistros = true;
        //    todosRegistros = respuesta == "T" ? todosRegistros : todosRegistros = false;
        //    if (!todosRegistros)
        //    {
        //        Console.WriteLine("Ingrese el numero de control el usuario");
        //        numeroDeControl = Convert.ToInt32(Console.ReadLine());
        //    }
        //    BL.Usuario.Show(todosRegistros, numeroDeControl);
        //}
        public static void GetAll()
        {
            ML.Result result = BL.Usuario.GetAllLINQ();

            if (result.Correct)
            {
                //mostrar los registros
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine(usuario.UserName);
                    Console.WriteLine(usuario.Nombre);
                    Console.WriteLine(usuario.ApellidoPaterno);
                    Console.WriteLine(usuario.ApellidoMaterno);
                    Console.WriteLine(usuario.Email);
                    Console.WriteLine(usuario.Password);
                    Console.WriteLine(usuario.FechaNacimiento);
                    Console.WriteLine(usuario.Sexo);
                    Console.WriteLine(usuario.Telefono);
                    Console.WriteLine(usuario.Celular);
                    Console.WriteLine(usuario.Estatus);
                    Console.WriteLine(usuario.CURP);
                    //Console.WriteLine(usuario.Imagen);
                    
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Hubo un error " + result.ErrorMessage);
            }
        }
        public static void GetById()
        {
            Console.Write("\nIngrese el numero de control: ");
            int idUsuario = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            ML.Result result = BL.Usuario.GetByIdLINQ(idUsuario);

            if (result.Correct)
            {
                //mostrar los registros
                ML.Usuario usuario = (ML.Usuario)result.Object;


                Console.WriteLine(usuario.UserName);
                Console.WriteLine(usuario.Nombre);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.WriteLine(usuario.ApellidoMaterno);
                Console.WriteLine(usuario.Email);
                Console.WriteLine(usuario.Password);
                Console.WriteLine(usuario.FechaNacimiento);
                Console.WriteLine(usuario.Sexo);
                Console.WriteLine(usuario.Telefono);
                Console.WriteLine(usuario.Celular);
                Console.WriteLine(usuario.Estatus);
                Console.WriteLine(usuario.CURP);
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Hubo un error " + result.ErrorMessage);
            }

        }
        public static void Menu()
        {
            int opcion = 0;
            do
            {
                Console.WriteLine("Elige una opcion\n" +
                    "1. Agregar registro\n" +
                    "2. Actualizar registro\n" +
                    "3. Eliminar registro\n" +
                    "4. Mostrar registros\n" +
                    "5. Mostrar un registro\n" +
                    "6. Salir\n");
                opcion = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (opcion)
                {
                    case 1: Add(); break;
                    case 2: Update(); break;
                    case 3: Delete(); break;
                    case 4: GetAll(); break;
                    case 5: GetById(); break;
                }
                Console.WriteLine();
            }
            while (opcion != 6);
        }
    }
}
