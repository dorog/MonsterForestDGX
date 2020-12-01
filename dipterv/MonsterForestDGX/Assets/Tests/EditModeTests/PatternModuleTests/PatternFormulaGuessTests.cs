using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PatternModule
{
    public class PatternFormulaGuessTests
    {

        private static List<Vector2> points = new List<Vector2>() { new Vector2(0, 0), new Vector2(0, 100) };
        private static PatternFormula patternFormula = null;

        private static List<Vector2> fbPoints = new List<Vector2>() { new Vector2(0, 0), new Vector2(0, 100), new Vector2(0, 0) };
        private static PatternFormula patternFormulaFB = null;

        [SetUp]
        public void SetUp()
        {
            patternFormula = new PatternFormula(points);
            patternFormulaFB = new PatternFormula(fbPoints);
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

        [Test]
        public void ForwardThanBackward()
        {
            patternFormulaFB.Guess(new Vector2(0, -5));
            patternFormulaFB.Guess(new Vector2(0, 105));
            patternFormulaFB.Guess(new Vector2(0, -5));

            Assert.IsTrue(patternFormulaFB.GetResult() == 1.0f);
        }

        [Test]
        public void JustForward()
        {
            patternFormulaFB.Guess(new Vector2(0, -5));
            patternFormulaFB.Guess(new Vector2(0, 105));

            Debug.Log(patternFormulaFB.GetResult());
            Assert.IsTrue(patternFormulaFB.GetResult() == 0.5f);
        }
    }
}
