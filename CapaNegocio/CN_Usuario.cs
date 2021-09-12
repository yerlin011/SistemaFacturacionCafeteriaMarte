using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
   public class CN_Usuario{

       private CD_Usuario objetoCD = new CD_Usuario();

        /// <summary>
        /// Metodo. Retorna los datos obtenidos del metodo ListarUsuarios() de la capa de datos. 
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ListarUsuarios()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.ListarUsuarios();
            return tabla;
        }
        /// <summary>
        /// Metodo. Permite insertar, al pasar los valores como parametros en el metodo Insertar() de la capa de datos. 
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="cargo"></param>
        /// <param name="email"></param>
        /// <param name="contraseña"></param>
        public void InsertarUsuarios(string dni, string nombre, string apellido, string cargo, string email, string contraseña)
        {

            objetoCD.Insertar(dni, nombre, apellido, cargo, email, contraseña);
        }
        /// <summary>
        /// Metodo. Permite editar, al pasar los valores como parametros en el metodo Editar() de la capa de datos. 
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="cargo"></param>
        /// <param name="email"></param>
        /// <param name="contraseña"></param>
        /// <param name="idUsuario"></param>
        public void EditarUsuarios(string dni, string nombre, string apellido, string cargo, string email, string contraseña, string idUsuario)
        {
            objetoCD.Editar(dni, nombre, apellido, cargo, email, contraseña, Convert.ToInt32(idUsuario));
        }

        /// <summary>
        /// Metodo elimina un registro, al pasar un parametro a el metodo Eliminar() de la capa de datos
        /// </summary>
        /// <param name="idUsuario"></param>
        public void EliminarUsuarios(string idUsuario)
        {

            objetoCD.Eliminar(Convert.ToInt32(idUsuario));
        }
        /// <summary>
        /// Metodo. Permite buscar un determinado registro, al pasar un valor como parametro en el metodo Buscar() de la capa de datos.
        /// </summary>
        /// <param name="buscar"></param>
        /// <returns>DataTable</returns>

        public DataTable BuscarUsuarios(string buscar)
        {

            DataTable dt = new DataTable();
            dt = objetoCD.Buscar(buscar);
            return dt;
        }

        /// <summary>
        /// Metodo llama y pasa valores al metodo DeshabilitarUsuario() de la capa de datos
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="estado"></param>
        public void DeshabilitarUsuario(string idUsuario, string estado)
        {
            objetoCD.Deshabilitar(Convert.ToInt32(idUsuario), Convert.ToBoolean(estado));
        }
    }
}
