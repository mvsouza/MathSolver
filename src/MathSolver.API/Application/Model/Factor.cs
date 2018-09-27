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
            if(Calculus.Contains("+"))
                return Calculus.Split("+").Select(s => new Factor(s).Solve()).Sum();
            if (Calculus.Contains("*"))
                return Calculus.Split("*").Select(s => new Factor(s).Solve()).Aggregate(1.0, (acc, x) => acc * x);
            return int.Parse(Calculus);
        }
    }
}
