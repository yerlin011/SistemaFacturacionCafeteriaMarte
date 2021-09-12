using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FormMantCliente : Form
    {
        CN_Cliente objetoCN = new CN_Cliente();
        public string idCliente = null;
        public string Operacion = "";

        public FormMantCliente()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.nombreCompleto.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese Nombre", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.nombreCompleto.Select();
                return;
            }
            if (this.direccion.Text.Trim().Length == 0)
            {
                MessageBox.Show("Ingrese Direccion", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.direccion.Select();
                return;
            }
            if (this.telefonoMasked.Text  == "    -")
            {
                MessageBox.Show("Ingrese Telefono", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.telefonoMasked.Select();
                return;
            }

            //INSERTAR
            if (Operacion == "Insertar")
            {
                try
                {
                    objetoCN.InsertarCliente(nombreCompleto.Text,direccion.Text,telefonoMasked.Text);
                    MessageBox.Show("Cliente Registrado Correctamente", "Mensaje Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar los datos por: " + ex);
                }
            }
            else if (Operacion == "Editar")
            {

                try
                {
                    objetoCN.EditarCliente(nombreCompleto.Text, direccion.Text, telefonoMasked.Text, idCliente);
                    MessageBox.Show("Se edito correctamente", "Mensaje Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo editar los datos por: " + ex);
                }
            }

        }

        private void BtnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
