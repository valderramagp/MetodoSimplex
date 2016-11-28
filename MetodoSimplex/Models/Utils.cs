using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodoSimplex.Models
{
    public static class Utils
    {
        public static int leerInt()
        {

            int result = 0;
            var success = Int32.TryParse(Console.ReadLine(), out result);
            if (!success)
            {
                Console.WriteLine("Por favor ingrese un valor válido");
                result = leerInt();
            }
            return result;
        }

        public static int leerVarHolgura()
        {

            var variable = leerInt();
            if (variable > 2 || variable < 1)
            {
                Console.WriteLine("Ingresa 1 o 2");
                variable = leerVarHolgura();
            }
            return variable;
        }
    }
}
