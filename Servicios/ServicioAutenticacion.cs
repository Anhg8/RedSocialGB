using RedSocialGB.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Servicios
{
    public class ServicioAutenticacion
    {
        #region **************** ATRIBUTOS ****************

        private ServicioUsuarios aServicioUsuarios;

        #endregion

        #region **************** CONSTRUCTORES ****************

        public ServicioAutenticacion(ServicioUsuarios pServicioUsuarios)
        {
            aServicioUsuarios = pServicioUsuarios;
        }
        #endregion

        #region **************** MÉTODOS ****************

        // -------------------------------------------------
        public string IniciarSesion(string pCelular, string pContrasena)
        {
            // Validar datos de entrada
            if (!Utilidades.cValidaciones.ValidarCelular(pCelular))
                return "Numero no Válido";

            if (!Utilidades.cValidaciones.ValidarContrasena(pContrasena))
                return "Constrasena no Válida";

            // Buscar usuario
            cUsuario usuario = aServicioUsuarios.BuscarPorCelular(pCelular);

            if (usuario == null)
                return "Usuario no encontrado";

            // Verificar contraseña
            if (usuario.Contrasena != pContrasena)
                return "Constraseña incorrecta";

            // Login correcto
            return "Usuario inicio sesion correctamente.";
        }

        // -------------------------------------------------
        public cUsuario Autenticar(string pCelular, string pContrasena)
        {
            string rpta = IniciarSesion(pCelular, pContrasena);
            if (rpta == "Usuario inicio sesion correctamente.")
            {
                return aServicioUsuarios.BuscarPorCelular(pCelular);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
