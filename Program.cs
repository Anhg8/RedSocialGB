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
            cUsuario u0 = new cUsuario("juanperez", "Juan", "Pérez Gómez", new DateTime(2003, 5, 14), "987654321", "1234");
            cUsuario u1 = new cUsuario("analizia", "Ana", "Lizárraga Quispe", new DateTime(2001, 3, 12), "912345678", "ana123");
            cUsuario u2 = new cUsuario("brunomama", "Bruno", "Mamani Ccoa", new DateTime(2000, 7, 8), "923456781", "bru456");
            cUsuario u3 = new cUsuario("carlitosv", "Carlos", "Vargas Huanca", new DateTime(2002, 11, 3), "934567812", "car789");
            cUsuario u4 = new cUsuario("dianachav", "Diana", "Chávez Pilco", new DateTime(2003, 1, 25), "945678123", "dia321");
            cUsuario u5 = new cUsuario("eduardoq", "Eduardo", "Quispe Flores", new DateTime(2001, 6, 17), "956781234", "edu654");
            cUsuario u6 = new cUsuario("fabiocond", "Fabio", "Condori Apaza", new DateTime(2000, 9, 30), "967812345", "fab987");
            cUsuario u7 = new cUsuario("ginellapm", "Ginella", "Pacheco Mendoza", new DateTime(2002, 4, 5), "978123456", "gin111");
            cUsuario u8 = new cUsuario("hectorsu", "Héctor", "Sucari Ttito", new DateTime(2003, 8, 19), "989234567", "hec222");
            cUsuario u9 = new cUsuario("inesalva", "Inés", "Álvarez Ccama", new DateTime(2001, 2, 28), "990345678", "ine333");
            cUsuario u10 = new cUsuario("jorgeluq", "Jorge", "Luna Quispe", new DateTime(2000, 12, 1), "901456789", "jor444");
            sistema.ServicioUsuarios.GuardarUsuario(u0);
            sistema.ServicioUsuarios.GuardarUsuario(u1);
            sistema.ServicioUsuarios.GuardarUsuario(u2);
            sistema.ServicioUsuarios.GuardarUsuario(u3);
            sistema.ServicioUsuarios.GuardarUsuario(u4);
            sistema.ServicioUsuarios.GuardarUsuario(u5);
            sistema.ServicioUsuarios.GuardarUsuario(u6);
            sistema.ServicioUsuarios.GuardarUsuario(u7);
            sistema.ServicioUsuarios.GuardarUsuario(u8);
            sistema.ServicioUsuarios.GuardarUsuario(u9);
            sistema.ServicioUsuarios.GuardarUsuario(u10);

            sistema.ServicioAmistades.AgregarAmistad(u0, u1);   // Juan ↔ Ana
            sistema.ServicioAmistades.AgregarAmistad(u1, u2);   // Ana ↔ Bruno
            sistema.ServicioAmistades.AgregarAmistad(u1, u3);   // Ana ↔ Carlos
            sistema.ServicioAmistades.AgregarAmistad(u1, u4);   // Ana ↔ Diana
            sistema.ServicioAmistades.AgregarAmistad(u2, u5);   // Bruno ↔ Eduardo
            sistema.ServicioAmistades.AgregarAmistad(u2, u6);   // Bruno ↔ Fabio
            sistema.ServicioAmistades.AgregarAmistad(u3, u7);   // Carlos ↔ Ginella
            sistema.ServicioAmistades.AgregarAmistad(u4, u8);   // Diana ↔ Héctor
            sistema.ServicioAmistades.AgregarAmistad(u5, u9);   // Eduardo ↔ Inés
            sistema.ServicioAmistades.AgregarAmistad(u6, u10);  // Fabio ↔ Jorge
            sistema.ServicioAmistades.AgregarAmistad(u7, u10);  // Ginella ↔ Jorge
            sistema.ServicioAmistades.AgregarAmistad(u8, u9);   // Héctor ↔ Inés
            sistema.ServicioAmistades.AgregarAmistad(u9, u10);  // Inés ↔ Jorge

            Application.Run(new FormLogin(sistema));
        }
    }
}
