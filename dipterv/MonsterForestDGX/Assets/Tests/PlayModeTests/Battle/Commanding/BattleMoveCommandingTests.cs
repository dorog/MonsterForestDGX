using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Battle
{
    public class BattleMoveCommandingTests
    {
        private MonsterMoveCommand monsterMoveCommand = null;
        private Controller controller = null;

        private TurnFill[] turnFills = null;

        private GameObject core;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/Commanding/Commanding.prefab");
            core = Object.Instantiate(coreGO);
            monsterMoveCommand = core.GetComponentInChildren<MonsterMoveCommand>();
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
            new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = true },
            new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = false },
            new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = true },
            new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = false },
        };

        [UnityTest]
        public IEnumerator MovingCommandTest([ValueSource(nameof(simpleMovingCommandTestParameters))] MovingCommandTestParameter parameters)
        {
            monsterMoveCommand.forward = parameters.Forward;
            monsterMoveCommand.turnFill = turnFills[parameters.TurnFillIndex];

            controller.commands = new AbstractCommand[] { monsterMoveCommand };

            Vector3 startPosition = turnFills[parameters.TurnFillIndex].transform.position;
            Vector3 expectedPosition = startPosition + (turnFills[parameters.TurnFillIndex].transform.forward * 10 * (parameters.Forward ? 1 : -1));

            controller.InitCommands();
            controller.Step();

            yield return new WaitForSeconds(2);

            Assert.AreEqual(expectedPosition, turnFills[parameters.TurnFillIndex].transform.position);
        }

        private static readonly List<MovingCommandTestParameter[]> combinedMovingCommandTestParameters = new List<MovingCommandTestParameter[]>()
        {
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = true },
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = true }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = false },
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = false }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = true },
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = true }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = false },
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = false }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = true },
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = true }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = false },
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = false }
            },
            new MovingCommandTestParameter[]
            {
                new MovingCommandTestParameter(){ TurnFillIndex = 0, Forward = true },
                new MovingCommandTestParameter(){ TurnFillIndex = 1, Forward = false }
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
                distance += parameter.Forward ? 10 : -10;
                monsterMoveCommand.forward = parameter.Forward;
                monsterMoveCommand.turnFill = turnFills[parameter.TurnFillIndex];

                yield return new WaitForSeconds(2);

                controller.Step();
            }

            yield return new WaitForSeconds(4);

            Vector3 expectedPosition = startPosition + monsterMoveCommand.transform.forward * distance;
            Assert.AreEqual(expectedPosition, monsterMoveCommand.transform.position);
        }

        public class MovingCommandTestParameter
        {
            public int TurnFillIndex { get; set; }
            public bool Forward { get; set; }
        }
    }
}
