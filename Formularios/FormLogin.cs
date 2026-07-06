using RedSocialGB.Modelos;
using RedSocialGB.Servicios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormLogin : FormBase
    {
        #region *************** ATRIBUTOS ***************

        private ServicioUsuarios aServicioUsuarios;
        private ServicioAutenticacion aServicioAutenticacion;

        // Labels
        private Label lblTitulo;
        private Label lblCelular;
        private Label lblContrasena;
        private Label lblMensaje;

        // TextBox
        private TextBox txtCelular;
        private TextBox txtContrasena;

        // Botones
        private Button btnIngresar;
        private Button btnRegistrarse;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public FormLogin(ServicioUsuarios pServicioUsuarios,
                         ServicioAutenticacion pServicioAutenticacion)
        {
            InitializeComponent();

            aServicioUsuarios = pServicioUsuarios;
            aServicioAutenticacion = pServicioAutenticacion;

            Text = "Red Social GB - Inicio de Sesión";

            ConfigurarControles();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        private void ConfigurarControles()
        {
            int x = 35;
            int ancho = 360;
            int alto = 32;
            int y = 30;

            lblTitulo = CrearTitulo("RED SOCIAL GB", y);

            y += 70;

            lblCelular = CrearLabel("Celular", x, y);

            y += 22;

            txtCelular = CrearTextBox(x, y, ancho, alto);
            txtCelular.MaxLength = 9;

            y += 50;

            lblContrasena = CrearLabel("Contraseña", x, y);

            y += 22;

            txtContrasena = CrearPassword(x, y, ancho, alto);

            y += 55;

            lblMensaje = CrearMensaje(x, y, ancho);

            y += 40;

            btnIngresar = CrearBoton(
                "Ingresar",
                35,
                y,
                170,
                40,
                Color.FromArgb(30, 80, 160),
                Color.White);

            btnRegistrarse = CrearBoton(
                "Registrarse",
                225,
                y,
                170,
                40,
                Color.FromArgb(40, 167, 69),
                Color.White);

            btnIngresar.Click += BtnIngresar_Click;
            btnRegistrarse.Click += BtnRegistrarse_Click;

            ClientSize = new Size(430, y + 70);
        }

        #endregion

        #region *************** EVENTOS ***************

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            string celular = txtCelular.Text.Trim();
            string contrasena = txtContrasena.Text;

            cUsuario usuario = aServicioAutenticacion.IniciarSesion(celular, contrasena);

            if (usuario == null)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Celular o contraseña incorrectos.";

                txtContrasena.Clear();
                txtContrasena.Focus();

                return;
            }

            lblMensaje.ForeColor = Color.Green;
            lblMensaje.Text = "Bienvenido " + usuario.Nombres + ".";

            MessageBox.Show(
                "Bienvenido " + usuario.Nombres,
                "Inicio de sesión",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Aquí abrirás el formulario principal
            // FormPrincipal frm = new FormPrincipal(usuario, aServicioUsuarios);
            // this.Hide();
            // frm.ShowDialog();
            // this.Show();
        }

        private void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            FormRegistro frm = new FormRegistro(aServicioUsuarios);

            frm.ShowDialog();
        }

        #endregion
    }
}