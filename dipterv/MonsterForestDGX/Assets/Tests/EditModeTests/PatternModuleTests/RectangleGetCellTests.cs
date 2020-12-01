using NUnit.Framework;
using UnityEngine;

namespace Tests.PatternModule
{

    public class RectangleGetCellTests
    {
        private static Rectangle rectangle = new Rectangle(0, 10, Vector2.zero, new Vector2(0, 100), 2);

        [Test]
        public void BottomTest()
        {
            Assert.IsTrue(rectangle.GetCell(Vector2.zero) == 0, message: "Center");
            Assert.IsTrue(rectangle.GetCell(new Vector2(-1, 0)) == 0, message: "Left");
            Assert.IsTrue(rectangle.GetCell(new Vector2(1, 0)) == 0, message: "Right");
        }

        [Test]
        public void UpTest()
        {
            Assert.IsTrue(rectangle.GetCell(new Vector2(0, 100)) == 9, message: "Center");
            Assert.IsTrue(rectangle.GetCell(new Vector2(-1, 100)) == 9, message: "Left");
            Assert.IsTrue(rectangle.GetCell(new Vector2(1, 100)) == 9, message: "Right");
        }

        [Test]
        public void SideInsideTest()
        {
            Assert.IsTrue(rectangle.GetCell(new Vector2(0, 5)) == 0, message: "First");
            Assert.IsTrue(rectangle.GetCell(new Vector2(-1, 15)) == 1, message: "Second");
            Assert.IsTrue(rectangle.GetCell(new Vector2(1, 25)) == 2, message: "Third");
            Assert.IsTrue(rectangle.GetCell(new Vector2(-1, 85)) == 8, message: "Fourth");
            Assert.IsTrue(rectangle.GetCell(new Vector2(1, 95)) == 9, message: "Fifth");
        }

        [Test]
        public void SideBorderTest()
        {
            Assert.IsTrue(rectangle.GetCell(new Vector2(2, 5)) == 0, message: "First");
            Assert.IsTrue(rectangle.GetCell(new Vector2(-2, 15)) == 1, message: "Second");
            Assert.IsTrue(rectangle.GetCell(new Vector2(2, 25)) == 2, message: "Third");
            Assert.IsTrue(rectangle.GetCell(new Vector2(-2, 85)) == 8, message: "Fourth");
            Assert.IsTrue(rectangle.GetCell(new Vector2(2, 95)) == 9, message: "Fifth");
        }
    }
}
