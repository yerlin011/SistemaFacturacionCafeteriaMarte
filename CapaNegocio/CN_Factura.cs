using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
   public class CN_Factura
    {
        private CD_Factura objetoCD = new CD_Factura();

        /// <summary>
        /// Metodo. Retorna los datos obtenidos del metodo ListarFacturas() de la capa de datos. 
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ListarFacturas()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.ListarFacturas();
            return tabla;
        }


        /// <summary>
        /// Metodo. Retorna los datos obtenidos del metodo ObtenerUltimoIdFacturaRegistro() de la capa de datos. 
        /// </summary>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ObtenerUltimoIdFacturaRegistro()
        {
            SqlDataReader datos;
            datos = objetoCD.ObtenerUltimoIdFacturaRegistro();

            return datos;
        }


        /// <summary>
        /// Metodo llama y pasa valores al metodo InsertarFactura() de la capa de datos
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="codResponsable"></param>
        /// <param name="codCliente"></param>
        /// <param name="total"></param>
        /// <param name="efectivo"></param>
        /// <param name="devuelta"></param>
        public void InsertarFactura(string fecha, string codResponsable, string codCliente, string total, string efectivo, string devuelta)
        {
            objetoCD.InsertarFactura(Convert.ToDateTime(fecha), Convert.ToInt32(codResponsable), Convert.ToInt32(codCliente),float.Parse(total), float.Parse(efectivo), float.Parse(devuelta));
        }

        /// <summary>
        /// Metodo llama y pasa valores al metodo EditarFactura() de la capa de datos
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="codResponsable"></param>
        /// <param name="codCliente"></param>
        /// <param name="idCliente"></param>
        public void EditarFactura(string fecha, string codResponsable, string codCliente, string idFactura)
        {
            objetoCD.EditarFactura(Convert.ToDateTime(fecha), Convert.ToInt32(codResponsable), Convert.ToInt32(codCliente), Convert.ToInt32(idFactura));
        }
        /// <summary>
        /// Metodo llama y pasa valores al metodo BuscarFactura() de la capa de datos
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>DataTable</returns>
        public DataTable BuscarFactura(string texto)
        {
            return objetoCD.BuscarFactura(texto);
        }
        /// <summary>
        /// Metodo llama y pasa valores al metodo DeshabilitarFactura() de la capa de datos
        /// </summary>
        /// <param name="idFactura"></param>
        /// <param name="estadoFactura"></param>
        public void DeshabilitarFactura(string idFactura, string estadoFactura)
        {
            objetoCD.DeshabilitarFactura(Convert.ToInt32(idFactura),Convert.ToBoolean(estadoFactura));
        }
    }
}
