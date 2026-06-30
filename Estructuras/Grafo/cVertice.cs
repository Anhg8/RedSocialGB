using System;

namespace RedSocialGB.Estructuras.Grafo
{
    class cVertice
    {
        public Object Dato { get; set; }

        public bool Visitado { get; set; }

        public cLista ListaAdyacencia { get; set; }

        public cVertice()
        {
            Dato = null;
            Visitado = false;
            ListaAdyacencia = new cLista();
        }

        public cVertice(Object pDato)
        {
            Dato = pDato;
            Visitado = false;
            ListaAdyacencia = new cLista();
        }

        //------------------------------------------------

        public void AgregarArista(cVertice pDestino)
        {
            ListaAdyacencia.Agregar(new cArista(pDestino));
        }

        public void AgregarArista(cVertice pDestino, int pPeso)
        {
            ListaAdyacencia.Agregar(new cArista(pDestino, pPeso));
        }

        //------------------------------------------------

        public int Grado()
        {
            return ListaAdyacencia.Longitud();
        }

        //------------------------------------------------

        public override string ToString()
        {
            return Dato.ToString();
        }
    }
}