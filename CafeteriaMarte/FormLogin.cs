using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaNegocio;
using System.Data.SqlClient;

namespace CapaPresentacion
{
    public partial class FormLogin : Form
    {
       

        public FormLogin()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                CN_Empleado objEmpleado = new CN_Empleado();
                

                objEmpleado.Usuario = txtUserName.Text;
                objEmpleado.Contraseña = txtpassword.Text;
                SqlDataReader logueado = objEmpleado.IniciarSesion();

                if (objEmpleado.Usuario.ToString().Length>0 && objEmpleado.Contraseña.ToString().Length > 0) {

                   if (logueado.Read() == true) {

                        FormCafeteriaMarte frmMenu = new FormCafeteriaMarte();
                        Program.Cargo = logueado["Cargo"].ToString();
                        Program.Nombres = logueado["Nombres"].ToString();
                        Program.Apellidos = logueado["Apellidos"].ToString();

                        frmMenu.Show();

                        this.Hide();

                    }else
                    {

                        MessageBox.Show("Usuario o Clave incorrectos, favor intentar nuevamente", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }else
                {

                    MessageBox.Show("Usuario y Clave Requeridos", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }





            }
            catch (Exception E)
            {

                MessageBox.Show("Error"+ E.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtpassword.Clear();
            txtUserName.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
