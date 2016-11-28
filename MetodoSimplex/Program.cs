using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetodoSimplex.Models;

namespace MetodoSimplex
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Parametros Iniciales
            Console.WriteLine("Bienvenido, ingrese la cantidad de variables que usará el problema");
            int numVariables = Utils.leerInt();
            Console.WriteLine("Ingrese la cantidad de restricciones que contendrá el problema");
            int numRestricciones = Utils.leerInt();
            var matrix = new Matriz();
            #endregion

            #region creación de restricciones
            for (int i = 0; i < numRestricciones; i++)
            {
                var restriccion = new Restriccion();
                for (int j = 0; j < numVariables; j++)
                {
                    Console.WriteLine("Ingrese el valor de X{0} para la restricción #{1}", (j + 1), (i + 1));
                    restriccion.variables.Add(Utils.leerInt());
                }

                Console.WriteLine("Elija una opción para el operador de la Solución para la restricción #{0}", (i+1));
                Console.WriteLine("1.- >= (Mayor o igual)");
                Console.WriteLine("2.- <= (Menor o igual)");
                restriccion.operSolucion = Utils.leerVarHolgura();

                Console.WriteLine("Ingrese el valor de la Solución para la restricción #{0}", (i + 1));
                restriccion.solucion = Utils.leerInt();
                matrix.restricciones.Add(restriccion);
            }
            #endregion

            #region Función Objetivo
            for (int i = 0; i < numVariables; i++)
            {
                Console.WriteLine("Ingrese el valor de X{0} para la función Objetivo", i + 1);
                var variable = Utils.leerInt();
                matrix.funcionObjetivo.variables.Add(variable);
            }
            #endregion

            #region PASO 1 - Modelación mediante programación Lineal
            Console.WriteLine("1.- Modelo");
            foreach(var restriccion in matrix.restricciones)
            {
                int posicion = 1;
                var oper = "";
                foreach (var variable in restriccion.variables)
                {
                    if (variable > 0)
                        Console.Write("+" + variable + "X" + posicion);
                    else if (variable < 0)
                        Console.Write("" + variable + "X" + posicion);
                    else
                        Console.Write("");
                    
                    posicion++;
                    Console.Write("\t");
                }
                oper = restriccion.operSolucion == 1 ? ">=" : "<=";
                Console.Write(oper + restriccion.solucion);
                Console.Write("\t");
                Console.WriteLine();            
            }
            #endregion
            Console.ReadKey();

            #region PASO 2 - Igualar a 0 la función objetivo
            Console.WriteLine("2.- Igualar a 0 la función Objetivo");
            
            foreach(var item in matrix.funcionObjetivo.variables)
            {
                var nuevoValor = item * -1;
                matrix.funcionObjetivo.igualada.Add(nuevoValor);
            }
            #endregion

            #region PASO 3 - Hacer la matriz aumentada
            var iteracion = 0;
            foreach (var restriccion in matrix.restricciones)
            {
                for(int i = 0; i < numVariables; i++)
                {
                    var signo = restriccion.operSolucion == 1 ? -1 : 1; //Se suman si la restricción es <= y se restan si es >=
                    var varHolgura = iteracion == i ? 1 : 0;
                    restriccion.variables.Add(varHolgura * signo);
                }
                iteracion++;
            }

            //leer de nuevo
            //HEAD
            for(int i = 1; i <= numVariables; i++)
            {
                Console.Write("X{0}", i);
                Console.Write("\t");
            }
            for(int i = 1; i <= numVariables; i++)
            {
                Console.Write("S{0}", i);
                Console.Write("\t");
            }
            Console.Write("Solución");
            Console.WriteLine();

            //body
            foreach(var variable in matrix.funcionObjetivo.igualada)
            {
                Console.Write(variable + "\t");
            }

            Console.WriteLine();

            foreach(var restriccion in matrix.restricciones)
            {
                var i = 0;
                foreach(var variable in restriccion.variables)
                {
                    Console.Write(variable + "\t");
                }
                Console.Write(restriccion.solucion + "\n");
            }
            #endregion
            Console.ReadKey();

            #region ITERACIÓN
            #region PASO 4 - Identificar en la función objetivo, el número más negativo
            var indexColumna = matrix.funcionObjetivo.variables.Select((x, i) => new { numero = x, index = i }).Min().index; //obtiene columna pivote
            #endregion

            #region PASO 5 - DIvidir la solución entre la columna pivote y tomar el cociente de menor valor; 
            var cocientes = new List<int>();
            foreach(var restriccion in matrix.restricciones)
            {
                var cociente = restriccion.solucion / restriccion.variables.ElementAtOrDefault(indexColumna);
                cocientes.Add(cociente);
            }
            var indexFila = cocientes.Select((x, i) => new { numero = x, index = i }).Where(x => x.numero > 0).Min().index; //si hay 0 o negativo no se considera
            #endregion

            #region PASO 6 - Convertir el elemento pivote en 1 dividiendo

            #endregion


            #endregion
        }
    }
}