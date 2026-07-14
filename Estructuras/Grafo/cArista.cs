using System;

namespace RedSocialGB.Estructuras.Grafo
{
    public class cArista
    {
        private cVertice aDestino;


        //Propiedades

        public cVertice Destino
        {
            get { return aDestino; }
            set { aDestino = value; }
        }
        public cArista()
        {
            aDestino = null;
            
        }

        public cArista(cVertice pDestino)
        {
            aDestino = pDestino;
            
        }


        public override string ToString()
        {
            return $"{aDestino}";
        }
    }
}