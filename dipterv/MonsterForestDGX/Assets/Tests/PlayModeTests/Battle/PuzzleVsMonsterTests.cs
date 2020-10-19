using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.BattleModule
{
    public class PuzzleVsMonsterTests
    {
        private BattleManager battleManager = null;

        private FighterTurnCommand[] fighterTurnCommands = null;
        private AttackCommand monsterAttackCommand = null;
        private Controller controller = null;

        private AnimatedAttack monsterAttack = null;

        private GameObject core;

        private AiFighter blueFighter;
        private AiFighter redFighter;

        private AnimatedAttackChance[] allAttackChances;
        private Health[] healths;

        private TextHealthShowerUI[] textHealthShowerUIs = null;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/PuzzleVsMonster.prefab");
            core = Object.Instantiate(coreGO);

            battleManager = core.GetComponentInChildren<BattleManager>();
            fighterTurnCommands = core.GetComponentsInChildren<FighterTurnCommand>();
            monsterAttackCommand = core.GetComponentInChildren<AttackCommand>();
            monsterAttack = core.GetComponentInChildren<AnimatedAttack>();
            textHealthShowerUIs = core.GetComponentsInChildren<TextHealthShowerUI>();
            healths = core.GetComponentsInChildren<Health>();
            controller = core.GetComponentInChildren<AutoController>();
            allAttackChances = core.GetComponentsInChildren<AnimatedAttackChance>();

            var fighters = core.GetComponentsInChildren<AiFighter>();
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
            Vector3 startPosition = healths[1].extraParts[0].transform.position;
            Vector3 fightPosition = startPosition + Vector3.down * 4;

            controller.looping = false;
            controller.commands = new AbstractCommand[] { };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(5);

            Assert.IsTrue(0.1 > (fightPosition - healths[1].extraParts[0].transform.position).magnitude);
            Assert.AreEqual("100/100", textHealthShowerUIs[0].hp.text);
            Assert.AreEqual("100/100", textHealthShowerUIs[1].hp.text);

            yield return null;
        }

        private static readonly PzVsMoTestParameter[] simpleMovingCommandTestParameters = new PzVsMoTestParameter[]
        {
            new PzVsMoTestParameter(){ AttackIndex = 0, ExpectedHealth = 100, PartAlive = true },
            new PzVsMoTestParameter(){ AttackIndex = 1, ExpectedHealth = 50, PartAlive = false },
        };

        [UnityTest]
        public IEnumerator SimpleAttackTest([ValueSource(nameof(simpleMovingCommandTestParameters))] PzVsMoTestParameter parameters)
        {
            AnimatedAttackChance[] attackChances = new AnimatedAttackChance[] { allAttackChances[parameters.AttackIndex] };
            monsterAttack.attackChances = attackChances;

            controller.looping = false;
            controller.commands = new AbstractCommand[] { monsterAttackCommand };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(5);

            Assert.AreEqual(100, healths[0].currentHp);
            Assert.AreEqual(parameters.ExpectedHealth, healths[1].currentHp);

            Assert.AreEqual("100/100", textHealthShowerUIs[0].hp.text);
            Assert.AreEqual(parameters.ExpectedHealth + "/100", textHealthShowerUIs[1].hp.text);

            Assert.AreEqual(parameters.PartAlive, healths[1].extraParts[0].gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator DieTest()
        {
            healths[1].currentHp = 1;

            AnimatedAttackChance[] attackChances = new AnimatedAttackChance[] { allAttackChances[1] };
            monsterAttack.attackChances = attackChances;

            controller.looping = false;
            controller.commands = new AbstractCommand[] { monsterAttackCommand };

            controller.InitCommands();

            battleManager.BattleStart();

            yield return new WaitForSeconds(5);

            Assert.AreEqual(100, healths[0].currentHp);
            Assert.IsTrue(0 > healths[1].currentHp);

            Assert.AreEqual("100/100", textHealthShowerUIs[0].hp.text);
            Assert.AreEqual("Dead", textHealthShowerUIs[1].hp.text);

            Assert.AreEqual(false, healths[1].extraParts[0].gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator WithDrawTest()
        {
            yield return new WaitForSeconds(2);

            battleManager.WithdrawFromFight();

            yield return new WaitForSeconds(4);

            Assert.AreEqual(true, blueFighter.gameObject.activeSelf);
            Assert.AreEqual(true, redFighter.gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator DisableTest()
        {
            Vector3 startPosition = healths[1].extraParts[0].transform.position;
            Vector3 fightPosition = startPosition + Vector3.down * 14;

            yield return new WaitForSeconds(5);

            redFighter.Disable();

            yield return new WaitForSeconds(2);

            Assert.AreEqual(false, redFighter.gameObject.activeSelf);
            Assert.AreEqual(false, redFighter.gameObject.activeSelf);
            Assert.IsTrue(0.1 > (fightPosition - healths[1].extraParts[0].transform.position).magnitude);
            Assert.AreEqual("100/100", textHealthShowerUIs[0].hp.text);
            Assert.AreEqual("100/100", textHealthShowerUIs[1].hp.text);

            yield return null;
        }

        public class PzVsMoTestParameter
        {
            public int AttackIndex;
            public int ExpectedHealth;
            public bool PartAlive;
        }
    }
}
