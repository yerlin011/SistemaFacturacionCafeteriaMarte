using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public class CD_Usuario {

        private CD_Conexion conexion = new CD_Conexion();
        private SqlDataReader leer;
        private DataTable tabla = new DataTable();
        private SqlCommand comando = new SqlCommand();

        /// <summary>
        /// Metodo. Obtiene la lista de todos los usuarios registrados
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ListarUsuarios()
        {

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = "SpListarUsuarios";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);

            conexion.CerrarConexion();
            return tabla;

        }

        /// <summary>
        /// Metodo. permite insertar un nuevo registro en la tabla USUARIOS
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="cargo"></param>
        /// <param name="email"></param>
        /// <param name="contraseña"></param>

        public void Insertar(string dni, string nombre, string apellido, string cargo, string email, string contraseña, bool estado=true)
        {
         

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpInsertarUsuarios";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Dni", dni);
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Apellido", apellido);
            comando.Parameters.AddWithValue("@Cargo", cargo);
            comando.Parameters.AddWithValue("@Email", email);
            comando.Parameters.AddWithValue("@Contraseña", contraseña);
            comando.Parameters.AddWithValue("@EstadoUsuario", estado);


            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }
        /// <summary>
        /// Metodo. permite editar un registro en la tabla USUARIOS
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="cargo"></param>
        /// <param name="email"></param>
        /// <param name="contraseña"></param>
        /// <param name="idUsuario"></param>
        public void Editar(string dni, string nombre, string apellido, string cargo, string email, string contraseña, int idUsuario)
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpEditarUsuarios";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Dni", dni);
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Apellido", apellido);
            comando.Parameters.AddWithValue("@Cargo", cargo);
            comando.Parameters.AddWithValue("@Email", email);
            comando.Parameters.AddWithValue("@Contraseña", contraseña);
            comando.Parameters.AddWithValue("@Id", idUsuario);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }

        /// <summary>
        /// Metodo permite eliminar un registro por el Id
        /// </summary>
        /// <param name="idUsuario"></param>
        public void Eliminar(int idUsuario)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpEliminarUsuarios";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", idUsuario);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }

        /// <summary>
        /// Metodo. Obtiene un determinado registro, segun un criterio de busqueda
        /// </summary>
        /// <param name="buscar"></param>
        /// <returns>DataTable</returns>

        public DataTable Buscar(string buscar)
        {
            DataTable dataEncontrada = new DataTable();
            SqlDataReader Leerfilas;
            string filtroInactivo = buscar;
            string sql = "";


            if (filtroInactivo.Contains("Activo"))
            {
                sql = "SELECT * FROM USUARIOS WHERE EstadoUsuario = 1";
            }
            else if (filtroInactivo.Contains("Inactivo"))
            {
                sql = "SELECT * FROM USUARIOS WHERE EstadoUsuario = 0";
            }
            else
            {
                sql = "SELECT * FROM USUARIOS WHERE Nombres LIKE('" + buscar + "%') AND EstadoUsuario=1";

            }
           
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = sql;
            Leerfilas = comando.ExecuteReader();
            dataEncontrada.Load(Leerfilas);


            conexion.CerrarConexion();
            return dataEncontrada;


        }

        /// <summary>
        /// Metodo permite colocar un usuario en estado false
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="estado"></param>
        public void Deshabilitar(int idUsuario, bool estado)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpCambiarEstadoUsuario";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", idUsuario);
            comando.Parameters.AddWithValue("@EstadoUsuario", estado);


            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
    }
}
