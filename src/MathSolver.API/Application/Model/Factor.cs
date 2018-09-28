using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathSolver.API.Application.Model
{
    public class Factor
    {
        public string Calculus { get; private set; }
        public IEnumerable<Factor> _factors;
        public IEnumerable<Factor> Factors
        {
            get
            {
                if (_factors == null)
                    _factors = BreakIntoFactors();

                return _factors;
            }
        }
        public (char desc, double seed, Func<double, double, double> func) Operation { get; private set; }
        
        private List<(char desc, int index)> _higherOperations;
        public List<(char desc, int index)> HigherOperations
        {
            get
            {
                if (_higherOperations != null)
                    return _higherOperations;
                Queue<int> parentesesQueue = new Queue<int>();
                List<(char desc, int index)> operations = new List<(char desc, int index)>();
                var numberIndex = 0;
                for (int index = 0; index < Calculus.Length; index++)
                {
                    switch (Calculus[index])
                    {
                        case '(':
                            parentesesQueue.Enqueue(index);
                            numberIndex = index + 1;
                            break;
                        case ')':
                            var startParenteses = parentesesQueue.Dequeue();
                            numberIndex = index + 1;
                            break;
                        case ('*'):
                            if (parentesesQueue.Count == 0)
                            {
                                operations.Add(('*', index));
                                numberIndex = index + 1;
                            }
                            break;
                        case ('+'):
                            if (parentesesQueue.Count == 0)
                            {
                                operations.Add(('+', index));
                                numberIndex = index + 1;
                            }
                            break;
                    }
                }
                _higherOperations = operations;
                return operations;
            }
        }
        public bool HasOperator => HigherOperations.Any();
        
        public Factor(string calculus)
        {
            Calculus = calculus;

        }
        private IEnumerable<Factor> BreakIntoFactors()
        {
            List<Factor> factors = new List<Factor>();
            if (HasOperator)
            {
                Operation = Precedence.First(p => HigherOperations.Any(o => o.desc.Equals(p.desc)));
                var factorStart = 0;
                

                for (int index = 0; index < HigherOperations.Count; index++)
                {
                    if (HigherOperations[index].desc != Operation.desc)
                        continue;
                    factors.Add(new Factor(Calculus.Substring(factorStart, (HigherOperations[index].index - factorStart))));
                    factorStart = HigherOperations[index].index + 1;
                }

                factors.Add(new Factor(Calculus.Substring(factorStart, (Calculus.Length - factorStart))));
            }
            return factors;
        }
        

        public static IEnumerable<(char desc, double seed, Func<double, double, double> func)> Precedence = new (char desc, double seed, Func<double, double, double> func)[] {
            ('+',0,(acc, x) => acc + x),
            ('*',1,(acc, x) => acc * x)
        };
        public double Solve()
        {
            if (HasOperator)
                return Factors.Select(f => f.Solve()).Aggregate(Operation.seed, Operation.func);
            if (Calculus[0] == '(' && Calculus[Calculus.Length-1] == ')')
            {
                Calculus = Calculus.Substring(1, Calculus.Length - 2);
            }
            double value;
            if (double.TryParse(Calculus, out value)){
                return value;
            }
            return new Factor(Calculus).Solve();


        }

    }
}
