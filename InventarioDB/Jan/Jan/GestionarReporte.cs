using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jan
{
    public partial class GestionarReporte : Form
    {
        private Conecta BaseConecta = new Conecta();

        public GestionarReporte()
        {
            InitializeComponent();
            Proveedores();
            Categorias();
            Reporte();
            Consulta();
        }

        private void Proveedores()
        {
            string query = "select IdProveedor, NombreEmpresa from Proveedores";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "NombreEmpresa";
                    comboBox2.ValueMember = "IdProveedor"; ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un problema al cargar proveedores: " + ex.Message);
                }
            }
        }

        //Cargar Categorias

        private void Categorias()
        {
            string query = "select IdCategoria, Nombre from Categorias";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "Nombre";
                    comboBox1.ValueMember = "IdCategoria"; ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un problema al cargar categorias: " + ex.Message);
                }
            }
        }

        //Cargar Consulta

        private void Consulta()
        {
            string query = "select p.CodigoProducto, p.NombreProduct, p.Precio, p.Cantidad, c.Nombre AS Categoria, pr.NombreEmpresa AS Proveedor, p.IdCategoria, p.IdProveedor FROM ProductoS p JOIN Categorias c ON p.IdCategoria = c.IdCategoria JOIN Proveedores pr ON p.IdProveedor = pr.IdProveedor where P.IdCategoria = @IdCategoria and P.IdProveedor = @IdProveedor ";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdCategoria", comboBox1.SelectedValue);
                cmd.Parameters.AddWithValue("@IdProveedor", comboBox2.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                try
                {

                }
                catch (Exception)
                {
                    MessageBox.Show("Primero debe existir Proveedores y Categorias: ");
                }
            }
        }

        //Cargar Reporte con select especificos para mostrar la informacion deseada.

        private void Reporte()
        {
            string query = "select p.CodigoProducto, p.NombreProduct, p.Precio, p.Cantidad, c.Nombre AS Categoria, pr.NombreEmpresa AS Proveedor, p.IdCategoria, p.IdProveedor FROM Productos p JOIN Categorias c ON p.IdCategoria = c.IdCategoria JOIN Proveedores pr ON p.IdProveedor = pr.IdProveedor where Cantidad <10 ";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.DataSource = dt;
                try
                {

                }
                catch (Exception)
                {
                    MessageBox.Show("Primero debe existir Proveedores y Categorias: ");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Consulta();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
