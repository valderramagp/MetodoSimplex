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
            this.variables = new List<int>();
            this.igualada = new List<int>();
        }

        public List<int> variables { get; set; }
        public List<int> igualada { get; set; }
    }
}
