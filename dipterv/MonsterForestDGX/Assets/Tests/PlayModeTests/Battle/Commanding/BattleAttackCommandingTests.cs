using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Battle.Commanding
{
    public class BattleAttackCommandingTests
    {
        private MonsterAttackCommand monsterAttackCommand = null;
        private Controller controller = null;

        private MockAttack mockAttack = null;

        private GameObject core = null;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/Commanding/Commanding.prefab");
            core = Object.Instantiate(coreGO);

            monsterAttackCommand = core.GetComponentInChildren<MonsterAttackCommand>();
            controller = core.GetComponentInChildren<Controller>();

            mockAttack = core.GetComponentInChildren<MockAttack>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator AttackOnceCommandTest()
        {
            controller.commands = new AbstractCommand[] { monsterAttackCommand };

            controller.InitCommands();
            controller.Step();

            Assert.AreEqual(1, mockAttack.AttackedCounter);

            yield return null;
        }

        [UnityTest]
        public IEnumerator AttackTwiceCommandTest()
        {
            monsterAttackCommand.extraAttackChances = new float[] { 100 };

            controller.commands = new AbstractCommand[] { monsterAttackCommand };

            controller.InitCommands();
            controller.Step();

            yield return new WaitForSeconds(2);

            Assert.AreEqual(2, mockAttack.AttackedCounter);
        }

        [UnityTest]
        public IEnumerator AttackOnceButHaveExtraAttackChanceCommandTest()
        {
            monsterAttackCommand.extraAttackChances = new float[] { 0 };

            controller.commands = new AbstractCommand[] { monsterAttackCommand };

            controller.InitCommands();
            controller.Step();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(1, mockAttack.AttackedCounter);
        }

        [UnityTest]
        public IEnumerator AttackTwiceButInDiffStepsCommandTest()
        {
            controller.commands = new AbstractCommand[] { monsterAttackCommand };
            controller.looping = true;

            controller.InitCommands();

            controller.Step();

            yield return new WaitForSeconds(1);

            controller.Step();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(2, mockAttack.AttackedCounter);
        }
    }
}
