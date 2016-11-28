using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodoSimplex.Models
{
    public class Matriz
    {
        public Matriz()
        {
            this.restricciones = new List<Restriccion>();
            this.funcionObjetivo = new FuncionObjetivo();
        }

        public List<Restriccion> restricciones { get; set; }
        public FuncionObjetivo funcionObjetivo { get; set; }
    }
}
