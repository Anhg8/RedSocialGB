using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Modelos
{
    public class cSolicitudAmistad
    {
        #region --- Atributos ---

        private cUsuario aRemitente;
        private cUsuario aDestinatario;
        private DateTime aFechaEnvio;
        private bool aPendiente;

        #endregion

        #region --- Constructores ---

        public cSolicitudAmistad()
        {
            aPendiente = true;
        }

        public cSolicitudAmistad(cUsuario pRemitente, cUsuario pDestinatario)
        {
            aRemitente = pRemitente;
            aDestinatario = pDestinatario;
            aFechaEnvio = DateTime.Now;
            aPendiente = true;
        }

        #endregion

        #region --- Propiedades ---

        public cUsuario Remitente
        {
            get { return aRemitente; }
            set { aRemitente = value; }
        }

        public cUsuario Destinatario
        {
            get { return aDestinatario; }
            set { aDestinatario = value; }
        }

        public DateTime FechaEnvio
        {
            get { return aFechaEnvio; }
            set { aFechaEnvio = value; }
        }

        public bool Pendiente
        {
            get { return aPendiente; }
            set { aPendiente = value; }
        }

        #endregion

        #region --- Métodos ---

        public void Aceptar()
        {
            aPendiente = false;
        }

        public void Rechazar()
        {
            aPendiente = false;
        }

        public bool InvolucraA(cUsuario pUsuario)
        {
            return aRemitente == pUsuario || aDestinatario == pUsuario;
        }

        public override string ToString()
        {
            return $"{aRemitente.Nombres} -> {aDestinatario.Nombres}";
        }

        #endregion
    }
}