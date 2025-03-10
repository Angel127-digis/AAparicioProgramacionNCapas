using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static void Add(ML.Materia materia)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    String sql = "INSERT INTO Materia(Id, Nombre, Creditos, Calificacion, Evaluacion) VALUES (@Id, @Nombre, @Creditos, @Calificacion, @Evaluacion)";

                    SqlCommand cmd = new SqlCommand(sql, context);
                    cmd.Parameters.AddWithValue("@Id", materia.Id);
                    cmd.Parameters.AddWithValue("@Nombre", materia.Nombre);
                    cmd.Parameters.AddWithValue("@Creditos", materia.Creditos);
                    cmd.Parameters.AddWithValue("@Calificacion", materia.Calificacion);
                    cmd.Parameters.AddWithValue("@Evaluacion", materia.Evaluacion);

                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0) {
                        Console.WriteLine("El registro se inserto correctamente");
                    } else {
                        Console.WriteLine("Error al insertar");
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error de conexion");
            }
        }
        public static void update(ML.Materia materia)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    String sql = "UPDATE Materia SET Nombre='@Nombre', Creditos='@Creditos', Calificacion='@Calificacion', Evaluacion='@Evaluacion' WHERE Id = '@Id';";

                    SqlCommand cmd = new SqlCommand(sql, context);
                    cmd.Parameters.AddWithValue("@Id", materia.Id);
                    cmd.Parameters.AddWithValue("@Nombre", materia.Nombre);
                    cmd.Parameters.AddWithValue("@Creditos", materia.Creditos);
                    cmd.Parameters.AddWithValue("@Calificacion", materia.Calificacion);
                    cmd.Parameters.AddWithValue("@Evaluacion", materia.Evaluacion);

                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("El registro se inserto correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al insertar");
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error de conexion");
            }
        }
        public static void delete(int idMateria)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    String sql = "DELETE FROM Materia WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(sql, context);
                    cmd.Parameters.AddWithValue("@Id", idMateria);

                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("El registro se inserto correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al insertar");
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error de conexion");
            }
        }
    }
}
