using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public class CD_DetalleFactura
    {

        private CD_Conexion conexion = new CD_Conexion();
        private SqlCommand comando = new SqlCommand();


        /// <summary>
        /// Metodo. permite insertar los detalles de factura
        /// </summary>
        /// <param name="codFactura"></param>
        /// <param name="codArticulo"></param>
        /// <param name="cantidad"></param>
        /// <param name="precioTotal"></param>
        /// <returns>Int</returns>
        public int InsertarDetalleFactura(int codFactura, int codArticulo, int cantidad, float precioTotal)
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SpInsertarDetalleFactura";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@CodFactura", codFactura);
            comando.Parameters.AddWithValue("@CodArticulo", codArticulo);
            comando.Parameters.AddWithValue("@Cantidad", cantidad);
            comando.Parameters.AddWithValue("@PrecioTotal", precioTotal);

            int cantidadFila = comando.ExecuteNonQuery();

            comando.Parameters.Clear();

            return cantidadFila;

        }

    }
}
