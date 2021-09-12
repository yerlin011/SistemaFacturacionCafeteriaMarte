using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Articulo
    {

        //private CD_Conexion con = new CD_Conexion();
        private CD_Conexion conexion = new CD_Conexion();
        private SqlDataReader registro;

        private DataTable tabla = new DataTable();
        private SqlCommand comando = new SqlCommand();

        /// <summary>
        /// Metodo retorna los nombres de los articulos
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable LeerNombresArticulos()
        {
          
                comando.Connection = conexion.AbrirConexion();

                comando.CommandText = "SpListarArticulos";
                comando.CommandType = CommandType.StoredProcedure;
                registro = comando.ExecuteReader();
                tabla.Load(registro);

                conexion.CerrarConexion();
            
            return tabla;
          }
     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idArticulo"></param>
        /// <returns></returns>
        public SqlDataReader obtenerDatosArticulo(int idArticulo)
        {
            SqlCommand comando = new SqlCommand("SpObtenerDatosArticulo", conexion.AbrirConexion());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdArticulo", idArticulo);


            registro = comando.ExecuteReader();

            return registro;
        }


        /// <summary>
        /// Metodo. permite insertar un nuevo articulo
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="precio"></param>
        /// <param name="stock"></param>
        /// <param name="estado"></param>
        public void InsertarArticulo(string descripcion, float precio, int stock, bool estado)
        {
          

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpInsertarArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Descripcion", descripcion);
            comando.Parameters.AddWithValue("@Precio", precio);
            comando.Parameters.AddWithValue("@Stock", stock);
            comando.Parameters.AddWithValue("@Estado", estado);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }

        /// <summary>
        /// Metodo. permite editar un articulo
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="precio"></param>
        /// <param name="stock"></param>
        /// <param name="estado"></param>
        /// <param name="idArticulo"></param>
        public void EditarArticulo(string descripcion, float precio, int stock, bool estado, 
            int idArticulo)
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpEditarArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Descripcion", descripcion);
            comando.Parameters.AddWithValue("@Precio", precio);
            comando.Parameters.AddWithValue("@Stock", stock);
            comando.Parameters.AddWithValue("@Estado", estado);

            comando.Parameters.AddWithValue("@IdArticulo", idArticulo);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }

        /// <summary>
        /// Metodo permite eliminar un registro por el Id
        /// </summary>
        /// <param name="idArticulo"></param>
        public void EliminarArticulo(int idArticulo)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpEliminarArticulo";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IdArticulo", idArticulo);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }

        /// <summary>
        /// Metodo. Obtiene un determinado registro, segun un criterio de busqueda
        /// </summary>
        /// <param name="textoBuscar"></param>
        /// <returns>DataTable</returns>

        public DataTable BuscarArticulo(string textoBuscar)
        {
            string filtroInactivo = textoBuscar;
            string sql="";
          

            if (filtroInactivo.Contains("Activo"))
            {
                sql = "SELECT * FROM ARTICULO WHERE Estado = 1";
            }
            else if (filtroInactivo.Contains("Inactivo"))
            {
                sql = "SELECT * FROM ARTICULO WHERE Estado = 0";
            }
            else
            {
                sql = "SELECT * FROM ARTICULO WHERE Descripcion LIKE('" + textoBuscar + "%') AND Estado=1";

            }

            DataTable dataEncontrada = new DataTable();
            SqlDataReader Leerfilas;
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
        public void DeshabilitarArticulo(int idArticulo, bool estado)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpCambiarEstadoArticulo";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IdArticulo", idArticulo);
            comando.Parameters.AddWithValue("@Estado", estado);


            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }
    }
}
