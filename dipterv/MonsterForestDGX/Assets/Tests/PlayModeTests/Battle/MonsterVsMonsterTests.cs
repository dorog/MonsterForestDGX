using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.BattleModule
{
    public class MonsterVsMonsterTests
    {
        private BattleManager battleManager = null;

        private FighterTurnCommand[] fighterTurnCommands = null;
        private MonsterAttackCommand[] monsterAttackCommands = null;
        private Controller controller = null;

        private MonsterAttack[] monsterAttack = null;

        private GameObject core;

        private Monster[] fighters = null;

        private TextHealthShowerUI[] textHealthShowerUIs = null;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/MonsterVsMonster.prefab");
            core = Object.Instantiate(coreGO);

            battleManager = core.GetComponentInChildren<BattleManager>();
            fighterTurnCommands = core.GetComponentsInChildren<FighterTurnCommand>();
            monsterAttackCommands = core.GetComponentsInChildren<MonsterAttackCommand>();
            monsterAttack = core.GetComponentsInChildren<MonsterAttack>();
            textHealthShowerUIs = core.GetComponentsInChildren<TextHealthShowerUI>();
            controller = core.GetComponentInChildren<AutoController>();

            fighters = core.GetComponentsInChildren<Monster>();

            battleManager.BattleLobby(fighters[1], fighters[0]);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator InitTest()
        {
            controller.looping = false;
            controller.commands = new AbstractCommand[] { };

            controller.InitCommands();

            battleManager.BattleStart();

            Assert.AreEqual("100/100", textHealthShowerUIs[0].hp.text);
            Assert.AreEqual("100/100", textHealthShowerUIs[1].hp.text);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SimpleAttackTest([Values(0, 1)] int monsterIndex, [Values (0, 1, 2)] int monsterAttackChanceIndex)
        {
            MonsterAttackChance[] allAttackChances = fighters[monsterIndex].GetComponentsInChildren<MonsterAttackChance>();
            MonsterAttackChance[] attackChances = new MonsterAttackChance[] { allAttackChances[monsterAttackChanceIndex] };
            monsterAttack[monsterIndex].attackChances = attackChances;

            controller.looping = false;
            controller.commands = new AbstractCommand[] { monsterAttackCommands[monsterIndex] };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(2);

            bool normalAttackDuringFight = allAttackChances[0].monsterAttacks[0].activate[0].activeSelf;
            bool hardAttackDuringFight = allAttackChances[1].monsterAttacks[0].activate[0].activeSelf;
            bool ultimateAttackDuringFight = allAttackChances[2].monsterAttacks[0].activate[0].activeSelf;

            bool[] expecteds = new bool[] { false, false, false };
            expecteds[monsterAttackChanceIndex] = true;

            Assert.AreEqual(expecteds[0], normalAttackDuringFight);
            Assert.AreEqual(expecteds[1], hardAttackDuringFight);
            Assert.AreEqual(expecteds[2], ultimateAttackDuringFight);

            yield return new WaitForSeconds(5);

            Health enemyHealth = monsterAttackCommands[monsterIndex].enemy.GetComponent<Health>();

            float expectedHp = 100 - (1 + monsterAttackChanceIndex) * 10;
            Assert.AreEqual(expectedHp, enemyHealth.currentHp);

            Assert.AreEqual("100/100", textHealthShowerUIs[monsterIndex].hp.text);
            Assert.AreEqual(expectedHp + "/100", textHealthShowerUIs[Revert(monsterIndex)].hp.text);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BothAttackTest([Values(0, 1)] int monsterIndex, [Values(0, 1, 2)] int monsterAttackChanceIndex)
        {
            Health enemyHealth = monsterAttackCommands[monsterIndex].enemy.GetComponent<Health>();
            Health ownHealth = monsterAttackCommands[monsterIndex].attack.GetComponent<Health>();

            MonsterAttackChance[] attackChances = new MonsterAttackChance[] { fighters[monsterIndex].GetComponentsInChildren<MonsterAttackChance>()[monsterAttackChanceIndex] };
            monsterAttack[monsterIndex].attackChances = attackChances;

            MonsterAttackChance[] enemyAttackChances = new MonsterAttackChance[] { fighters[Revert(monsterIndex)].GetComponentsInChildren<MonsterAttackChance>()[monsterAttackChanceIndex] };
            monsterAttack[Revert(monsterIndex)].attackChances = enemyAttackChances;

            controller.looping = false;
            controller.commands = new AbstractCommand[] { 
                fighterTurnCommands[monsterIndex], monsterAttackCommands[monsterIndex], 
                fighterTurnCommands[Revert(monsterIndex)], monsterAttackCommands[Revert(monsterIndex)] 
            };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(10);

            float expectedHp = 100 - (1 + monsterAttackChanceIndex) * 10;
            Assert.AreEqual(expectedHp, enemyHealth.currentHp);
            Assert.AreEqual(expectedHp, ownHealth.currentHp);

            Assert.AreEqual(expectedHp + "/100", textHealthShowerUIs[monsterIndex].hp.text);
            Assert.AreEqual(expectedHp + "/100", textHealthShowerUIs[Revert(monsterIndex)].hp.text);

            yield return null;
        }

        [UnityTest]
        public IEnumerator DieTest([Values(0, 1)] int monsterIndex)
        {
            Health enemyHealth = monsterAttackCommands[monsterIndex].enemy.GetComponent<Health>();
            Health ownHealth = monsterAttackCommands[monsterIndex].attack.GetComponent<Health>();

            Vector3 startPosition = monsterAttackCommands[monsterIndex].enemy.transform.position;
            Vector3 disappearPosition = startPosition + Vector3.down * 10;

            enemyHealth.TakeDamage(99);

            monsterAttack[monsterIndex].attackChances = fighters[monsterIndex].GetComponentsInChildren<MonsterAttackChance>();

            controller.looping = true;
            controller.commands = new AbstractCommand[] { monsterAttackCommands[monsterIndex] };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(15);

            Vector3 actualPosition = monsterAttackCommands[monsterIndex].enemy.transform.position;

            Assert.IsTrue(0 > enemyHealth.currentHp);
            Assert.AreEqual(100, ownHealth.currentHp);
            Assert.AreEqual(disappearPosition, actualPosition);

            Assert.AreEqual("Dead", textHealthShowerUIs[Revert(monsterIndex)].hp.text);
            Assert.AreEqual("100/100", textHealthShowerUIs[monsterIndex].hp.text);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BlockTest([Values(0, 1)] int monsterIndex, [Values (0, 2)] int monsterAttackIndex)
        {
            Health enemyHealth = monsterAttackCommands[monsterIndex].enemy.GetComponent<Health>();
            Health ownHealth = monsterAttackCommands[monsterIndex].attack.GetComponent<Health>();

            MonsterAttackChance[] attackChances = new MonsterAttackChance[] { fighters[monsterIndex].GetComponentsInChildren<MonsterAttackChance>()[monsterAttackIndex] };
            monsterAttack[monsterIndex].attackChances = attackChances;

            Monster[] monsters = core.GetComponentsInChildren<Monster>();

            monsters[Revert(monsterIndex)].blockChance = 100;

            controller.looping = true;
            controller.commands = new AbstractCommand[] { monsterAttackCommands[monsterIndex] };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(5);

            float dmg = (1 + monsterAttackIndex) * 10 - 15;
            float expectedHp = 100 - (dmg >= 0 ? dmg : 0);

            Assert.AreEqual(expectedHp, enemyHealth.currentHp);
            Assert.AreEqual(100, ownHealth.currentHp);

            Assert.AreEqual(expectedHp + "/100", textHealthShowerUIs[Revert(monsterIndex)].hp.text);
            Assert.AreEqual("100/100", textHealthShowerUIs[monsterIndex].hp.text);

            yield return null;
        }

        [UnityTest]
        public IEnumerator MissAttackTest([Values(0, 1)] int monsterIndex)
        {
            Health enemyHealth = monsterAttackCommands[monsterIndex].enemy.GetComponent<Health>();
            Health ownHealth = monsterAttackCommands[monsterIndex].attack.GetComponent<Health>();

            MonsterAttackChance[] attackChances = new MonsterAttackChance[] { fighters[monsterIndex].GetComponentsInChildren<MonsterAttackChance>()[0] };
            monsterAttack[monsterIndex].attackChances = attackChances;

            MonsterMoveCommand[] monsterMoveCommands = core.GetComponentsInChildren<MonsterMoveCommand>();

            controller.looping = false;
            controller.commands = new AbstractCommand[] { monsterMoveCommands[Revert(monsterIndex)], monsterAttackCommands[monsterIndex] };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(10);

            Assert.AreEqual(100, enemyHealth.currentHp);
            Assert.AreEqual(100, ownHealth.currentHp);

            Assert.AreEqual("100/100", textHealthShowerUIs[Revert(monsterIndex)].hp.text);
            Assert.AreEqual("100/100", textHealthShowerUIs[monsterIndex].hp.text);

            yield return null;
        }

        private int Revert(int index)
        {
            return 1 == index ? 0 : 1;
        }
    }
}
