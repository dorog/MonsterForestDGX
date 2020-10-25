using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Mfx
{
    public class UiVisitingTests
    {
        private UiShower uiShower;
        private CharacterController player;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Mfx/UiVisiting/UiVisiting.prefab");
            core = Object.Instantiate(coreGO);

            uiShower = core.GetComponentInChildren<UiShower>();
            player = core.GetComponentInChildren<CharacterController>();

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
            Assert.AreEqual(false, uiShower.menu.gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TriggerEnterTest()
        {
            player.transform.position += new Vector3(-3, 0, 0);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(true, uiShower.menu.gameObject.activeSelf);
            Assert.IsTrue(0.01 > (uiShower.transform.position - uiShower.menu .position + Vector3.up * 4).magnitude);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TriggerExitTest()
        {
            player.transform.position += new Vector3(-3, 0, 0);

            yield return new WaitForSeconds(1);

            player.transform.position += new Vector3(3, 0, 0);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, uiShower.menu.gameObject.activeSelf);

            yield return null;
        }
    }
}
