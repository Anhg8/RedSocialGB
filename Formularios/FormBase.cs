using RedSocialGB.Servicios;
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
    public partial class FormBase : Form
    {
        #region *************** CONSTRUCTOR ***************
        protected const int MargenIzquierdo = 35;
        protected const int AnchoTextBox = 360;
        protected const int AltoTextBox = 32;
        public FormBase()
        {
            ConfigurarFormularioBase();
        }

        #endregion

        #region *************** CONFIGURACIÓN ***************

        protected virtual void ConfigurarFormularioBase()
        {
            Text = "Red Social GB";

            Size = new Size(460, 560);

            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.FixedSingle;

            MaximizeBox = false;

            MinimizeBox = false;

            BackColor = Color.White;

            Font = new Font("Segoe UI", 10);
        }

        #endregion

        #region *************** HELPERS ***************

        protected Label CrearLabel(string texto, int x, int y)
        {
            Label lbl = new Label();

            lbl.Text = texto;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(60, 60, 60);

            Controls.Add(lbl);

            return lbl;
        }

        protected TextBox CrearTextBox(int x, int y, int ancho, int alto)
        {
            TextBox txt = new TextBox();

            txt.Location = new Point(x, y);
            txt.Size = new Size(ancho, alto);
            txt.Font = new Font("Segoe UI", 10);
            txt.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(txt);

            return txt;
        }

        protected Button CrearBoton(
            string texto,
            int x,
            int y,
            int ancho,
            int alto,
            Color colorFondo,
            Color colorTexto)
        {
            Button btn = new Button();

            btn.Text = texto;
            btn.Location = new Point(x, y);
            btn.Size = new Size(ancho, alto);

            btn.BackColor = colorFondo;
            btn.ForeColor = colorTexto;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            btn.Cursor = Cursors.Hand;

            Controls.Add(btn);

            return btn;
        }

        protected DateTimePicker CrearDatePicker(int x, int y, int ancho, int alto)
        {
            DateTimePicker dtp = new DateTimePicker();

            dtp.Location = new Point(x, y);
            dtp.Size = new Size(ancho, alto);
            dtp.Font = new Font("Segoe UI", 10);
            dtp.Format = DateTimePickerFormat.Short;
            dtp.MaxDate = DateTime.Today;

            Controls.Add(dtp);

            return dtp;
        }

        protected Label CrearTitulo(string texto, int x, int y)
        {
            Label lbl = new Label();

            lbl.Text = texto;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;

            lbl.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            lbl.ForeColor = Color.FromArgb(30, 80, 160);

            Controls.Add(lbl);

            return lbl;
        }
        protected TextBox CrearPassword(int x, int y, int ancho, int alto)
        {
            TextBox txt = CrearTextBox(x, y, ancho, alto);
            txt.PasswordChar = '●';
            return txt;
        }
        protected Label CrearMensaje(int x, int y, int ancho)
        {
            Label lbl = new Label();

            lbl.Location = new Point(x, y);

            lbl.Size = new Size(ancho, 20);

            lbl.TextAlign = ContentAlignment.MiddleCenter;

            lbl.Font = new Font("Segoe UI", 9, FontStyle.Italic);

            lbl.ForeColor = Color.Red;

            Controls.Add(lbl);

            return lbl;
        }
        protected Label CrearTitulo(string texto, int y)
        {
            Label lbl = new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 80, 160),
                AutoSize = true
            };

            lbl.Location = new Point((ClientSize.Width - lbl.PreferredWidth) / 2, y);

            Controls.Add(lbl);

            return lbl;
        }

        #endregion
    }
}