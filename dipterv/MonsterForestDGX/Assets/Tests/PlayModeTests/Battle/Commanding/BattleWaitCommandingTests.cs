using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Battle.Commanding
{
    public class BattleWaitCommandingTests
    {
        private MockController controller = null;

        private GameObject core = null;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/Commanding/Commanding.prefab");
            core = Object.Instantiate(coreGO);

            controller = core.GetComponentInChildren<MockController>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator WaitCommandTest()
        {
            controller.InitCommands();
            controller.Step();

            yield return new WaitForSeconds(3);

            Assert.AreEqual(true, controller.Finished);
        }

        [UnityTest]
        public IEnumerator WaitCommandBeforeFinishedTest()
        {
            controller.InitCommands();
            controller.Step();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, controller.Finished);
        }

        [UnityTest]
        public IEnumerator WaitCommandCallTwiceTest()
        {
            controller.InitCommands();
            controller.Step();

            yield return new WaitForSeconds(3);

            Assert.AreEqual(true, controller.Finished);
            controller.Finished = false;

            controller.Step();
            yield return new WaitForSeconds(3);

            Assert.AreEqual(true, controller.Finished);
        }

        [UnityTest]
        public IEnumerator WaitCommandCallTwiceButSecondBeforeTimeTest()
        {
            controller.InitCommands();
            controller.Step();

            yield return new WaitForSeconds(3);

            Assert.AreEqual(true, controller.Finished);
            controller.Finished = false;

            controller.Step();
            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, controller.Finished);
        }
    }
}
