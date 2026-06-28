using RedSocialGB.Estructuras;
using RedSocialGB.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Modelos
{
    public class cUsuario
    {
        #region *********************** ATRIBUTOS ************************

        private string aNombres;
        private string aApellidos;
        private DateTime aFechaNacimiento;
        private string aCelular;
        private string aContrasena;
        private cLista Amigos = new cLista();
        private cPila SolicitudesRecibidas= new cPila();
       

        #endregion *********************** ATRIBUTOS ************************


        #region *********************** METODOS ************************

        #region ==================== CONSTRUCTORES =======================

        // --------------------------------------------------------------
        public cUsuario()
        {
            aNombres = "";
            aApellidos = "";
            aFechaNacimiento = DateTime.Now;
            aCelular = "";
            aContrasena = "";
            
        }

        // --------------------------------------------------------------
        public cUsuario(string pNombres,
                        string pApellidos,
                        DateTime pFechaNacimiento,
                        string pCelular,
                        string pContrasena)
        {
            aNombres = pNombres;
            aApellidos = pApellidos;
            aFechaNacimiento = pFechaNacimiento;
            aCelular = pCelular;
            aContrasena = pContrasena;
        }

        #endregion ==================== CONSTRUCTORES =======================


        #region ==================== PROPIEDADES =======================

        // --------------------------------------------------------------
        public string Nombres
        {
            get { return aNombres; }
            set { aNombres = value; }
        }

        // --------------------------------------------------------------
        public string Apellidos
        {
            get { return aApellidos; }
            set { aApellidos = value; }
        }

        // --------------------------------------------------------------
        public DateTime FechaNacimiento
        {
            get { return aFechaNacimiento; }
            set { aFechaNacimiento = value; }
        }

        // --------------------------------------------------------------
        public string Celular
        {
            get { return aCelular; }
            set { aCelular = value; }
        }

        // --------------------------------------------------------------
        public string Contrasena
        {
            get { return aContrasena; }
            set { aContrasena = value; }
        }
        public cPila Solicitudes
        {
            get { return SolicitudesRecibidas; }
            set { SolicitudesRecibidas = value; }
        }

        #endregion ==================== PROPIEDADES =======================


        #region ==================== MÉTODOS PROCESO =======================

        // --------------------------------------------------------------
        public int GetEdad()
        {
            int edad = DateTime.Today.Year - aFechaNacimiento.Year;

            if (aFechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;

            return edad;
        }

        // --------------------------------------------------------------
        public override string ToString()
        {
            return aCelular;
        }

        // --------------------------------------------------------------
        public override bool Equals(object O)
        {
            return aCelular.Equals(O.ToString());
        }

        // --------------------------------------------------------------
        public void Leer(cArbolB ArbolUsuarios)
        {
            Console.WriteLine("INGRESAR DATOS DEL USUARIO");
            Console.WriteLine("==========================");

            Nombres = cValidaciones.LeerCadena("Ingrese Nombres: ");

            Apellidos = cValidaciones.LeerCadena("Ingrese Apellidos: ");

            FechaNacimiento = cValidaciones.LeerFecha();

            Celular = cValidaciones.LeerCelularNoRepetido(ArbolUsuarios);

            Contrasena = cValidaciones.LeerContrasena();
        }

        // --------------------------------------------------------------
        public void Mostrar()
        {
            Console.WriteLine("DATOS DEL USUARIO");
            Console.WriteLine("=================");

            Console.WriteLine("Nombres          : " + Nombres);
            Console.WriteLine("Apellidos        : " + Apellidos);
            Console.WriteLine("FechaNacimiento  : " + FechaNacimiento.ToShortDateString());
            Console.WriteLine("Edad             : " + GetEdad());
            Console.WriteLine("Celular          : " + Celular);
        }

        // --------------------------------------------------------------
        public void Escribir()
        {
            Console.WriteLine(
                Celular.PadRight(15) +
                Nombres.PadRight(20) +
                Apellidos.PadRight(20) +
                GetEdad().ToString().PadLeft(5)
            );
        }

        #endregion ==================== MÉTODOS PROCESO =======================


        #endregion *********************** METODOS ************************
    }
}
