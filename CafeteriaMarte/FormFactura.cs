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
using System.Data.SqlClient;

namespace CapaPresentacion
{
    public partial class FormFactura : Form
    {

        private CN_Factura objetoCN = new CN_Factura();
        private CN_Usuario objetoUsuarioCN = new CN_Usuario();
        private CN_Cliente objetoClienteCN = new CN_Cliente();


        public string Operacion = "Insertar";

        public FormFactura()
        {
            InitializeComponent();
        }

        private void FormFactura_Load(object sender, EventArgs e)
        {
          
            MostrarFacturas();
           
        }


        private void MostrarFacturas()
        {

            CN_Factura objeto = new CN_Factura();
            dataGridViewFacturas.DataSource = objeto.ListarFacturas();
            this.dataGridViewFacturas.Columns["IdFactura"].Visible = false;

        }

   

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormMantFactura frm = new FormMantFactura();
            frm.Operacion = "Insertar";
            frm.cambioIndice += 1;
            frm.ShowDialog();
            MostrarFacturas();

           
        }
     
        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            //EDITAR
            //Editar = true;
            if (dataGridViewFacturas.SelectedRows.Count > 0)
            {
                FormMantFactura frm = new FormMantFactura();
                frm.Operacion = "Editar";
                frm.Fecha.Text = dataGridViewFacturas.CurrentRow.Cells["Fecha"].Value.ToString();
                frm.comboBoxCliente.Text = dataGridViewFacturas.CurrentRow.Cells["NombreCliente"].Value.ToString();
                frm.comboBoxResponsable.Text = dataGridViewFacturas.CurrentRow.Cells["Vendedor"].Value.ToString();


                frm.idFactura = dataGridViewFacturas.CurrentRow.Cells["IdFactura"].Value.ToString();
                frm.ShowDialog();
                MostrarFacturas();

                /*FormMantMaestros frm = new FormMantMaestros();
                frm.Operacion = "Editar";
                frm.txtCodigo.Text = dataGridViewMaestros.CurrentRow.Cells[1].Value.ToString();
                frm.txtNombre.Text = dataGridViewMaestros.CurrentRow.Cells["Nombre"].Value.ToString();
                frm.txtDepartamento.Text = dataGridViewMaestros.CurrentRow.Cells["Departamento"].Value.ToString();
                frm.txtCorreo.Text = dataGridViewMaestros.CurrentRow.Cells["Correo"].Value.ToString();
                frm.maskedTxtTelefono.Text = dataGridViewMaestros.CurrentRow.Cells["Telefono"].Value.ToString();*/


                /*frm.idMaestro = dataGridViewMaestros.CurrentRow.Cells["Id_Maestro"].Value.ToString();
                frm.ShowDialog();
                MostrarMaestros();*/


            }

            else
                MessageBox.Show("seleccione una fila por favor");
        }

        
        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            CN_Factura objFactura = new CN_Factura();
            dataGridViewFacturas.DataSource = objFactura.BuscarFactura(txtBuscar.Text);

        }



        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            string idFactura = "";
            bool estadoFactura = false;

            if (dataGridViewFacturas.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Esta seguro que desea deshabilitar este registro?", "Mensage de Confirmacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    idFactura = dataGridViewFacturas.CurrentRow.Cells["IdFactura"].Value.ToString();
                    estadoFactura = Convert.ToBoolean(dataGridViewFacturas.CurrentRow.Cells["EstadoFactura"].Value.ToString());

                    estadoFactura = estadoFactura ? false : true;
                    objetoCN.DeshabilitarFactura(idFactura, estadoFactura.ToString());

                    // MessageBox.Show("Eliminado Correctamente");
                    MostrarFacturas();
                }

            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }
    }

       
    }


