using RedSocialGB.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormRegistro : Form
    {
        #region *************** ATRIBUTOS ***************

        private ServicioUsuarios aServicio;

        // Labels
        private Label lblTitulo;
        private Label lblNombreUsuario;
        private Label lblNombres;
        private Label lblApellidos;
        private Label lblFechaNacimiento;
        private Label lblCelular;
        private Label lblContrasena;
        private Label lblMensaje;

        // Inputs
        private TextBox txtNombreUsuario;
        private TextBox txtNombres;
        private TextBox txtApellidos;
        private DateTimePicker dtpFechaNacimiento;
        private TextBox txtCelular;
        private TextBox txtContrasena;

        // Botones
        private Button btnRegistrarse;
        private Button btnCancelar;

        #endregion

        #region *************** CONSTRUCTORES ***************

        public FormRegistro(ServicioUsuarios pServicio)
        {
            aServicio = pServicio;
            InitializeComponent();
            ConfigurarFormulario();
            ConfigurarControles();
        }

        #endregion

        #region *************** CONFIGURACIÓN UI ***************

        private void ConfigurarFormulario()
        {
            this.Text = "Red Social GB - Registro";
            this.Size = new Size(460, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
        }

        private void ConfigurarControles()
        {
            int xLabel = 30;
            int xInput = 30;
            int anchoInput = 380;
            int altoInput = 32;
            int espacioEntreGrupos = 8;
            int yActual = 20;

            // ── Título ────────────────────────────────────────────────
            lblTitulo = new Label
            {
                Text = "RED SOCIAL GB",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 80, 160),
                AutoSize = true,
                Location = new Point(130, yActual)
            };
            this.Controls.Add(lblTitulo);
            yActual += 50;

            // ── Nombre de usuario ─────────────────────────────────────
            lblNombreUsuario = CrearLabel("Nombre de usuario", xLabel, yActual);
            yActual += 22;
            txtNombreUsuario = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            yActual += altoInput + espacioEntreGrupos;

            // ── Nombres ───────────────────────────────────────────────
            lblNombres = CrearLabel("Nombres", xLabel, yActual);
            yActual += 22;
            txtNombres = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            yActual += altoInput + espacioEntreGrupos;

            // ── Apellidos ─────────────────────────────────────────────
            lblApellidos = CrearLabel("Apellidos", xLabel, yActual);
            yActual += 22;
            txtApellidos = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            yActual += altoInput + espacioEntreGrupos;

            // ── Fecha de nacimiento ───────────────────────────────────
            lblFechaNacimiento = CrearLabel("Fecha de nacimiento", xLabel, yActual);
            yActual += 22;
            dtpFechaNacimiento = new DateTimePicker
            {
                Location = new Point(xInput, yActual),
                Size = new Size(anchoInput, altoInput),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10),
                MaxDate = DateTime.Today
            };
            this.Controls.Add(dtpFechaNacimiento);
            yActual += altoInput + espacioEntreGrupos;

            // ── Celular ───────────────────────────────────────────────
            lblCelular = CrearLabel("Celular", xLabel, yActual);
            yActual += 22;
            txtCelular = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            txtCelular.MaxLength = 9;
            yActual += altoInput + espacioEntreGrupos;

            // ── Contraseña ────────────────────────────────────────────
            lblContrasena = CrearLabel("Contraseña", xLabel, yActual);
            yActual += 22;
            txtContrasena = CrearTextBox(xInput, yActual, anchoInput, altoInput);
            txtContrasena.PasswordChar = '●';
            yActual += altoInput + 14;

            // ── Mensaje de resultado ──────────────────────────────────
            lblMensaje = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Red,
                Size = new Size(anchoInput, 20),
                Location = new Point(xLabel, yActual),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblMensaje);
            yActual += 26;

            // ── Botones ───────────────────────────────────────────────
            btnRegistrarse = new Button
            {
                Text = "Registrarse",
                Size = new Size(150, 38),
                Location = new Point(70, yActual),
                BackColor = Color.FromArgb(30, 80, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegistrarse.FlatAppearance.BorderSize = 0;
            btnRegistrarse.Click += BtnRegistrarse_Click;
            this.Controls.Add(btnRegistrarse);

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Size = new Size(150, 38),
                Location = new Point(240, yActual),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += BtnCancelar_Click;
            this.Controls.Add(btnCancelar);

            yActual += 60;
            this.ClientSize = new Size(460, yActual);
        }

        // ── Helpers para no repetir código ───────────────────────────
        private Label CrearLabel(string texto, int x, int y)
        {
            Label lbl = new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = true,
                Location = new Point(x, y)
            };
            this.Controls.Add(lbl);
            return lbl;
        }

        private TextBox CrearTextBox(int x, int y, int ancho, int alto)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(ancho, alto),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txt);
            return txt;
        }

        #endregion

        #region *************** EVENTOS ***************

        private void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            string resultado = aServicio.RegistrarUsuario(
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
                MessageBox.Show(resultado, "Registro exitoso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            this.Close();
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
        }

        #endregion
    }
}


