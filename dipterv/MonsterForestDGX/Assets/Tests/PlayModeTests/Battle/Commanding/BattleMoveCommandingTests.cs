using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.BattleModule.Commanding
{
    public class BattleMoveCommandingTests
    {
        private MoveCommand monsterMoveCommand = null;
        private Controller controller = null;

        private TurnFill[] turnFills = null;

        private GameObject core;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/Commanding/Commanding.prefab");
            core = Object.Instantiate(coreGO);
            monsterMoveCommand = core.GetComponentInChildren<MoveCommand>();
            controller = core.GetComponentInChildren<Controller>();

            turnFills = core.GetComponentsInChildren<TurnFill>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        private static readonly MovingCommandTestParameter[] simpleMovingCommandTestParameters = new MovingCommandTestParameter[]
        {
            new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Forward },
            new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Backward },
            new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Forward },
            new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Backward },
        };

        [UnityTest]
        public IEnumerator MovingCommandTest([ValueSource(nameof(simpleMovingCommandTestParameters))] MovingCommandTestParameter parameters)
        {
            monsterMoveCommand.direction = parameters.Direction;
            monsterMoveCommand.turnFill = turnFills[parameters.TurnFillIndex];

            controller.commands = new AbstractCommand[] { monsterMoveCommand };

            Vector3 startPosition = turnFills[parameters.TurnFillIndex].transform.position;
            Vector3 expectedPosition = startPosition + (turnFills[parameters.TurnFillIndex].transform.forward * 10 * (parameters.Direction == MovingDirection.Forward ? 1 : -1));

            controller.InitCommands();
            controller.StartController();

            yield return new WaitForSeconds(2);

            Assert.IsTrue(0.1f > (expectedPosition - turnFills[parameters.TurnFillIndex].transform.position).magnitude);
        }

        private static readonly List<MovingCommandTestParameter[]> combinedMovingCommandTestParameters = new List<MovingCommandTestParameter[]>()
        {
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Forward },
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Forward }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Backward },
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Backward }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Forward },
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Forward }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Backward },
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Backward }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Forward },
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Forward }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Backward },
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Backward }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Direction = MovingDirection.Forward },
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Direction = MovingDirection.Backward }
            }
        };

        [UnityTest]
        public IEnumerator CombinedMovingCommandTest([ValueSource(nameof(combinedMovingCommandTestParameters))] MovingCommandTestParameter[] parameters)
        {
            controller.commands = new AbstractCommand[] { monsterMoveCommand };
            controller.looping = true;

            Vector3 startPosition = monsterMoveCommand.transform.position;

            controller.InitCommands();

            int distance = 0;
            foreach (var parameter in parameters)
            {
                distance += parameter.Direction == MovingDirection.Forward ? 10 : -10;
                monsterMoveCommand.direction = parameter.Direction;
                monsterMoveCommand.turnFill = turnFills[parameter.TurnFillIndex];

                yield return new WaitForSeconds(2);

                controller.StartController();
            }

            yield return new WaitForSeconds(4);

            Vector3 expectedPosition = startPosition + monsterMoveCommand.transform.forward * distance;
            Assert.AreEqual(expectedPosition, monsterMoveCommand.transform.position);
            Assert.IsTrue(0.1f > (expectedPosition - monsterMoveCommand.transform.position).magnitude);
        }

        public class MovingCommandTestParameter
        {
            public int TurnFillIndex { get; set; }
            public MovingDirection Direction { get; set; }
        }
    }
}
