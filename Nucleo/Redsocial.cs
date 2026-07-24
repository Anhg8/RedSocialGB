using RedSocialGB.Datos;
using RedSocialGB.Estructuras;
using RedSocialGB.Estructuras.Grafo;
using RedSocialGB.Modelos;
using RedSocialGB.Servicios;

namespace RedSocialGB.Nucleo
{
    public class Redsocial
    {
        #region *************** ATRIBUTOS ***************

        // Estructuras principales
        private cArbolB aArbolUsuarios;
        private cGrafo aGrafoUsuarios;

        // Servicios
        private ServicioUsuarios aServicioUsuarios;
        private ServicioAutenticacion aServicioAutenticacion;
        private ServicioAmistades aServicioAmistades;
        private ServicioSolicitud aServicioSolicitud;

        // Usuario que tiene la sesión iniciada
        private cUsuario aUsuarioActual;

        #endregion

        #region *************** PROPIEDADES ***************

        public ServicioUsuarios ServicioUsuarios
        {
            get => aServicioUsuarios;
        }

        public ServicioAutenticacion ServicioAutenticacion
        {
            get => aServicioAutenticacion;
        }

        public ServicioAmistades ServicioAmistades
        {
            get => aServicioAmistades;
        }

        internal ServicioSolicitud ServicioSolicitud
        {
            get => aServicioSolicitud;
        }

        public cUsuario UsuarioActual
        {
            get => aUsuarioActual;
            set => aUsuarioActual = value;
        }

        #endregion

        #region *************** CONSTRUCTOR ***************

        public Redsocial()
        {
            // Crear estructuras principales
            SolicitudDAO solicitudDAO = new SolicitudDAO();
            AmistadDAO amistadDAO = new AmistadDAO();
            UsuarioDAO usuarioDAO = new UsuarioDAO();

            aArbolUsuarios = usuarioDAO.CargarDatos();
            aGrafoUsuarios = amistadDAO.CargarDatos();

            // Crear servicios
            aServicioUsuarios = new ServicioUsuarios(
                aArbolUsuarios,
                aGrafoUsuarios,usuarioDAO);

            aServicioAmistades = new ServicioAmistades(
                aGrafoUsuarios,amistadDAO);

            aServicioAutenticacion = new ServicioAutenticacion(
                aServicioUsuarios);

            aServicioSolicitud = new ServicioSolicitud(
                aServicioUsuarios,
                aServicioAmistades,solicitudDAO);

            // Aún no existe un usuario autenticado
            aUsuarioActual = null;
        }

        #endregion
    }
}