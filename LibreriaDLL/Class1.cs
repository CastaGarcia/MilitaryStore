using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace LibreriaDLL
{
    public class Biblioteca
    {
        public static DataSet Herramientas(string cmd)
        {
            SqlConnection conexion = new SqlConnection("Server=DESKTOP-0OAR274\\DBMSSQLSERVER;Database=Sistema;Trusted_Connection=True;MultipleActiveResultSets=true");
            conexion.Open();

            DataSet dll= new DataSet();
            SqlDataAdapter dll1 = new SqlDataAdapter(cmd, conexion);

            dll1.Fill(dll);

            conexion.Close();

            return dll;
        }
        public static Boolean ValidarFormulario(Control ObjetoError, ErrorProvider ErrorProvider)//ObjetoError es var de Control pasamos el ErrorProvider que creamos
        {
            Boolean SiError = false;

            foreach (Control campo in ObjetoError.Controls)//en una variable tipo control llamada campo, verifica que si por cada campo en ObjetoError.Controls, hereda los campos de using System,Windows.Forms
            {
                if (campo is ErrorTxtBox)//mire si el campo es ErrorTxtBox; si es asi creamos una instancia llamada objeto
                {
                    ErrorTxtBox objeto = (ErrorTxtBox)campo;//creamos una instancia del objeto de ErrorTxtBox

                    if (objeto.Validar == true)
                    {
                        if (string.IsNullOrEmpty(objeto.Text.Trim()))//si el campo del objeto este vacio paramos el ErrorProvider
                        {
                            ErrorProvider.SetError(objeto, "Los campos no pueden estar vacios");
                            SiError = true;//cambie a true la var SiError
                        }

                    }
                    if (objeto.ValidarNumeros == true)
                    {
                        int contador = 0, EncontrarLetras = 0;

                        foreach (char letra in objeto.Text.Trim())
                        {
                            if (char.IsLetter(objeto.Text.Trim(), contador))
                            {
                                EncontrarLetras++;
                            }
                            contador++;
                        }
                        if (EncontrarLetras != 0)
                        {
                            SiError = true;
                            ErrorProvider.SetError(objeto, "Solo se aceptan numeros");
                        }
                    }
                }
            }
            return SiError;
        }
    }
}
