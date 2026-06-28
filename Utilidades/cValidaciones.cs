using RedSocialGB.Estructuras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialGB.Utilidades
{
    public class cValidaciones
    {
        #region ==================== VALIDACIONES =======================

        // --------------------------------------------------------------
        public static int LeerOpcion(int pMinimo, int pMaximo)
        {
            int opcion;

            do
            {
                Console.Write("Ingrese una opción: ");
            }
            while (!int.TryParse(Console.ReadLine(), out opcion) ||
                   opcion < pMinimo ||
                   opcion > pMaximo);

            return opcion;
        }

        // --------------------------------------------------------------
        public static int LeerEntero(string pMensaje)
        {
            int numero;

            do
            {
                Console.Write(pMensaje);
            }
            while (!int.TryParse(Console.ReadLine(), out numero));

            return numero;
        }

        // --------------------------------------------------------------
        public static string LeerCadena(string pMensaje)
        {
            string cadena;

            do
            {
                Console.Write(pMensaje);
                cadena = Console.ReadLine().Trim();

            } while (cadena == "");

            return cadena;
        }

        // --------------------------------------------------------------
        public static string LeerCelular()
        {
            string celular;
            bool correcto;

            do
            {
                Console.Write("Ingrese Número de Celular: ");
                celular = Console.ReadLine();

                correcto = celular.Length == 9;

                if (correcto)
                {
                    foreach (char c in celular)
                    {
                        if (!Char.IsDigit(c))
                        {
                            correcto = false;
                            break;
                        }
                    }
                }

                if (!correcto)
                    Console.WriteLine("ERROR... Debe ingresar un número de 9 dígitos.");

            } while (!correcto);

            return celular;
        }

        // --------------------------------------------------------------
        public static string LeerCelularNoRepetido(cArbolB pArbol)
        {
            string celular;

            do
            {
                celular = LeerCelular();
                

                if (pArbol.Buscar(celular) != null)
                    Console.WriteLine("ERROR... El número ya se encuentra registrado.");

            } while (pArbol.Buscar(celular) != null);

            return celular;
        }

        // --------------------------------------------------------------
        public static DateTime LeerFecha()
        {
            DateTime fecha;

            do
            {
                Console.Write("Ingrese Fecha (dd/MM/yyyy): ");

            } while (!DateTime.TryParse(Console.ReadLine(), out fecha));

            return fecha;
        }

        // --------------------------------------------------------------
        public static string LeerContrasena()
        {
            string contrasena;

            do
            {
                Console.Write("Ingrese Contraseña: ");
                contrasena = Console.ReadLine();

                if (contrasena.Length < 4)
                    Console.WriteLine("ERROR... La contraseña debe tener al menos 4 caracteres.");

            } while (contrasena.Length < 4);

            return contrasena;
        }

        // --------------------------------------------------------------
        public static bool Confirmar(string pMensaje)
        {
            string respuesta;

            do
            {
                Console.Write(pMensaje + " (S/N): ");
                respuesta = Console.ReadLine().ToUpper();

            } while (respuesta != "S" && respuesta != "N");

            return respuesta == "S";
        }

        #endregion ==================== VALIDACIONES =======================
    }
}
