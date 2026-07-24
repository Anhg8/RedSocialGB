using RedSocialGB.Modelos;
using RedSocialGB.Servicios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormPerfil : FormBase
    {
        #region *************** ATRIBUTOS ***************

        private cUsuario aUsuario;

        // Labels
        private Label lblTitulo;
        private Label lblNombreUsuario;
        private Label lblNombres;
        private Label lblFechaNacimiento;
        private Label lblCelular;

        private Button btnCancelar;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public FormPerfil(cUsuario pUsuario)
        {
            InitializeComponent();

            aUsuario = pUsuario;

            Text = "Red Social GB - Mi Perfil";

            ConfigurarControles();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        private void ConfigurarControles()
        {
            int x = MargenIzquierdo;
            int y = 25;

            ClientSize = new Size(430, 340);

            lblTitulo = CrearTitulo("MI PERFIL", y);

            y += 70;

            lblNombreUsuario = CrearLabel($"Nombre de usuario:  {aUsuario.NombreUsuario}", x, y);
            y += 35;

            lblNombres = CrearLabel($"Nombre completo:  {aUsuario.Nombres} {aUsuario.Apellidos}", x, y);
            y += 35;

            lblFechaNacimiento = CrearLabel(
                $"Fecha de nacimiento:  {aUsuario.FechaNacimiento:dd/MM/yyyy}  ({aUsuario.GetEdad()} años)",
                x, y);
            y += 35;

            lblCelular = CrearLabel($"Número celular:  {aUsuario.Celular}", x, y);
            y += 55;

            btnCancelar = CrearBoton(
                "Cerrar",
                x,
                y,
                150,
                38,
                Color.FromArgb(150, 150, 150),
                Color.White);

            btnCancelar.Click += BtnCancelar_Click;

            ClientSize = new Size(430, y + 70);
        }

        #endregion

        #region *************** EVENTOS ***************

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}