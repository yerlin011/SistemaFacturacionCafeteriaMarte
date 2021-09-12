using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace CapaDatos
{
    public class CD_Empleado
    {
        private CD_Conexion conexion = new CD_Conexion();
        private SqlDataReader leer;
        private SqlCommand Comando = new SqlCommand();

        /// <summary>
        /// Metodo permite iniciar sesion, siempre que coincidan los registros de la tabla empleados
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader IniciarSesion(string user, string pass)
        {
            SqlCommand comando = new SqlCommand("SpLogin", conexion.AbrirConexion());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Usuario", user);
            comando.Parameters.AddWithValue("@Password",pass);

            leer = comando.ExecuteReader();

            return leer;
        }
    }
}
