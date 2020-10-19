using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.BattleModule
{
    public class NothingVsMonster
    {
        private BattleManager battleManager = null;

        private AttackCommand[] monsterAttackCommands = null;
        private Controller controller = null;

        private AnimatedAttack[] monsterAttack = null;

        private GameObject core;

        private AnimatedFighter[] fighters = null;

        private TextHealthShowerUI[] textHealthShowerUIs = null;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/NothingVsMonster.prefab");
            core = Object.Instantiate(coreGO);

            battleManager = core.GetComponentInChildren<BattleManager>();
            monsterAttackCommands = core.GetComponentsInChildren<AttackCommand>();
            monsterAttack = core.GetComponentsInChildren<AnimatedAttack>();
            textHealthShowerUIs = core.GetComponentsInChildren<TextHealthShowerUI>();
            controller = core.GetComponentInChildren<AutoController>();

            fighters = core.GetComponentsInChildren<AnimatedFighter>();

            battleManager.BattleLobby(fighters[1], fighters[0]);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator NotFighterHitAttackTest()
        {
            Health ownHealth = monsterAttackCommands[0].attack.GetComponent<Health>();

            AnimatedAttackChance[] attackChances = new AnimatedAttackChance[] { fighters[0].GetComponentsInChildren<AnimatedAttackChance>()[0] };
            monsterAttack[0].attackChances = attackChances;

            controller.looping = false;
            controller.commands = new AbstractCommand[] { monsterAttackCommands[0] };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(10);

            Assert.AreEqual(100, ownHealth.currentHp);

            Assert.AreEqual("100/100", textHealthShowerUIs[0].hp.text);

            yield return null;
        }
    }
}
