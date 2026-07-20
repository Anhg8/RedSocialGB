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

        
        public cLista ObtenerAmigosDeAmigos(cUsuario pUsuario)
        {
            cLista recorrido = BFS.RecorridoBFS(pUsuario, 2);

            cLista resultado = new cLista();

            cLista amigos = aGrafo.ObtenerAmigos(pUsuario.ToString());

            recorrido.ProcesarObjetosLista(obj =>
            {
                cVertice v = obj as cVertice;

                if (v == null)
                    return;

                cUsuario u = (cUsuario)v.Nodo;

                if (u.ToString() == pUsuario.ToString())
                    return;

                if (amigos.Existe(u))
                    return;

                if (!resultado.Existe(u))
                    resultado.Agregar(u);
            });

            return resultado;
        }

        // -------------------------------------------------
        


        #endregion
    }
}
