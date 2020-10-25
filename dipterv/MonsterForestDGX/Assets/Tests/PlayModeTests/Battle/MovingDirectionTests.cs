using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Tests.BattleModule
{
    public class MovingDirectionTests
    {
        private Transform core;

        [OneTimeSetUp]
        public void Setup()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/MovingDirection.prefab");
            core = Object.Instantiate(coreGO).transform;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Object.Destroy(core.gameObject);
        }

        private static readonly MovingDirectionTestParameter[] testParameters = new MovingDirectionTestParameter[]
        {
            new MovingDirectionTestParameter(){ direction = MovingDirection.Backward, expected = Vector3.back },
            new MovingDirectionTestParameter(){ direction = MovingDirection.Down, expected = Vector3.down },
            new MovingDirectionTestParameter(){ direction = MovingDirection.Forward, expected = Vector3.forward },
            new MovingDirectionTestParameter(){ direction = MovingDirection.Left, expected = Vector3.left },
            new MovingDirectionTestParameter(){ direction = MovingDirection.Right, expected = Vector3.right },
            new MovingDirectionTestParameter(){ direction = MovingDirection.Up, expected = Vector3.up },
        };

        [Test]
        public void MovingDirectionTest([ValueSource(nameof(testParameters))] MovingDirectionTestParameter testParameter)
        {
            Assert.IsTrue(0.01 > (testParameter.direction.GetDirection(core) - testParameter.expected).magnitude);
        }

        public class MovingDirectionTestParameter
        {
            public MovingDirection direction;
            public Vector3 expected;
        }
    }
}
