﻿using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PatternModule
{
    public class PatternFormulaTests
    {

        private static List<Vector2> points = new List<Vector2>() { new Vector2(0, 0), new Vector2(0, 100) };
        private static PatternFormula patternFormula = null;

        [SetUp]
        public void SetUp()
        {
            patternFormula = new PatternFormula(points);
        }

        [TearDown]
        public void TearDown()
        {
            patternFormula = null;
        }

        [Test]
        public void OneCorrectGuessTest()
        {
            patternFormula.Guess(new Vector2(0, 1));
            Assert.IsTrue(patternFormula.GetResult() == 0.1f);
        }

        [Test]
        public void MultiplyCorrectGuessTest()
        {
            patternFormula.Guess(new Vector2(0, 1));
            patternFormula.Guess(new Vector2(0, 12));
            patternFormula.Guess(new Vector2(0, 15));
            patternFormula.Guess(new Vector2(0, 24));

            Assert.IsTrue(patternFormula.GetResult() == 0.3f);
        }
    }
}
