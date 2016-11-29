using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodoSimplex.Models
{
    public class FuncionObjetivo
    {
        public FuncionObjetivo()
        {
            this.variables = new List<decimal>();
            this.igualada = new List<decimal>();
        }

        public List<decimal> variables { get; set; }
        public List<decimal> igualada { get; set; }
    }
}
