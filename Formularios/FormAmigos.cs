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

        private ListBox lstAmigos;
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

            lstAmigos = new ListBox();
            lstAmigos.Location = new Point(MargenIzquierdo, 80);
            lstAmigos.Size = new Size(360, 350);
            lstAmigos.Font = new Font("Segoe UI", 10);
            lstAmigos.BorderStyle = BorderStyle.FixedSingle;
            lstAmigos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(lstAmigos);

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
            lstAmigos.Items.Clear();

            cLista amigos = aSistema.ServicioAmistades.ObtenerAmigos(aSistema.UsuarioActual);

            if (amigos == null || amigos.EsVacia())
            {
                lstAmigos.Items.Add("Aún no tienes amigos agregados.");
                return;
            }

            amigos.ProcesarObjetosLista(obj =>
            {
                if (obj is cUsuario u)
                    lstAmigos.Items.Add($"{u.NombreUsuario}  —  {u.Nombres} {u.Apellidos}");
            });
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