using MySql.Data.MySqlClient;
using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using System;
using RedSocialGB.Estructuras.Grafo;

namespace RedSocialGB.Datos
{
    public class AmistadDAO
    {
        #region *************** ATRIBUTOS ***************

        private ConexionBD aConexion;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public AmistadDAO()
        {
            aConexion = new ConexionBD();
        }

        #endregion

        private cUsuario CrearUsuario(
MySqlDataReader lector,
string sufijo)
        {
            return new cUsuario(
                lector["NombreUsuario" + sufijo].ToString(),
                lector["Nombres" + sufijo].ToString(),
                lector["Apellidos" + sufijo].ToString(),
                Convert.ToDateTime(lector["Fecha" + sufijo]),
                lector["Celular" + sufijo].ToString(),
                lector["Contrasena" + sufijo].ToString()
            );
        }
        public bool Insertar(string pCelular1,string pCelular2)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql = @"INSERT INTO Amistad (Celular1,Celular2) VALUES (@Celular1,@Celular2)";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Celular1", pCelular1);
                comando.Parameters.AddWithValue("@Celular2", pCelular2);

                return comando.ExecuteNonQuery() > 0; //si se inserto al menos un registro, true, sino false
            }

        }

        public bool Eliminar(string pCelular1,
                             string pCelular2)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql = @"DELETE FROM Amistad WHERE (Celular1 = @Celular1 AND Celular2 = @Celular2)
   OR (Celular1 = @Celular2 AND Celular2 = @Celular1) ";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Celular1", pCelular1);
                comando.Parameters.AddWithValue("@Celular2", pCelular2);

                return comando.ExecuteNonQuery() > 0; //si se elimino al menos un registro, true, sino false
            }
        }

        public bool SonAmigos(string pCelular1,
                              string pCelular2)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();
                //la amistad es un grafo no dirigido buscamos en ambos sentidos
                //con count(*) contamos las filas que cumplen con el where
                string sql = @"SELECT COUNT(*) FROM Amistad WHERE (Celular1 = @Celular1 AND Celular2 = @Celular2)
   OR (Celular1 = @Celular2 AND Celular2 = @Celular1) ";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Celular1", pCelular1);
                comando.Parameters.AddWithValue("@Celular2", pCelular2);
                int cantidad = int.Parse(comando.ExecuteScalar().ToString());
                return cantidad > 0;
                
            }
        }

        public cGrafo CargarDatos()
        {

            cGrafo grafo = new cGrafo();
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();
                string sql = @" SELECT
u1.Celular AS Celular1,
u1.NombreUsuario AS NombreUsuario1,
u1.Nombres AS Nombres1,
u1.Apellidos AS Apellidos1,
u1.FechaNacimiento AS Fecha1,
u1.Contrasena AS Contrasena1,

u2.Celular AS Celular2,
u2.NombreUsuario AS NombreUsuario2,
u2.Nombres AS Nombres2,
u2.Apellidos AS Apellidos2,
u2.FechaNacimiento AS Fecha2,
u2.Contrasena AS Contrasena2

FROM Amistad a
JOIN Usuario u1 ON a.Celular1 = u1.Celular
JOIN Usuario u2 ON a.Celular2 = u2.Celular";
                MySqlCommand comando = new MySqlCommand(sql, conexion);

                MySqlDataReader lector = comando.ExecuteReader();
                while (lector.Read()) //mientras exista fila
                {
                    cUsuario Usuario1 = CrearUsuario(lector, "1");
                    cUsuario Usuario2 = CrearUsuario(lector, "2");

                    grafo.AgregarVertice(Usuario1);
                    grafo.AgregarVertice(Usuario2);

                    grafo.AgregarArista(
                        Usuario1.Celular,
                        Usuario2.Celular);

                }

            }
            return grafo;
        }

        public cLista ObtenerAmigos(string pCelular)
        {
            cLista lista = new cLista();
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();
                //si soy celular 1 y celular2= pCelular o si soy celular 2 y celular1= pCelular
                string sql = @" SELECT

u2.Celular AS Celular2,
u2.NombreUsuario AS NombreUsuario2,
u2.Nombres AS Nombres2,
u2.Apellidos AS Apellidos2,
u2.FechaNacimiento AS Fecha2,
u2.Contrasena AS Contrasena2

FROM Amistad a    
JOIN Usuario u2 ON (a.Celular1=@Celular AND a.Celular2=u2.Celular) OR (a.Celular2=@Celular AND a.Celular1=u2.Celular)";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Celular", pCelular);
                
                MySqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    cUsuario Usuario2 = CrearUsuario(lector, "2");
                    lista.Agregar(Usuario2);
                }

            }
            return lista;
        }
    }
}