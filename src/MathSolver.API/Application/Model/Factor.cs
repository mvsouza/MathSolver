using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathSolver.API.Application.Model
{
    public class Factor
    {
        public string Calculus { get; private set; }
        public IEnumerable<Factor> Factors { get; private set; }
        public (char desc, double seed, Func<double, double, double> func) Operation { get; private set; }
        public bool HasOperator
        {
            get; private set;
        }
        public Factor(string calculus)
        {
            Calculus = calculus;
            BreakIntoFactors();

        }
        private void BreakIntoFactors()
        {
            Queue<int> parentesesQueue = new Queue<int>();
            List<char> operations = new List<char>();
            List<int> operationsIndexes = new List<int>();
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
                            operations.Add('*');
                            operationsIndexes.Add(index);
                            numberIndex = index + 1;
                        }
                        break;
                    case ('+'):
                        if (parentesesQueue.Count == 0)
                        {
                            operations.Add('+');
                            operationsIndexes.Add(index);
                            numberIndex = index + 1;
                        }
                        break;
                }
            }
            HasOperator = operations.Any();
            if (HasOperator)
            {
                Operation = Precedence.First(p => operations.Contains(p.desc));
                var factorStart = 0;

                List<Factor> factors = new List<Factor>();

                for (int index = 0; index < operations.Count; index++)
                {
                    if (operations[index] != Operation.desc)
                        continue;
                    factors.Add(new Factor(Calculus.Substring(factorStart, (operationsIndexes[index]- factorStart))));
                    factorStart = operationsIndexes[index] + 1;
                }

                factors.Add(new Factor(Calculus.Substring(factorStart, (Calculus.Length - factorStart))));
                Factors = factors;
            }
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
                Calculus = Calculus.Substring(1,Calculus.Length-2);
            }
            double value;
            if (double.TryParse(Calculus, out value)){
                return value;
            }
            return new Factor(Calculus).Solve();


        }

    }
}
