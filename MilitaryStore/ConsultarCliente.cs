﻿using LibreriaDLL;
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
    public partial class ConsultarClientes : Consultas
    {
        public ConsultarClientes()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) == false)//si no esta vacio el TextBox1 hara lo siguiente
            {
                try
                {
                    DataSet DS;
                    string buscar = "Select * from Clientes WHERE Nombre_cliente LIKE ('%" + textBox1.Text.Trim() + "%')";

                    DS = Biblioteca.Herramientas(buscar);//pasamos como parametro buscar

                    dataGridView1.DataSource = DS.Tables[0];//para que termine la busqueda correctamente

                }
                catch (Exception error)
                {
                    MessageBox.Show("No se puede conectar, Error: ", error.Message);
                }

            }
        }

        private void ConsultarClientes_Load(object sender, EventArgs e)
        {
            //dataGridView1 nombre por defecto de DataGridView
            dataGridView1.DataSource = MostrarInfoDG("Clientes").Tables[0];
        }
    }
    
}