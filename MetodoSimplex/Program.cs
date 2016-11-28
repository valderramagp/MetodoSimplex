using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodoSimplex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido, ingrese la cantidad de variables que usará el problema");
            int numVariables = leerInt();
            Console.WriteLine("Ingrese la cantidad de restricciones que contendrá el problema");
            int numRestricciones = leerInt();

            var matrix = new List<Restriccion>();
            for (int i = 0; i < numRestricciones; i++)
            {
                var restriccion = new Restriccion();
                restriccion.variable = new List<int>();
                for (int j = 0; j < numVariables; j++)
                {
                    Console.WriteLine("Ingrese el valor de X{0} para la restricción #{1}", (j + 1), (i + 1));
                    restriccion.variable.Add(leerInt());
                }

                Console.WriteLine("Elija una opción para el operador de la variable de holgura para la restricción #{0}", (i+1));
                Console.WriteLine("1.- >= (Mayor o igual)");
                Console.WriteLine("2.- <= (Menor o igual)");
                restriccion.operVarHolgura = leerVarHolgura();

                Console.WriteLine("Ingrese el valor de la variable de holgura para la restricción #{0}", (i + 1));
                restriccion.varHolgura = leerInt();
                matrix.Add(restriccion);
            }

            Console.WriteLine("Modelo Matemático:");
            foreach(var restriccion in matrix)
            {
                int posicion = 1;
                var oper = "";
                foreach (var variable in restriccion.variable)
                {
                    if (variable > 0)
                        Console.Write("+" + variable + "X" + posicion);
                    else if (variable < 0)
                        Console.Write("-" + variable + "X" + posicion);
                    else
                        Console.Write("  ");
                    
                    posicion++;
                }
                oper = restriccion.operVarHolgura == 1 ? ">=" : "<=";
                Console.Write(oper + restriccion.varHolgura);
                Console.WriteLine();            
            }

            Console.ReadKey();
        }

        private static int leerVarHolgura()
        {
            
            var variable = leerInt();
            if(variable > 2 || variable < 1)
            {
                Console.WriteLine("Ingresa 1 o 2");
                variable = leerVarHolgura();
            }
            return variable;
        }

        private static int leerInt()
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


    }

    public class Restriccion
    {
        public List<int> variable { get; set; }
        public int varHolgura { get; set; }
        public int operVarHolgura { get; set; }


    }
}