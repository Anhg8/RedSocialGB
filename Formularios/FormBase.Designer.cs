using System.ComponentModel;

namespace RedSocialGB.Formularios
{
    partial class FormBase
    {
        /// <summary>
        /// Contenedor de componentes.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Libera los recursos utilizados por el formulario.
        /// </summary>
        /// <param name="disposing">
        /// true si se deben liberar recursos administrados; false en caso contrario.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Método requerido para el Diseñador.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
        }
    }
}