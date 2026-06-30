using System;

namespace RedSocialGB.Estructuras.Grafo
{
    internal class cArista
    {
        public cVertice Destino { get; set; }

        public int Peso { get; set; }

        public cArista()
        {
            Destino = null;
            Peso = 1;
        }

        public cArista(cVertice pDestino)
        {
            Destino = pDestino;
            Peso = 1;
        }

        public cArista(cVertice pDestino, int pPeso)
        {
            Destino = pDestino;
            Peso = pPeso;
        }

        public override string ToString()
        {
            return $"{Destino}";
        }
    }
}