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
        public Operator Operation { get => Operator.Precedent(HigherOperations.Select(o => o.desc)); }
        
        private List<(string desc, int index)> _higherOperations;
        public List<(string desc, int index)> HigherOperations
        {
            get
            {
                if (_higherOperations != null)
                    return _higherOperations;
                Queue<int> parentesesQueue = new Queue<int>();
                List<(string desc, int index)> operations = new List<(string desc, int index)>();
                for (int index = 0; index < Calculus.Length; index++)
                {
                    switch (Calculus[index])
                    {
                        case '(':
                            parentesesQueue.Enqueue(index);
                            break;
                        case ')':
                            var startParenteses = parentesesQueue.Dequeue();
                            break;
                        case ('*'):
                            if (parentesesQueue.Count == 0)
                            {
                                operations.Add(("*", index));
                            }
                            break;
                        case ('+'):
                            if (parentesesQueue.Count == 0)
                            {
                                operations.Add(("+", index));
                            }
                            break;
                    }
                }
                _higherOperations = operations;
                return operations;
            }
        }
        public bool IsComposedFactor => HigherOperations.Any();
        
        public Factor(string calculus)
        {
            Calculus = calculus;

        }
        private IEnumerable<Factor> BreakIntoFactors()
        {
            List<Factor> factors = new List<Factor>();
            if (IsComposedFactor)
            {
                var factorStart = 0;
                

                for (int index = 0; index < HigherOperations.Count; index++)
                {
                    if (HigherOperations[index].desc != Operation.Description)
                        continue;
                    factors.Add(new Factor(Calculus.Substring(factorStart, (HigherOperations[index].index - factorStart))));
                    factorStart = HigherOperations[index].index + 1;
                }

                factors.Add(new Factor(Calculus.Substring(factorStart, (Calculus.Length - factorStart))));
            }
            return factors;
        }
        
        
        public double Solve()
        {
            if (IsComposedFactor)
                return this.Operation.Solve(Factors);
            double value;
            if (double.TryParse(Calculus, out value))
            {
                return value;
            }
            return new Factor(ExtractCalculusBetweenPatenthesis()).Solve();
        }

        private string ExtractCalculusBetweenPatenthesis()
        {
            return !IsComposedFactor && Calculus[0] == '(' && Calculus[Calculus.Length - 1] == ')'?
                Calculus.Substring(1, Calculus.Length - 2)
                : Calculus;
        }
    }
}
