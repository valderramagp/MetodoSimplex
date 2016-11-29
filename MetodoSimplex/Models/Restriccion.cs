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
            this.variables = new List<decimal>();
        }

        public List<decimal> variables { get; set; }
        public decimal solucion { get; set; }
        public decimal operSolucion { get; set; }
    }
}
