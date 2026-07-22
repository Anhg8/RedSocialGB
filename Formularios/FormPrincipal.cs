using RedSocialGB.Estructuras.Grafo;
using RedSocialGB.Modelos;
using RedSocialGB.Servicios;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormPrincipal : FormBase
    {
        #region *************** ATRIBUTOS ***************

        private ServicioAmistades aServicioAmistades;
        private cUsuario aUsuario;

        // Labels
        private Label lblBienvenida;
        private Label lblSugerencias;

        // TextBox
        private TextBox txtBuscador;

        // PictureBox
        private PictureBox picPerfil;

        // Botones menú
        private Button btnPerfil;
        private Button btnSolicitudes;
        private Button btnAmigos;
        private Button btnCerrarSesion;

        // Panel de sugerencias
        private Panel panelSugerencias;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public FormPrincipal(cUsuario pUsuario)
        {
            InitializeComponent();
            aUsuario = pUsuario;
            Text = "Red Social GB - Inicio";

            ConfigurarControles();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        private void ConfigurarControles()
        {
            ClientSize = new Size(900, 540);

            // Buscador (gris)
            txtBuscador = CrearTextBox(MargenIzquierdo, 30, 430, AltoTextBox);
            txtBuscador.BackColor = Color.FromArgb(224, 224, 224);

            // Perfil (arriba derecha)
            lblBienvenida = CrearLabel($"Bienvenido, {aUsuario.Nombres}", 570, 15);

            picPerfil = new PictureBox();
            picPerfil.Size = new Size(90, 90);
            picPerfil.Location = new Point(650, 40);
            picPerfil.BorderStyle = BorderStyle.FixedSingle;
            picPerfil.SizeMode = PictureBoxSizeMode.CenterImage;
            picPerfil.BackColor = Color.White;
            Controls.Add(picPerfil);

            // Botones azules (menú)
            int x = MargenIzquierdo;
            int anchoBtn = 260;
            int altoBtn = 45;
            int y = 150;

            btnPerfil = CrearBoton("Perfil", x, y, anchoBtn, altoBtn,
                Color.FromArgb(51, 51, 204), Color.White);

            y += 55;

            btnSolicitudes = CrearBoton("Solicitudes pendientes", x, y, anchoBtn, altoBtn,
                Color.FromArgb(51, 51, 204), Color.White);

            y += 55;

            btnAmigos = CrearBoton("Amigos", x, y, anchoBtn, altoBtn,
                Color.FromArgb(51, 51, 204), Color.White);

            y += 75;

            // Botón rojo (cerrar sesión)
            btnCerrarSesion = CrearBoton("Cerrar sesión", x, y, anchoBtn, altoBtn,
                Color.FromArgb(220, 30, 30), Color.White);

            // Panel de sugerencias
            panelSugerencias = new Panel();
            panelSugerencias.Location = new Point(570, 150);
            panelSugerencias.Size = new Size(260, 300);
            panelSugerencias.BorderStyle = BorderStyle.FixedSingle;
            panelSugerencias.BackColor = Color.White;
            Controls.Add(panelSugerencias);

            lblSugerencias = new Label();
            lblSugerencias.Text = "sugerencias";
            lblSugerencias.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSugerencias.AutoSize = true;
            lblSugerencias.Location = new Point(10, 8);
            panelSugerencias.Controls.Add(lblSugerencias);

            btnPerfil.Click += BtnPerfil_Click;
            btnSolicitudes.Click += BtnSolicitudes_Click;
            btnAmigos.Click += BtnAmigos_Click;
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
        }

        #endregion

        #region *************** EVENTOS ***************

        private void BtnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfil frm_perfil = new FormPerfil(aUsuario);
            this.Hide();
            frm_perfil.ShowDialog();
            this.Show();
        }

        private void BtnSolicitudes_Click(object sender, EventArgs e)
        {
            // Lógica pendiente
        }

        private void BtnAmigos_Click(object sender, EventArgs e)
        {
            FormAmigos frm_amigos = new FormAmigos(aServicioAmistades.ObtenerAmigos(aUsuario));
            this.Hide();
            frm_amigos.ShowDialog(); 
            this.Show();
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}