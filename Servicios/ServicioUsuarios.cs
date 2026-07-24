using RedSocialGB.Datos;
using RedSocialGB.Estructuras;
using RedSocialGB.Estructuras.Grafo;
using RedSocialGB.Modelos;
using RedSocialGB.Utilidades;
using System;

namespace RedSocialGB.Servicios
{
    public class ServicioUsuarios
    {
        #region *************** ATRIBUTOS ***************
        private UsuarioDAO aUsuarioDao;
        private cArbolB aArbolUsuarios;
        private cGrafo aGrafoUsuarios;

        #endregion

        #region *************** CONSTRUCTORES ***************

        public ServicioUsuarios()
        {
            aGrafoUsuarios = new cGrafo();
            aArbolUsuarios = new cArbolB(20);
            aUsuarioDao = new UsuarioDAO();
        }
        public ServicioUsuarios(cArbolB pArbolUsuarios,cGrafo pGrafoU,UsuarioDAO pUsuarioDAO)
        {
            aArbolUsuarios = pArbolUsuarios;
            aGrafoUsuarios= pGrafoU;
            aUsuarioDao = pUsuarioDAO;
        }
        #region *****************PROPIEDADES*******************
        public cArbolB ArbolUsuarios { get => aArbolUsuarios; set => aArbolUsuarios = value; }
        public cGrafo GrafoUsuarios { get => aGrafoUsuarios; set => aGrafoUsuarios = value; }
        public UsuarioDAO UsuarioDAO { get => aUsuarioDao; set => aUsuarioDao = value; }
        #endregion
        #endregion

        #region *************** MÉTODOS ***************

        // -------------------------------------------------
        public string RegistrarUsuario(string NombreU,string pNombres,
                                       string pApellidos,
                                       DateTime pFechaNacimiento,
                                       string pCelular,
                                       string pContrasena)
        {
            // Validaciones
            if (!cValidaciones.ValidarNombreUsuario(NombreU))
                return "Debe ingresar un nombre de usuario válido.";
            if (!cValidaciones.ValidarCadena(pNombres))
                return "Debe ingresar los nombres.";

            if (!cValidaciones.ValidarCadena(pApellidos))
                return "Debe ingresar los apellidos.";

            if (!cValidaciones.ValidarFechaNacimiento(pFechaNacimiento))
                return "La fecha de nacimiento no es válida.";

            if (!cValidaciones.ValidarCelular(pCelular))
                return "El número de celular debe tener 9 dígitos.";

            if (!cValidaciones.ValidarContrasena(pContrasena))
                return "La contraseña debe tener al menos 4 caracteres.";
            

            if (!cValidaciones.ValidarFechaNacimiento(pFechaNacimiento))
            {
                return "Fecha no valida";
            }
            if (!cValidaciones.ValidarEdad(pFechaNacimiento))
            {
                return "Debe ser mayor de edad.";
            }
            // ¿Ya existe?
            if (ExisteCelular(pCelular))
                return "El número de celular ya está registrado.";

            // Crear usuario
            cUsuario nuevoUsuario = new cUsuario(NombreU,
                pNombres,
                pApellidos,
                pFechaNacimiento,
                pCelular,
                pContrasena);

            // Insertar en el Árbol B
            aArbolUsuarios.Insertar(nuevoUsuario);
            aGrafoUsuarios.AgregarVertice(nuevoUsuario);
            aUsuarioDao.Insertar(nuevoUsuario);

            return "Usuario registrado correctamente.";
        }


        // -------------------------------------------------
        public cUsuario BuscarPorCelular(string pCelular)
        {
            return (aArbolUsuarios.Buscar(pCelular,u =>(u as cUsuario).Celular)) as cUsuario;
        }
        public cUsuario BuscarPorCelularMySql(string pCelular)
        {
            return (aUsuarioDao.BuscarPorCelular(pCelular));
        }

        // -------------------------------------------------
        public cLista BuscarUsuarios(string pNombre)
        {
            cLista encontrados = new cLista();
            aArbolUsuarios.Recorrer(obj =>
            {
                cUsuario usuario = obj as cUsuario;

                if (usuario == null)
                    return;
                //comparamos si el nombre a buscar esta contenido, en apellidos, nombres o nombre de usuario, si es asi lo agregamos a la lista de encontrados
                if (usuario.NombreUsuario.ToLower().Contains(pNombre.ToLower()) ||
                    usuario.Nombres.ToLower().Contains(pNombre.ToLower()) ||
                    usuario.Apellidos.ToLower().Contains(pNombre.ToLower()))
                {
                    encontrados.Agregar(usuario);
                }
            });

            return encontrados;
        
        }
        public cLista BuscarUsuariosMysql(string pNombre)
        {
            return aUsuarioDao.BuscarPorNombre(pNombre);
        }
        
        public bool ExisteCelular(string pCelular)
        {
            return BuscarPorCelular(pCelular) != null;
        }

        // -------------------------------------------------
        public bool EliminarUsuario(string pCelular)
        {
            if (!ExisteCelular(pCelular))
                return false;

            aArbolUsuarios.Eliminar(pCelular);
            aUsuarioDao.Eliminar(pCelular);
            return true;
        }
        

        #endregion
    }
}