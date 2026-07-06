using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using RedSocialGB.Utilidades;
using System;

namespace RedSocialGB.Servicios
{
    public class ServicioUsuarios
    {
        #region *************** ATRIBUTOS ***************

        private cArbolB aArbolUsuarios;

        #endregion

        #region *************** CONSTRUCTORES ***************

        public ServicioUsuarios(cArbolB pArbolUsuarios)
        {
            aArbolUsuarios = pArbolUsuarios;
        }

        #endregion

        #region *************** MÉTODOS ***************

        // -------------------------------------------------
        public string RegistrarUsuario(string NombreU,string pNombres,
                                       string pApellidos,
                                       DateTime pFechaNacimiento,
                                       string pCelular,
                                       string pContrasena)
        {
            // Validaciones
            if (!cValidaciones.ValidarNombreUsuario(NombreU))
                return "Debe ingresar un nombre de usuario válido.";
            if (!cValidaciones.ValidarCadena(pNombres))
                return "Debe ingresar los nombres.";

            if (!cValidaciones.ValidarCadena(pApellidos))
                return "Debe ingresar los apellidos.";

            if (!cValidaciones.ValidarFechaNacimiento(pFechaNacimiento))
                return "La fecha de nacimiento no es válida.";

            if (!cValidaciones.ValidarCelular(pCelular))
                return "El número de celular debe tener 9 dígitos.";

            if (!cValidaciones.ValidarContrasena(pContrasena))
                return "La contraseña debe tener al menos 4 caracteres.";
            

            if (!cValidaciones.ValidarFechaNacimiento(pFechaNacimiento))
            {
                return "Fecha no valida";
            }
            if (!cValidaciones.ValidarEdad(pFechaNacimiento))
            {
                return "Debe ser mayor de edad.";
            }
            // ¿Ya existe?
            if (ExisteCelular(pCelular))
                return "El número de celular ya está registrado.";

            // Crear usuario
            cUsuario nuevoUsuario = new cUsuario(NombreU,
                pNombres,
                pApellidos,
                pFechaNacimiento,
                pCelular,
                pContrasena);

            // Insertar en el Árbol B
            aArbolUsuarios.Insertar(nuevoUsuario);

            return "Usuario registrado correctamente.";
        }
        // -------------------------------------------------
        public string ModificarUsuario(string pCelular,string pNombreUsuario)
        {
            // Buscar usuario
            cUsuario usuario = BuscarPorCelular(pCelular);

            if (usuario == null)
                return "El usuario no existe.";

            // Validaciones
            if (!cValidaciones.ValidarNombreUsuario(pNombreUsuario))
                return "Debe ingresar un nombre de usuario válido.";


            // Modificar datos
            usuario.NombreUsuario = pNombreUsuario;

            return "Usuario modificado correctamente.";
        }

        // -------------------------------------------------
        public cUsuario BuscarPorCelular(string pCelular)
        {
            return aArbolUsuarios.Buscar(pCelular,u=>u.Celular);
        }

        // -------------------------------------------------
        public bool ExisteCelular(string pCelular)
        {
            return BuscarPorCelular(pCelular) != null;
        }

        // -------------------------------------------------
        public bool EliminarUsuario(string pCelular)
        {
            if (!ExisteCelular(pCelular))
                return false;

            aArbolUsuarios.Eliminar(pCelular);

            return true;
        }

        // -------------------------------------------------
        public cArbolB ObtenerArbol()
        {
            return aArbolUsuarios;
        }

        #endregion
    }
}