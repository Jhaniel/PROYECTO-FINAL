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
    public partial class GestionCategorias : Form
    {
        //Referencia para conectar base de datos a las acciones.

        private Conecta BaseDatos = new Conecta();

        public GestionCategorias()
        {
            InitializeComponent();
            PonerCategoria();
        }

        private void GestionCategorias_Load(object sender, EventArgs e)
        {

        }
       
        private void PonerCategoria()
        {
            string query = "Select * from Categorias";

            using (SqlConnection conn = BaseDatos.Conectar())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        //Sirve para cada vez que se le da al boton se agrege un registro.
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "Insert Into Categorias (Nombre, Descripcion) values (@Nombre, @Descripcion)";

            using (SqlConnection conn = BaseDatos.Conectar())
            {

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", textBox2.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se agrego la Categoria");
                    PonerCategoria();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error para agregar la categoria: " + ex.Message);
                }
            }
        }
        string ID;

        //Sirve para cada vez que se le da al boton se elimine un registro.
        private void button2_Click(object sender, EventArgs e)
        {
            string query = "Delete from Categorias where IdCategoria = @IdCategoria";

            using (SqlConnection conn = BaseDatos.Conectar())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdCategoria", ID);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("La Categoria se elimino exitosamente");
                    PonerCategoria();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al eliminar la categoria: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "Update Categorias set Nombre = @Nombre, Descripcion = @Descripcion where IdCategoria =  @IdCategoria";

            using (SqlConnection conn = BaseDatos.Conectar())
            {

                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCategoria", ID);
                    cmd.Parameters.AddWithValue("@Nombre", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", textBox2.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha actualizado la categoria");
                    PonerCategoria();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al actualizar la categoria: " + ex.Message);
                }
            }
        }

        //Termina la vida del Form
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Sirve para obtener valores de las tablas al hacer clic.
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ID = row.Cells["IdCategoria"].Value.ToString();
                textBox1.Text = row.Cells["Nombre"].Value.ToString();
                textBox2.Text = row.Cells["Descripcion"].Value.ToString();
            }
        }
    }
}
