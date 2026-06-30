using System;
using RedSocialGB.Estructuras;

namespace RedSocialGB.Estructuras.Grafo
{
    internal class cGrafo
    {
        #region --- Atributos

        private cLista aVertices;
        private bool aEsDirigido;

        #endregion

        #region --- Propiedades

        public cLista Vertices
        {
            get => aVertices;
            set => aVertices = value;
        }

        public bool EsDirigido
        {
            get => aEsDirigido;
            set => aEsDirigido = value;
        }

        #endregion

        #region --- Constructores

        public cGrafo()
        {
            aVertices = new cLista();
            aEsDirigido = false;
        }

        public cGrafo(bool pEsDirigido)
        {
            aVertices = new cLista();
            aEsDirigido = pEsDirigido;
        }

        #endregion

        #region --- Vértices

        public void AgregarVertice(object pDato)
        {
            Console.WriteLine("Falta implementar.");
        }

        public void EliminarVertice(object pDato)
        {
            Console.WriteLine("Falta implementar.");
        }

        public cVertice BuscarVertice(object pDato)
        {
            Console.WriteLine("Falta implementar.");
            return null;
        }

        public bool ExisteVertice(object pDato)
        {
            Console.WriteLine("Falta implementar.");
            return false;
        }

        public int CantidadVertices()
        {
            Console.WriteLine("Falta implementar.");
            return 0;
        }

        #endregion

        #region --- Aristas

        public void AgregarArista(object pOrigen, object pDestino)
        {
            Console.WriteLine("Falta implementar.");
        }

        public void AgregarArista(object pOrigen, object pDestino, int pPeso)
        {
            Console.WriteLine("Falta implementar.");
        }

        public void EliminarArista(object pOrigen, object pDestino)
        {
            Console.WriteLine("Falta implementar.");
        }

        public bool ExisteArista(object pOrigen, object pDestino)
        {
            Console.WriteLine("Falta implementar.");
            return false;
        }

        public int CantidadAristas()
        {
            Console.WriteLine("Falta implementar.");
            return 0;
        }

        public int Grado(object pVertice)
        {
            Console.WriteLine("Falta implementar.");
            return 0;
        }

        #endregion

        #region --- Recorridos

        public void DFS(object pOrigen)
        {
            Console.WriteLine("Falta implementar.");
        }

        public void BFS(object pOrigen)
        {
            Console.WriteLine("Falta implementar.");
        }

        public bool ExisteCamino(object pOrigen, object pDestino)
        {
            Console.WriteLine("Falta implementar.");
            return false;
        }

        public void LimpiarVisitados()
        {
            Console.WriteLine("Falta implementar.");
        }

        #endregion

        #region --- Información

        public void Mostrar()
        {
            Console.WriteLine("Falta implementar.");
        }

        public void MostrarVertices()
        {
            Console.WriteLine("Falta implementar.");
        }

        public void MostrarAristas()
        {
            Console.WriteLine("Falta implementar.");
        }

        #endregion

        #region --- Utilidades

        public bool EsVacio()
        {
            Console.WriteLine("Falta implementar.");
            return false;
        }

        public void Vaciar()
        {
            Console.WriteLine("Falta implementar.");
        }

        #endregion

        #region --- Algoritmos Avanzados

        public void Dijkstra(object pOrigen)
        {
            Console.WriteLine("Falta implementar.");
        }

        public void FloydWarshall()
        {
            Console.WriteLine("Falta implementar.");
        }

        public void Prim()
        {
            Console.WriteLine("Falta implementar.");
        }

        public void Kruskal()
        {
            Console.WriteLine("Falta implementar.");
        }

        #endregion
    }
}