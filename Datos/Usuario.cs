using MySql.Data.MySqlClient;
using RedSocialGB.Datos;
using RedSocialGB.Estructuras;
using RedSocialGB.Modelos;
using System;

namespace RedSocialGB.Datos
{
    public class UsuarioDAO
    {
        #region *************** ATRIBUTOS ***************

        private ConexionBD aConexion;

        #endregion

        #region *************** CONSTRUCTOR ***************

        public UsuarioDAO()
        {
            aConexion = new ConexionBD();
        }

        #endregion

        #region *************** MÉTODOS ***************
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
        //-------------------------------------------------
        public bool Insertar(cUsuario pUsuario)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql =
                @"INSERT INTO Usuario
        (NombreUsuario,Nombres,Apellidos,FechaNacimiento,Celular,Contrasena)
        VALUES
        (@NombreUsuario,@Nombres,@Apellidos,@Fecha,@Celular,@Contrasena)";

                MySqlCommand comando = new MySqlCommand(sql, conexion);

                comando.Parameters.AddWithValue("@NombreUsuario", pUsuario.NombreUsuario);
                comando.Parameters.AddWithValue("@Nombres", pUsuario.Nombres);
                comando.Parameters.AddWithValue("@Apellidos", pUsuario.Apellidos);
                comando.Parameters.AddWithValue("@Fecha", pUsuario.FechaNacimiento);
                comando.Parameters.AddWithValue("@Celular", pUsuario.Celular);
                comando.Parameters.AddWithValue("@Contrasena", pUsuario.Contrasena);

                return comando.ExecuteNonQuery() > 0;
            }
        }

        //-------------------------------------------------
        public cUsuario BuscarPorCelular(string pCelular)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql =
                    @"SELECT *
              FROM Usuario
              WHERE Celular=@Celular";

                MySqlCommand comando =
                    new MySqlCommand(sql, conexion);

                comando.Parameters.AddWithValue("@Celular", pCelular);

                MySqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    return CrearUsuario(lector, "");
                }

                return null;
            }
        }

        //-------------------------------------------------
        public bool Eliminar(string pCelular)
        {
            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql =
                    @"DELETE
              FROM Usuario
              WHERE Celular=@Celular";

                MySqlCommand comando =
                    new MySqlCommand(sql, conexion);

                comando.Parameters.AddWithValue("@Celular", pCelular);

                return comando.ExecuteNonQuery() > 0;
            }
        }

        //-------------------------------------------------
        public cArbolB CargarDatos()
        {
            cArbolB arbol = new cArbolB(20);

            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql =
                    @"SELECT *
              FROM Usuario";

                MySqlCommand comando =
                    new MySqlCommand(sql, conexion);

                MySqlDataReader lector =
                    comando.ExecuteReader();

                while (lector.Read())
                {
                    cUsuario Usuario = CrearUsuario(lector, "");

                    arbol.Insertar(Usuario);
                }
            }

            return arbol;
        }

        //-------------------------------------------------
        public cLista BuscarPorNombre(string pNombre)
        {
            cLista lista = new cLista();

            using (MySqlConnection conexion = aConexion.ObtenerConexion())
            {
                conexion.Open();

                string sql =
                @"SELECT *
          FROM Usuario
          WHERE NombreUsuario LIKE @Nombre
             OR Nombres LIKE @Nombre OR Apellidos LIKE @Nombre";

                MySqlCommand comando =
                    new MySqlCommand(sql, conexion);

                comando.Parameters.AddWithValue("@Nombre",
                    "%" + pNombre + "%"); //el porcentaje ayuda con like para buscar palabras iguales o que contengan pNombre

                MySqlDataReader lector =
                    comando.ExecuteReader();

                while (lector.Read())
                {
                    cUsuario Usuario = CrearUsuario(lector, "");

                    lista.Agregar(Usuario);
                }
            }

            return lista;
        }

        #endregion
    }
}
