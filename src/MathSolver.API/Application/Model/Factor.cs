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
        public Operator OperationPrecedent { get => Operator.Precedent(Operations.Select(o => o.@operator.Simbol)); }
        
        private List<(Operator @operator, int index)> _operations;
        public List<(Operator @operator, int index)> Operations
        {
            get
            {
                if (_operations != null)
                    return _operations;
                List<(Operator @operator, int index)> operations = FindOperations();
                return operations;
            }
        }
        public bool IsComposedFactor => Operations.Any();

        public Factor(string calculus)
        {
            Calculus = calculus;
        }

        private List<(Operator @operator, int index)> FindOperations()
        {
            Queue<int> parentesesQueue = new Queue<int>();
            List<(Operator @operator, int index)> operations = new List<(Operator @operator, int index)>();
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
                    default:
                        if (parentesesQueue.Count == 0 && Operator.TryFindOperator(Calculus[index], out Operator reference))
                        {
                            operations.Add((reference, index));
                        }
                        break;
                }
            }
            _operations = operations;
            return operations;
        }
        private IEnumerable<Factor> BreakIntoFactors()
        {
            List<Factor> factors = new List<Factor>();
            if (IsComposedFactor)
            {
                var factorStart = 0;
                
                for (int index = 0; index < Operations.Count; index++)
                {
                    if (Operations[index].@operator.Simbol != OperationPrecedent.Simbol)
                        continue;
                    factors.Add(new Factor(Calculus.Substring(factorStart, (Operations[index].index - factorStart))));
                    factorStart = Operations[index].index + 1;
                }

                factors.Add(new Factor(Calculus.Substring(factorStart, (Calculus.Length - factorStart))));
            }
            return factors;
        }
        
        
        public double Solve()
        {
            if (IsComposedFactor)
                return this.OperationPrecedent.Solve(Factors);
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
