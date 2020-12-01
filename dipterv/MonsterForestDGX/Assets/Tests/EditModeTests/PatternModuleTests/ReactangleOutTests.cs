using NUnit.Framework;
using UnityEngine;

namespace Tests.PatternModule
{
    public class ReactangleOutTests
    {
        [Test]
        public void HalfOutBeforeGuess()
        {
            Rectangle rectangle = new Rectangle(0, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(Vector2.zero, new Vector2(0, -10), -1);
            var hitResult = rectangle.Guess(new Vector2(0, -10), new Vector2(0, 10), -1);

            Assert.IsTrue((hitResult.StartPoint - Vector2.zero).magnitude < 0.02f);
            Assert.IsTrue((hitResult.LastPoint - new Vector2(0, 10)).magnitude < 0.02f);
        }

        [Test]
        public void HalfOutAfterGuess()
        {
            Rectangle rectangle = new Rectangle(0, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(Vector2.zero, new Vector2(0, 85), -1);
            var hitResult = rectangle.Guess(new Vector2(0, 85), new Vector2(0, 110), 0);

            Assert.IsTrue((hitResult.StartPoint - new Vector2(0, 85)).magnitude < 0.02f);
            Assert.IsTrue((hitResult.LastPoint - new Vector2(0, 100)).magnitude < 0.02f);
        }
    }
}
