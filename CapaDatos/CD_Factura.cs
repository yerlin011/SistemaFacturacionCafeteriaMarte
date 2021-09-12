using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
  public class CD_Factura
    {

        private CD_Conexion conexion = new CD_Conexion();
        private SqlDataReader leer;
        private DataTable tabla = new DataTable();
        private SqlCommand comando = new SqlCommand();

        /// <summary>
        /// Metodo. Obtiene la lista de todas las facturas registradas
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ListarFacturas()
        {

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = "SpListarFactura";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);

            conexion.CerrarConexion();
            return tabla;

        }

        /// <summary>
        /// Metodo. Obtiene el ultimo idFactura de las facturas registradas
        /// </summary>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ObtenerUltimoIdFacturaRegistro()
        {


            SqlCommand comando = new SqlCommand("SpObtenerIdUltimoRegistro", conexion.AbrirConexion());
            comando.CommandType = CommandType.StoredProcedure;


            leer = comando.ExecuteReader();

            return leer;

        }



        /// <summary>
        /// Metodo. permite insertar una nueva factura
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="codResponsable"></param>
        /// <param name="codCliente"></param>
        /// <param name="total"></param>
        /// <param name="efectivo"></param>
        /// <param name="devuelta"></param>
        /// <param name="estadoFactura"></param>
        public void InsertarFactura(DateTime fecha, int codResponsable, int codCliente, float total, float efectivo, float devuelta, bool estadoFactura=true) {


            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpInsertarFacturas";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Fecha", fecha);
            comando.Parameters.AddWithValue("@CodResponsable", codResponsable);
            comando.Parameters.AddWithValue("@CodCliente", codCliente);
            comando.Parameters.AddWithValue("@Total", total);
            comando.Parameters.AddWithValue("@Efectivo", efectivo);
            comando.Parameters.AddWithValue("@Devuelta", devuelta);
            comando.Parameters.AddWithValue("@EstadoFactura", estadoFactura);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }

        /// <summary>
        /// Metodo. permite editar una factura
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="codResponsable"></param>
        /// <param name="codCliente"></param>
        /// <param name="IdFactura"></param>
        public void EditarFactura(DateTime fecha, int codResponsable, int codCliente, int IdFactura)
        {


            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpEditarFactura";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Fecha", fecha);
            comando.Parameters.AddWithValue("@CodResponsable", codResponsable);
            comando.Parameters.AddWithValue("@CodCliente", codCliente);
            comando.Parameters.AddWithValue("@IdFactura", IdFactura);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

        }
        /// <summary>
        /// Metodo permite buscar un factura por nombre cliente o vendedor
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public DataTable BuscarFactura(string texto)
        {

            DataTable dataEncontrada = new DataTable();
            SqlDataReader Leerfilas;

            string filtroInactivo = texto;
            string sql = "";


            if (filtroInactivo.Contains("Activo") || filtroInactivo.Equals("activo") 
                || filtroInactivo.Contains("pagado") || filtroInactivo.Contains("Pagado"))
            {
                sql = "SELECT F.IdFactura, F.Fecha, U.Nombres AS Vendedor,C.NombreCompleto AS NombreCliente,F.Total, F.Efectivo, F.Devuelta, F.EstadoFactura FROM FACTURA AS F INNER JOIN USUARIOS AS U ON U.Id = F.CodResponsable INNER JOIN CLIENTE AS C ON C.IdCliente = F.CodCliente WHERE EstadoFactura = 1";
            }
            else if (filtroInactivo.Contains("Inactivo") || filtroInactivo.Contains("inactivo") 
                || filtroInactivo.Contains("reversado") || filtroInactivo.Contains("Reversado"))
            {
                sql = "SELECT F.IdFactura, F.Fecha, U.Nombres AS Vendedor,C.NombreCompleto AS NombreCliente,F.Total, F.Efectivo, F.Devuelta, F.EstadoFactura FROM FACTURA AS F INNER JOIN USUARIOS AS U ON U.Id = F.CodResponsable INNER JOIN CLIENTE AS C ON C.IdCliente = F.CodCliente WHERE EstadoFactura = 0";
            }
            else
            {
                sql = "SELECT F.IdFactura, F.Fecha, U.Nombres AS Vendedor,C.NombreCompleto AS NombreCliente,F.Total, F.Efectivo, F.Devuelta, F.EstadoFactura FROM FACTURA AS F INNER JOIN USUARIOS AS U ON U.Id = F.CodResponsable INNER JOIN CLIENTE AS C ON C.IdCliente = F.CodCliente WHERE U.Nombres LIKE ('" + texto + "%') OR C.NombreCompleto LIKE ('" + texto + "%') AND EstadoFactura = 1";

            }

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText =sql;
            Leerfilas = comando.ExecuteReader();
            dataEncontrada.Load(Leerfilas);


            return dataEncontrada;
            
        }


        /// <summary>
        /// Metodo permite colocar una factura en estado false
        /// </summary>
        /// <param name="idFactura"></param>
        /// <param name="estadoFactura"></param>
        public void DeshabilitarFactura(int idFactura, bool estadoFactura)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpCambiarEstadoFactura";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IdFactura", idFactura);
            comando.Parameters.AddWithValue("@EstadoFactura", estadoFactura);

            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
        }

    }
}
