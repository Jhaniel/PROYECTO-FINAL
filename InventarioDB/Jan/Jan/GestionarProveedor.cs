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
    public partial class GestionarProveedor : Form
    {
        string IdProveedor;
        private Conecta BaseDatos = new Conecta();
        public GestionarProveedor()
        {
            InitializeComponent();
            Proveedores();
        }

        private void Proveedores()
        {
            string query = "Select * from Proveedores";

            using (SqlConnection conn = BaseDatos.Conectar())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }


        private void GestionarProveedor_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "Insert Into Proveedores (NombreEmpresa, Contacto, Direccion) values (@Empresa, @Telefono, @Direccion)";

            using (SqlConnection conn = BaseDatos.Conectar())
            {

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Empresa", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Telefono", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Direccion", textBox3.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se agrego el Proveedor");
                    Proveedores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error para agregar al proveedor: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "Delete from Proveedores where IdProveedor = @IdProveedor";

            using (SqlConnection conn = BaseDatos.Conectar())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("El proveedor se elimino exitosamente");
                    Proveedores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al eliminar el proveedor: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                IdProveedor = row.Cells["IdProveedor"].Value.ToString();
                textBox1.Text = row.Cells["NombreEmpresa"].Value.ToString();
                textBox2.Text = row.Cells["Contacto"].Value.ToString();
                textBox3.Text = row.Cells["Direccion"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "Update Proveedores set NombreEmpresa = @Empresa, Contacto = @Telefono, Direccion = @Direccion where IdProveedor = @IdProveedor";

            using (SqlConnection conn = BaseDatos.Conectar())
            {

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    cmd.Parameters.AddWithValue("@Empresa", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Telefono", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Direccion", textBox3.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha actualizado el proveedor");
                    Proveedores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al actualizar el proveedor: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
