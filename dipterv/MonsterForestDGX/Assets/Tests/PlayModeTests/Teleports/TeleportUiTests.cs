using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.TeleportModule
{
    public class TeleportUiTests
    {
        private MfxTeleportPoint[] teleportPoints;
        private CharacterController player;
        private TeleportUiComponent teleportUI;
        private Transform teleportParent;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Teleports/Teleporting.prefab");
            core = Object.Instantiate(coreGO);

            player = core.GetComponentInChildren<CharacterController>();
            teleportUI = core.GetComponentInChildren<TeleportUiComponent>();
            teleportParent = GameObject.Find("teleportParent").transform;
            teleportPoints = core.GetComponentsInChildren<MfxTeleportPoint>();

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
            yield return new WaitForSeconds(1);

            Assert.AreEqual(3, teleportParent.childCount);
            Assert.AreEqual(false, teleportParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(true, teleportParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportParent.GetChild(2).gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockTest()
        {
            player.transform.position = teleportPoints[2].transform.position;

            yield return new WaitForSeconds(1);

            Assert.AreEqual(true, teleportParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(true, teleportParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportParent.GetChild(2).gameObject.activeSelf);

            player.transform.position = teleportPoints[0].transform.position;

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, teleportParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(true, teleportParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(true, teleportParent.GetChild(2).gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ManualTeleportTest()
        {
            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, teleportParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(true, teleportParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportParent.GetChild(2).gameObject.activeSelf);

            teleportPoints[1].TeleportTarget(player.transform);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(true, teleportParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(false, teleportParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportParent.GetChild(2).gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UiTeleportTest()
        {
            yield return new WaitForSeconds(1);

            Button[] buttons = core.GetComponentsInChildren<Button>();
            buttons[0].onClick.Invoke();

            yield return new WaitForSeconds(1);

            Assert.IsTrue(0.1 > (player.transform.position - teleportPoints[1].transform.position).magnitude);
            Assert.AreEqual(true, teleportParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(false, teleportParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportParent.GetChild(2).gameObject.activeSelf);

            yield return null;
        }
    }
}
