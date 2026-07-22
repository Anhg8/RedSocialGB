using RedSocialGB.Servicios;
using System;
using System.Drawing;
using System.Windows.Forms;
using RedSocialGB.Nucleo;
using RedSocialGB.Modelos;

namespace RedSocialGB.Formularios
{
    public partial class FormRegistro : FormBase
    {
        #region *************** ATRIBUTOS ***************

        private Redsocial aSistema;

        private Label lblTitulo;
        private Label lblNombreUsuario;
        private Label lblNombres;
        private Label lblApellidos;
        private Label lblFechaNacimiento;
        private Label lblCelular;
        private Label lblContrasena;
        private Label lblMensaje;

        private TextBox txtNombreUsuario;
        private TextBox txtNombres;
        private TextBox txtApellidos;
        private DateTimePicker dtpFechaNacimiento;
        private TextBox txtCelular;
        private TextBox txtContrasena;

        private Button btnRegistrarse;
        private Button btnCancelar;

        #endregion

        #region *************** CONSTRUCTORES ***************

        public FormRegistro(Redsocial pSistema)
        {
            InitializeComponent();
            aSistema = pSistema;
            Text = "Red Social GB - Registro";
            ConfigurarControles();
        }

        #endregion

        #region *************** CONFIGURACIÓN UI ***************

        private void ConfigurarControles()
        {
            int xLabel = 30;
            int xInput = 30;
            int anchoInput = 380;
            int altoInput = 32;
            int espacioEntreGrupos = 8;
            int yActual = 20;

            lblTitulo = CrearTitulo("RED SOCIAL GB", yActual);
            yActual += 50;

            lblNombreUsuario = CrearLabel("Nombre de usuario", xLabel, yActual);
            yActual += 22;
            txtNombreUsuario = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            yActual += altoInput + espacioEntreGrupos;

            lblNombres = CrearLabel("Nombres", xLabel, yActual);
            yActual += 22;
            txtNombres = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            yActual += altoInput + espacioEntreGrupos;

            lblApellidos = CrearLabel("Apellidos", xLabel, yActual);
            yActual += 22;
            txtApellidos = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            yActual += altoInput + espacioEntreGrupos;

            lblFechaNacimiento = CrearLabel("Fecha de nacimiento", xLabel, yActual);
            yActual += 22;
            dtpFechaNacimiento = CrearDatePicker(xInput, yActual, anchoInput, altoInput);
            yActual += altoInput + espacioEntreGrupos;

            lblCelular = CrearLabel("Celular", xLabel, yActual);
            yActual += 22;
            txtCelular = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            txtCelular.MaxLength = 9;
            yActual += altoInput + espacioEntreGrupos;

            lblContrasena = CrearLabel("Contraseña", xLabel, yActual);
            yActual += 22;
            txtContrasena = CrearPassword(xInput, yActual, anchoInput, altoInput);
            yActual += altoInput + 14;

            lblMensaje = CrearMensaje(xLabel, yActual, anchoInput);
            yActual += 26;

            btnRegistrarse = CrearBoton("Registrarse", 70, yActual, 150, 38,
                                    Color.FromArgb(30, 80, 160), Color.White);

            btnCancelar = CrearBoton("Cancelar", 240, yActual, 150, 38,
                                    Color.FromArgb(220, 53, 69), Color.White);

            btnRegistrarse.Click += BtnRegistrarse_Click;
            btnCancelar.Click += BtnCancelar_Click;

            yActual += 60;
            ClientSize = new Size(460, yActual);
        }

        #endregion 

        #region *************** EVENTOS ***************

        private void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            string resultado = aSistema.ServicioUsuarios.RegistrarUsuario(
                txtNombreUsuario.Text.Trim(),
                txtNombres.Text.Trim(),
                txtApellidos.Text.Trim(),
                dtpFechaNacimiento.Value,
                txtCelular.Text.Trim(),
                txtContrasena.Text
            );

            bool exito = resultado == "Usuario registrado correctamente.";

            lblMensaje.ForeColor = exito ? Color.Green : Color.Red;
            lblMensaje.Text = resultado;

            if (exito)
            {
                aSistema.UsuarioActual =
                    aSistema.ServicioUsuarios.BuscarPorCelular(
                        txtCelular.Text.Trim());

                FormPrincipal frm_principal = new FormPrincipal(aSistema);

            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            Close();
        }

        #endregion  

        #region *************** UTILIDADES ***************

        private void LimpiarCampos()
        {
            txtNombreUsuario.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtCelular.Clear();
            txtContrasena.Clear();

            dtpFechaNacimiento.Value = DateTime.Today;

            lblMensaje.Text = "";

            txtNombreUsuario.Focus();
        }

        #endregion

    }
}