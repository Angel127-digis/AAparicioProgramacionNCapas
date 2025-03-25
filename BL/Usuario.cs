using DL_EF;
using Microsoft.SqlServer.Server;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    //String query = "INSERT INTO Usuario(UserName, Nombre, ApellidoPaterno, ApellidoMaterno, Email, Passwordd, FechaNacimiento, Sexo, Telefono, Celular, Estatus, CURP, Imagen) VALUES (@UserName, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Email, @Passwordd, @FechaNacimiento, @Sexo, @Telefono, @Celular, @Estatus, @CURP, @Imagen)";
                    String query = "UsuarioADD";
                    int binario = usuario.Estatus == true ? 1 : 0;

                    SqlCommand cmd = new SqlCommand(query, context);
                    //manera de implementar un insert con Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Passwordd", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", binario);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);


                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el registro";
                    }
                }
            }
            catch (Exception ex)
            {

                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    //String sql = "UPDATE Usuario SET UserName = @UserName, Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, Email = @Email, Passwordd = @Passwordd, FechaNacimiento = @FechaNacimiento, Sexo = @Sexo, Telefono = @Telefono, Celular = @Celular, Estatus = @Estatus, CURP = @CURP WHERE IdUsuario = @IdUsuario";
                    String query = "UsuarioUpdate";
                    int binario = usuario.Estatus == true ? 1 : 0;

                    SqlCommand cmd = new SqlCommand(query, context);
                    //manera de implementar un update con Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Passwordd", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", binario);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    //cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);

                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    //String sql = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario";
                    String query = "UsuarioDelete";

                    SqlCommand cmd = new SqlCommand(query, context);
                    //manera de implementar un update con Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el registro";
                    }
                }
            }
            catch (Exception ex)
            {

                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        //public static void Show(bool todosLosRegistros, int numero)
        //{
        //    try
        //    {
        //        using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
        //        {
        //            context.Open(); //abrir la conexion con la BD


        //            if (todosLosRegistros)
        //            {
        //                String sql = "SELECT * FROM Usuario";

        //                SqlCommand cmd = new SqlCommand(sql, context);
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    Console.Write(reader.GetName(i) + "\t");
        //                }
        //                Console.WriteLine();

        //                while (reader.Read())
        //                {
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        Console.Write(reader[i] + "\t");
        //                    }
        //                    Console.WriteLine();
        //                }
        //            }
        //            else
        //            {
        //                String sql = "SELECT * FROM Usuario WHERE NumeroControl = " + numero;

        //                SqlCommand cmd = new SqlCommand(sql, context);
        //                SqlDataReader reader = cmd.ExecuteReader();

        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    Console.Write(reader.GetName(i) + "\t");
        //                }
        //                Console.WriteLine();

        //                while (reader.Read())
        //                {
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        Console.Write(reader[i] + "\t");
        //                    }
        //                    Console.WriteLine();
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        Console.WriteLine("Error de conexion");
        //    }
        //}
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    //string query = "SELECT * FROM Usuario";

                    String query = "UsuarioGetAll";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        //si trae registros
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.UserName = row[1].ToString();
                            usuario.Nombre = row[2].ToString();
                            usuario.ApellidoPaterno = row[3].ToString();
                            usuario.ApellidoMaterno = row[4].ToString();
                            usuario.Email = row[5].ToString();
                            usuario.Password = row[6].ToString();
                            usuario.FechaNacimiento = row[7].ToString();
                            usuario.Sexo = row[8].ToString();
                            usuario.Telefono = row[9].ToString();
                            usuario.Celular = row[10].ToString();
                            usuario.Estatus = Convert.ToBoolean(row[11]);
                            usuario.CURP = row[12].ToString();

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;

                    }
                    else
                    {
                        //no hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay datos/registros";
                    }

                }

            }
            catch (Exception ex)
            {
                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    //string query = "SELECT * FROM Usuario WHERE NumeroControl = @idUsuario";
                    String query = "UsuarioGetById";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        //si trae registros
                        DataRow row = dataTable.Rows[0];
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                        usuario.UserName = row[1].ToString();
                        usuario.Nombre = row[2].ToString();
                        usuario.ApellidoPaterno = row[3].ToString();
                        usuario.ApellidoMaterno = row[4].ToString();
                        usuario.Email = row[5].ToString();
                        usuario.Password = row[6].ToString();
                        usuario.FechaNacimiento = row[7].ToString();
                        usuario.Sexo = row[8].ToString();
                        usuario.Telefono = row[9].ToString();
                        usuario.Celular = row[10].ToString();
                        usuario.Estatus = Convert.ToBoolean(row[11]);
                        usuario.CURP = row[12].ToString();

                        result.Object = usuario; //BOXING
                        result.Correct = true;

                    }
                    else
                    {
                        //no hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay datos/registros";
                    }

                }

            }
            catch (Exception ex)
            {
                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {
                    string fecha = usuario.FechaNacimiento;
                    string formato = "dd/MM/yyyy";
                    DateTime dateTime = DateTime.ParseExact(fecha, formato, CultureInfo.InvariantCulture);

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();

                    int rowsAffect = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, dateTime, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el registro";
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
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {
                    string fecha = usuario.FechaNacimiento;
                    string formato = "dd/MM/yyyy";
                    DateTime dateTime = DateTime.ParseExact(fecha, formato, CultureInfo.InvariantCulture);

                    int rowsAffect = context.UsuarioUpdate(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, dateTime, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Imagen, usuario.Rol.IdRol, usuario.IdUsuario, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar el registro";
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
        public static ML.Result DeleteEF(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {

                    int rowsAffect = context.UsuarioDelete(idUsuario);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar el registro";
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
        public static ML.Result GetAllEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {
                    result.Objects = new List<object>();

                    var listUsuario = context.UsuarioGetAll(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Rol.IdRol).ToList();
                    if (listUsuario != null)
                    {
                        foreach (var row in listUsuario)
                        {
                            ML.Usuario usuarioItem = new ML.Usuario();
                            usuarioItem.Rol = new ML.Rol();
                            usuarioItem.Direccion = new ML.Direccion();
                            usuarioItem.Direccion.Colonia = new ML.Colonia();
                            usuarioItem.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioItem.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                            usuarioItem.IdUsuario = row.IdUsuario;
                            usuarioItem.UserName = row.UserName;
                            usuarioItem.Nombre = row.NombreUsuario;
                            usuarioItem.ApellidoPaterno = row.ApellidoPaterno;
                            usuarioItem.ApellidoMaterno = row.ApellidoMaterno;
                            usuarioItem.Email = row.Email;
                            usuarioItem.Password = row.Passwordd;
                            usuarioItem.FechaNacimiento = row.FechaNacimiento;
                            usuarioItem.Sexo = row.Sexo;
                            usuarioItem.Telefono = row.Telefono;
                            usuarioItem.Celular = row.Celular;
                            usuarioItem.Estatus = row.Estatus;
                            usuarioItem.CURP = row.CURP;
                            usuarioItem.Imagen = row.Imagen; //array bytes

                          
                            usuarioItem.ImagenBase64 = Convert.ToBase64String(row.Imagen ?? new byte[0]);
                            usuarioItem.Rol.Nombre = row.NombreRol;
                            usuarioItem.Direccion.Calle = row.Calle;
                            usuarioItem.Direccion.NumeroExterior = row.NumeroExterior;
                            usuarioItem.Direccion.NumeroInterior = row.NumeroInterior;
                            usuarioItem.Direccion.Colonia.CodigoPostal = row.CodigoPostal;
                            if (row.IdColonia != null)
                            {
                                usuarioItem.Direccion.Colonia.IdColonia = row.IdColonia.Value;
                            }
                            else
                            {
                                usuarioItem.Direccion.Colonia.IdColonia = 0;
                            }
                            if (row.IdMunicipio != null)
                            {
                                usuarioItem.Direccion.Colonia.Municipio.IdMunicipio = row.IdMunicipio.Value;
                            }
                            else
                            {
                                usuarioItem.Direccion.Colonia.IdColonia = 0;
                            }
                            if (row.IdEstado != null)
                            {
                                usuarioItem.Direccion.Colonia.Municipio.Estado.IdEstado = row.IdEstado.Value;
                            }
                            else
                            {
                                usuarioItem.Direccion.Colonia.IdColonia = 0;
                            }

                            result.Objects.Add(usuarioItem);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
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
        public static ML.Result GetByIdEF(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new DL_EF.AAparicioProgramacionNCapasEntities())
                {
                    var listUsuario = context.UsuarioGetById(idUsuario).SingleOrDefault();
                    if (listUsuario != null)
                    {
                        var row = listUsuario;

                        ML.Usuario usuarioItem = new ML.Usuario();
                        usuarioItem.Rol = new ML.Rol();
                        usuarioItem.Direccion = new ML.Direccion();
                        usuarioItem.Direccion.Colonia = new ML.Colonia();
                        usuarioItem.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuarioItem.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuarioItem.IdUsuario = row.IdUsuario;
                        usuarioItem.UserName = row.UserName;
                        usuarioItem.Nombre = row.NombreUsuario;
                        usuarioItem.ApellidoPaterno = row.ApellidoPaterno;
                        usuarioItem.ApellidoMaterno = row.ApellidoMaterno;
                        usuarioItem.Email = row.Email;
                        usuarioItem.Password = row.Passwordd;
                        usuarioItem.FechaNacimiento = row.FechaNacimiento;
                        usuarioItem.Sexo = row.Sexo;
                        usuarioItem.Telefono = row.Telefono;
                        usuarioItem.Celular = row.Celular;
                        usuarioItem.Estatus = row.Estatus;
                        usuarioItem.CURP = row.CURP;
                        usuarioItem.Imagen = row.Imagen;
                        usuarioItem.ImagenBase64 = Convert.ToBase64String(row.Imagen ?? new byte[0]);
                        usuarioItem.Direccion.Calle = row.Calle;
                        usuarioItem.Direccion.NumeroExterior = row.NumeroExterior;
                        usuarioItem.Direccion.NumeroInterior = row.NumeroInterior;
                        if (row.IdRol != null)
                        {
                            usuarioItem.Rol.IdRol = row.IdRol.Value;
                        }
                        else
                        {
                            usuarioItem.Rol.IdRol = 0;
                        }
                        if (row.IdColonia != null)
                        {
                            usuarioItem.Direccion.Colonia.IdColonia = row.IdColonia.Value;
                        }
                        else
                        {
                            usuarioItem.Direccion.Colonia.IdColonia = 0;
                        }
                        if (row.IdMunicipio != null)
                        {
                            usuarioItem.Direccion.Colonia.Municipio.IdMunicipio = row.IdMunicipio.Value;
                        }
                        else
                        {
                            usuarioItem.Direccion.Colonia.IdColonia = 0;
                        }
                        if (row.IdEstado != null)
                        {
                            usuarioItem.Direccion.Colonia.Municipio.Estado.IdEstado = row.IdEstado.Value;
                        }
                        else
                        {
                            usuarioItem.Direccion.Colonia.IdColonia = 0;
                        }


                        result.Object = usuarioItem;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
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
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new AAparicioProgramacionNCapasEntities())
                {
                    string fecha = usuario.FechaNacimiento;
                    string formato = "dd/MM/yyyy";
                    DateTime dateTime = DateTime.ParseExact(fecha, formato, CultureInfo.InvariantCulture);

                    DL_EF.Usuario usuarioBD = new DL_EF.Usuario();
                    usuarioBD.UserName = usuario.UserName;
                    usuarioBD.Nombre = usuario.Nombre;
                    usuarioBD.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioBD.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioBD.Email = usuario.Email;
                    usuarioBD.Passwordd = usuario.Password;
                    usuarioBD.FechaNacimiento = dateTime;
                    usuarioBD.Sexo = usuario.Sexo;
                    usuarioBD.Telefono = usuario.Telefono;
                    usuarioBD.Celular = usuario.Celular;
                    usuarioBD.Estatus = usuario.Estatus;
                    usuarioBD.CURP = usuario.CURP;

                    context.Usuarios.Add(usuarioBD); //GENERAR EL INSERT --> INSERT INTO Usuario VALUES

                    int filasAfectadas = context.SaveChanges(); //Ejecutar el insert
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el registro";
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
        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new AAparicioProgramacionNCapasEntities())
                {
                    string fecha = usuario.FechaNacimiento;
                    string formato = "dd/MM/yyyy";
                    DateTime dateTime = DateTime.ParseExact(fecha, formato, CultureInfo.InvariantCulture);
                    var registroUsuario = (from usuarioUpdate in context.Usuarios
                                           where usuarioUpdate.IdUsuario == usuario.IdUsuario
                                           select usuarioUpdate).SingleOrDefault();


                    if (registroUsuario != null)
                    {
                        registroUsuario.UserName = usuario.UserName;
                        registroUsuario.Nombre = usuario.Nombre;
                        registroUsuario.ApellidoPaterno = usuario.ApellidoPaterno;
                        registroUsuario.ApellidoMaterno = usuario.ApellidoMaterno;
                        registroUsuario.Email = usuario.Email;
                        registroUsuario.Passwordd = usuario.Password;
                        registroUsuario.FechaNacimiento = dateTime;
                        registroUsuario.Sexo = usuario.Sexo;
                        registroUsuario.Telefono = usuario.Telefono;
                        registroUsuario.Celular = usuario.Celular;
                        registroUsuario.Estatus = usuario.Estatus;
                        registroUsuario.CURP = usuario.CURP;


                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al actualizar el registro";
                        }
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
        public static ML.Result DeleteLINQ(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new AAparicioProgramacionNCapasEntities())
                {
                    var registroElimiado = (from usuarioEliminado in context.Usuarios
                                            where usuarioEliminado.IdUsuario == IdUsuario
                                            select usuarioEliminado).SingleOrDefault();


                    if (registroElimiado != null)
                    {
                        context.Usuarios.Remove(registroElimiado);
                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al eliminar el registro";
                        }
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
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new AAparicioProgramacionNCapasEntities())
                {
                    var registroUsuario = (from usuario in context.Usuarios
                                           select usuario).ToList();

                    if (registroUsuario != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in registroUsuario)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = registro.IdUsuario;
                            usuario.UserName = registro.UserName;
                            usuario.Nombre = registro.Nombre;
                            usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            usuario.Email = registro.Email;
                            usuario.Password = registro.Passwordd;
                            usuario.FechaNacimiento = registro.FechaNacimiento.ToString();
                            usuario.Sexo = registro.Sexo;
                            usuario.Telefono = registro.Telefono;
                            usuario.Celular = registro.Celular;
                            usuario.Estatus = registro.Estatus;
                            usuario.CURP = registro.CURP;
                            result.Objects.Add(usuario);
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
        public static ML.Result GetByIdLINQ(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new AAparicioProgramacionNCapasEntities())
                {
                    var registroUsuario = (from usuario in context.Usuarios
                                           where usuario.IdUsuario == idUsuario
                                           select usuario).SingleOrDefault();

                    if (registroUsuario != null)
                    {


                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = registroUsuario.IdUsuario;
                        usuario.UserName = registroUsuario.UserName;
                        usuario.Nombre = registroUsuario.Nombre;
                        usuario.ApellidoPaterno = registroUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = registroUsuario.ApellidoMaterno;
                        usuario.Email = registroUsuario.Email;
                        usuario.Password = registroUsuario.Passwordd;
                        usuario.FechaNacimiento = registroUsuario.FechaNacimiento.ToString();
                        usuario.Sexo = registroUsuario.Sexo;
                        usuario.Telefono = registroUsuario.Telefono;
                        usuario.Celular = registroUsuario.Celular;
                        usuario.Estatus = registroUsuario.Estatus;
                        usuario.CURP = registroUsuario.CURP;

                        result.Object = usuario;

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
        public static ML.Result CambiarStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AAparicioProgramacionNCapasEntities context = new AAparicioProgramacionNCapasEntities())
                {
                    int rowsAffect = context.UsuarioCambiarStatus(IdUsuario, Status);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizo el registro";
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
        public static ML.Result LeerExcel(String cadenaConexion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(cadenaConexion))
                {
                    String query = "SELECT * FROM[Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand(query, context))
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable tablaUsuario = new DataTable();
                        adapter.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow registros in tablaUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Rol = new ML.Rol();

                                usuario.Nombre = registros[0].ToString();
                                usuario.ApellidoPaterno = registros[1].ToString();
                                usuario.ApellidoMaterno = registros[2].ToString();
                                usuario.Telefono = registros[3].ToString();
                                usuario.Email = registros[4].ToString();
                                usuario.Password = registros[5].ToString();
                                usuario.FechaNacimiento = registros[6].ToString();
                                usuario.Sexo = registros[7].ToString();
                                usuario.Celular = registros[8].ToString();
                                usuario.Estatus = Convert.ToBoolean(Convert.ToInt16(registros[9]));
                                usuario.CURP = registros[10].ToString();
                                usuario.Rol.IdRol = Convert.ToInt16(registros[11]);
                                usuario.UserName = registros[12].ToString();
                                usuario.Imagen = null;

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
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

        public static ML.Result ValidarExcel(List<object> registros)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            int contador = 1;
            //result.ErrorMessage = "";
            String error = "";

            foreach (ML.Usuario usuario in registros)
            {
               
            ML.ResultExcel resultExcel = new ML.ResultExcel();

                if (usuario.Nombre.Length > 50 || usuario.Nombre == "" || usuario.Nombre == null)
                {
                    error += "El nombre es muy largo o es vacio|";
                }

                if (usuario.ApellidoPaterno.Length > 50 || usuario.ApellidoPaterno == "" || usuario.ApellidoPaterno == null)
                {
                    error += "El Apellido Paterno es muy largo o es vacio|";
                }

                if (usuario.ApellidoMaterno.Length > 50 || usuario.ApellidoMaterno == "" || usuario.ApellidoMaterno == null)
                {
                    error += "El Apellido Paterno es muy largo o es vacio|";
                }

                if (usuario.Telefono.Length > 20 || usuario.Telefono == "" || usuario.Telefono == null)
                {
                    error += "El numero de telefono es muy largo o es vacio|";
                }

                if (usuario.Email.Length > 254 || usuario.Email == "" || usuario.Email == null)
                {
                    error += "El email es muy largo o es vacio|";
                }

                if (usuario.Password.Length > 50 || usuario.Password == "" || usuario.Password == null)
                {
                    error += "La contraseña es muy larga o es vacia|";
                }

                if (usuario.FechaNacimiento == "" || usuario.FechaNacimiento == null)
                {
                    error += "La fecha de nacimiento es vacia|";
                }

                if (usuario.Sexo.Length > 2 || usuario.Sexo == "" || usuario.Sexo == null)
                {
                    error += "El sexo es muy largo o es vacio|";
                }

                if (usuario.Celular.Length > 20 || usuario.Celular == "" || usuario.Celular == null)
                {
                    error += "El numero celularl es muy largo o es vacio|";
                }

                if (usuario.Estatus == null)
                {
                    error += "El estatus es vacio|";
                }

                if (usuario.CURP.Length > 50 || usuario.CURP == "" || usuario.CURP == null)
                {
                    error += "La CURP es muy larga o es vacio|";
                }

                if (error != "")
                {
                    //Hubo un error
                    //result.ErrorMessage = error;
                    resultExcel.ErrorMessage = error;
                    resultExcel.NumeroRegistro = contador;
                    result.Objects.Add(resultExcel);
                    error = "";
                }
                contador++;
            }
            return result;
        }
    }
}

