using MathSolver.API.Application.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MathSolver.API.Application.Command
{
    public class SolveCalculusCommandHandler: IRequestHandler<SolveCalculusCommand, double>
    {
        public async Task<double> Handle(SolveCalculusCommand command, CancellationToken cancellationToken)
        {
            Factor f = new Factor(command.Calculus);

            return f.Solve();
        }
    }
}