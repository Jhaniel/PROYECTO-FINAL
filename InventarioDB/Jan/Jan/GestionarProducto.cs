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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Jan
{
    public partial class GestionarProducto : Form
    {
        // Variable para almacenar el ID del producto seleccionado
        string ID;

        // Conexión a la base de datos usando la clase Conecta
        private Conecta BaseConecta = new Conecta();

        // Constructor del formulario
        public GestionarProducto()
        {
            InitializeComponent();  // Inicializa los componentes visuales del formulario
            Proveedores();  // Carga los proveedores en el comboBox correspondiente
            Categorias();   // Carga las categorías en el comboBox correspondiente
            Productos();    // Carga los productos en el DataGridView
        }

        // Método para cargar los productos en el DataGridView
        private void Productos()
        {
            // Consulta SQL para obtener los productos junto con sus categorías y proveedores
            string query = "SELECT p.CodigoProducto, p.NombreProduct, p.Precio, p.Cantidad, c.Nombre AS Categoria, pr.NombreEmpresa AS Proveedor, p.IdCategoria, p.IdProveedor FROM Productos p JOIN Categorias c ON p.IdCategoria = c.IdCategoria JOIN Proveedores pr ON p.IdProveedor = pr.IdProveedor";

            // Usando la conexión a la base de datos
            using (SqlConnection conn = BaseConecta.Conectar())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);  // Adaptador de datos
                    DataTable dt = new DataTable();  // Tabla para almacenar los resultados
                    da.Fill(dt);  // Llena la tabla con los datos obtenidos

                    dataGridView1.DataSource = dt;  // Asigna la tabla al DataGridView
                    dataGridView1.Columns["IdCategoria"].Visible = false;  // Oculta la columna de IdCategoria
                    dataGridView1.Columns["IdProveedor"].Visible = false;  // Oculta la columna de IdProveedor
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    MessageBox.Show("Hubo un problema al cargar los productos: " + ex.Message);
                }
            }
        }

        // Método para cargar los proveedores en el comboBox
        private void Proveedores()
        {
            string query = "SELECT IdProveedor, NombreEmpresa FROM Proveedores";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox2.DataSource = dt;  // Asigna la fuente de datos al comboBox
                    comboBox2.DisplayMember = "NombreEmpresa";  // Nombre visible
                    comboBox2.ValueMember = "IdProveedor";  // Valor interno (ID)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un problema al cargar proveedores: " + ex.Message);
                }
            }
        }

        // Método para cargar las categorías en el comboBox
        private void Categorias()
        {
            string query = "SELECT IdCategoria, Nombre FROM Categorias";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.DataSource = dt;  // Asigna la fuente de datos al comboBox
                    comboBox1.DisplayMember = "Nombre";  // Nombre visible
                    comboBox1.ValueMember = "IdCategoria";  // Valor interno (ID)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un problema al cargar categorías: " + ex.Message);
                }
            }
        }

        // Este método se llama cuando el formulario se carga, en este caso no hace nada
        private void GestionarProducto_Load(object sender, EventArgs e)
        {
        }

        // Cierra el formulario cuando se hace clic en el botón
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Método para agregar un nuevo producto a la base de datos
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Productos (NombreProduct, IdProveedor, IdCategoria, Precio, Cantidad) VALUES (@NombreProduct, @IdProveedor, @IdCategoria, @Precio, @Cantidad)";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Asignando los parámetros de la consulta SQL
                    cmd.Parameters.AddWithValue("@NombreProduct", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Precio", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Cantidad", textBox2.Text);
                    cmd.Parameters.AddWithValue("@IdCategoria", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdProveedor", comboBox2.SelectedValue);

                    cmd.ExecuteNonQuery();  // Ejecuta la consulta
                    MessageBox.Show("Se agregó el producto");
                    Productos();  // Recarga los productos en el DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al agregar el producto: " + ex.Message);
                }
            }
        }

        // Método para eliminar un producto de la base de datos
        private void button2_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Productos WHERE CodigoProducto = @CodigoProducto";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CodigoProducto", ID);  // Usa el ID del producto seleccionado

                try
                {
                    cmd.ExecuteNonQuery();  // Ejecuta la consulta
                    MessageBox.Show("El producto se eliminó exitosamente");
                    Productos();  // Recarga los productos
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al eliminar el producto: " + ex.Message);
                }
            }
        }

        // Método para actualizar un producto existente en la base de datos
        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Productos SET NombreProduct = @NombreProduct, IdProveedor = @IdProveedor, IdCategoria = @IdCategoria, Precio = @Precio, Cantidad = @Cantidad WHERE CodigoProducto = @CodigoProducto";

            using (SqlConnection conn = BaseConecta.Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Asignando los parámetros para actualizar el producto
                    cmd.Parameters.AddWithValue("@CodigoProducto", ID);
                    cmd.Parameters.AddWithValue("@NombreProduct", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Precio", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Cantidad", textBox5.Text);
                    cmd.Parameters.AddWithValue("@IdCategoria", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdProveedor", comboBox2.SelectedValue);

                    cmd.ExecuteNonQuery();  // Ejecuta la consulta
                    MessageBox.Show("Se ha actualizado el producto");
                    Productos();  // Recarga los productos en el DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al actualizar el producto: " + ex.Message);
                }
            }
        }

        // Método que se llama cuando se hace clic en una celda del DataGridView
        // Carga los datos del producto seleccionado en los campos del formulario
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];  // Obtiene la fila seleccionada
                ID = row.Cells["CodigoProducto"].Value.ToString();  // Asigna el ID del producto
                textBox1.Text = row.Cells["NombreProduct"].Value.ToString();  // Carga el nombre del producto
                textBox2.Text = row.Cells["Precio"].Value.ToString();  // Carga el precio
                textBox5.Text = row.Cells["Cantidad"].Value.ToString();  // Carga la cantidad
                comboBox1.SelectedValue = row.Cells["IdCategoria"].Value;  // Carga la categoría
                comboBox2.SelectedValue = row.Cells["IdProveedor"].Value;  // Carga el proveedor
            }
        }
    }
}

