using RedSocialGB.Modelos;
using System;

namespace RedSocialGB.Estructuras.Grafo
{
    public class cVertice
    {
        private cUsuario aNodo;


        private cLista aListaAdyacencia;

        //Propiedades
        public cUsuario Nodo
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
            aNodo = new cUsuario();

            aListaAdyacencia = new cLista();
        }

        public cVertice(cUsuario Usuario)
        {
            aNodo = Usuario;
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