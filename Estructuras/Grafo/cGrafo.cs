using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using System;
using System.Collections.Generic;

namespace RedSocialGB.Estructuras.Grafo
{
    public class cGrafo
    {
        #region --- Atributos

        private cLista aVertices;

        #endregion

        #region --- Propiedades

        public cLista Vertices
        {
            get => aVertices;
            set => aVertices = value;
        }

        #endregion

        #region --- Constructores

        public cGrafo()
        {
            aVertices = new cLista();
        }

        #endregion

        #region --- Metodos para Vértices y Aristas

        //------------------------------------------------
        public int CantidadVertices()
        {
            return aVertices.Longitud();
        }
        //------------------------------------------------
        public int CantidadAristas()
        {
            int total = 0;
            aVertices.ProcesarObjetosLista(obj =>
            {
                cVertice v = obj as cVertice;
                if (v != null) total += v.ListaAdyacencia.Longitud();
            });
            // Grafo no dirigido: cada arista se cuenta dos veces
            return total / 2;
        }
        //------------------------------------------------

        public int Grado(string pVertice)
        {
            cVertice v = BuscarVertice(pVertice);
            return v != null ? v.Grado() : 0;
        }
        //------------------------------------------------

        public cVertice BuscarVertice(string pValor)
        {
            cVertice buscado = null;
            //Buscamos en la lista de vertices
            aVertices.ProcesarObjetosLista(obj =>
            {
                cVertice v = obj as cVertice;
                if (v != null && v.Nodo.ToString() == pValor)
                    buscado = v;
            });

            return buscado;
        }
        //------------------------------------------------
        public bool ExisteVertice(string pValor)
        {
            return BuscarVertice(pValor) != null;
        }
        //------------------------------------------------
        public bool ExisteArista(string pOrigen, string pDestino)
        {
            cVertice vOrigen = BuscarVertice(pOrigen);
            if (vOrigen == null) return false;
            //Buscamos en la lista de adyacencia la arista con vertice pDestino
            bool existe = false;
            vOrigen.ListaAdyacencia.ProcesarObjetosLista(obj =>
            {
                cArista a = obj as cArista;
                //a.Destino = cVertice, a.Destino.Nodo = object
                if (a != null && a.Destino.Nodo.ToString() == pDestino)
                    existe = true;
            });

            return existe;
        }
        //------------------------------------------------
        public void AgregarVertice(object obj)
        {
            if (!ExisteVertice(obj.ToString()))
                //Agrega a lista de vertices 
                aVertices.Agregar(new cVertice(obj));
        }
        //------------------------------------------------
        public bool AgregarArista(string pOrigen, string pDestino)
        {
            cVertice vOrigen = BuscarVertice(pOrigen);
            if (vOrigen == null)
                throw new Exception($"El vértice '{pOrigen}' no existe en el grafo.");

            cVertice vDestino = BuscarVertice(pDestino);
            if (vDestino == null)
                throw new Exception($"El vértice '{pDestino}' no existe en el grafo.");

            //llamamod a AgregarArista(cVertice pOrigen, cVertice pDestino)
            return AgregarArista(vOrigen, vDestino);
        }
        //------------------------------------------------

        public bool AgregarArista(cVertice pOrigen, cVertice pDestino)
        {
            // Evitar duplicados
            if (ExisteArista(pOrigen.Nodo.ToString(), pDestino.Nodo.ToString()))
                return false;

            // Grafo no dirigido: agregar en ambas direcciones
            pOrigen.ListaAdyacencia.Agregar(new cArista(pDestino));
            pDestino.ListaAdyacencia.Agregar(new cArista(pOrigen));
            return true;
        }
        //------------------------------------------------
        public void EliminarVertice(string pValor)
        {
            cVertice v = BuscarVertice(pValor);
            if (v == null) return;

            // Primero eliminar todas las aristas que apuntan a este vértice
            aVertices.ProcesarObjetosLista(obj =>
            {
                cVertice otro = obj as cVertice;
                if (otro != null && otro != v)
                    EliminarAristaInterno(otro, pValor);
            });

            // Luego eliminar el vértice encontrado
            aVertices.Eliminar(v);
        }
        //------------------------------------------------
        private void EliminarAristaInterno(cVertice pVertice, string pValorDestino)
        {
            cArista encontrada = null;

            pVertice.ListaAdyacencia.ProcesarObjetosLista(obj =>
            {
                cArista a = obj as cArista;
                if (a != null && a.Destino.Nodo.ToString() == pValorDestino)
                    encontrada = a;
            });

            if (encontrada != null)
                pVertice.ListaAdyacencia.Eliminar(encontrada);
        }
        //------------------------------------------------
        public void EliminarArista(string pOrigen, string pDestino)
        {
            cVertice vOrigen = BuscarVertice(pOrigen);
            cVertice vDestino = BuscarVertice(pDestino);
            if (vOrigen == null || vDestino == null) return;

            // Grafo no dirigido: eliminar en ambas direcciones
            EliminarAristaInterno(vOrigen, pDestino);
            EliminarAristaInterno(vDestino, pOrigen);
        }
        public cLista ObtenerAmigos(string obj)
        {
            cLista amigos = new cLista();

            cVertice vertice =BuscarVertice(obj);

            if (vertice == null)
                return amigos;

            vertice.ListaAdyacencia.ProcesarObjetosLista(O =>
            {
                cArista arista = O as cArista;

                if (arista != null)
                    amigos.Agregar(arista.Destino.Nodo);
            });

            return amigos;
        }
        
        #endregion
    }
}