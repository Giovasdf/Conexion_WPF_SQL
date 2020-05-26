using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases
{
    public class EjecutarQuery
    {

        static string connectionString = @"Server = YouServerName; Database=Usuarios;Trusted_Connection=True";

        public static List<usuario> listaUsuarios;


        #region InsertSQL
        public static void insertSQL( string nombre,string apellido)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);

                // Create and prepare an SQL statement.
                command.CommandText = "INSERT INTO usuario (nombre, apellido) " + "VALUES (@nombre, @apellido)";             
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@apellido", apellido);

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                connection.Close();
            }
        }
        public static void insertSQL(string nombre)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);

                // Create and prepare an SQL statement.
                command.CommandText = "INSERT INTO usuario (nombre) " + "VALUES (@nombre)";
                command.Parameters.AddWithValue("@nombre", nombre);

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                connection.Close();
            }
        }
        #endregion InsertSQL

        #region SelectSQL
        //se obtiene 1 usuario utilizando el id
        public static List<usuario> selectSQL(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);
                DataTable dt = new DataTable();
                // Create and prepare an SQL statement.
                command.CommandText = $"select * from usuario where id_usuario = {id}";

                try
                {
                    connection.Open();
                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);
                    usuario _usuario = new usuario();
                    listaUsuarios = new List<usuario>();
                    _usuario.id_usuario = int.Parse((dt.Rows[0]["id_usuario"].ToString()));
                    _usuario.nombre = dt.Rows[0]["nombre"].ToString();
                    _usuario.apellido = dt.Rows[0]["apellido"].ToString();
                    listaUsuarios.Add(_usuario);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                connection.Close();
            }
            return listaUsuarios;
        }

        //se obtienen todos los usuarios en la BD
        public static List<usuario> selectAllUsuariosSQL()
        {
            var dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //command nos prepara la query a ejecutar
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "select * from usuario ";

                connection.Open();
                var DataAdapter = new SqlDataAdapter(command);
                DataAdapter.Fill(dt);
                listaUsuarios = new List<usuario>();

                foreach (DataRow row in dt.Rows)
                {
                    usuario usuarioTemp = new usuario();
                    usuarioTemp.id_usuario = int.Parse(row["id_usuario"].ToString());
                    usuarioTemp.nombre = row["nombre"].ToString();
                    usuarioTemp.apellido = row["apellido"].ToString();
                    listaUsuarios.Add(usuarioTemp);
                }



            }
            return listaUsuarios;
        }
        #endregion SelectSQL

    }
}
