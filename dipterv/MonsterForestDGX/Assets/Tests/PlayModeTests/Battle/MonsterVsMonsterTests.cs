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

        private MonsterAttackChances[] monsterAttackChances = null;
        private MonsterAttack[] monsterAttack = null;

        private GameObject core;

        private Monster blueFighter = null;
        private Monster redFighter = null;

        private TextHealthShowerUI[] textHealthShowerUIs = null;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/MonsterVsMonster.prefab");
            core = Object.Instantiate(coreGO);

            battleManager = core.GetComponentInChildren<BattleManager>();
            fighterTurnCommands = core.GetComponentsInChildren<FighterTurnCommand>();
            monsterAttackCommands = core.GetComponentsInChildren<MonsterAttackCommand>();
            monsterAttackChances = core.GetComponentsInChildren<MonsterAttackChances>();
            monsterAttack = core.GetComponentsInChildren<MonsterAttack>();
            textHealthShowerUIs = core.GetComponentsInChildren<TextHealthShowerUI>();
            controller = core.GetComponentInChildren<AutoController>();

            var fighters = core.GetComponentsInChildren<Monster>();
            blueFighter = fighters[0];
            redFighter = fighters[1];

            battleManager.BattleLobby(redFighter, blueFighter);
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

            bool blueNormalAttackBeforeFight = monsterAttack[0].normalAttacks[0].activate[0].activeSelf;
            bool blueHardAttackBeforeFight = monsterAttack[0].hardAttacks[0].activate[0].activeSelf;
            bool blueUltimateAttackBeforeFight = monsterAttack[0].ultimateAttacks[0].activate[0].activeSelf;

            Assert.AreEqual(false, blueNormalAttackBeforeFight);
            Assert.AreEqual(false, blueHardAttackBeforeFight);
            Assert.AreEqual(false, blueUltimateAttackBeforeFight);

            bool redNormalAttackBeforeFight = monsterAttack[1].normalAttacks[0].activate[0].activeSelf;
            bool redHardAttackBeforeFight = monsterAttack[1].hardAttacks[0].activate[0].activeSelf;
            bool redUltimateAttackBeforeFight = monsterAttack[1].ultimateAttacks[0].activate[0].activeSelf;

            Assert.AreEqual(false, redNormalAttackBeforeFight);
            Assert.AreEqual(false, redHardAttackBeforeFight);
            Assert.AreEqual(false, redUltimateAttackBeforeFight);

            Assert.AreEqual("100/100", textHealthShowerUIs[0].hp.text);
            Assert.AreEqual("100/100", textHealthShowerUIs[1].hp.text);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SimpleAttackTest([Values(0, 1)] int monsterIndex, [Values (0, 1, 2)] int monsterAttackChanceIndex)
        {
            monsterAttack[monsterIndex].attackChances = monsterAttackChances[monsterAttackChanceIndex];

            controller.looping = false;
            controller.commands = new AbstractCommand[] { fighterTurnCommands[monsterIndex], monsterAttackCommands[monsterIndex] };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(1);

            bool normalAttackDuringFight = monsterAttack[monsterIndex].normalAttacks[0].activate[0].activeSelf;
            bool hardAttackDuringFight = monsterAttack[monsterIndex].hardAttacks[0].activate[0].activeSelf;
            bool ultimateAttackDuringFight = monsterAttack[monsterIndex].ultimateAttacks[0].activate[0].activeSelf;

            bool[] expecteds = new bool[] { false, false, false };
            expecteds[monsterAttackChanceIndex] = true;

            Assert.AreEqual(expecteds[0], normalAttackDuringFight);
            Assert.AreEqual(expecteds[1], hardAttackDuringFight);
            Assert.AreEqual(expecteds[2], ultimateAttackDuringFight);

            yield return new WaitForSeconds(10);

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

            monsterAttack[monsterIndex].attackChances = monsterAttackChances[monsterAttackChanceIndex];
            monsterAttack[Revert(monsterIndex)].attackChances = monsterAttackChances[monsterAttackChanceIndex];

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

            enemyHealth.TakeDamage(90, ElementType.TrueDamage);

            monsterAttack[monsterIndex].attackChances = monsterAttackChances[2];

            controller.looping = true;
            controller.commands = new AbstractCommand[] { monsterAttackCommands[monsterIndex] };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(10);

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

            monsterAttack[monsterIndex].attackChances = monsterAttackChances[monsterAttackIndex];

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

            monsterAttack[monsterIndex].attackChances = monsterAttackChances[0];

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
