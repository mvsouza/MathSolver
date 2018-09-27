using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathSolver.API.Application.Model
{
    public class Factor
    {
        public string Calculus { get; set; }
        public Factor(string calculus)
        {
            Calculus = calculus;
        }
        public double Solve()
        {
            return Calculus.Split("+").Select(s => int.Parse(s)).Sum();
        }
    }
}
