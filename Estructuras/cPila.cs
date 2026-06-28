using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Estructuras
{
    public class cPila
    {
        #region *************  ATRIBUTOS    **************** 
        private Object aElemento;
        private cPila aSubPila;
        #endregion ATRIBUTOS

        #region *************  CONSTRUCTORES    **************** 
        public cPila()
        {
            aElemento = null;
            aSubPila = null;
        }
        /* -------------------------------------------- */
        public cPila(Object pElemento, cPila pSubPila)
        {
            aElemento = pElemento;
            aSubPila = pSubPila;
        }
        #endregion CONSTRUCTORES


        #region ***********  PROPIEDADES   *************
        public object Elemento
        {
            get
            {
                return aElemento;
            }
            set
            {
                aElemento = value;
            }
        }
        /* --------------------------------------------- */
        public cPila SubPila
        {
            get
            {
                return aSubPila;
            }
            set
            {
                aSubPila = value;
            }
        }
        #endregion PROPIEDADES

        #region ***********  OTROS METODOS  *************     
        /* --------------------------------------------- */
        public bool EsVacia()
        {
            return ((aElemento == null) && (aSubPila == null));
        }

        /* --------------------------------------------- */
        public object Cima()
        {
            return aElemento;
        }

        /* --------------------------------------------- */
        public void Apilar(Object pElemento)
        {
            if (EsVacia())
            {
                aSubPila = new cPila();
                aElemento = pElemento;
            }
            else
            {
                aSubPila = new cPila(aElemento, aSubPila);
                aElemento = pElemento;
            }
        }

        /* --------------------------------------------- */
        public void Desapilar()
        {
            if (!EsVacia())
            {
                aElemento = aSubPila.Elemento;
                aSubPila = aSubPila.SubPila;
            }
        }
        #endregion OTROS METODOS
    }
}
