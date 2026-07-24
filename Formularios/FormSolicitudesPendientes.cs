using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using RedSocialGB.Nucleo;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormSolicitudes : FormBase
    {
        #region *************** ATRIBUTOS ***************

        private Redsocial aSistema;

        private FlowLayoutPanel pnlSolicitudes;
        private Label lblTitulo;
        private Button btnCerrar;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public FormSolicitudes(Redsocial pSistema)
        {
            InitializeComponent();

            aSistema = pSistema;

            Text = "Red Social GB - Solicitudes";

            ConfigurarControles();
            CargarSolicitudes();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        private void ConfigurarControles()
        {
            ClientSize = new Size(470, 520);

            lblTitulo = CrearTitulo("SOLICITUDES PENDIENTES", 20);

            pnlSolicitudes = new FlowLayoutPanel();
            pnlSolicitudes.Location = new Point(MargenIzquierdo, 80);
            pnlSolicitudes.Size = new Size(400, 350);
            pnlSolicitudes.AutoScroll = true;
            pnlSolicitudes.FlowDirection = FlowDirection.TopDown;
            pnlSolicitudes.WrapContents = false;
            pnlSolicitudes.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(pnlSolicitudes);

            btnCerrar = CrearBoton(
                "Cerrar",
                MargenIzquierdo,
                445,
                150,
                35,
                Color.FromArgb(150, 150, 150),
                Color.White);

            btnCerrar.Click += BtnCerrar_Click;
        }

        #endregion

        #region *************** MÉTODOS ***************

        private void CargarSolicitudes()
        {
            pnlSolicitudes.Controls.Clear();

            cLista solicitudes = aSistema.ServicioSolicitud
                .ObtenerSolicitudesPendientes(aSistema.UsuarioActual);

            if (solicitudes == null || solicitudes.EsVacia())
            {
                Label lbl = new Label();
                lbl.Text = "No tienes solicitudes pendientes.";
                lbl.AutoSize = true;
                lbl.Font = new Font("Segoe UI", 10);

                pnlSolicitudes.Controls.Add(lbl);
                return;
            }

            solicitudes.ProcesarObjetosLista(obj =>
            {
                if (obj is cSolicitudAmistad solicitud)
                {
                    pnlSolicitudes.Controls.Add(CrearPanelSolicitud(solicitud));
                }
            });
        }

        private Panel CrearPanelSolicitud(cSolicitudAmistad solicitud)
        {
            Panel panel = new Panel();

            panel.Width = 370;
            panel.Height = 70;
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;

            Label lblNombre = new Label();

            // Mostrar el nombre del EMISOR
            lblNombre.Text = $"{solicitud.Remitente.Nombres} {solicitud.Remitente.Apellidos}";

            lblNombre.Location = new Point(10, 8);
            lblNombre.AutoSize = true;

            Label lblUsuario = new Label();

            // Mostrar el nombre de usuario del EMISOR
            lblUsuario.Text = $"{solicitud.Remitente.NombreUsuario}";

            lblUsuario.Location = new Point(10, 28);
            lblUsuario.AutoSize = true;

            Label lblFecha = new Label();

            // Mostrar la fecha de la solicitud
            lblFecha.Text = $"{solicitud.FechaEnvio}";

            lblFecha.Location = new Point(10, 48);
            lblFecha.AutoSize = true;

            Button btnPerfil = new Button();

            btnPerfil.Text = "Perfil";
            btnPerfil.Size = new Size(55, 28);
            btnPerfil.Location = new Point(170, 20);

            btnPerfil.Tag = solicitud.Remitente;
            btnPerfil.Click += BtnPerfil_Click;

            Button btnAceptar = new Button();

            btnAceptar.Text = "Aceptar";
            btnAceptar.Size = new Size(70, 28);
            btnAceptar.Location = new Point(230, 20);

            btnAceptar.Tag = solicitud.Remitente;
            btnAceptar.Click += BtnAceptar_Click;

            Button btnRechazar = new Button();

            btnRechazar.Text = "Rechazar";
            btnRechazar.Size = new Size(75, 28);
            btnRechazar.Location = new Point(305, 20);

            btnRechazar.Tag = solicitud.Remitente;
            btnRechazar.Click += BtnRechazar_Click;

            panel.Controls.Add(lblNombre);
            panel.Controls.Add(lblUsuario);
            panel.Controls.Add(lblFecha);

            panel.Controls.Add(btnPerfil);
            panel.Controls.Add(btnAceptar);
            panel.Controls.Add(btnRechazar);

            return panel;
        }

        #endregion

        #region *************** EVENTOS ***************

        private void BtnPerfil_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is cUsuario usuario)
            {
                FormPerfilUsuario frm_usuario = new FormPerfilUsuario(usuario);
                this.Close();
                frm_usuario.ShowDialog();
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is cUsuario usuario)
            {
                aSistema.ServicioSolicitud.AceptarSolicitud(aSistema.UsuarioActual.Celular, usuario.Celular);
                MessageBox.Show(
                "La amistad fue eliminada correctamente.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

        private void BtnRechazar_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is cUsuario usuario)
            {
                aSistema.ServicioSolicitud.RechazarSolicitud(aSistema.UsuarioActual.Celular, usuario.Celular);
                MessageBox.Show(
                "La amistad fue eliminada correctamente.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}