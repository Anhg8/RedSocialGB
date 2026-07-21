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
        private Label lblApellidos;
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
            int x = 35;
            int y = 30;
            lblNombreUsuario = CrearLabel($"Nombre de usuario: {aUsuario.NombreUsuario}", x, y);
            y += 22;
            lblNombres = CrearLabel($"Nombre completo: {aUsuario.Nombres} {aUsuario.Apellidos}", x, y);
            y += 40;
            lblCelular = CrearLabel($"Numero celular: {aUsuario.Celular}", x, y);
            y += 40;
            btnCancelar = CrearBoton("Cancelar", x, y, 150, 20, Color.FromArgb(40, 167, 69), Color.White);
            btnCancelar.Click += BtnCancelar_Click;
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        #endregion
    }
}