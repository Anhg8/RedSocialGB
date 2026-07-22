using RedSocialGB.Estructuras.Grafo;
using RedSocialGB.Modelos;
using RedSocialGB.Nucleo;
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

        private Redsocial aSistema;

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

        public FormPrincipal(Redsocial pSistema)
        {
            InitializeComponent();

            aSistema = pSistema;

            Text = "Red Social GB - Inicio";

            ConfigurarControles();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        private void ConfigurarControles()
        {
            ClientSize = new Size(900, 540);

            int margenIzq = MargenIzquierdo;
            int margenDer = 570;
            int anchoDerecho = 900 - margenDer - MargenIzquierdo; // 295

            // ---------- ENCABEZADO ----------

            // Buscador (gris)
            txtBuscador = CrearTextBox(margenIzq, 30, margenDer - margenIzq - 25, AltoTextBox);
            txtBuscador.BackColor = Color.FromArgb(224, 224, 224);

            // Perfil (arriba derecha)
            picPerfil = new PictureBox();
            picPerfil.Size = new Size(70, 70);
            picPerfil.Location = new Point(900 - MargenIzquierdo - 70, 25);
            picPerfil.BorderStyle = BorderStyle.FixedSingle;
            picPerfil.SizeMode = PictureBoxSizeMode.CenterImage;
            picPerfil.BackColor = Color.White;
            Controls.Add(picPerfil);

            lblBienvenida = new Label();
            lblBienvenida.Text = $"Bienvenido, {aSistema.UsuarioActual.Nombres}";
            lblBienvenida.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblBienvenida.AutoSize = false;
            lblBienvenida.TextAlign = ContentAlignment.MiddleRight;
            lblBienvenida.Size = new Size(picPerfil.Left - margenDer, 30);
            lblBienvenida.Location = new Point(margenDer, picPerfil.Top + (picPerfil.Height - 30) / 2);
            Controls.Add(lblBienvenida);

            // ---------- BLOQUE MENÚ (izquierda) ----------

            int x = margenIzq;
            int anchoBtn = margenDer - margenIzq - 25; // mismo ancho que el buscador
            int altoBtn = 45;
            int espaciado = 15;
            int yMenu = 150;

            btnPerfil = CrearBoton("Perfil", x, yMenu, anchoBtn, altoBtn,
                Color.FromArgb(51, 51, 204), Color.White);
            yMenu += altoBtn + espaciado;

            btnSolicitudes = CrearBoton("Solicitudes pendientes", x, yMenu, anchoBtn, altoBtn,
                Color.FromArgb(51, 51, 204), Color.White);
            yMenu += altoBtn + espaciado;

            btnAmigos = CrearBoton("Amigos", x, yMenu, anchoBtn, altoBtn,
                Color.FromArgb(51, 51, 204), Color.White);
            yMenu += altoBtn + espaciado * 2;

            btnCerrarSesion = CrearBoton("Cerrar sesión", x, yMenu, anchoBtn, altoBtn,
                Color.FromArgb(220, 30, 30), Color.White);

            // ---------- BLOQUE SUGERENCIAS (derecha) ----------

            panelSugerencias = new Panel();
            panelSugerencias.Location = new Point(margenDer, 150);
            panelSugerencias.Size = new Size(anchoDerecho, yMenu + altoBtn - 150);
            panelSugerencias.BorderStyle = BorderStyle.FixedSingle;
            panelSugerencias.BackColor = Color.White;
            Controls.Add(panelSugerencias);

            lblSugerencias = new Label();
            lblSugerencias.Text = "Sugerencias";
            lblSugerencias.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSugerencias.ForeColor = Color.FromArgb(60, 60, 60);
            lblSugerencias.AutoSize = true;
            lblSugerencias.Location = new Point(12, 10);
            panelSugerencias.Controls.Add(lblSugerencias);

            // ---------- EVENTOS ----------

            btnPerfil.Click += BtnPerfil_Click;
            btnSolicitudes.Click += BtnSolicitudes_Click;
            btnAmigos.Click += BtnAmigos_Click;
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
        }

        #endregion

        #region *************** EVENTOS ***************

        private void BtnPerfil_Click(object sender, EventArgs e)
        {
            FormPerfil frm_perfil = new FormPerfil(aSistema.UsuarioActual);
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
            FormAmigos frm_amigos = new FormAmigos(aSistema);
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