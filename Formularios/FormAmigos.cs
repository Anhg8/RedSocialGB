using RedSocialGB.Estructuras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedSocialGB.Formularios
{
    public partial class FormAmigos : FormBase
    {
        private cLista aListaAmigos = new cLista();
        private ListBox lstAmigos;

        private Button btnCancelar;

        public FormAmigos(cLista pListaAmigos)
        {
            aListaAmigos = pListaAmigos;
            InitializeComponent();
            CrearListaAmigosControl();
            CargarAmigos();

        }
        public void ConfigurarControles()
        {
            btnCancelar = CrearBoton("Cancelar", 35, 150, 150, 20, Color.FromArgb(40, 167, 69), Color.White);
            btnCancelar.Click += BtnCancelar_Click;
        }
        private void CrearListaAmigosControl()
        {
            lstAmigos = new ListBox();
            lstAmigos.Location = new Point(10, 10);
            lstAmigos.Size = new Size(400, 400);
            lstAmigos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(lstAmigos);
        }
        private void CargarAmigos()
        {
            lstAmigos.Items.Clear();
            if (aListaAmigos == null || aListaAmigos.EsVacia()) return;

            aListaAmigos.ProcesarObjetosLista(obj =>
            {
                if (obj is Modelos.cUsuario u)
                    lstAmigos.Items.Add($"{u.NombreUsuario} — {u.Nombres} {u.Apellidos}");
                else
                    lstAmigos.Items.Add(obj?.ToString() ?? "(nulo)");
            });
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
