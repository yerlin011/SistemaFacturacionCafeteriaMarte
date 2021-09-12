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
    public partial class FormClientes : Form
    {
        CN_Cliente objetoCN = new CN_Cliente();
        public string idCliente;
        public bool Editar = false;
        public string Operacion = "Insertar";

        public FormClientes()
        {
            InitializeComponent();
        }


        private void FormClientes_Load(object sender, EventArgs e)
        {
            MostrarClientes();
        }
        private void MostrarClientes()
        {
            CN_Cliente objeto = new CN_Cliente();
            dataGridViewClientes.DataSource = objeto.ListarClientes();
            this.dataGridViewClientes.Columns["IdCliente"].Visible = false;
        }


        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            CN_Cliente objetobusqueda = new CN_Cliente();
            dataGridViewClientes.DataSource = objetobusqueda.BuscarCliente(txtBuscar.Text);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo2_Click(object sender, EventArgs e)
        {
            FormMantCliente frm = new FormMantCliente();
            frm.Operacion = "Insertar";
            frm.ShowDialog();
            MostrarClientes();
        }

       

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {

                FormMantCliente frm = new FormMantCliente();


                //EDITAR
                frm.Operacion = "Editar";
                frm.idCliente = dataGridViewClientes.CurrentRow.Cells["IdCliente"].Value.ToString();

                frm.nombreCompleto.Text = dataGridViewClientes.CurrentRow.Cells["NombreCompleto"].Value.ToString();
                frm.direccion.Text = dataGridViewClientes.CurrentRow.Cells["Direccion"].Value.ToString();
                frm.telefonoMasked.Text = dataGridViewClientes.CurrentRow.Cells["Telefono"].Value.ToString();


                frm.ShowDialog();
                MostrarClientes();

            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void txtBuscar_KeyUp_1(object sender, KeyEventArgs e)
        {
            CN_Cliente objetobusqueda = new CN_Cliente();
            dataGridViewClientes.DataSource = objetobusqueda.BuscarCliente(txtBuscar.Text);
        }
    }
}
