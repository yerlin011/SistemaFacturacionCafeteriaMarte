using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objetoCD = new CD_Cliente();

        /// <summary>
        /// Metodo. Retorna los datos obtenidos del metodo ListarClientes() de la capa de datos. 
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ListarClientes()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.ListarClientes();
            return tabla;
        }

        /// <summary>
        /// Metodo llama y pasa valores al metodo InsertarCliente() de la capa de datos
        /// </summary>
        /// <param name="nombreCompleto"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        public void InsertarCliente(string nombreCompleto, string direccion, string telefono)
        {
            objetoCD.InsertarCliente(nombreCompleto,direccion,telefono);
        }

        /// <summary>
        /// Metodo llama y pasa valores al metodo EditarCliente() de la capa de datos
        /// </summary>
        /// <param name="nombreCompleto"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        /// <param name="idCliente"></param>
        public void EditarCliente(string nombreCompleto, string direccion, string telefono, string idCliente)
        {
            objetoCD.EditarCliente(nombreCompleto, direccion, telefono, Convert.ToInt32(idCliente));
        }

        /// <summary>
        /// Metodo llama y pasa valores al metodo EliminarCliente() de la capa de datos
        /// </summary>
        /// <param name="idCliente"></param>
        public void EliminarCliente(string idCliente)
        {

            objetoCD.EliminarCliente(Convert.ToInt32(idCliente));
        }

        /// <summary>
        /// Metodo retorna un determinado el metodo BuscarCliente() de la capa de datos
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>DataTable</returns>
        public DataTable BuscarCliente(string texto)
        {
           
            return objetoCD.BuscarCliente(texto);
        }


        /// <summary>
        /// Metodo llama y pasa valores al metodo DeshabilitarCliente() de la capa de datos
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="estado"></param>
        public void DeshabilitarCliente(string idCliente, string estado)
        {
            objetoCD.DeshabilitarCliente(Convert.ToInt32(idCliente), Convert.ToBoolean(estado));
        }

    }
}
