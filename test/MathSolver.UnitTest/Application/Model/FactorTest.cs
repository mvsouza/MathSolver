using MathSolver.API.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MathSolver.UnitTest.Application.Model
{
    public class FactorTest
    {
        [Fact]
        public void AdditionOfNumbers()
        {
            var factor = new Factor("5+5");
            Assert.Equal(10.0, factor.Solve());
            factor = new Factor("5+1");
            Assert.Equal(6.0, factor.Solve());
            factor = new Factor("5+1+3");
            Assert.Equal(9.0, factor.Solve());
        }
    }
}
