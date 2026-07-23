using RedSocialGB.Modelos;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormPerfilUsuario : FormBase
    {
        #region *************** ATRIBUTOS ***************

        private cUsuario aUsuario;

        private Label lblTitulo;
        private Label lblNombreUsuario;
        private Label lblNombreCompleto;
        private Label lblFechaNacimiento;
        private Label lblCelular;

        private Button btnCerrar;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public FormPerfilUsuario(cUsuario pUsuario)
        {
            InitializeComponent();

            aUsuario = pUsuario;

            Text = "Red Social GB - Perfil de Usuario";

            ConfigurarControles();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        private void ConfigurarControles()
        {
            ClientSize = new Size(430, 320);

            int x = 35;
            int y = 30;

            lblTitulo = CrearTitulo("PERFIL DE USUARIO", y);

            y += 60;

            lblNombreUsuario = CrearLabel(
                $"Nombre de usuario: {aUsuario.NombreUsuario}",
                x,
                y);

            y += 35;

            lblNombreCompleto = CrearLabel(
                $"Nombre completo: {aUsuario.Nombres} {aUsuario.Apellidos}",
                x,
                y);

            y += 35;

            lblFechaNacimiento = CrearLabel(
                $"Fecha de nacimiento: {aUsuario.FechaNacimiento.ToShortDateString()}",
                x,
                y);

            y += 35;

            lblCelular = CrearLabel(
                $"Número celular: {aUsuario.Celular}",
                x,
                y);

            y += 50;

            btnCerrar = CrearBoton(
                "Cerrar",
                x,
                y,
                150,
                35,
                Color.FromArgb(150, 150, 150),
                Color.White);

            btnCerrar.Click += BtnCerrar_Click;
        }

        #endregion

        #region *************** EVENTOS ***************

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}