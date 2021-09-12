using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Cliente
    {
        private CD_Conexion conexion = new CD_Conexion();
        private SqlDataReader leer;
        private DataTable tabla = new DataTable();
        private SqlCommand comando = new SqlCommand();

        /// <summary>
        /// Metodo. Obtiene la lista de todos los clientes registrados
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ListarClientes()
        {

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = "SpListarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);

            conexion.CerrarConexion();
            return tabla;

        }


        /// <summary>
        /// Metodo. permite insertar una nueva cliente
        /// </summary>
        /// <param name="nombreCompleto"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        public void InsertarCliente(string nombreCompleto, string direccion, string telefono, bool estado=true)
        {


            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpInsertarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
            comando.Parameters.AddWithValue("@Direccion", direccion);
            comando.Parameters.AddWithValue("@Telefono", telefono);
            comando.Parameters.AddWithValue("@EstadoCliente", estado);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }


        /// <summary>
        /// Metodo. permite editar un cliente
        /// </summary>
        /// <param name="nombreCliente"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        /// <param name="idCliente"></param>
        public void EditarCliente(string nombreCliente, string direccion, string telefono,int idCliente)
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpEditarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NombreCompleto", nombreCliente);
            comando.Parameters.AddWithValue("@Direccion", direccion);
            comando.Parameters.AddWithValue("@Telefono", telefono);
            comando.Parameters.AddWithValue("@IdCliente", idCliente);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }

         /// <summary>
        /// Metodo permite eliminar un cliente por el Id
        /// </summary>
        /// <param name="idCliente"></param>
        public void EliminarCliente(int idCliente)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpEliminarCliente";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IdCliente", idCliente);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
        /// <summary>
        /// Buscar un determinado nombre completo  de un cliente
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public DataTable BuscarCliente(string texto)
        {
            DataTable dataEncontrada = new DataTable();
            SqlDataReader Leerfilas;
            string filtroInactivo = texto;
            string sql = "";


            if (filtroInactivo.Contains("Activo"))
            {
                sql = "SELECT * FROM CLIENTE WHERE EstadoCliente = 1";
            }
            else if (filtroInactivo.Contains("Inactivo"))
            {
                sql = "SELECT * FROM CLIENTE WHERE EstadoCliente = 0";
            }
            else
            {
                sql = "SELECT * FROM CLIENTE WHERE NombreCompleto LIKE('" + texto + "%') AND EstadoCliente=1";

            }

          
            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = sql; 
            Leerfilas = comando.ExecuteReader();
            dataEncontrada.Load(Leerfilas);


            conexion.CerrarConexion();
            return dataEncontrada;
        }

        /// <summary>
        /// Metodo permite colocar un articulo en estado false
        /// </summary>
        /// <param name="idArticulo"></param>
        /// <param name="estado"></param>
        public void DeshabilitarCliente(int idCliente, bool estado)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpCambiarEstadoCliente";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IdCliente", idCliente);
            comando.Parameters.AddWithValue("@EstadoCliente", estado);


            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
    }
}
