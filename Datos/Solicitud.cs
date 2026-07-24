using MySql.Data.MySqlClient;
using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using System;

namespace RedSocialGB.Datos
{
    public class SolicitudDAO
    {
        #region *************** ATRIBUTOS ***************

        private ConexionBD aConexion;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public SolicitudDAO()
        {
            aConexion = new ConexionBD();
        }
        private cUsuario CrearUsuario(
    MySqlDataReader lector,
    string sufijo)
        {
            return new cUsuario(
                lector["NombreUsuario" + sufijo].ToString(),
                lector["Nombres" + sufijo].ToString(),
                lector["Apellidos" + sufijo].ToString(),
                Convert.ToDateTime(lector["FechaNacimiento" + sufijo]),
                lector["Celular" + sufijo].ToString(),
                lector["Contrasena" + sufijo].ToString()
            );
        }
        #endregion
        public bool Insertar(cSolicitudAmistad pSolicitud)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql = @"INSERT INTO SolicitudAmistad (Remitente,Destinatario,FechaEnvio) VALUES (@Remitente,@Destinatario,@Fecha)";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Remitente", pSolicitud.Remitente.ToString());
                comando.Parameters.AddWithValue("@Destinatario", pSolicitud.Destinatario.ToString());
                comando.Parameters.AddWithValue("@Fecha", pSolicitud.FechaEnvio);


                return comando.ExecuteNonQuery() > 0; //si se inserto al menos un registro, true, sino false
            }
        }

        public bool Eliminar(string pRemitente,
                             string pDestinatario)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql = @"DELETE FROM SolicitudAmistad WHERE (Remitente = @Remitente AND Destinatario = @Destinatario)";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Remitente", pRemitente);
                comando.Parameters.AddWithValue("@Destinatario", pDestinatario);

                return comando.ExecuteNonQuery() > 0; //si se elimino al menos un registro, true, sino false
            }
        }

        public cSolicitudAmistad Buscar(string pRemitente,
                                        string pDestinatario)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql =
                    @"SELECT

s.FechaEnvio,

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

FROM SolicitudAmistad s

JOIN Usuario u1
ON s.Remitente = u1.Celular

JOIN Usuario u2
ON s.Destinatario = u2.Celular

WHERE s.Remitente = @Remitente
AND s.Destinatario = @Destinatario;";
                MySqlCommand comando =
                    new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Remitente", pRemitente);
                comando.Parameters.AddWithValue("@Destinatario", pDestinatario);
                MySqlDataReader lector = comando.ExecuteReader();
                if (lector.Read())
                {
                    cUsuario remitente = CrearUsuario(lector, "1");

                    cUsuario destinatario = CrearUsuario(lector, "2");
                    DateTime fechaenvio = Convert.ToDateTime(lector["FechaEnvio"]);
                    return new cSolicitudAmistad(remitente, destinatario, fechaenvio);
                }
                return null;
            }
        }

        public cLista CargarDatos()
        {
            cLista lista = new cLista();
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {

                conexion.Open();
                string sql = @" SELECT
s.FechaEnvio,
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

FROM SolicitudAmistad s
JOIN Usuario u1 ON s.Remitente = u1.Celular
JOIN Usuario u2 ON s.Destinatario = u2.Celular;";
                MySqlCommand comando = new MySqlCommand(sql, conexion);

                MySqlDataReader lector = comando.ExecuteReader();
                while (lector.Read()) //mientras exista fila
                {
                    cUsuario remitente = CrearUsuario(lector, "1");

                    cUsuario destinatario = CrearUsuario(lector, "2");
                    DateTime fechaenvio = Convert.ToDateTime(lector["FechaEnvio"]);
                    cSolicitudAmistad solicitud = new cSolicitudAmistad(remitente,destinatario,fechaenvio);
                    lista.Agregar(solicitud);
                }
            }

            return lista;
        }

        public cLista ObtenerEnviadas(string pCelular) //pCelular es remitente
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

FROM SolicitudAmistad s
JOIN Usuario u2 ON (s.Remitente = @Celular AND u2.Celular=s.Destinatario)";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Celular", pCelular);

                MySqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    cUsuario destinatario = CrearUsuario(lector, "2");
                    lista.Agregar(destinatario);
                }

            }
            return lista;
        }

        public cLista ObtenerPendientes(string pCelular)
        {
            cLista lista = new cLista();
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();
                //si soy celular 1 y celular2= pCelular o si soy celular 2 y celular1= pCelular
                string sql = @" SELECT

u1.Celular AS Celular1,
u1.NombreUsuario AS NombreUsuario1,
u1.Nombres AS Nombres1,
u1.Apellidos AS Apellidos1,
u1.FechaNacimiento AS Fecha1,
u1.Contrasena AS Contrasena1

FROM SolicitudAmistad s
JOIN Usuario u1 ON (s.Destinatario = @Celular AND u1.Celular=s.Remitente)";
                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Celular", pCelular);

                MySqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    cUsuario remitente = CrearUsuario(lector, "1");
                    
                    lista.Agregar(remitente);
                }

            }
            return lista;
        }
    }
}