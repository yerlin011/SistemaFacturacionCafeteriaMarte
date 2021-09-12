using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
  public class CN_Articulo
    {

        CD_Articulo objCN = new CD_Articulo();

     
   
        public int IdArticulo { get; set; }
        public SqlDataReader ObtenerDatosArticuloEspefico()
        {
            SqlDataReader datos;
            datos = objCN.obtenerDatosArticulo(IdArticulo);

            return datos;
        }

        public DataTable ListarArticulos()
        {
            return objCN.LeerNombresArticulos();
        }
        /// <summary>
        /// Metodo llama y pasa valores al metodo InsertarArticulo() de la capa de datos
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="precio"></param>
        /// <param name="stock"></param>
        /// <param name="estado"></param>
        public void InsertarArticulo(string descripcion, string precio, string stock, string estado)
        {
            objCN.InsertarArticulo(descripcion,float.Parse(precio),Convert.ToInt32(stock),Convert.ToBoolean(estado));
        }

        /// <summary>
        /// Metodo llama y pasa valores al metodo EditarArticulo() de la capa de datos
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="precio"></param>
        /// <param name="stock"></param>
        /// <param name="estado"></param>
        /// <param name="idArticulo"></param>
        public void EditarArticulo(string descripcion, string precio, string stock, string estado, string idArticulo)
        {
            objCN.EditarArticulo(descripcion, float.Parse(precio), Convert.ToInt32(stock), Convert.ToBoolean(estado), Convert.ToInt32(idArticulo));
        }

        /// <summary>
        /// Metodo llama y pasa valores al metodo EliminarArticulo() de la capa de datos
        /// </summary>
        /// <param name="idArticulo"></param>
        public void EliminarArticulo(string idArticulo)
        {
            objCN.EliminarArticulo(Convert.ToInt32(idArticulo));
        }

        /// <summary>
        /// Metodo llama y pasa valores al metodo BuscarArticulo() de la capa de datos
        /// </summary>
        /// <param name="buscarTexto"></param>
        /// <returns>DataTable</returns>

        public DataTable BuscarArticulo(string buscarTexto)
        {
            return objCN.BuscarArticulo(buscarTexto);

        }
        /// <summary>
        /// Metodo llama y pasa valores al metodo DeshabilitarArticulo() de la capa de datos
        /// </summary>
        /// <param name="idArticulo"></param>
        /// <param name="estado"></param>
        public void DeshabilitarArticulo(string idArticulo,string estado)
        {
            objCN.DeshabilitarArticulo(Convert.ToInt32(idArticulo), Convert.ToBoolean(estado));
        }
    }
}
