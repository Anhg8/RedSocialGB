using RedSocialGB.Datos;
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
        private AmistadDAO aAmistadDAO;
        private cGrafo aGrafo;

        #endregion
        #region *************** PROPIEDADES ***************
        public cGrafo Grafo
        {
            get => aGrafo;
            set => aGrafo = value;
        }
        public AmistadDAO AmistadDAO
        {
            get => aAmistadDAO;
            set => aAmistadDAO = value;
        }
        #endregion

        #region *************** CONSTRUCTOR ***************
        public ServicioAmistades()
        {
            aGrafo = new cGrafo();
            aAmistadDAO = new AmistadDAO();
        }
        public ServicioAmistades(cGrafo pGrafo, AmistadDAO pAmistadDAO)
        {
            aGrafo = pGrafo;
            aAmistadDAO = pAmistadDAO;
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

            cAmistad amistad = new cAmistad(pUsuario1, pUsuario2);
            aGrafo.AgregarArista(pUsuario1.ToString(), pUsuario2.ToString());
            aAmistadDAO.Insertar(pUsuario1.ToString(),pUsuario2.ToString());

            return true;
        }

        // -------------------------------------------------
        public bool EliminarAmistad(cUsuario pUsuario1,
                                    cUsuario pUsuario2)
        {
            if (!SonAmigos(pUsuario1, pUsuario2))
                return false;

            aGrafo.EliminarArista(pUsuario1.ToString(), pUsuario2.ToString());

            aAmistadDAO.Eliminar(pUsuario1.ToString(), pUsuario2.ToString());

            return true;
        }

        // -------------------------------------------------
        public bool SonAmigos(cUsuario pUsuario1,
                              cUsuario pUsuario2)
        {
            return aGrafo.ExisteArista(pUsuario1.ToString(), pUsuario2.ToString());
        }
        public bool SonAmigosMySql(string pCelular1,string pCelular2)
        {
            return aAmistadDAO.SonAmigos(pCelular1, pCelular2);
        }

        // -------------------------------------------------
        public cLista ObtenerAmigos(cUsuario usuario)
        {
            cLista amigos = new cLista();

            cVertice vertice = aGrafo.BuscarVertice(usuario.ToString());

            if (vertice == null)
                return amigos;

            vertice.ListaAdyacencia.ProcesarObjetosLista(obj =>
            {
                cArista arista = obj as cArista;

                if (arista != null)
                    amigos.Agregar(arista.Destino.Nodo as cUsuario);
            });

            return amigos;
        }
        public cLista ObtenerAmigosMySql(string pCelular)
        {
            return aAmistadDAO.ObtenerAmigos(pCelular);
        }
        public cLista ObtenerAmigosBFS(cUsuario usuario)
        {
            cLista amigos = new cLista();
            cLista recorrido = cRecorridos.RecorridoBFS(usuario, 1, aGrafo);
            recorrido.ProcesarObjetosLista(O =>
            {
                cVertice v = O as cVertice;
                if (v == null)
                    return;
                cUsuario u = (cUsuario)v.Nodo;
                if (u.ToString() == usuario.ToString())
                    return;
                amigos.Agregar(u);
            });
            return amigos;
        }

        // -------------------------------------------------

        //Obtiene los amigos de amigos, eso en bfs es busqueda hasta nivel 2, nivel 1 son los amigos directos.
        public cLista ObtenerAmigosDeAmigos(cUsuario usuario)
        {
            cLista recorrido = cRecorridos.RecorridoBFS(usuario, 2,aGrafo);

            cLista resultado = new cLista();

            cLista amigos = ObtenerAmigos(usuario);

            recorrido.ProcesarObjetosLista(O =>
            {
                cVertice v = O as cVertice;

                if (v == null)
                    return;
                //casteamos el nodo a cUsuario
                cUsuario u = (cUsuario)v.Nodo;

                if (u.ToString() == usuario.ToString())
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

        #endregion
    }
}
