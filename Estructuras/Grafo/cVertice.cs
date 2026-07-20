using RedSocialGB.Modelos;
using System;

namespace RedSocialGB.Estructuras.Grafo
{
    public class cVertice
    {
        private object aNodo;
        //cada vertice tiene su lista de adyacencia
        //lista de cAristas
        private cLista aListaAdyacencia;

        //Propiedades
        public Object Nodo
        {
            get { return aNodo; }
            set { aNodo = value; }
        }
        public cLista ListaAdyacencia
        {
            get { return aListaAdyacencia; }
            set { aListaAdyacencia = value; }
        }
        public cVertice()
        {
            aNodo = null;

            aListaAdyacencia = new cLista();
        }

        public cVertice(object obj)
        {
            aNodo = obj;
            aListaAdyacencia = new cLista();
        }

        //------------------------------------------------

        public int Grado()
        {
            return aListaAdyacencia.Longitud();
        }

        //------------------------------------------------

        public override string ToString()
        {
            return aNodo.ToString();
        }
    }
}