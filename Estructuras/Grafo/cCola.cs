using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Estructuras.Grafo
{
    internal class cCola
    {
        private object aElemento;
        private cCola aSubcola;
        public cCola()
        {
            this.aElemento = null;
            this.aSubcola = null;


        }
        public cCola(object pElemento, cCola pSubpila)
        {
            this.aElemento = pElemento;
            this.aSubcola = pSubpila;
        }
        public object Elemento { get => aElemento; set => aElemento = value; }
        public cCola Subcola { get => aSubcola; set => aSubcola = value; }
        public object Primero()
        {
            return aElemento;
        }
        public bool EstaVacia()
        {
            return (aSubcola == null && aElemento == null);
        }
        public void Encolar(object p)
        {
            if (EstaVacia())
            {
                aSubcola = new cCola();
                aElemento = p;
            }
            else
            {
                aSubcola.Encolar(p);
            }


        }
        public void Desencolar()
        {
            if (EstaVacia())
            {
                return;
            }
            else
            {
                aElemento = Subcola.Elemento;
                aSubcola = aSubcola.Subcola;
            }
        }
        public void Mostrar()
        {
            if (EstaVacia())
            {
                return;
            }
            else

            {

                Console.WriteLine(aElemento);

                aSubcola.Mostrar();
            }

        }
    }
}
