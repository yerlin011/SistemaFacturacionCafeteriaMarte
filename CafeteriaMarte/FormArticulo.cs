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

    public partial class FormArticulos : Form
    {
        #region
        //SqlConnection conexion = new SqlConnection("Data Source = YERLIN022\\SQLEXPRESS;Initial Catalog = REGISTRO_TUTORIAS_ACADEMICA; Integrated Security = True");
        #endregion

        CN_Articulo objetoCN = new CN_Articulo();
        public string idArticulo = "";
        public bool estado=false;
        public string Operacion = "Insertar";


        public FormArticulos()
        {
            InitializeComponent();
        }

        private void FormArticulos_Load(object sender, EventArgs e)
        {
            MostrarArticulos();
        }


        private void MostrarArticulos()
        {

           CN_Articulo objeto = new CN_Articulo();
            dataGridViewArticulos.DataSource = objeto.ListarArticulos();
            this.dataGridViewArticulos.Columns["IdArticulo"].Visible = false;
        }
        private void MostrarCarreras()
        {

            //CN_Estudiantes objeto = new CN_Estudiantes();
            //FormMantEstudiantes frm = new FormMantEstudiantes();
            //frm.cbmCarrera.DataSource = objeto.MostrarCarr();
            //frm.cbmCarrera.DisplayMember = "Nombre_Carrera";
            //frm.cbmCarrera.ValueMember = "Id_Carrera";
        }
        private void MostrarMaterias()
        {
            //CN_Estudiantes objeto = new CN_Estudiantes();
            //FormMantEstudiantes frm = new FormMantEstudiantes();

            //frm.cmbMateria.DataSource = objeto.MostrarMate();
            //frm.cmbMateria.DisplayMember = "Nombre_Materia";
            //frm.cmbMateria.ValueMember = "Id_Materia";

        }

       
        private void LimpiarForm()
        {
            //txtMatricula.Clear();
            //txtNombre.Clear();
            //txtApellido.Clear();
            //cbmCarrera.Text = "Carrera";
            //txtEstado.Clear();
        }

       

     
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

            //CN_Empleado objbe = new CN_Empleado();
            //dataGridView2.DataSource = objetoCN.BuscarEst(txtBuscar.Text);
            //objetoCN.BuscarEst(txtMatricula.Text, txtNombre.Text);
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
             CN_Articulo objetobusqueda = new CN_Articulo();
             dataGridViewArticulos.DataSource =objetobusqueda.BuscarArticulo(txtBuscar.Text);

        }

       
        private void btnNuevo_Click(object sender, EventArgs e)
        {

            FormMantArticulo frm = new FormMantArticulo();
            frm.Operacion = "Insertar";
            frm.ShowDialog();
            MostrarArticulos();

        }

        private void btnEditarF2_Click(object sender, EventArgs e)
        {

           
            
            if (dataGridViewArticulos.SelectedRows.Count > 0)
            {


                FormMantArticulo frm = new FormMantArticulo();


                //EDITAR
              
                 frm.Operacion = "Editar";
               
                
                 frm.idArticulo = dataGridViewArticulos.CurrentRow.Cells["IdArticulo"].Value.ToString();

                 frm.descripcion.Text = dataGridViewArticulos.CurrentRow.Cells["Descripcion"].Value.ToString();
                 frm.precio.Text = dataGridViewArticulos.CurrentRow.Cells["Precio"].Value.ToString();
                 frm.stock.Text = dataGridViewArticulos.CurrentRow.Cells["Stock"].Value.ToString();
                 frm.estadoCheckBox.Checked= Convert.ToBoolean(dataGridViewArticulos.CurrentRow.Cells["Estado"].Value.ToString());

                 frm.ShowDialog();
                 MostrarArticulos();

            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dataGridViewArticulos.SelectedRows.Count > 0)
            {
                DialogResult resultoAccion = MessageBox.Show("¿Desea deshabitar el articulo?",
                "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultoAccion == DialogResult.Yes)
                {

                    idArticulo = dataGridViewArticulos.CurrentRow.Cells["IdArticulo"].Value.ToString();
                    estado = Convert.ToBoolean(dataGridViewArticulos.CurrentRow.Cells["Estado"].Value.ToString());
                    estado = estado ? false : true;
                    objetoCN.DeshabilitarArticulo(idArticulo, estado.ToString());
                    MostrarArticulos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }
    }
}


        

 

