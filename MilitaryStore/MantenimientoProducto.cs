using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriaDLL;

namespace MilitaryStore
{
    public partial class MantenimientoProductos : Mantenimiento
    {
        public MantenimientoProductos()
        {
            InitializeComponent();
        }

        private void MantenimientoProductos_Load(object sender, EventArgs e)
        {

        }
        public override Boolean Guardar()
        {
            if (Biblioteca.ValidarFormulario(this, errorProvider1) == false)//si no esta vacio el campo haremos los procedimientos siguientes
            {
                try
                {
                    string insertar = string.Format("EXEC ActualizarProductos '{0}','{1}','{2}'", textId_Producto.Text.Trim(), textDescripcion.Text.Trim(), textPrecio.Text.Trim());//usando codigo desde SQL
                    Biblioteca.Herramientas(insertar);  //llamamos la libreria que creamos
                    MessageBox.Show("Producto Guardado Correctamente");
                    return true;// por ser booleanos debe devolver true que se guardo
                }
                catch (Exception error)
                {
                    MessageBox.Show("Ha ocurrido un error: " + error);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public override void Eliminar()
        {
            try
            {
                string eliminar = string.Format("EXEC EliminarProductos '{0}'", textId_Producto.Text.Trim());
                Biblioteca.Herramientas(eliminar); //llamamos a la libreria para eliminar 
                MessageBox.Show("El Producto Se Elimino Correctamente");
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error: " + error);
            }
        }

        private void textId_Producto_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textId_Producto.Text.Trim()) == false && string.IsNullOrEmpty(textDescripcion.Text.Trim()) == false && string.IsNullOrEmpty(textPrecio.Text.Trim()) == false)
            {
                textId_Producto.Text = "";
                textDescripcion.Text = "";
                textPrecio.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarProductos ConsulProd = new ConsultarProductos();
            ConsulProd.Show();
        }
    }
}
