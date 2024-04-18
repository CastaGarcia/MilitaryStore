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
    public partial class MantenimientoClientes : Mantenimiento
    {
        public MantenimientoClientes()
        {
            InitializeComponent();
        }
        public override Boolean Guardar()
        {//errorProvider es el objeto que arrastramos de nuestro cuadro de herramientas, el nombre es por defecto

            if(Biblioteca.ValidarFormulario(this, errorProvider1) == false)//si no esta vacio el campo haremos los procedimientos siguientes
            {
                try
                {
                    string insertar = string.Format("EXEC ActualizarClientes '{0}','{1}','{2}'", textId_Cliente.Text.Trim(), textNombre_Cliente.Text.Trim(), textApellido_Cliente.Text.Trim());//usando codigo desde SQL
                    Biblioteca.Herramientas(insertar);  //llamamos la libreria que creamos
                    MessageBox.Show("Cliente Guardado Correctamente");
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
                return false; //sino que nos retorne todo el error que programamos en Class1
            }
        }
        public override void Eliminar()
        {
            try
            {
                string eliminar = string.Format("EXEC EliminarClientes '{0}'", textId_Cliente.Text.Trim());
                Biblioteca.Herramientas(eliminar); //llamamos a la libreria para eliminar 
                MessageBox.Show("El Cliente Se Elimino Correctamente");
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error: " + error);
            }
        }

        private void textId_Cliente_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textId_Cliente.Text.Trim()) == false && string.IsNullOrEmpty(textNombre_Cliente.Text.Trim()) == false && string.IsNullOrEmpty(textApellido_Cliente.Text.Trim()) == false)
            {
                textId_Cliente.Text = "";
                textNombre_Cliente.Text = "";
                textApellido_Cliente.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarClientes ConsulClien = new ConsultarClientes();
            ConsulClien.Show();
        }
    }

}
