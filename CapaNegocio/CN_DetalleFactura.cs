using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
   public class CN_DetalleFactura
    {

        private CD_DetalleFactura objetoCD = new CD_DetalleFactura();


        /// <summary>
        /// Metodo llama y pasa valores al metodo InsertarDetalleFactura() de la capa de datos
        /// </summary>
        /// <param name="codFactura"></param>
        /// <param name="codArticulo"></param>
        /// <param name="cantidad"></param>
        /// <param name="precioTotal"></param>
        /// <returns>Int</returns>
        public int InsertarDetalleFactura(string codFactura,string codArticulo,string cantidad, string precioTotal)
        {

           return objetoCD.InsertarDetalleFactura(Convert.ToInt32(codFactura),Convert.ToInt32(codArticulo), Convert.ToInt32(cantidad), float.Parse(precioTotal));
        }

    }
}
