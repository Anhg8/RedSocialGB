using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using RedSocialGB.Nucleo;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormResultadosBusqueda : FormBase
    {
        #region *************** ATRIBUTOS ***************

        private Redsocial aSistema;
        private cLista aResultados;

        private Label lblTitulo;
        private Panel panelContenedor;
        private Button btnCerrar;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public FormResultadosBusqueda(Redsocial pSistema, cLista pResultados)
        {
            InitializeComponent();

            aSistema = pSistema;
            aResultados = pResultados;

            Text = "Red Social GB - Resultados de búsqueda";

            ConfigurarControles();
            CargarResultados();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        private void ConfigurarControles()
        {
            ClientSize = new Size(460, 500);

            lblTitulo = CrearTitulo("RESULTADOS DE BÚSQUEDA", 20);

            panelContenedor = new Panel();
            panelContenedor.Location = new Point(MargenIzquierdo, 80);
            panelContenedor.Size = new Size(390, 350);
            panelContenedor.BorderStyle = BorderStyle.FixedSingle;
            panelContenedor.AutoScroll = true;
            panelContenedor.BackColor = Color.White;
            Controls.Add(panelContenedor);

            btnCerrar = CrearBoton(
                "Cerrar",
                MargenIzquierdo,
                445,
                150,
                38,
                Color.FromArgb(150, 150, 150),
                Color.White);

            btnCerrar.Click += BtnCerrar_Click;
        }

        #endregion

        #region *************** MÉTODOS ***************

        private void CargarResultados()
        {
            panelContenedor.Controls.Clear();

            if (aResultados == null || aResultados.EsVacia())
            {
                Label lblVacio = new Label();
                lblVacio.Text = "No se encontraron usuarios.";
                lblVacio.Font = new Font("Segoe UI", 9, FontStyle.Italic);
                lblVacio.ForeColor = Color.Gray;
                lblVacio.AutoSize = true;
                lblVacio.Location = new Point(12, 12);
                panelContenedor.Controls.Add(lblVacio);
                return;
            }

            int y = 10;

            aResultados.ProcesarObjetosLista(obj =>
            {
                if (obj is cUsuario u)
                {
                    CrearFilaUsuario(u, y);
                    y += 65;
                }
            });
        }

        private void CrearFilaUsuario(cUsuario pUsuario, int y)
        {
            Panel fila = new Panel();
            fila.Location = new Point(10, y);
            fila.Size = new Size(panelContenedor.Width - 40, 55);
            fila.BorderStyle = BorderStyle.FixedSingle;
            panelContenedor.Controls.Add(fila);

            Label lblNombre = new Label();
            lblNombre.Text = $"{pUsuario.NombreUsuario}  —  {pUsuario.Nombres} {pUsuario.Apellidos}";
            lblNombre.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(8, 6);
            fila.Controls.Add(lblNombre);

            // Mismo usuario logueado: no mostramos botón de acción
            if (pUsuario.Celular == aSistema.UsuarioActual.Celular)
            {
                Label lblYoMismo = new Label();
                lblYoMismo.Text = "(Tú)";
                lblYoMismo.Font = new Font("Segoe UI", 9, FontStyle.Italic);
                lblYoMismo.ForeColor = Color.Gray;
                lblYoMismo.AutoSize = true;
                lblYoMismo.Location = new Point(8, 28);
                fila.Controls.Add(lblYoMismo);
                return;
            }

            bool sonAmigos = aSistema.ServicioAmistades.SonAmigos(aSistema.UsuarioActual, pUsuario);
            bool existeSolicitud = aSistema.ServicioSolicitud.ExisteSolicitud(aSistema.UsuarioActual.Celular, pUsuario.Celular);

            Button btnAccion = new Button();
            btnAccion.Size = new Size(110, 26);
            btnAccion.Location = new Point(8, 28);
            btnAccion.FlatStyle = FlatStyle.Flat;
            btnAccion.FlatAppearance.BorderSize = 0;
            btnAccion.ForeColor = Color.White;
            fila.Controls.Add(btnAccion);

            if (sonAmigos)
            {
                btnAccion.Text = "Ya son amigos";
                btnAccion.BackColor = Color.FromArgb(150, 150, 150);
                btnAccion.Enabled = false;
            }
            else if (existeSolicitud)
            {
                btnAccion.Text = "Solicitud enviada";
                btnAccion.BackColor = Color.FromArgb(150, 150, 150);
                btnAccion.Enabled = false;
            }
            else
            {
                btnAccion.Text = "Agregar";
                btnAccion.BackColor = Color.FromArgb(40, 167, 69);
                btnAccion.Cursor = Cursors.Hand;

                btnAccion.Click += (s, e) =>
                {
                    aSistema.ServicioSolicitud.EnviarSolicitud(aSistema.UsuarioActual.Celular, pUsuario.Celular);

                    btnAccion.Text = "Solicitud enviada";
                    btnAccion.BackColor = Color.FromArgb(150, 150, 150);
                    btnAccion.Enabled = false;
                };
            }
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