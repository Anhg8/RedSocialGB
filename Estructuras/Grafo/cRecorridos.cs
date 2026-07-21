using RedSocialGB.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Estructuras.Grafo
{
    public class cNodoRecorrido
    {
        public cVertice Vertice { get; set; }

        public int Nivel { get; set; }

        public cNodoRecorrido(cVertice pVertice, int pNivel)
        {
            Vertice = pVertice;
            Nivel = pNivel;
        }
    }
    public class cRecorridos:cGrafo
    {
        public cLista RecorridoBFS(string pvalor, int pNivelMaximo)
        {
            cLista recorrido = new cLista();

            cLista visitados = new cLista();

            cVertice inicio = BuscarVertice(pvalor);

            if (inicio == null)
                return recorrido;

            cCola cola = new cCola();
            //Encolamos el vertice de inicio con nivel 0
            cola.Encolar(new cNodoRecorrido(inicio, 0));
            //Agregamos a visitaod el vertice de inicio
            visitados.Agregar(inicio);

            while (!cola.EstaVacia())
            {
                //descolamos el primer elemento de la cola y lo guardamos en actual

                cNodoRecorrido actual = (cNodoRecorrido)cola.Primero();

                //lo borramos de la cola

                cola.Desencolar();

                //agregamos el vertice actual a la lista de recorrido
                
                recorrido.Agregar(actual.Vertice);

                //si llega a nivel 2, no se procesan sus vecinos, se continua hasta que se vacie la cola

                if (actual.Nivel == pNivelMaximo)
                    continue;

                actual.Vertice.ListaAdyacencia.ProcesarObjetosLista(O =>
                {
                    cArista arista = O as cArista;

                    cVertice vecino = arista.Destino;
                    //si no esta el vecino en visitados
                    if (!visitados.Existe(vecino))
                    {
                        //agregamos al vecino a visitados
                        visitados.Agregar(vecino);

                        cola.Encolar(new cNodoRecorrido(vecino, actual.Nivel + 1));
                    }
                });
            }

            return recorrido;
        }
    }
}
