using System;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{  public class CD_Conexion
    {
        public SqlConnection Conexion = new SqlConnection(@"Data Source = WINDOWS-C86J0N0\SQLEXPRESS;Initial Catalog =DBCAFETERIA; Integrated Security = True");

        /// <summary>
        /// Metodo abre la conexion con la base de datos, en caso de que la conexion este cerrada.
        /// </summary>
        /// <returns>SqlConnection</returns>
        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        /// <summary>
        /// Metodo cierra la conexion con la base de datos, en caso de que la conexion este abierta.
        /// </summary>
        /// <returns>SqlConnection</returns>
        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }
}
