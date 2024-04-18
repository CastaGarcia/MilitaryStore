using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//using de conexion a BD
using LibreriaDLL;

namespace MilitaryStore
{
    public partial class Login : FormBase
    {
        public Login()
        {
            InitializeComponent();
        }

        public static String Codigo = "";//Variable publica para conexion

        private void button1_Click(object sender, EventArgs e)
        {
            /*try
            {
                SqlConnection conexion = new SqlConnection("Data Source=.;Initial Catalog=Sistema;Integrated Security=True;");
                conexion.Open();
                MessageBox.Show("Conexion exitosa");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }*/
            //Biblioteca.Herramientas("Select * From Clientes Where ID=1");
            try
            {
                string validar = string.Format("Select * FROM Usuarios WHERE account='{0}' AND password ='{1}'", textUsuario.Text.Trim(), textPassWord.Text.Trim());
                DataSet conectar = Biblioteca.Herramientas(validar);//guarda informacion de la consulta en un catche 

                Codigo = conectar.Tables[0].Rows[0]["id_usuario"].ToString().Trim();//accedemos al id_usuario de la tabla de BD para determinar priviligios

                string cuenta = conectar.Tables[0].Rows[0]["account"].ToString().Trim();
                string contrasena = conectar.Tables[0].Rows[0]["password"].ToString().Trim();

                if (cuenta == textUsuario.Text.Trim() && contrasena == textPassWord.Text.Trim())
                {
                    if (Convert.ToBoolean(conectar.Tables[0].Rows[0]["validar_admin"].ToString().Trim()) == true)
                    {
                        Administrador Admin = new Administrador();
                        this.Hide();
                        Admin.Show();
                    }
                    else
                    {
                        Usuario User = new Usuario();
                        this.Hide();
                        User.Show();
                    }
                }
                
            }
            catch (Exception error)
            {
                MessageBox.Show("credenciales invalidas "+ error);
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
