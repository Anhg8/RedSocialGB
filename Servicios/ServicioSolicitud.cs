using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Servicios
{
    internal class ServicioSolicitud
    {
        #region *************** ATRIBUTOS ***************

        private Estructuras.cLista aSolicitudesPendientes;
        private ServicioUsuarios aServicioUsuarios;
        private ServicioAmistades aServicioAmistades;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public ServicioSolicitud(
            ServicioUsuarios pServicioUsuarios,
            ServicioAmistades pServicioAmistades)
        {
            aSolicitudesPendientes = new Estructuras.cLista();

            aServicioUsuarios = pServicioUsuarios;
            aServicioAmistades = pServicioAmistades;
        }

        #endregion

        #region *************** MÉTODOS ***************

        //-------------------------------------------------------
        public string EnviarSolicitud(string pCelularRemitente,
                                      string pCelularDestinatario)
        {
            if (pCelularRemitente == pCelularDestinatario)
                return "No puede enviarse una solicitud a usted mismo.";

            cUsuario remitente =
                aServicioUsuarios.BuscarPorCelular(pCelularRemitente);

            cUsuario destinatario =
                aServicioUsuarios.BuscarPorCelular(pCelularDestinatario);

            if (remitente == null)
                return "El remitente no existe.";

            if (destinatario == null)
                return "El destinatario no existe.";

            if (ExisteSolicitud(pCelularRemitente,
                                pCelularDestinatario))
                return "La solicitud ya fue enviada.";

            if (aServicioAmistades.SonAmigos(remitente, destinatario))
                return "Los usuarios ya son amigos.";

            cSolicitudAmistad solicitud =
                new cSolicitudAmistad(remitente, destinatario);

            aSolicitudesPendientes.Agregar(solicitud);

            return "Solicitud enviada correctamente.";
        }

        //-------------------------------------------------------
        public string AceptarSolicitud(string pCelularRemitente,
                                       string pCelularDestinatario)
        {
            cSolicitudAmistad solicitud =
                BuscarSolicitud(pCelularRemitente,
                                pCelularDestinatario);

            if (solicitud == null)
                return "La solicitud no existe.";

            aServicioAmistades.AgregarAmistad(
                solicitud.Remitente,
                solicitud.Destinatario);

            aSolicitudesPendientes.Eliminar(solicitud);

            return "Solicitud aceptada correctamente.";
        }

        //-------------------------------------------------------
        public bool RechazarSolicitud(string pCelularRemitente,
                                      string pCelularDestinatario)
        {
            cSolicitudAmistad solicitud =
                BuscarSolicitud(pCelularRemitente,
                                pCelularDestinatario);

            if (solicitud == null)
                return false;

            aSolicitudesPendientes.Eliminar(solicitud);

            return true;
        }

        //-------------------------------------------------------
        public bool CancelarSolicitud(string pCelularRemitente,
                                      string pCelularDestinatario)
        {
            return RechazarSolicitud(
                pCelularRemitente,
                pCelularDestinatario);
        }

        //-------------------------------------------------------
        public cSolicitudAmistad BuscarSolicitud(string pCelularRemitente, string pCelularDestinatario)
        {
            cSolicitudAmistad s = null;
            aSolicitudesPendientes.ProcesarObjetosLista(obj =>
            {
                cSolicitudAmistad Amistad = obj as cSolicitudAmistad;
                if (Amistad == null)
                {
                    return;
                }
                if (Amistad.Remitente.ToString() == pCelularRemitente &&
                     Amistad.Destinatario.ToString() == pCelularDestinatario)
                {
                    s = Amistad;
                }
            });
            return s;
               
        }

        //-------------------------------------------------------
        public bool ExisteSolicitud(string pCelularRemitente,string pCelularDestinatario)
        {
            return BuscarSolicitud(
                pCelularRemitente,
                pCelularDestinatario) != null;
        }

        //-------------------------------------------------------
        public List<cSolicitudAmistad> ObtenerSolicitudesPendientes(
            string pCelular)
        {
            //return aSolicitudesPendientes
                //.Where(s => s.Destinatario.Celular == pCelular)
                //.ToList();
        }

        //-------------------------------------------------------
        public List<cSolicitudAmistad> ObtenerSolicitudesEnviadas(
            string pCelular)
        {
            //return aSolicitudesPendientes
                //.Where(s => s.Remitente.Celular == pCelular)
                //.ToList();
        }
        public cLista ObtenerSugerencias(cUsuario pUsuario)
        {
            cLista amigosDeAmigos = aServicioAmistades.ObtenerAmigosDeAmigos(pUsuario);
            cLista sugerencias=new cLista();
            if (amigosDeAmigos == null)
            {
                return null;
            }
            amigosDeAmigos.ProcesarObjetosLista(obj =>
            {
                cUsuario u = obj as cUsuario;
                if (u == null)
                {
                    return;
                }
                if (!ExisteSolicitud(pUsuario.ToString(), u.ToString()){
                    sugerencias.Agregar(u);
                }
            });
            return sugerencias;

        }
        #endregion
    }
}
