using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FormMantFactura : Form
    {

        private CN_Factura objetoCN = new CN_Factura();
        private CN_Usuario objetoUsuarioCN = new CN_Usuario();
        private CN_Cliente objetoClienteCN = new CN_Cliente();
        private CN_Articulo objetoArticuloCN = new CN_Articulo();

        public string Operacion = "";
        public string idFactura;
        public int cambioIndice = 0;


        public FormMantFactura()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void FormMantFactura_Load(object sender, EventArgs e)
        {
            ListarClientes();
            ListarVendedorResponsable();
            ListarArticulos();
        }

        private void ListarArticulos()
        {
            comboBoxArticulo.DataSource = objetoArticuloCN.ListarArticulos();
            comboBoxArticulo.DisplayMember = "Descripcion";
            comboBoxArticulo.ValueMember = "IdArticulo";
        }
        private void ListarClientes()
        {
            comboBoxCliente.DataSource = objetoClienteCN.ListarClientes();
            comboBoxCliente.DisplayMember = "NombreCompleto";
            comboBoxCliente.ValueMember = "IdCliente";
        }

        private void ListarVendedorResponsable()
        {
            comboBoxResponsable.DataSource = objetoUsuarioCN.ListarUsuarios();
            comboBoxResponsable.DisplayMember = "Nombres";
            comboBoxResponsable.ValueMember = "Id";
        }
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.cantidad.Text.Length == 0)
                {
                    MessageBox.Show("Ingrese cantidad", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cantidad.Select();
                    return;
                }

                DataGridViewRow file = new DataGridViewRow();

                file.CreateCells(dataGridViewDetalleFactura);

                file.Cells[0].Value = codigoArticulo.Text;
                file.Cells[1].Value = descripcion.Text;
                file.Cells[2].Value = precio.Text;
                file.Cells[3].Value = cantidad.Text;
                file.Cells[4].Value = float.Parse(precio.Text) * float.Parse(cantidad.Text);


                dataGridViewDetalleFactura.Rows.Add(file);
                LimpiarCampos();

                ObtenerMontoTotal();

            }
            catch (Exception E)
            {
                MessageBox.Show("Ha ocurrido un error " + E.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewDetalleFactura.Rows.Count > 0)
                {
                    DialogResult objRespuestaConfirmacion = MessageBox.Show("¿Desea eliminar el articulo?",
                  "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (objRespuestaConfirmacion == DialogResult.Yes)
                    {
                        dataGridViewDetalleFactura.Rows.Remove(dataGridViewDetalleFactura.CurrentRow);
                    }
                }
                else
                {
                    MessageBox.Show("Favor de seleccionar una fila!");

                }

            }
            catch (Exception E)
            {
                MessageBox.Show("Ha ocurrido un error: " + E.Message);
            }

            ObtenerMontoTotal();
        }
      
           

        
        private void BtnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cambioIndice > 2)
            {

                CN_Articulo objArticulo = new CN_Articulo();
                SqlDataReader registro;

                objArticulo.IdArticulo = Convert.ToInt32(comboBoxArticulo.SelectedValue.ToString());

                registro = objArticulo.ObtenerDatosArticuloEspefico();

                if (registro.Read().Equals(true))
                {

                    codigoArticulo.Text = registro["IdArticulo"].ToString();
                    descripcion.Text = registro["Descripcion"].ToString();
                    precio.Text = registro["Precio"].ToString();

                }
                else {
                    MessageBox.Show("No se encontro el articulo");
                }

            } else
            {
                cambioIndice += 1;
            }

        }
        /// <summary>
        /// Obtener idFactura
        /// </summary>
        private int ObtenerUltimoIdFactura()
        {
            CN_Factura objFactura = new CN_Factura();
            SqlDataReader registro;
            int codFactura=0;
            registro = objFactura.ObtenerUltimoIdFacturaRegistro();

            if (registro.Read().Equals(true))
            {
               codFactura = Convert.ToInt32(registro["IdFactura"].ToString());
            }

            return codFactura;
        }
    

        /// <summary>
        /// Metodo permite limpiar los campos
        /// </summary>
        private void LimpiarCampos()
        {

            codigoArticulo.Text = "";
            descripcion.Text = "";
            precio.Text = "";
            cantidad.Text = "";
        }

        /// <summary>
        /// Metodo permite obtener el monto total a pagar.
        /// </summary>
        private void ObtenerMontoTotal()
        {
           float sumaTotalAPagar=0;

           for(int i= 0;i < dataGridViewDetalleFactura.RowCount; i++){

                sumaTotalAPagar += float.Parse(dataGridViewDetalleFactura.Rows[i].Cells[4].Value.ToString());
            }


            labelTotal.Text = sumaTotalAPagar.ToString();
        }

       

        private void efectivo_TextChanged(object sender, EventArgs e)
        {
            if (efectivo.Text.Length > 0)
            {
                if (!labelTotal.Text.Equals("-") && float.Parse(labelTotal.Text) > 0)
                {
                    labelDevuelta.Text = (float.Parse(efectivo.Text) - float.Parse(labelTotal.Text)).ToString();

                }
            }
          
        }

        private void efectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        /// <summary>
        /// Metodo permite registrar factura
        /// </summary>

        private bool RegistrarFactura()
        {
           bool resultado = false;


            if (this.Fecha.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese Fecha", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Fecha.Select();
                return false;
            }

            if (this.efectivo.Text.Length == 0)
            {
                MessageBox.Show("Ingrese efectivo", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.efectivo.Select();
                return false;
            }

            ////INSERTAR
            if (Operacion.Equals("Insertar"))
            {
                try
                {
                    float total = !labelTotal.Text.Equals("-") && float.Parse(labelTotal.Text) > 0? float.Parse(labelTotal.Text) : 0;
                    float devuelta = !labelDevuelta.Text.Equals("-") && float.Parse(labelDevuelta.Text) >= 0 ? float.Parse(labelDevuelta.Text) : 0;

                    if (float.Parse(efectivo.Text) >= total)
                    {
                        objetoCN.InsertarFactura(Fecha.Text, comboBoxResponsable.SelectedValue.ToString(), comboBoxCliente.SelectedValue.ToString(), total.ToString(), efectivo.Text, devuelta.ToString());
                        resultado = true;

                    }
                    else
                    {
                        MessageBox.Show("El efectivo es inferior a la deuda!", "Mensaje error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar los datos por: " + ex);
                }
            }
            if (Operacion.Equals("Editar"))
            {

                try
                {
                    objetoCN.EditarFactura(Fecha.Text, comboBoxResponsable.SelectedValue.ToString(), comboBoxCliente.SelectedValue.ToString(), idFactura);
                    MessageBox.Show("Factura editada correctamente!", "Mensaje exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo editar los datos por: " + ex);
                }
            }

            return resultado;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                CN_DetalleFactura objDetalleFactura = new CN_DetalleFactura();
                int codigoArticulo = 0;
                int cantidad = 0;
                float precioTotal = 0;
                int filaRegistrada = 0;

                if (RegistrarFactura())
                {

                    if (ObtenerUltimoIdFactura() > 0)
                    {

                        for (int i = 0; i < dataGridViewDetalleFactura.RowCount; i++)
                        {
                            codigoArticulo = Convert.ToInt32(dataGridViewDetalleFactura.Rows[i].Cells[0].Value.ToString());
                            precioTotal = Convert.ToInt32(dataGridViewDetalleFactura.Rows[i].Cells[2].Value.ToString());
                            cantidad = Convert.ToInt32(dataGridViewDetalleFactura.Rows[i].Cells[3].Value.ToString());

                            filaRegistrada = objDetalleFactura.InsertarDetalleFactura(ObtenerUltimoIdFactura().ToString(), codigoArticulo.ToString(),
                                cantidad.ToString(), precioTotal.ToString());

                        }

                        if (filaRegistrada > 0)
                        {
                            MessageBox.Show("Factura ingresada correctamente!", "Mensaje exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CrearTicket();
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("No se encuentro el idfactura para registrar");
                    }
                }
                else
                {
                    MessageBox.Show("No registro la factura");
                }
            }
            catch (SqlException E)
            {

                MessageBox.Show("Error: "+E.Message);
            }

        }

        /// <summary>
        /// Metodo genera u ticket con los detalles de la factura
        /// </summary>
        private void CrearTicket()
        {

            CD_TicketFactura.CreaTicket Ticket1 = new CD_TicketFactura.CreaTicket();

            Ticket1.TextoCentro("Empresa D Marte Cafeteria "); //imprime una linea de descripcion
            Ticket1.TextoCentro("**********************************");

            Ticket1.TextoIzquierda("");
            Ticket1.TextoCentro("Factura de Venta"); //imprime una linea de descripcion
            Ticket1.TextoIzquierda("No Fac: "+ObtenerUltimoIdFactura());
            Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString());
            Ticket1.TextoIzquierda("Le Atendio: "+ Program.Nombres+" "+Program.Apellidos);
            Ticket1.TextoIzquierda("");
            CD_TicketFactura.CreaTicket.LineasGuion();

            CD_TicketFactura.CreaTicket.EncabezadoVenta();
            CD_TicketFactura.CreaTicket.LineasGuion();
            foreach (DataGridViewRow r in dataGridViewDetalleFactura.Rows)
            {
                                                     // PROD                            //PRECIO                                    CANT                         TOTAL
                Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString())); //imprime una linea de descripcion
            }


            CD_TicketFactura.CreaTicket.LineasGuion();
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Total", double.Parse(labelTotal.Text)); // imprime linea con total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(efectivo.Text));
            Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(labelDevuelta.Text));


            // Ticket1.LineasTotales(); // imprime linea 

            Ticket1.TextoIzquierda(" ");
            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoCentro("*     Gracias por preferirnos    *");

            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoIzquierda(" ");
            string impresora = "Microsoft XPS Document Writer";
            Ticket1.ImprimirTiket(impresora);

            MessageBox.Show("Gracias por preferirnos");

            this.Close();
        }

      
    }
}
