using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodoSimplex.Models
{
    public class Restriccion
    {
        public Restriccion()
        {
            this.variables = new List<int>();
        }

        public List<int> variables { get; set; }
        public int solucion { get; set; }
        public int operSolucion { get; set; }
    }
}
