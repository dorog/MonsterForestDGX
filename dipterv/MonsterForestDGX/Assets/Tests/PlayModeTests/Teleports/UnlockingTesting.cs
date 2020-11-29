using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.TeleportModule
{
    public class UnlockingTesting
    {
        private MfxTeleportPoint[] teleportPoints;
        private CharacterController player;
        private MockDataIO mockDataIO;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Teleports/Teleporting.prefab");
            core = Object.Instantiate(coreGO);

            player = core.GetComponentInChildren<CharacterController>();
            teleportPoints = core.GetComponentsInChildren<MfxTeleportPoint>();
            mockDataIO = core.GetComponentInChildren<MockDataIO>();

            yield return null;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator VisitAlreadyUnlockedLocationTest()
        {
            Assert.AreEqual(true, teleportPoints[0].GetState());
            Assert.AreEqual(true, teleportPoints[1].GetState());
            Assert.AreEqual(false, teleportPoints[2].GetState());

            teleportPoints[1].TeleportTarget(player.transform);

            Assert.AreEqual(true, teleportPoints[0].GetState());
            Assert.AreEqual(true, teleportPoints[1].GetState());
            Assert.AreEqual(false, teleportPoints[2].GetState());

            yield return null;
        }

        [UnityTest]
        public IEnumerator VisitNotUnlockedLocationTest()
        {
            Assert.AreEqual(true, teleportPoints[0].GetState());
            Assert.AreEqual(true, teleportPoints[1].GetState());
            Assert.AreEqual(false, teleportPoints[2].GetState());

            teleportPoints[2].TeleportTarget(player.transform);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(true, teleportPoints[0].GetState());
            Assert.AreEqual(true, teleportPoints[1].GetState());
            Assert.AreEqual(true, teleportPoints[2].GetState());

            Assert.AreEqual(true, mockDataIO.LocalGameData.teleports[0]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.teleports[1]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.teleports[2]);


            yield return null;
        }
    }
}
