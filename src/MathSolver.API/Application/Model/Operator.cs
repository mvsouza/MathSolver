using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathSolver.API.Application.Model
{
    public class Operator
    {
        public static IEnumerable<Operator> Precedence = new List<Operator> {
            new Operator('+',0,(acc, x) => acc + x),
            new Operator('*',1,(acc, x) => acc * x)
        };
        public char Simbol { get; }
        public double AggregationBase { get; }
        public Func<double,double,double> SolveAggregator { get; }

        public Operator(char simbol, double aggregationBase, Func<double, double, double> solveAggregator)
        {
            AggregationBase = aggregationBase;
            Simbol = simbol;
            SolveAggregator = solveAggregator;
        }

        public double Solve(IEnumerable<Factor> factors)
        {
            return factors.Select(f => f.Solve()).Aggregate(AggregationBase, SolveAggregator);
        }
        
        public static Operator Precedent(IEnumerable<char> operations)
        {
            return Precedence.First(o => operations.Any(desc => desc.Equals(o.Simbol)));
        }

        public static bool TryFindOperator(char simbol, out Operator reference)
        {
            reference = Precedence.FirstOrDefault(o=> o.Simbol==simbol);
            return reference != null;
        }
    }
}
