using RedSocialGB.Estructuras;
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
            CargarSugerencias();
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
            txtBuscador.KeyDown += TxtBuscador_KeyDown;

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

            // ---------- EVENTOS ----------

            btnPerfil.Click += BtnPerfil_Click;
            btnSolicitudes.Click += BtnSolicitudes_Click;
            btnAmigos.Click += BtnAmigos_Click;
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
        }

        private void CargarSugerencias()
        {
            panelSugerencias.Controls.Clear();

            lblSugerencias = new Label();
            lblSugerencias.Text = "Sugerencias";
            lblSugerencias.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSugerencias.ForeColor = Color.FromArgb(60, 60, 60);
            lblSugerencias.AutoSize = true;
            lblSugerencias.Location = new Point(12, 10);
            panelSugerencias.Controls.Add(lblSugerencias);

            cLista sugerencias = aSistema.ServicioAmistades.ObtenerAmigosDeAmigos(aSistema.UsuarioActual);

            if (sugerencias == null || sugerencias.EsVacia())
            {
                Label lblVacio = new Label();
                lblVacio.Text = "No hay sugerencias por ahora.";
                lblVacio.Font = new Font("Segoe UI", 9, FontStyle.Italic);
                lblVacio.ForeColor = Color.Gray;
                lblVacio.AutoSize = true;
                lblVacio.Location = new Point(12, 45);
                panelSugerencias.Controls.Add(lblVacio);
                return;
            }

            int y = 45;
            int contador = 0;

            sugerencias.ProcesarObjetosLista(obj =>
            {
                if (contador >= 5) return; // límite visual, evita overflow del panel

                if (obj is cUsuario u)
                {
                    CrearFilaSugerencia(u, y);
                    y += 65;
                    contador++;
                }
            });
        }

        private void CrearFilaSugerencia(cUsuario pUsuario, int y)
        {
            Panel fila = new Panel();
            fila.Location = new Point(10, y);
            fila.Size = new Size(panelSugerencias.Width - 25, 55);
            fila.BorderStyle = BorderStyle.FixedSingle;
            panelSugerencias.Controls.Add(fila);

            Label lblNombre = new Label();
            lblNombre.Text = $"{pUsuario.Nombres} {pUsuario.Apellidos}";
            lblNombre.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(8, 6);
            fila.Controls.Add(lblNombre);

            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar";
            btnAgregar.Size = new Size(90, 26);
            btnAgregar.Location = new Point(8, 26);
            btnAgregar.BackColor = Color.FromArgb(40, 167, 69);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.Cursor = Cursors.Hand;
            fila.Controls.Add(btnAgregar);

            btnAgregar.Click += (s, e) =>
            {
                aSistema.ServicioSolicitud.EnviarSolicitud(aSistema.UsuarioActual.Celular, pUsuario.Celular);
                MessageBox.Show("Solicitud enviada.", "Red Social GB",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                fila.Enabled = false;
                btnAgregar.Text = "Enviada";
            };
        }

        private void TxtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BuscarUsuarios();
            }
        }

        private void BuscarUsuarios()
        {
            string texto = txtBuscador.Text.Trim();

            if (string.IsNullOrEmpty(texto))
                return;

            cLista resultados = aSistema.ServicioUsuarios.BuscarUsuarios(texto);

            FormResultadosBusqueda frm = new FormResultadosBusqueda(aSistema, resultados);
            this.Hide();
            frm.ShowDialog();
            this.Show();
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
            FormSolicitudes frm_solicitudes = new FormSolicitudes(aSistema);
            this.Hide();
            frm_solicitudes.ShowDialog();
            this.Show();
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