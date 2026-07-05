using System;

namespace RedSocialGB.Utilidades
{
    public static class cValidaciones
    {
        #region ==================== VALIDACIONES =======================

        // --------------------------------------------------------------
        public static bool ValidarNombres(string pNombres)
        {
            return !string.IsNullOrWhiteSpace(pNombres);
        }

        // --------------------------------------------------------------
        public static bool ValidarApellidos(string pApellidos)
        {
            return !string.IsNullOrWhiteSpace(pApellidos);
        }

        // --------------------------------------------------------------
        public static bool ValidarCelular(string pCelular)
        {
            if (string.IsNullOrWhiteSpace(pCelular))
                return false;

            if (pCelular.Length != 9)
                return false;

            foreach (char c in pCelular)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        // --------------------------------------------------------------
        public static bool ValidarContrasena(string pContrasena)
        {
            if (string.IsNullOrWhiteSpace(pContrasena))
                return false;

            return pContrasena.Length >= 4;
        }

        // --------------------------------------------------------------
        public static bool ValidarFechaNacimiento(DateTime pFechaNacimiento)
        {
            // No puede ser una fecha futura
            if (pFechaNacimiento > DateTime.Today)
                return false;
            

            return true;
        }
        public static bool ValidarEdad(DateTime pFechaNacimiento)
        {
            // Calcular la edad
            int edad = DateTime.Today.Year - pFechaNacimiento.Year;
            if (pFechaNacimiento > DateTime.Today.AddYears(-edad))
                edad--;
            // Debe ser mayor de edad (18 años o más)
            return edad >= 18;
        }

        #endregion ==================== VALIDACIONES =======================
    }
}