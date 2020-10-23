using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.TeleportModule
{
    public class TeleportingTests
    {

        private MfxTeleportPoint[] teleportPoints;
        private TeleporterComponent teleporter;
        private BattleManager battleManager;
        private CharacterController player;

        private MockFighter[] mockFighters;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Teleports/Teleporting.prefab");
            core = Object.Instantiate(coreGO);

            player = core.GetComponentInChildren<CharacterController>();
            teleporter = core.GetComponentInChildren<TeleporterComponent>();
            battleManager = core.GetComponentInChildren<BattleManager>();
            teleportPoints = core.GetComponentsInChildren<MfxTeleportPoint>();
            mockFighters = core.GetComponentsInChildren<MockFighter>();

            battleManager.BattleLobby(mockFighters[0], mockFighters[1]);

            yield return null;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator InitTest()
        {
            Assert.IsTrue(0.1 > (teleportPoints[0].transform.position - player.transform.position).magnitude);

            yield return null;
        }

        [UnityTest]
        public IEnumerator WalkThenTeleportToLastPositionTest()
        {
            Vector3 randomPosition = new Vector3(100, 100, 100);
            player.transform.position = randomPosition;

            yield return new WaitForSeconds(1);

            Assert.IsTrue(0.1 > (randomPosition - player.transform.position).magnitude);

            teleporter.TeleportToLastPosition();

            Assert.IsTrue(0.1 > (teleportPoints[0].transform.position - player.transform.position).magnitude);

            yield return null;
        }

        [UnityTest]
        public IEnumerator PortThenWalkThenTeleportToLastPositionTest()
        {
            teleporter.TeleportTarget(1);

            yield return new WaitForSeconds(2);

            Assert.IsTrue(0.1 > (teleportPoints[1].transform.position - player.transform.position).magnitude);

            Vector3 randomPosition = new Vector3(100, 100, 100);
            player.transform.position = randomPosition;

            yield return new WaitForSeconds(1);

            Assert.IsTrue(0.1 > (randomPosition - player.transform.position).magnitude);

            teleporter.TeleportToLastPosition();

            yield return new WaitForSeconds(1);

            Assert.IsTrue(0.1 > (teleportPoints[1].transform.position - player.transform.position).magnitude);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BattleManagerWithdrawTest()
        {
            Vector3 randomPosition = new Vector3(100, 100, 100);
            player.transform.position = randomPosition;

            battleManager.WithdrawFromFight();

            yield return new WaitForSeconds(1);

            Assert.IsTrue(0.1 > (teleportPoints[0].transform.position - player.transform.position).magnitude);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BattleManagerDrawTest()
        {
            Vector3 randomPosition = new Vector3(100, 100, 100);
            player.transform.position = randomPosition;

            battleManager.DrawFight();

            yield return new WaitForSeconds(1);

            Assert.IsTrue(0.1 > (teleportPoints[0].transform.position - player.transform.position).magnitude);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BattleManagerLostTest()
        {
            Vector3 randomPosition = new Vector3(100, 100, 100);
            player.transform.position = randomPosition;

            battleManager.BlueFighterDied();

            yield return new WaitForSeconds(1);

            Assert.IsTrue(0.1 > (teleportPoints[0].transform.position - player.transform.position).magnitude);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BattleManagerWonTest()
        {
            Vector3 randomPosition = new Vector3(100, 100, 100);
            player.transform.position = randomPosition;

            battleManager.RedFighterDied();

            yield return new WaitForSeconds(1);

            Assert.IsTrue(0.1 > (randomPosition - player.transform.position).magnitude);

            yield return null;
        }
    }
}
