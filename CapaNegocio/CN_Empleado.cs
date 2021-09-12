using System;
using CapaDatos;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class CN_Empleado
    {
        CD_Empleado objdato = new CD_Empleado();

        String _Usuario;
        String _Contraseña;

        public String Usuario
        {
            set { _Usuario = value; }
            get { return _Usuario; }
        }

        public String Contraseña
        {
            set { _Contraseña = value; }
            get { return _Contraseña; }
        }

        public CN_Empleado() { }

        /// <summary>
        /// Metodo llama al otro metodo IniciarSesion() de la capa de datos
        /// </summary>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader IniciarSesion()
        {
            SqlDataReader Loguear;
            Loguear = objdato.IniciarSesion(Usuario, Contraseña);

            return Loguear;
        }

    }
}
