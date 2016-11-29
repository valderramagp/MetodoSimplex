using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodoSimplex.Models
{
    public static class Utils
    {
        public static decimal leerNumero()
        {

            decimal result = 0;
            var success = Decimal.TryParse(Console.ReadLine(), out result);
            if (!success)
            {
                Console.WriteLine("Por favor ingrese un valor válido");
                result = leerNumero();
            }
            return result;
        }

        public static decimal leerVarHolgura()
        {

            var variable = leerNumero();
            if (variable > 2 || variable < 1)
            {
                Console.WriteLine("Ingresa 1 o 2");
                variable = leerVarHolgura();
            }
            return variable;
        }

        public static int leerNumeroEntero()
        {
            int result = 0;
            var success = Int32.TryParse(Console.ReadLine(), out result);
            if (!success)
            {
                Console.WriteLine("Por favor ingrese un valor válido");
                result = leerNumeroEntero();
            }
            return result;
        }
    }
}
