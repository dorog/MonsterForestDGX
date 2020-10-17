using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.ExperienceModule
{
    public class ExperienceCollectingTests
    {
        private BattleManager battleManager = null;
        private MockDataIO mockDataIO = null;
        private MagicCircleHandler magicCircleHandler = null;

        private MockFighter blueFighter = null;
        private MockFighter redFighter = null;

        private GameObject core;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Experience/ExpCollecting/ExpCollecting.prefab");
            core = Object.Instantiate(coreGO);

            battleManager = core.GetComponentInChildren<BattleManager>();
            mockDataIO = core.GetComponentInChildren<MockDataIO>();
            magicCircleHandler = core.GetComponentInChildren<MagicCircleHandler>();

            var mockFighters = core.GetComponentsInChildren<MockFighter>();
            blueFighter = mockFighters[0];
            redFighter = mockFighters[1];

            battleManager.BattleLobby(redFighter, blueFighter);
            battleManager.BattleStart();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator CastExperienceTest()
        {
            SpellResult spellResult = new SpellResult() { Index = 0, Coverage = 1 };
            magicCircleHandler.CastSpell(spellResult);

            battleManager.DrawFight();

            Assert.AreEqual(ExpType.Cast.GetExp(), mockDataIO.LocalGameData.exp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator CastAndKillExperienceTest()
        {
            magicCircleHandler.transform.localRotation = Quaternion.Euler(0, 45, 0);

            SpellResult spellResult = new SpellResult() { Index = 0, Coverage = 1 };
            magicCircleHandler.CastSpell(spellResult);

            yield return new WaitForSeconds(10);

            battleManager.RedFighterDied();

            Assert.AreEqual(ExpType.Cast.GetExp() + ExpType.Kill.GetExp(), mockDataIO.LocalGameData.exp);
        }

        [UnityTest]
        public IEnumerator AllExperienceTest()
        {
            SpellResult spellResult = new SpellResult() { Index = 0, Coverage = 1 };
            magicCircleHandler.CastSpell(spellResult);

            yield return new WaitForSeconds(10);

            battleManager.RedFighterDied();

            Assert.AreEqual(ExpType.Cast.GetExp() + ExpType.Hit.GetExp() + ExpType.Kill.GetExp(), mockDataIO.LocalGameData.exp);
        }

        [UnityTest]
        public IEnumerator CastButDieExperienceTest()
        {
            SpellResult spellResult = new SpellResult() { Index = 0, Coverage = 1 };
            magicCircleHandler.CastSpell(spellResult);

            battleManager.BlueFighterDied();

            Assert.AreEqual(0, mockDataIO.LocalGameData.exp);

            yield return null;
        }
    }
}
