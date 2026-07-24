using MySql.Data.MySqlClient;
using System;
namespace RedSocialGB.Datos
{
    public class ConexionBD
    {
        #region *************** ATRIBUTOS ***************

        private string cadenaConexion ="server=localhost;" +
            "database=RedSocial2;" +"user=root;" +"password=violettcass8A#;";


        #endregion

        #region *************** MÉTODOS ***************

        //-------------------------------------------------
        public MySqlConnection ObtenerConexion()
        {
            try
            {
                MySqlConnection conexion =
                new MySqlConnection(cadenaConexion);
                
                return conexion;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al conectar a la base de datos: " + ex.Message);
            }

        }

        //-------------------------------------------------
        public void CerrarConexion(MySqlConnection pConexion)
        {
            if (pConexion != null &&
                pConexion.State == System.Data.ConnectionState.Open)
            {
                pConexion.Close();
            }
        }

        #endregion
    }
}
