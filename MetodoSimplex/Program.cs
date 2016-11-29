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
            var numVariables = Utils.leerNumeroEntero();
            Console.WriteLine("Ingrese la cantidad de restricciones que contendrá el problema");
            var numRestricciones = Utils.leerNumeroEntero();
            var matrix = new Matriz(numVariables, numRestricciones);
            #endregion

            #region creación de restricciones
            for (int i = 0; i < numRestricciones; i++)
            {
                var restriccion = new Restriccion();
                for (int j = 0; j < numVariables; j++)
                {
                    Console.WriteLine("Ingrese el valor de X{0} para la restricción #{1}", (j + 1), (i + 1));
                    restriccion.variables.Add(Utils.leerNumero());
                }

                Console.WriteLine("Elija una opción para el operador de la Solución para la restricción #{0}", (i+1));
                Console.WriteLine("1.- >= (Mayor o igual)");
                Console.WriteLine("2.- <= (Menor o igual)");
                restriccion.operSolucion = Utils.leerVarHolgura();

                Console.WriteLine("Ingrese el valor de la Solución para la restricción #{0}", (i + 1));
                restriccion.solucion = Utils.leerNumero();
                matrix.restricciones.Add(restriccion);
            }
            #endregion

            #region Función Objetivo
            for (int i = 0; i < numVariables; i++)
            {
                Console.WriteLine("Ingrese el valor de X{0} para la función Objetivo", i + 1);
                var variable = Utils.leerNumero();
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
            var funObj = new Restriccion();
            foreach(var item in matrix.funcionObjetivo.variables)
            {
                var nuevoValor = item * -1;
                Console.Write(nuevoValor + "\t");
                funObj.variables.Add(nuevoValor);
            }
            for (int i = 0; i < numRestricciones; i++)
            {
                funObj.variables.Add(0);
            }
            funObj.solucion = 0;
            matrix.restricciones.Insert(0, funObj); //se inserta la función objetivo al principio de la lista
            Console.Write("= \t0");
            Console.WriteLine();
            #endregion

            #region PASO 3 - Hacer la matriz aumentada
            Console.WriteLine("3.- Desarrollar la matriz aumentada");
            var iteracion = 0;
            var iteracion2 = 0;
            foreach (var restriccion in matrix.restricciones)
            {
                if (iteracion2 == 0)
                {
                    iteracion2++;
                    continue;
                }

                for(int i = 0; i < numRestricciones; i++)
                {
                    var signo = restriccion.operSolucion == 1 ? -1 : 1; //Se suman si la restricción es <= y se restan si es >=
                    var varHolgura = iteracion == i ? 1 : 0;
                    restriccion.variables.Add(varHolgura * signo);
                }
                iteracion++;
            }
            #endregion
            matrix.Print();
            Console.ReadKey();

            #region ITERACIÓN
            while (matrix.hayNegativos())
            {

            
                #region PASO 4 - Identificar en la función objetivo, el número más negativo
                Console.WriteLine("4.- Identificar en la función objetivo el número más negativo");
                var col = matrix.restricciones.ElementAtOrDefault(0).variables.Select((x, i) => new { numero = x, index = i }).Where(x => x.numero < 0).OrderBy(x => x.numero).FirstOrDefault(); //obtiene columna pivote
                Console.WriteLine("Número más negativo es {0} de la columna {1}", col.numero, col.index + 1);
                #endregion

                Console.ReadKey();

                #region PASO 5 - Dividir la solución entre la columna pivote y tomar el cociente de menor valor; 
                Console.WriteLine("5.- Dividir la solución entre la columna pivote y tomar el cociente de menor valor");
                var cocientes = new List<decimal>();
                foreach(var restriccion in matrix.restricciones)
                {
                
                    var cociente = restriccion.solucion / restriccion.variables.ElementAtOrDefault(col.index);
                    cocientes.Add(cociente);
                    Console.WriteLine("{0}/{1} = {2}", restriccion.solucion, restriccion.variables.ElementAtOrDefault(col.index), cociente);
                }
                var fila = cocientes.Select((x, i) => new { numero = x, index = i }).Where(x => x.numero > 0).OrderBy(x => x.numero).FirstOrDefault(); //si hay 0 o negativo no se considera
                Console.WriteLine("El cociente de menor valor es: {0} de S{1}", fila.numero, fila.index);
                #endregion

                Console.ReadKey();

                #region PASO 6 - Convertir el elemento pivote en 1 dividiendo
                Console.WriteLine("6.- Convertir el elemento pivote en 1 dividiendo");
                var filaPivote = matrix.restricciones.ElementAtOrDefault(fila.index);
                var colPivote = filaPivote.variables.ElementAtOrDefault(col.index);
                var restTemp = new Restriccion();
                foreach(var variable in filaPivote.variables)
                {
                    var temp = variable / colPivote;
                    restTemp.variables.Add(temp);
                    restTemp.solucion = filaPivote.solucion / colPivote;
                }

                //reemplaza el elemento temporal por la nueva restricción
                matrix.restricciones.RemoveAt(fila.index);
                matrix.restricciones.Insert(fila.index, restTemp);

                #endregion
                matrix.Print();
                Console.ReadKey();

                #region PASO 7 - Convertir a 0 las demás restricciones
                Console.WriteLine("7.- Convertir a 0 las demás restricciones");

                filaPivote = matrix.restricciones.ElementAtOrDefault(fila.index);

                for (int i = 0; i < matrix.numRestricciones; i++)
                {
                    if (i == fila.index) continue;
                    restTemp = matrix.restricciones.ElementAtOrDefault(i);
                    var constante = restTemp.variables.ElementAtOrDefault(col.index);
                    for(int j = 0; j < restTemp.variables.Count; j++)
                    {
                        var n1 = restTemp.variables.ElementAtOrDefault(j);
                        var n2 = filaPivote.variables.ElementAtOrDefault(j);
                        decimal n3 = n1 - constante * n2;
                        restTemp.variables.RemoveAt(j);
                        restTemp.variables.Insert(j, Math.Round(n3, 2));
                    }
                    restTemp.solucion = restTemp.solucion - constante * filaPivote.solucion;

                    matrix.restricciones.RemoveAt(i);
                    matrix.restricciones.Insert(i, restTemp);
                    matrix.Print();
                    Console.ReadKey();
                }
                    #endregion

            }

            #endregion

            Console.WriteLine("Z = {0}", matrix.restricciones.ElementAtOrDefault(0).solucion);
            for(int i = 1; i <= numVariables; i++)
            {
                Console.WriteLine("X{0} = {1}", i, matrix.restricciones.ElementAtOrDefault(i).solucion);
            }
            Console.ReadKey();
        }
    }
}