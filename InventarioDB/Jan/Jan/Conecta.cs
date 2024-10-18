using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Jan
{
    public class Conecta
    {
        private static string connectionString = "Server=localhost\\SQLEXPRESS; Initial Catalog=GestionInventario; Integrated Security=True;";

        public SqlConnection Conectar()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar a la base de datos: " + ex.Message);
            }
        }
    }
}
