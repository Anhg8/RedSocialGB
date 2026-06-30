using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RedSocialGB.Estructuras
{
    internal class cLista
    {
        #region --- Atributos

        private object aElemento;
        private cLista aSubLista;

        #endregion

        #region --- Propiedades

        public object Elemento
        {
            get => aElemento;
            set => aElemento = value;
        }

        public cLista Sublista
        {
            get => aSubLista;
            set => aSubLista = value;
        }

        #endregion

        #region --- Constructores

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

        #endregion

        #region --- Métodos básicos

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
                aElemento = pElemento;
                aSubLista = new cLista();
            }
            else
            {
                aSubLista.Agregar(pElemento);
            }
        }

        public void Insertar(object pElemento, int pPosicion)
        {
            if (pPosicion < 0 || pPosicion > Longitud())
                return;

            if (pPosicion == 0)
            {
                aSubLista = new cLista(aElemento, aSubLista);
                aElemento = pElemento;
            }
            else
            {
                aSubLista.Insertar(pElemento, pPosicion - 1);
            }
        }

        public void Eliminar(int pPosicion)
        {
            if (pPosicion < 0 || pPosicion >= Longitud())
                return;

            if (pPosicion == 0)
            {
                aElemento = aSubLista.Elemento;
                aSubLista = aSubLista.Sublista;
            }
            else
            {
                aSubLista.Eliminar(pPosicion - 1);
            }
        }

        public void Eliminar(object pElemento)
        {
            Eliminar(Indice(pElemento));
        }

        public object Iesimo(int pPosicion)
        {
            if (pPosicion < 0 || pPosicion >= Longitud())
                throw new IndexOutOfRangeException("Posición fuera de rango.");

            if (pPosicion == 0)
                return aElemento;

            return aSubLista.Iesimo(pPosicion - 1);
        }

        public int Indice(object pElemento)
        {
            if (EsVacia())
                return -1;

            if (aElemento.Equals(pElemento))
                return 0;

            int indice = aSubLista.Indice(pElemento);

            return (indice == -1) ? -1 : indice + 1;
        }

        public bool Existe(object pElemento)
        {
            return Indice(pElemento) != -1;
        }

        #endregion

        #region --- Procesamiento

        public void ProcesarObjeto(object pObjeto)
        {
            Console.WriteLine(pObjeto);
        }

        public void ProcesarObjetosLista(Action<object> pModulo = null)
        {
            if (!EsVacia())
            {
                if (pModulo == null)
                    ProcesarObjeto(aElemento);
                else
                    pModulo(aElemento);

                aSubLista.ProcesarObjetosLista(pModulo);
            }
        }

        #endregion

        #region --- Métodos pendientes

        public object Buscar(object pElemento)
        {
            Console.WriteLine("Falta implementar.");
            return null;
        }

        public void Reemplazar(int pPosicion, object pElemento)
        {
            Console.WriteLine("Falta implementar.");
        }

        public object Primero()
        {
            Console.WriteLine("Falta implementar.");
            return null;
        }

        public object Ultimo()
        {
            Console.WriteLine("Falta implementar.");
            return null;
        }

        public void EliminarPrimero()
        {
            Console.WriteLine("Falta implementar.");
        }

        public void EliminarUltimo()
        {
            Console.WriteLine("Falta implementar.");
        }

        public void Vaciar()
        {
            Console.WriteLine("Falta implementar.");
        }

        public void Invertir()
        {
            Console.WriteLine("Falta implementar.");
        }

        public void Limpiar()
        {
            Console.WriteLine("Falta implementar.");
        }

        public override string ToString()
        {
            Console.WriteLine("Falta implementar.");
            return base.ToString();
        }

        #endregion
    }
}