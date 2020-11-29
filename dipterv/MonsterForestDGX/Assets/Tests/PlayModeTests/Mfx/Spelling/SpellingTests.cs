using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Mfx
{
    public class SpellingTests
    {
        private MockButtonInput pressedInput;
        private MockPressingInput pressingInput;
        private GameEvents gameEvents;
        private BattleManager battleManager;

        private CircleStateHandler circleState;

        private MockFighter redFighter;
        private MockFighter blueFighter;

        private MagicCircleHandler magicCircleHandler;

        private GameObject core;

        private RoundHandler roundHandler;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Mfx/Spelling/Spelling.prefab");
            core = Object.Instantiate(coreGO);

            pressedInput = core.GetComponentInChildren<MockButtonInput>();
            pressingInput = core.GetComponentInChildren<MockPressingInput>();

            gameEvents = core.GetComponentInChildren<GameEvents>();

            circleState = core.GetComponentInChildren<CircleStateHandler>();
            magicCircleHandler = core.GetComponentInChildren<MagicCircleHandler>();

            battleManager = core.GetComponentInChildren<BattleManager>();
            roundHandler = core.GetComponentInChildren<RoundHandler>();
            var mockFighters = core.GetComponentsInChildren<MockFighter>();
            redFighter = mockFighters[0];
            blueFighter = mockFighters[1];

            battleManager.BattleLobby(redFighter, blueFighter);

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
            Assert.AreEqual(null, circleState);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BattleStartTest()
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            Assert.AreEqual(null, circleState);

            yield return null;
        }

        [UnityTest]
        public IEnumerator MonsterTurnTest()
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            yield return new WaitForSeconds(1);

            roundHandler.Fight();

            yield return new WaitForSeconds(1);

            pressedInput.Pressed();

            yield return new WaitForSeconds(1);

            circleState = core.GetComponentInChildren<CircleStateHandler>();

            Assert.AreEqual(null, circleState);

            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerTurnTest()
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            yield return new WaitForSeconds(1);

            roundHandler.Fight();

            yield return new WaitForSeconds(1);

            pressedInput.Pressed();

            yield return new WaitForSeconds(1);

            circleState = core.GetComponentInChildren<CircleStateHandler>();

            Assert.AreNotEqual(null, circleState);
            Assert.IsTrue(0.1 > (circleState.transform.position - magicCircleHandler.transform.position - magicCircleHandler.transform.forward * 2).magnitude);

            yield return null;
        }

        private static readonly GuessTestParameter[] guessTestParameters = new GuessTestParameter[]
        {
            new GuessTestParameter(){ MovePosition = new Vector3(0, 0, 0), Expected = new Vector2(){ x = 0, y = 0 } },
            new GuessTestParameter(){ MovePosition = new Vector3(0, 0, 1), Expected = new Vector2(){ x = 0, y = 0 } },
            new GuessTestParameter(){ MovePosition = new Vector3(0, 0, -1), Expected = new Vector2(){ x = 0, y = 0 } },
            new GuessTestParameter(){ MovePosition = new Vector3(0, 1, -1), Expected = new Vector2(){ x = 0, y = 1 } },
            new GuessTestParameter(){ MovePosition = new Vector3(0, -1, 1), Expected = new Vector2(){ x = 0, y = -1 } },
            new GuessTestParameter(){ MovePosition = new Vector3(1, 0, -1), Expected = new Vector2(){ x = 1, y = 0 } },
        };

        [UnityTest]
        public IEnumerator GuessTest([ValueSource(nameof(guessTestParameters))] GuessTestParameter parameters)
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            yield return new WaitForSeconds(1);

            roundHandler.Fight();

            yield return new WaitForSeconds(1);

            pressedInput.Pressed();

            yield return new WaitForSeconds(1);

            magicCircleHandler.transform.position += parameters.MovePosition;

            yield return new WaitForSeconds(1);

            pressingInput.PressingActivate();

            yield return new WaitForSeconds(1);

            MockSpellCastingHandler spellCastingHandler = core.GetComponentInChildren<MockSpellCastingHandler>();
            Assert.IsTrue(0.1f > (spellCastingHandler.guess - parameters.Expected).magnitude);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetResultTest()
        {
            RecognizingResult result = new RecognizingResult() { Coverage = 0.5f, Index = 0 };

            MockSpellCastingHandler spellCastingHandler = core.GetComponentInChildren<MockSpellCastingHandler>();
            spellCastingHandler.result = result;

            gameEvents.Fight();
            battleManager.BattleStart();

            yield return new WaitForSeconds(1);

            roundHandler.Fight();

            yield return new WaitForSeconds(1);

            pressedInput.Pressed();

            yield return new WaitForSeconds(1);

            pressingInput.PressingActivate();

            yield return new WaitForSeconds(1);

            MockSpellCaster spellCaster = core.GetComponentInChildren<MockSpellCaster>();
            LineRenderer lineRenderer = core.GetComponentInChildren<LineRenderer>();

            Assert.IsTrue(0 < lineRenderer.positionCount);

            pressingInput.PressingDeactivate();

            yield return new WaitForSeconds(2);

            Assert.AreEqual(spellCaster.result, result);
            Assert.AreEqual(null, spellCastingHandler.result);
            Assert.AreEqual(0, lineRenderer.positionCount);

            yield return null;
        }

        [UnityTest]
        public IEnumerator CanceledByMonsterTurnTest()
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            yield return new WaitForSeconds(1);

            roundHandler.Fight();

            yield return new WaitForSeconds(1);

            pressedInput.Pressed();

            yield return new WaitForSeconds(1);

            pressingInput.PressingActivate();

            yield return new WaitForSeconds(2);

            MockSpellCaster spellCaster = core.GetComponentInChildren<MockSpellCaster>();
            LineRenderer lineRenderer = core.GetComponentInChildren<LineRenderer>();

            Assert.IsTrue(0 < lineRenderer.positionCount);

            roundHandler.Def();

            yield return new WaitForSeconds(2);

            Assert.AreEqual(null, spellCaster.result);
            Assert.AreEqual(false, spellCaster.gameObject.activeSelf);
            Assert.AreEqual(0, lineRenderer.positionCount);

            yield return null;
        }

        public class GuessTestParameter
        {
            public Vector3 MovePosition;
            public Vector2 Expected;
        }
    }
}
