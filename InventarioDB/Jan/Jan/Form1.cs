using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jan
{
    public partial class Form1 : Form
    {
        // Constructor principal de la clase Form1
        public Form1()
        {
            InitializeComponent();  // Inicializa los componentes visuales del formulario
        }

        // Método que se ejecuta cuando el formulario carga (actualmente no hace nada)
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Botón para abrir el formulario de gestión de categorías
        private void button1_Click(object sender, EventArgs e)
        {
            GestionCategorias formcategoria = new GestionCategorias();  // Crea una nueva instancia del formulario GestionCategorias
            formcategoria.Show();  // Muestra el formulario sin cerrar el formulario principal
        }

        // Botón para abrir el formulario de gestión de proveedores
        private void button3_Click(object sender, EventArgs e)
        {
            GestionarProveedor formproveedore = new GestionarProveedor();  // Crea una nueva instancia del formulario GestionarProveedor
            formproveedore.Show();  // Muestra el formulario sin cerrar el formulario principal
        }

        // Botón para abrir el formulario de gestión de productos
        private void button2_Click(object sender, EventArgs e)
        {
            GestionarProducto formproducto = new GestionarProducto();  // Crea una nueva instancia del formulario GestionarProducto
            formproducto.Show();  // Muestra el formulario sin cerrar el formulario principal
        }

        // Botón para abrir el formulario de gestión de reportes
        private void button4_Click(object sender, EventArgs e)
        {
            GestionarReporte formreporte = new GestionarReporte();  // Crea una nueva instancia del formulario GestionarReporte
            formreporte.Show();  // Muestra el formulario sin cerrar el formulario principal
        }

        // Botón para cerrar el formulario principal
        private void button5_Click(object sender, EventArgs e)
        {
            Close();  // Cierra el formulario principal
        }
    }
}

