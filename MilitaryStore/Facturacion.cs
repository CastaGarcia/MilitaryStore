using LibreriaDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilitaryStore
{
    public partial class Facturacion : Procesos
    {
        public Facturacion()
        {
            InitializeComponent();
        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            string vendedor = "Select * from Usuarios Where id_usuario = " + Login.Codigo;

            DataSet ds;

            ds = Biblioteca.Herramientas(vendedor);

            lbVendedor.Text = ds.Tables[0].Rows[0]["username"].ToString().Trim();
        }

        private void BtBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtCodigoCliente.Text.Trim()) == false)
                {
                    string cmd = string.Format("Select Nombre_Cliente from Clientes where id_clientes = '{0}'", TxtCodigoCliente.Text.Trim());
                    DataSet ds = Biblioteca.Herramientas(cmd);

                    TxtCliente.Text = ds.Tables[0].Rows[0]["Nombre_Cliente"].ToString().Trim();

                    TxtCodigoProducto.Focus();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error:" + error.Message);
            }
        }

        public static int contadorFila = 0; //declaramos una variable publica
        public static double total; //variable publica para eliminar y almacenar sumatoria

        private void BtColocar_Click(object sender, EventArgs e)
        {
            if (Biblioteca.ValidarFormulario(this, errorProvider1) == false)
            {
                bool existe = false;
                int numeroFila = 0;

                if (contadorFila == 0)
                {
                    dataGridView1.Rows.Add(TxtCodigoProducto.Text, TxtDescripcion.Text, TxtPrecio.Text, TxtCantidad.Text);
                    double importe = Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[3].Value);
                    dataGridView1.Rows[contadorFila].Cells[4].Value = importe;

                    contadorFila++;
                }
                else
                {
                    foreach (DataGridViewRow Fila in dataGridView1.Rows)
                    {
                        if (Fila.Cells[0].Value.ToString() == TxtCodigoProducto.Text)
                        {
                            existe = true;
                            numeroFila = Fila.Index;
                        }
                    }
                    if (existe == true)
                    {
                        dataGridView1.Rows[numeroFila].Cells[3].Value = (Convert.ToDouble(TxtCantidad.Text) + Convert.ToDouble(dataGridView1.Rows[numeroFila].Cells[3].Value)).ToString();
                        double importe = Convert.ToDouble(dataGridView1.Rows[numeroFila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[numeroFila].Cells[3].Value);
                        dataGridView1.Rows[numeroFila].Cells[4].Value = importe;
                    }
                    else
                    {
                        dataGridView1.Rows.Add(TxtCodigoProducto.Text, TxtDescripcion.Text, TxtPrecio.Text, TxtCantidad.Text);
                        double importe = Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[contadorFila].Cells[3].Value);
                        dataGridView1.Rows[contadorFila].Cells[4].Value = importe;

                        contadorFila++;
                    }
                }
            }

            total = 0;
            foreach (DataGridViewRow Fila in dataGridView1.Rows)
            {
                total += Convert.ToDouble(Fila.Cells[4].Value);

            }
            lbTotal.Text = "USD$ " + total.ToString();
        }

        private void BtEliminar_Click(object sender, EventArgs e)
        {
            if (contadorFila > 0)//si existe en nuestro dataGridView1
            {
                //que lo elimine de usando CurrentRow para ubicar la celta que no queremos
                total = total - (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value));
                lbTotal.Text = "USD$ " + total.ToString();

                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                contadorFila--;
            }
        }

        private void BtClientes_Click(object sender, EventArgs e)
        {
            ConsultarClientes ConsulClient = new ConsultarClientes();//creamos una instancia de la ventana cosulta.cs
            ConsulClient.ShowDialog();//muestra resultado de ese evento

            if (ConsulClient.DialogResult == DialogResult.OK)
            {
                TxtCodigoCliente.Text = ConsulClient.dataGridView1.Rows[ConsulClient.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                TxtCliente.Text = ConsulClient.dataGridView1.Rows[ConsulClient.dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();

                TxtCodigoProducto.Focus();
            }
        }

        private void BtProductos_Click(object sender, EventArgs e)
        {
            ConsultarProductos ConsultarProd = new ConsultarProductos();
            ConsultarProd.ShowDialog();

            if (ConsultarProd.DialogResult == DialogResult.OK)
            {
                TxtCodigoProducto.Text = ConsultarProd.dataGridView1.Rows[ConsultarProd.dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                TxtDescripcion.Text = ConsultarProd.dataGridView1.Rows[ConsultarProd.dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                TxtPrecio.Text = ConsultarProd.dataGridView1.Rows[ConsultarProd.dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

                TxtCantidad.Focus();
            }
        }

        private void BtNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        public override void Nuevo()
        {
            TxtCodigoCliente.Text = "";
            TxtCliente.Text = "";
            TxtCodigoProducto.Text = "";
            TxtDescripcion.Text = "";
            TxtPrecio.Text = "";
            TxtCantidad.Text = "";
            lbTotal.Text = "USD$ 0";
            dataGridView1.Rows.Clear();
            contadorFila = 0;
            total = 0;

            TxtCodigoCliente.Focus();
        }

        private void BtFacturar_Click(object sender, EventArgs e)
        {
            if (contadorFila != 0)
            {
                try
                {
                    string cmd = string.Format("Exec ActualizarFacturas '{0}'", TxtCodigoCliente.Text.Trim());
                    DataSet DS = Biblioteca.Herramientas(cmd);

                    if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0) // Verificar si existen tablas y filas en el DataSet
                    {
                        string NumeroFactura = DS.Tables[0].Rows[0]["NumeroFactura"].ToString().Trim();

                        foreach (DataGridViewRow Fila in dataGridView1.Rows)
                        {
                            cmd = string.Format("Exec ActualizarDetalles '{0}','{1}','{2}','{3}'", NumeroFactura, Fila.Cells[0].Value.ToString(), Fila.Cells[2].Value.ToString(), Fila.Cells[3].Value.ToString());
                            DS = Biblioteca.Herramientas(cmd);
                        }
                        cmd = "Exec DatosFactura " + NumeroFactura;

                        DS = Biblioteca.Herramientas(cmd);

                        if (DS.Tables.Count > 0)
                        {
                            Factura fac = new Factura();
                            fac.reportViewer1.LocalReport.DataSources.Clear();
                            fac.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", DS.Tables[0]));
                            fac.ShowDialog();

                            Nuevo();
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron datos para generar la factura.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos para generar la factura.");
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: " + error.Message);
                }
            }
        }

    }
}
