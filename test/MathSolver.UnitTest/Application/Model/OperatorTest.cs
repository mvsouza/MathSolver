using MathSolver.API.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MathSolver.UnitTest.Application.Model
{
    public class OperatorTest
    {
        [Fact]
        public void Should_Not_Find_Operator()
        {
            Assert.False(Operator.TryFindOperator('$', out Operator r));
            Assert.Null(r);
        }
        [Fact]
        public void Should_Find_Addition()
        {
            Assert.True(Operator.TryFindOperator('+', out Operator r));
            Assert.NotNull(r);
        }
    }
}
