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

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Teleports/Teleporting.prefab");
            core = Object.Instantiate(coreGO);

            player = core.GetComponentInChildren<CharacterController>();
            teleportUI = core.GetComponentInChildren<TeleportUiComponent>();
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

            Assert.AreEqual(3, teleportUI.teleportsParent.childCount);
            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(true, teleportUI.teleportsParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(2).gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockTest()
        {
            player.transform.position = teleportPoints[2].transform.position;

            yield return new WaitForSeconds(1);

            Assert.AreEqual(true, teleportUI.teleportsParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(true, teleportUI.teleportsParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(2).gameObject.activeSelf);

            player.transform.position = teleportPoints[0].transform.position;

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(true, teleportUI.teleportsParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(true, teleportUI.teleportsParent.GetChild(2).gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ManualTeleportTest()
        {
            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(true, teleportUI.teleportsParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(2).gameObject.activeSelf);

            teleportPoints[1].TeleportTarget(player.transform);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(true, teleportUI.teleportsParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(2).gameObject.activeSelf);

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
            Assert.AreEqual(true, teleportUI.teleportsParent.GetChild(0).gameObject.activeSelf);
            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(1).gameObject.activeSelf);
            Assert.AreEqual(false, teleportUI.teleportsParent.GetChild(2).gameObject.activeSelf);

            yield return null;
        }
    }
}
