using Org.BouncyCastle.Crypto;
using RedSocialGB.Estructuras;
using RedSocialGB.Estructuras.Grafo;
using RedSocialGB.Formularios;
using RedSocialGB.Modelos;
using RedSocialGB.Nucleo;
using RedSocialGB.Servicios;
using System;
using System.Windows.Forms;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Redsocial sistema = new Redsocial();


            Application.Run(new FormLogin(sistema));
        }
    }
}
