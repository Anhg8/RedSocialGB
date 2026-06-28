using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Estructuras
{
    internal class cLista
    {
        #region ---Atributos
        private object aElemento;
        private cLista aSubLista;
        #endregion
        #region ---Propiedades
        public object Elemento { get => aElemento; set => aElemento = value; }
        public cLista Sublista { get => aSubLista; set => aSubLista = value; }
        #endregion
        //Constructores

        public cLista()
        {
            aElemento = null;
            aSubLista = null;
        }
        public cLista(object pElemento, cLista pSublista)
        {
            aElemento = pElemento;
            aSubLista = pSublista;
        }
        #region ---Metodos de proceso
        public bool EsVacia()
        {
            return aElemento == null && aSubLista == null;
        }
        public int Longitud()
        {
            return EsVacia() ? 0 : 1 + aSubLista.Longitud();

        }
        public void Agregar(object pElemento)
        {
            if (EsVacia())
            {
                aSubLista = new cLista();
                aElemento = pElemento;
            }
            else
            {
                aSubLista.Agregar(pElemento);
            }
        }
        public void Insertar(object pElemento, int pos)
        {
            if (pos < 0 || pos > Longitud())
            {
                return;
            }
            if (pos == 0)
            {
                aSubLista = new cLista(aElemento, aSubLista);
                aElemento = pElemento;
            }
            else
            {
                aSubLista.Insertar(pElemento, pos - 1);
            }
        }
        public void Eliminar(int pos)
        {
            if (pos < 0 || pos > Longitud())
            {
                return;
            }
            if (pos == 0)
            {
                aElemento = aSubLista.Elemento;
                aSubLista = aSubLista.Sublista;
            }
            else
            {
                aSubLista.Eliminar(pos - 1);
            }
        }
        public int Indice(object pElemento)
        {
            if (EsVacia())
            {
                return -1;
            }
            if (aElemento.Equals(pElemento))
            {
                return 0;
            }
            else
            {
                int i = aSubLista.Indice(pElemento);
                return (i >= 0) ? 1 + i : -1;

            }
        }
        public void Eliminar(object pElemento)
        {
            Eliminar(Indice(pElemento));
        }
        public object Iesimo(int pos)
        {
            if (pos < 0 || pos > Longitud())
            {
                throw new IndexOutOfRangeException("Posición fuera de rango");
            }
            if (EsVacia())
            {
                return null;
            }
            if (pos == 0)
            {
                return aElemento;
            }
            else
            {
                return aSubLista.Iesimo(pos - 1);
            }

        }
        public void ProcesarObjeto(object Ob)
        {
            Console.WriteLine(Ob.ToString());
        }
        //con null la funcion que pasaremos es opcional, sino por defecto pasa procesar objeto
        public void ProcesarObjetosLista(Action<object> Modulo = null)
        {
            if (!EsVacia())
            {
                if (Modulo == null)
                {
                    ProcesarObjeto(aElemento);
                }
                else
                {
                    Modulo(aElemento);
                }
                aSubLista.ProcesarObjetosLista(Modulo);

            }
        }
    }
    #endregion
}