using RedSocialGB.Estructuras;
using RedSocialGB.Formularios;
using RedSocialGB.Modelos;
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
            // Desde frmPrincipal o frmAcceso
            cArbolB s= new cArbolB(3);
            s.Insertar(new Modelos.cUsuario(
                "juanperez",              // Nombre de usuario
                "Juan",                   // Nombres
                "Pérez Gómez",            // Apellidos
                new DateTime(2003, 5, 14),// Fecha de nacimiento
                "987654321",              // Celular
                "1234"                    // Contraseña
            ));
            ServicioUsuarios miServicioUsuarios= new ServicioUsuarios(s);
            ServicioAutenticacion miServicioAutenticacion = new ServicioAutenticacion(miServicioUsuarios);
            FormLogin frm = new FormLogin(miServicioUsuarios, miServicioAutenticacion);
            frm.ShowDialog();
        }
    }
}
