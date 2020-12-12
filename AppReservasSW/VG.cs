using System;
using AppReservasSW.Models;
using System.Text.RegularExpressions;

namespace AppReservasSW
{
    public class VG
    {
        public static Usuario usuarioActual { get; set; }

        /// <summary>
        /// Verifica que lo que se le haya dado sea solo números.
        /// </summary>
        /// <param name="input">Cadena de texto a evaluar.</param>
        /// <returns>True si es una cadena de números compuesta de
        /// solo números.</returns>
        public static bool CadenaSoloNumeros(string input)
        {
            string pattern = @"^[0-9]*$";
            RegexOptions options = RegexOptions.Multiline;

            foreach (Match m in Regex.Matches(input, pattern, options))
            {
                return true;
            }
            return false;
        }

    }
}