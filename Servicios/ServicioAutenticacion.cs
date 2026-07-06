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
        public cUsuario IniciarSesion(string pCelular, string pContrasena)
        {
            // Validar datos de entrada
            if (!Utilidades.cValidaciones.ValidarCelular(pCelular))
                return null;

            if (!Utilidades.cValidaciones.ValidarContrasena(pContrasena))
                return null;

            // Buscar usuario
            cUsuario usuario = aServicioUsuarios.BuscarPorCelular(pCelular);

            if (usuario == null)
                return null;

            // Verificar contraseña
            if (usuario.Contrasena != pContrasena)
                return null;

            // Login correcto
            return usuario;
        }

        // -------------------------------------------------
        public bool Autenticar(string pCelular, string pContrasena)
        {
            return IniciarSesion(pCelular, pContrasena) != null;
        }
        #endregion
    }
}
