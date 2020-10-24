using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Mfx
{
    public class MonsterAttackingTests
    {
        private MockAxisInput[] axisInputs;

        private MfxTeleportPoint[] teleports;
        private PlayerHealth player;

        private MoveCommand moveForwardCommand;
        private MoveCommand moveBackwardCommand;

        private AttackCommand attackCommand;
        private FighterTurnCommand monsterTurnCommand;
        private WaitCommand waitCommand;

        private AutoController controller;

        private BattleLobby battleLobby;

        private AnimatedFighter animatedFighter;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Mfx/MfxBattle.prefab");
            core = Object.Instantiate(coreGO);

            axisInputs = core.GetComponentsInChildren<MockAxisInput>();
            teleports = core.GetComponentsInChildren<MfxTeleportPoint>();
            player = core.GetComponentInChildren<PlayerHealth>();

            var moveCommands = core.GetComponentsInChildren<MoveCommand>();
            moveForwardCommand = moveCommands[0];
            moveBackwardCommand = moveCommands[1];

            attackCommand = core.GetComponentInChildren<AttackCommand>();
            monsterTurnCommand = core.GetComponentInChildren<FighterTurnCommand>();
            waitCommand = core.GetComponentInChildren<WaitCommand>();

            controller = core.GetComponentInChildren<AutoController>();
            battleLobby = core.GetComponentInChildren<BattleLobby>();
            animatedFighter = core.GetComponentInChildren<AnimatedFighter>();

            yield return null;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }



        [UnityTest]
        public IEnumerator StartFightTest()
        {
            controller.commands = new AbstractCommand[] { };

            yield return new WaitForSeconds(2);

            axisInputs[0].SetGetValue(new Vector3(0, 1), 3);

            yield return new WaitForSeconds(6);

            GameObject startGO = GameObject.Find("Start");
            Button start = startGO.GetComponent<Button>();

            start.onClick.Invoke();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, battleLobby.battleLobbyUI.activeSelf);

            yield return null;
        }


        [UnityTest]
        public IEnumerator MonsterAttacktWithoutShieldTest()
        {
            controller.commands = new AbstractCommand[] { moveForwardCommand, waitCommand, attackCommand };
            controller.InitCommands();

            Vector3 monsterOriginalPosition = animatedFighter.transform.position;

            yield return new WaitForSeconds(2);

            axisInputs[0].SetGetValue(new Vector3(0, 1), 3);

            yield return new WaitForSeconds(6);

            GameObject startGO = GameObject.Find("Start");
            Button start = startGO.GetComponent<Button>();

            start.onClick.Invoke();

            yield return new WaitForSeconds(6);

            Assert.IsTrue(0.1 > (monsterOriginalPosition - animatedFighter.transform.position + animatedFighter.transform.forward * 10).magnitude);

            yield return new WaitForSeconds(15);

            Assert.AreEqual(90, player.currentHp);

            yield return null;
        }
    }
}
