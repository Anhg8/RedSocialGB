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
        public cLista RecorridoBFS(object obj, int pNivelMaximo)
        {
            cLista recorrido = new cLista();

            cLista visitados = new cLista();

            cVertice inicio = BuscarVertice(obj.ToString());

            if (inicio == null)
                return recorrido;

            cCola cola = new cCola();

            cola.Encolar(new cNodoRecorrido(inicio, 0));

            visitados.Agregar(inicio);

            while (!cola.EstaVacia())
            {
                cNodoRecorrido actual = (cNodoRecorrido)cola.Primero();

                cola.Desencolar();

                recorrido.Agregar(actual.Vertice);

                if (actual.Nivel == pNivelMaximo)
                    continue;

                actual.Vertice.ListaAdyacencia.ProcesarObjetosLista(O =>
                {
                    cArista arista = O as cArista;

                    cVertice vecino = arista.Destino;

                    if (!visitados.Existe(vecino))
                    {
                        visitados.Agregar(vecino);

                        cola.Encolar(new cNodoRecorrido(vecino, actual.Nivel + 1));
                    }
                });
            }

            return recorrido;
        }
    }
}
