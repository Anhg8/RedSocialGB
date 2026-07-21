using RedSocialGB.Estructuras;
using RedSocialGB.Estructuras.Grafo;
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
            // ── Usuarios ──────────────────────────────────────────────
            cUsuario u0 = new Modelos.cUsuario(
                "juanperez",              // Nombre de usuario
                "Juan",                   // Nombres
                "Pérez Gómez",            // Apellidos
                new DateTime(2003, 5, 14),// Fecha de nacimiento
                "987654321",              // Celular
                "1234"                    // Contraseña
            );
            cUsuario u1 = new Modelos.cUsuario("analizia", "Ana", "Lizárraga Quispe", new DateTime(2001, 3, 12), "912345678", "ana123");
            cUsuario u2 = new Modelos.cUsuario("brunomama", "Bruno", "Mamani Ccoa", new DateTime(2000, 7, 8), "923456781", "bru456");
            cUsuario u3 = new Modelos.cUsuario("carlitosv", "Carlos", "Vargas Huanca", new DateTime(2002, 11, 3), "934567812", "car789");
            cUsuario u4 = new Modelos.cUsuario("dianachav", "Diana", "Chávez Pilco", new DateTime(2003, 1, 25), "945678123", "dia321");
            cUsuario u5 = new Modelos.cUsuario("eduardoq", "Eduardo", "Quispe Flores", new DateTime(2001, 6, 17), "956781234", "edu654");
            cUsuario u6 = new Modelos.cUsuario("fabiocond", "Fabio", "Condori Apaza", new DateTime(2000, 9, 30), "967812345", "fab987");
            cUsuario u7 = new Modelos.cUsuario("ginellapm", "Ginella", "Pacheco Mendoza", new DateTime(2002, 4, 5), "978123456", "gin111");
            cUsuario u8 = new Modelos.cUsuario("hectorsu", "Héctor", "Sucari Ttito", new DateTime(2003, 8, 19), "989234567", "hec222");
            cUsuario u9 = new Modelos.cUsuario("inesalva", "Inés", "Álvarez Ccama", new DateTime(2001, 2, 28), "990345678", "ine333");
            cUsuario u10 = new Modelos.cUsuario("jorgeluq", "Jorge", "Luna Quispe", new DateTime(2000, 12, 1), "901456789", "jor444");
            s.Insertar(u0);
            s.Insertar(u1);
            s.Insertar(u2);
            s.Insertar(u3);
            s.Insertar(u4);
            s.Insertar(u5);
            s.Insertar(u6);
            s.Insertar(u7);
            s.Insertar(u8);
            s.Insertar(u9);
            s.Insertar(u10);
            cGrafo g = new cGrafo();

            g.AgregarVertice(u0);
            g.AgregarVertice(u1);
            g.AgregarVertice(u2);
            g.AgregarVertice(u3);
            g.AgregarVertice(u4);
            g.AgregarVertice(u5);
            g.AgregarVertice(u6);
            g.AgregarVertice(u7);
            g.AgregarVertice(u8);
            g.AgregarVertice(u9);
            g.AgregarVertice(u10);

            g.AgregarArista("987654321", "912345678"); // Juan   ↔ Ana
            g.AgregarArista("912345678", "923456781"); // Ana    ↔ Bruno
            g.AgregarArista("912345678", "934567812"); // Ana    ↔ Carlos
            g.AgregarArista("912345678", "945678123"); // Ana    ↔ Diana
            g.AgregarArista("923456781", "956781234"); // Bruno  ↔ Eduardo
            g.AgregarArista("923456781", "967812345"); // Bruno  ↔ Fabio
            g.AgregarArista("934567812", "978123456"); // Carlos ↔ Ginella
            g.AgregarArista("945678123", "989234567"); // Diana  ↔ Héctor
            g.AgregarArista("956781234", "990345678"); // Eduardo↔ Inés
            g.AgregarArista("967812345", "901456789"); // Fabio  ↔ Jorge
            g.AgregarArista("978123456", "901456789"); // Ginella↔ Jorge
            g.AgregarArista("989234567", "990345678"); // Héctor ↔ Inés
            g.AgregarArista("990345678", "901456789"); // Inés   ↔ Jorge 

            


            ServicioUsuarios miServicioUsuarios= new ServicioUsuarios(s,g);
            ServicioAmistades miServicioA = new ServicioAmistades(g);
            cLista amigosdeAna = miServicioA.ObtenerAmigos(u1);
            cLista amigosdeAnaBfs = miServicioA.ObtenerAmigosBFS(u1);
            amigosdeAna.ProcesarObjetosLista(obj =>
            {
                cUsuario amigos = obj as cUsuario;
                Console.WriteLine(amigos.NombreUsuario);
            }
            );
            Console.WriteLine("");
            amigosdeAnaBfs.ProcesarObjetosLista(obj =>
            {
                cUsuario amigos = obj as cUsuario;
                Console.WriteLine(amigos.NombreUsuario);
            }
            );

            Console.WriteLine("AMIGOS DE AMIGOS");
            cLista AMIGOSDEAMIGOSANA = miServicioA.ObtenerAmigosDeAmigos(u1);
            AMIGOSDEAMIGOSANA.ProcesarObjetosLista(obj =>
            {
                cUsuario u = obj as cUsuario;
                Console.WriteLine(u.NombreUsuario);
            });

            ServicioSolicitud solicitudserv = new ServicioSolicitud(miServicioUsuarios, miServicioA);
            string rpta = solicitudserv.EnviarSolicitud(u1.ToString(), u7.ToString());
            Console.WriteLine(rpta);
            cLista enviadas = solicitudserv.ObtenerSolicitudesEnviadas(u1);
            Console.WriteLine("solicituddes enviadas para: ");
            enviadas.ProcesarObjetosLista(obj =>
            {
                cUsuario u = obj as cUsuario;
                Console.WriteLine(u.NombreUsuario);

            });
            Console.WriteLine("Sugerencias para ana");
            cLista sugerencias = solicitudserv.ObtenerSugerencias(u1);
            sugerencias.ProcesarObjetosLista(obj =>
            {
                cUsuario u = obj as cUsuario;
                Console.WriteLine(u.NombreUsuario);
            });
            //ServicioAutenticacion miServicioAutenticacion = new ServicioAutenticacion(miServicioUsuarios);
            //FormLogin frm = new FormLogin(miServicioUsuarios, miServicioAutenticacion);
            //frm.ShowDialog();
        }
    }
}
