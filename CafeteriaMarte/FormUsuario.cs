using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FormUsuario : Form
    {
        private CN_Usuario objetoCN = new CN_Usuario();
        private string idUsuario = null;

        public FormUsuario()
        {
            InitializeComponent();
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {
            ListarDatosUsuarios();
        }

        /// <summary>
        /// Metodo carga los datos de la tabla usuarios a el objeto dataGridView
        /// </summary>
        private void ListarDatosUsuarios()
        {
            CN_Usuario objeto = new CN_Usuario();
            dataGridViewUsuarios.DataSource = objeto.ListarUsuarios();
            this.dataGridViewUsuarios.Columns["Id"].Visible = false;


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FormMantUser frm = new FormMantUser();
            frm.Operacion = "Insertar";
            frm.ShowDialog();
            ListarDatosUsuarios();

        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            int cantidadFilas = dataGridViewUsuarios.SelectedRows.Count;

            if (cantidadFilas > 0)
            {
                FormMantUser frm = new FormMantUser();
                frm.Operacion = "Editar";

                frm.txtDn.Text = dataGridViewUsuarios.CurrentRow.Cells["Dni"].Value.ToString();
                frm.txtnombre.Text = dataGridViewUsuarios.CurrentRow.Cells["Nombres"].Value.ToString();
                frm.txtApellido.Text = dataGridViewUsuarios.CurrentRow.Cells["Apellidos"].Value.ToString();
                frm.txtCargo.Text = dataGridViewUsuarios.CurrentRow.Cells["Cargo"].Value.ToString();
                frm.txtEmail.Text = dataGridViewUsuarios.CurrentRow.Cells["Email"].Value.ToString();
                frm.txtPassword.Text = dataGridViewUsuarios.CurrentRow.Cells["Contraseña"].Value.ToString();
                frm.idUsuario = dataGridViewUsuarios.CurrentRow.Cells["Id"].Value.ToString();
                frm.ShowDialog();
                ListarDatosUsuarios();
            }

            else  {
                MessageBox.Show("Seleccione una fila por favor");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            bool estado = false;
            if (dataGridViewUsuarios.SelectedRows.Count > 0)
            {
                DialogResult resultoAccion = MessageBox.Show("¿Desea deshabilitar el usuario?",
                  "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultoAccion == DialogResult.Yes)
                {
                    idUsuario = dataGridViewUsuarios.CurrentRow.Cells["Id"].Value.ToString();
                    estado = Convert.ToBoolean(dataGridViewUsuarios.CurrentRow.Cells["EstadoUsuario"].Value.ToString());

                    estado = estado ? false : true;
                    objetoCN.DeshabilitarUsuario(idUsuario,estado.ToString());
                    //objetoCN.EliminarUsuarios(idUsuario);
                    ListarDatosUsuarios();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

      
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Metodo carga los datos encontrados por termino de busqueda de la tabla usuarios, en el dataGridView.
        /// </summary>
        private void Buscar()
        {
            CN_Usuario objetobusqueda = new CN_Usuario();
            dataGridViewUsuarios.DataSource = objetobusqueda.BuscarUsuarios(txtBuscar.Text);
        }

      
    }
    }

    


