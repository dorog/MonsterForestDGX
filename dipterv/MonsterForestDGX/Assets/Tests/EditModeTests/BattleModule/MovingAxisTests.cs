using NUnit.Framework;

namespace Tests.BattleModule
{
    public class MovingAxisTests
    {
        private static readonly MovingAxisTestParameter[] testParameters = new MovingAxisTestParameter[]
        {
            new MovingAxisTestParameter(){ axis = MovingAxis.X, revert = false, expected = MovingDirection.Right },
            new MovingAxisTestParameter(){ axis = MovingAxis.X, revert = true, expected = MovingDirection.Left },
            new MovingAxisTestParameter(){ axis = MovingAxis.Y, revert = false, expected = MovingDirection.Up },
            new MovingAxisTestParameter(){ axis = MovingAxis.Y, revert = true, expected = MovingDirection.Down },
            new MovingAxisTestParameter(){ axis = MovingAxis.Z, revert = false, expected = MovingDirection.Forward },
            new MovingAxisTestParameter(){ axis = MovingAxis.Z, revert = true, expected = MovingDirection.Backward },
        };

        [Test]
        public void MovingAxisTest([ValueSource(nameof(testParameters))] MovingAxisTestParameter testParameter)
        {
            Assert.AreEqual(testParameter.expected, testParameter.axis.GetPositiveDirection(testParameter.revert));
        }

        public class MovingAxisTestParameter
        {
            public MovingAxis axis;
            public bool revert;
            public MovingDirection expected;
        }
    }
}
