﻿using MathSolver.API.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MathSolver.UnitTest.Application.Model
{
    public class FactorTest
    {
        [Theory]
        [InlineData("5", 5.0)]
        [InlineData("13", 13.0)]
        public void SingleFactor(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }
        [Theory]
        [InlineData("5+5", 10.0)]
        [InlineData("5+1", 6.0)]
        [InlineData("5+1+3", 9.0)]
        public void AdditionOfNumbers(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }
        [Theory]
        [InlineData("5*5", 25.0)]
        [InlineData("5*1", 5.0)]
        [InlineData("5*1*2", 10.0)]
        public void MutiplicationOfNumbers(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }

        [Theory]
        [InlineData("5*5+1", 26.0)]
        [InlineData("5*1+3", 8.0)]
        [InlineData("3+5*1*2", 13.0)]
        public void MutiplicationAndAdditionOfNumbers(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }
        [Theory]
        [InlineData("(5)", 5.0)]
        [InlineData("(13)", 13.0)]
        public void SingleFactorWithParenteses(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }
        [Theory]
        [InlineData("(5+1)", 6.0)]
        [InlineData("(13+13)", 26.0)]
        public void AdditionBetweenParenteses(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }
        [Theory]
        [InlineData("(5)+(1)", 6.0)]
        [InlineData("(13)+(13)", 26.0)]
        public void AdditionOfSigleFactoresBetweenParenteses(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }
        [Theory]
        [InlineData("(5+1)*3", 18.0)]
        [InlineData("2*(13+13)", 52.0)]
        public void AdditionPredecending(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }

        [Theory]
        [InlineData("((5+1))*3", 18.0)]
        [InlineData("((2))*(13+13)", 52.0)]
        [InlineData("(5)+((1))", 6.0)]
        [InlineData("((13)+(13))", 26.0)]
        public void DoubleParenteses(string calculus, double result)
        {
            var factor = new Factor(calculus);
            Assert.Equal(result, factor.Solve());
        }
    }
}
