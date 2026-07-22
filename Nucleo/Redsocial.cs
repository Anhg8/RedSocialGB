using RedSocialGB.Estructuras;
using RedSocialGB.Estructuras.Grafo;

namespace RedSocialGB.Nucleo
{
    public class Redsocial
    {
        #region *************** ATRIBUTOS ***************

        private cArbolB aUsuarios;
        private cGrafo aGrafoAmistades;
        private cLista aSolicitudes;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public Redsocial()
        {
            aUsuarios = new cArbolB(3);
            aGrafoAmistades = new cGrafo();
            aSolicitudes = new cLista();
        }

        #endregion

        #region *************** PROPIEDADES ***************

        public cArbolB Usuarios => aUsuarios;
        public cGrafo GrafoAmistades => aGrafoAmistades;
        public cLista Solicitudes => aSolicitudes;

        #endregion
    }
}