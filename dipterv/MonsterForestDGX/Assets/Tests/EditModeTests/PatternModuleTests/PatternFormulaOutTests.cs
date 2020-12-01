using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PatternModule
{

    public class PatternFormulaOutTests : MonoBehaviour
    {
        private static readonly List<Vector2> points = new List<Vector2>() { new Vector2(0, 0), new Vector2(0, 50), new Vector2(0, 100) };
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
        public void SummLength()
        {
            Assert.IsTrue(Mathf.Abs(patternFormula.PatternLength - 100f) < 0.01f);
        }

        [Test]
        public void OneOut()
        {
            patternFormula.Guess(new Vector2(0, -10));

            Assert.IsTrue(Mathf.Abs(patternFormula.MissLength - 0f) < 0.02f);
        }

        [Test]
        public void HalfOutBefore()
        {
            patternFormula.Guess(new Vector2(0, -10));
            patternFormula.Guess(new Vector2(0, 10));

            Assert.IsTrue(Mathf.Abs(patternFormula.MissLength - 10f) < 0.02f);
        }

        [Test]
        public void HalfOutAfter()
        {
            patternFormula.Guess(new Vector2(0, 85));
            patternFormula.Guess(new Vector2(0, 110));

            Assert.IsTrue(Mathf.Abs(patternFormula.MissLength - 10f) < 0.02f);
        }

        [Test]
        public void BetweenInAfter()
        {
            patternFormula.Guess(new Vector2(0, -10));
            patternFormula.Guess(new Vector2(0, 110));

            Assert.IsTrue(Mathf.Abs(patternFormula.MissLength - 20f) < 0.02f);
        }

        [Test]
        public void NoOut()
        {
            patternFormula.Guess(new Vector2(0, 10));
            patternFormula.Guess(new Vector2(0, 30));

            Assert.IsTrue(Mathf.Abs(patternFormula.MissLength - 0f) < 0.02f);
        }

        [Test]
        public void FullOut()
        {
            patternFormula.Guess(new Vector2(0, -20));
            patternFormula.Guess(new Vector2(0, -10));

            Assert.IsTrue(Mathf.Abs(patternFormula.MissLength - 10f) < 0.02f);
        }
    }
}
