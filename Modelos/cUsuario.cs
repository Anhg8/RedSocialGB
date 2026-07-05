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
        private string aNombreUsuario;
        private string aNombres;
        private string aApellidos;
        private DateTime aFechaNacimiento;
        private string aCelular;
        private string aContrasena;
       
        #endregion *********************** ATRIBUTOS ************************


        #region *********************** METODOS ************************

        #region ==================== CONSTRUCTORES =======================

        // --------------------------------------------------------------
        public cUsuario()
        {
            aNombreUsuario = "";
            aNombres = "";
            aApellidos = "";
            aFechaNacimiento = DateTime.Now;
            aCelular = "";
            aContrasena = "";
            
        }

        // --------------------------------------------------------------
        public cUsuario(string pNombreU, string pNombres,
                        string pApellidos,
                        DateTime pFechaNacimiento,
                        string pCelular,
                        string pContrasena)
        {
            aNombreUsuario = pNombreU;
            aNombres = pNombres;
            aApellidos = pApellidos;
            aFechaNacimiento = pFechaNacimiento;
            aCelular = pCelular;
            aContrasena = pContrasena;
        }

        #endregion ==================== CONSTRUCTORES =======================


        #region ==================== PROPIEDADES =======================

        // --------------------------------------------------------------
        public string NombreUsuario
        {
            get { return aNombreUsuario; }
            set { aNombreUsuario = value; }
        }
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

        #endregion ==================== MÉTODOS PROCESO =======================


        #endregion *********************** METODOS ************************
    }
}
