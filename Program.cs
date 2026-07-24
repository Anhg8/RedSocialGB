using Org.BouncyCastle.Crypto;
using RedSocialGB.Estructuras;
using RedSocialGB.Estructuras.Grafo;
using RedSocialGB.Formularios;
using RedSocialGB.Modelos;
using RedSocialGB.Persistencia;
using RedSocialGB.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB
{
    internal class Program
    {
        static void Main(string[] args)
        {

            cGrafo g;
            cArbolB b;
            cLista l;
            InicializarDatos.CargarTodo(out b, out l,out  g);
            ServicioAmistades A = new ServicioAmistades(g);


        }
    }
}
