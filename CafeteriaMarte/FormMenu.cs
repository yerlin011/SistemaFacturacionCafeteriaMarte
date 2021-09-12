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


namespace CapaPresentacion
{
    public partial class FormCafeteriaMarte : Form
    {

        private int posX = 0;
        private int posY = 0;

        public FormCafeteriaMarte()
        {
            InitializeComponent();
        }


        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FormCafeteriaMarte_Load(object sender, EventArgs e)
        {
            if (Program.Cargo != "Administrador" && Program.Cargo != "Admin")
            {
                btnUsuarios.Enabled = false;
            }
        }
        private void PrivilegiosUsuarios()
        {
            
        }
         
    private void FormCafeteriaMarte_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBoxIconoCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Metodo permite mover con el mouse un panel
        /// </summary>
        /// <param name="evento"></param>

        private void MoverPanel(MouseEventArgs evento)
        {


            if (evento.Button != MouseButtons.Left)
            {
                posX = evento.X;
                posY = evento.Y;

            }
            else
            {
                Left = Left + (evento.X - posX);
                Top = Top + (evento.Y - posY);
            }
        }

        private void panelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            MoverPanel(e);

        }

        private void panelLogo_MouseMove(object sender, MouseEventArgs e)
        {
            MoverPanel(e);
        }

        private void panelContenedor_MouseMove(object sender, MouseEventArgs e)
        {
            MoverPanel(e);
        }

        private void panelSideMenu_MouseMove(object sender, MouseEventArgs e)
        {
            MoverPanel(e);
        }

        private void pictureBoxMinimizar_Click(object sender, EventArgs e)
        {
           this.WindowState = FormWindowState.Minimized;
        }

        private void labelTituloLogo_Click(object sender, EventArgs e)
        {

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            panelSideMenu.Height = btnUsuarios.Height;
            panelSideMenu.Top = btnUsuarios.Top;
            AbrirFrmHija(new FormUsuario());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frmHija"></param>
        private void AbrirFrmHija(object frmHija)
        {
            if (this.panelContenedor.Controls.Count > 0)
            
            this.panelContenedor.Controls.RemoveAt(0);
            Form fr = frmHija as Form;
            fr.TopLevel = false;
            fr.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fr);
            this.panelContenedor.Tag = fr;
            fr.Show();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult resul = MessageBox.Show("¿Esta seguro que desea cerrar sesión?", "Mensage de Confirmacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resul == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
                FormLogin frmL = new FormLogin();
                frmL.Show();
            }
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            panelSideMenu.Height = btnFacturacion.Height;
            panelSideMenu.Top = btnFacturacion.Top;
            AbrirFrmHija(new FormFactura());
        }

        private void btnArticulos_Click(object sender, EventArgs e)
        {
            panelSideMenu.Height = btnArticulos.Height;
            panelSideMenu.Top = btnArticulos.Top;
            AbrirFrmHija(new FormArticulos());
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            panelSideMenu.Height = btnClientes.Height;
            panelSideMenu.Top = btnClientes.Top;
            AbrirFrmHija(new FormClientes());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resul = MessageBox.Show("¿Esta seguro que desea cerrar sesión?", "Mensage de Confirmacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resul == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
                FormLogin frmL = new FormLogin();
                frmL.Show();
            }
        }
    }
}
