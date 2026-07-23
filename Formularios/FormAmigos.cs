using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using RedSocialGB.Nucleo;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormAmigos : FormBase
    {
        #region *************** ATRIBUTOS ***************

        private Redsocial aSistema;

        private FlowLayoutPanel pnlAmigos;
        private Label lblTitulo;
        private Button btnCancelar;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public FormAmigos(Redsocial pSistema)
        {
            InitializeComponent();

            aSistema = pSistema;

            Text = "Red Social GB - Amigos";

            ConfigurarControles();
            CargarAmigos();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        private void ConfigurarControles()
        {
            ClientSize = new Size(430, 500);

            lblTitulo = CrearTitulo("MIS AMIGOS", 20);

            pnlAmigos = new FlowLayoutPanel();
            pnlAmigos.Location = new Point(MargenIzquierdo, 80);
            pnlAmigos.Size = new Size(360, 350);
            pnlAmigos.AutoScroll = true;
            pnlAmigos.FlowDirection = FlowDirection.TopDown;
            pnlAmigos.WrapContents = false;
            pnlAmigos.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(pnlAmigos);

            btnCancelar = CrearBoton(
                "Cerrar",
                MargenIzquierdo,
                445,
                150,
                35,
                Color.FromArgb(150, 150, 150),
                Color.White);

            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancelar.Click += BtnCancelar_Click;
        }

        #endregion

        #region *************** MÉTODOS ***************

        private void CargarAmigos()
        {
            pnlAmigos.Controls.Clear();

            cLista amigos =
                aSistema.ServicioAmistades.ObtenerAmigos(aSistema.UsuarioActual);

            if (amigos == null || amigos.EsVacia())
            {
                Label lbl = new Label();
                lbl.Text = "Aún no tienes amigos.";
                lbl.AutoSize = true;

                pnlAmigos.Controls.Add(lbl);
                return;
            }

            amigos.ProcesarObjetosLista(obj =>
            {
                if (obj is cUsuario u)
                    pnlAmigos.Controls.Add(CrearPanelAmigo(u));
            });
        }
        private Panel CrearPanelAmigo(cUsuario amigo)
        {
            Panel panel = new Panel();

            panel.Width = pnlAmigos.ClientSize.Width - 25;
            panel.Height = 60;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = Color.White;

            Label lblNombre = new Label();

            lblNombre.Text = $"{amigo.Nombres} {amigo.Apellidos}";
            lblNombre.Location = new Point(10, 8);
            lblNombre.AutoSize = true;

            Label lblUsuario = new Label();

            lblUsuario.Text = "@" + amigo.NombreUsuario;
            lblUsuario.Location = new Point(10, 30);
            lblUsuario.AutoSize = true;

            Button btnPerfil = new Button();

            btnPerfil.Text = "Perfil";
            btnPerfil.Size = new Size(55, 28);
            btnPerfil.Location = new Point(180, 15);

            btnPerfil.Tag = amigo;
            btnPerfil.Click += BtnPerfil_Click;

            Button btnEliminar = new Button();

            btnEliminar.Text = "Eliminar";
            btnEliminar.Size = new Size(75, 28);
            btnEliminar.Location = new Point(250, 15);

            btnEliminar.Tag = amigo;
            btnEliminar.Click += BtnEliminar_Click;

            panel.Controls.Add(lblNombre);
            panel.Controls.Add(lblUsuario);
            panel.Controls.Add(btnPerfil);
            panel.Controls.Add(btnEliminar);

            return panel;
        }

        #endregion

        #region *************** EVENTOS ***************

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void BtnPerfil_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn?.Tag is cUsuario usuario)
            {
                FormPerfilUsuario frm = new FormPerfilUsuario(usuario);

                frm.ShowDialog();
            }
        }
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (!(btn?.Tag is cUsuario amigo))
                return;

            DialogResult respuesta = MessageBox.Show(
                $"¿Deseas eliminar a {amigo.Nombres} {amigo.Apellidos} de tu lista de amigos?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            aSistema.ServicioAmistades.EliminarAmistad(
                aSistema.UsuarioActual,
                amigo);

            CargarAmigos();

            MessageBox.Show(
                "La amistad fue eliminada correctamente.",
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        #endregion
    }
}