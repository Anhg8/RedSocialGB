using RedSocialGB.Estructuras;
using RedSocialGB.Estructuras.Grafo;
using RedSocialGB.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Servicios
{
    public class ServicioAmistades
    {
        #region *************** ATRIBUTOS ***************
        private cRecorridos aBFS;
        private cGrafo aGrafo;

        #endregion
        #region *************** PROPIEDADES ***************
        public cGrafo Grafo
        {
            get => aGrafo;
            set => aGrafo = value;
        }
        public cRecorridos BFS
        {
            get => aBFS;
            set => aBFS = value;
        }

        #endregion

        #region *************** CONSTRUCTOR ***************

        public ServicioAmistades(cGrafo pGrafo)
        {
            aGrafo = pGrafo;
        }

        #endregion

        #region *************** MÉTODOS ***************

        // -------------------------------------------------
        public bool AgregarAmistad(cUsuario pUsuario1,
                                   cUsuario pUsuario2)
        {
            if (pUsuario1 == null || pUsuario2 == null)
                return false;

            if (SonAmigos(pUsuario1, pUsuario2))
                return false;

            aGrafo.AgregarArista(pUsuario1.ToString(), pUsuario2.ToString());

            return true;
        }

        // -------------------------------------------------
        public bool EliminarAmistad(cUsuario pUsuario1,
                                    cUsuario pUsuario2)
        {
            if (!SonAmigos(pUsuario1, pUsuario2))
                return false;

            aGrafo.EliminarArista(pUsuario1.ToString(), pUsuario2.ToString());

            return true;
        }

        // -------------------------------------------------
        public bool SonAmigos(cUsuario pUsuario1,
                              cUsuario pUsuario2)
        {
            return aGrafo.ExisteArista(pUsuario1.ToString(), pUsuario2.ToString());
        }

        //Obtiene los amigos de amigos, eso en bfs es busqueda hasta nivel 2, nivel 1 son los amigos directos.
        public cLista ObtenerAmigosDeAmigos(string pvalor)
        {
            cLista recorrido = BFS.RecorridoBFS(pvalor, 2);

            cLista resultado = new cLista();

            cLista amigos = ObtenerAmigos(pvalor);

            recorrido.ProcesarObjetosLista(obj =>
            {
                cVertice v = obj as cVertice;

                if (v == null)
                    return;
                //casteamos el nodo a cUsuario
                cUsuario u = (cUsuario)v.Nodo;

                if (u.ToString() == pvalor)
                    return;
                //si ya son amigos , lo ignora y no lo agrega para evitar duplicados
                if (amigos.Existe(u))
                    return;
                //si no existe en la lista de resultado, lo agrega
                if (!resultado.Existe(u))
                    resultado.Agregar(u);
            });

            return resultado;
        }

        // -------------------------------------------------
        public cLista ObtenerAmigos(string pvalor)
        {
            cLista amigos = new cLista();

            cVertice vertice = aGrafo.BuscarVertice(pvalor);

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
