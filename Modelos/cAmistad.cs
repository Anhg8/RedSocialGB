using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//clase por revisar

namespace RedSocialGB.Modelos
{
    public class cAmistad
    {
        #region ---Atributos---
        private cUsuario aUsuario1;
        private cUsuario aUsuario2;
        private DateTime aFecha;
        #endregion

        #region ---Metodos
        //-----Constructores-----
        public cAmistad()
        {
            aUsuario1 = new cUsuario();
            aUsuario2 = new cUsuario();
            aFecha = DateTime.MinValue;
        }
        public cAmistad(cUsuario pUsuario1, cUsuario pUsuario2, DateTime pFecha)
        {
            aUsuario1 = pUsuario1;
            aUsuario2 = pUsuario2;
            aFecha = pFecha;
        }
        //----Propiedades----
        public cUsuario Usuario1
        {
            get { return aUsuario1; }
            set { aUsuario1 = value; }
        }

        public cUsuario Usuario2
        {
            get { return aUsuario2; }
            set { aUsuario2 = value; }
        }

        public DateTime Fecha
        {
            get { return aFecha; }
            set { aFecha = value; }
        }
        //----Otros metodos----
        public bool ContieneUsuario(cUsuario usuario)
        {
            return Usuario1.Celular == usuario.Celular ||
                   Usuario2.Celular == usuario.Celular;
        }
        public cUsuario ObtenerAmigo(cUsuario usuario)
        {
            if (Usuario1.Celular == usuario.Celular)
                return Usuario2;

            if (Usuario2.Celular == usuario.Celular)
                return Usuario1;

            return null;
        }
        public override string ToString()
        {
            return $"{Usuario1.Nombres} - {Usuario2.Nombres}";
        }
        #endregion

    }
}
