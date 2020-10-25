using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.BattleModule.Commanding
{
    public class BattleFightTurnCommandingTests
    {
        private BattleManager battleManager = null;

        private FighterTurnCommand[] fighterTurnCommands = null;
        private Controller controller = null;

        private GameObject core;

        private MockFighter blueFighter = null;
        private MockFighter redFighter = null;

        private CallChecker BlueFighterTurnStart;
        private CallChecker RedFighterTurnStart;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Battle/Commanding/Commanding.prefab");
            core = Object.Instantiate(coreGO);

            battleManager = core.GetComponentInChildren<BattleManager>();
            fighterTurnCommands = core.GetComponentsInChildren<FighterTurnCommand>();
            controller = core.GetComponentInChildren<Controller>();

            BlueFighterTurnStart = new CallChecker();
            RedFighterTurnStart = new CallChecker();

            battleManager.BlueFighterTurnStartDelegateEvent += BlueFighterTurnStart.Call;
            battleManager.RedFighterTurnStartDelegateEvent += RedFighterTurnStart.Call;

            var mockFighters = core.GetComponentsInChildren<MockFighter>();
            blueFighter = mockFighters[0];
            redFighter = mockFighters[1];

            battleManager.BattleLobby(redFighter, blueFighter);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator BlueFighterTurnTest()
        {
            controller.commands = new AbstractCommand[] { fighterTurnCommands[0] };

            controller.InitCommands();
            controller.StartController();

            Assert.AreEqual(1, BlueFighterTurnStart.Called);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BlueFighterTurnTwiceTest()
        {
            controller.commands = new AbstractCommand[] { fighterTurnCommands[0] };

            controller.looping = true;

            controller.InitCommands();
            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            Assert.AreEqual(2, BlueFighterTurnStart.Called);

            yield return null;
        }

        [UnityTest]
        public IEnumerator RedFighterTurnTest()
        {
            controller.commands = new AbstractCommand[] { fighterTurnCommands[1] };

            controller.InitCommands();
            controller.StartController();

            Assert.AreEqual(1, RedFighterTurnStart.Called);

            yield return null;
        }

        [UnityTest]
        public IEnumerator RedFighterTurnTwiceTest()
        {
            controller.commands = new AbstractCommand[] { fighterTurnCommands[1] };

            controller.looping = true;

            controller.InitCommands();
            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            Assert.AreEqual(2, RedFighterTurnStart.Called);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BlueFighterThenRedFighterTest()
        {
            controller.commands = new AbstractCommand[] { fighterTurnCommands[0], fighterTurnCommands[1] };

            controller.InitCommands();
            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            Assert.AreEqual(1, RedFighterTurnStart.Called);
            Assert.AreEqual(1, BlueFighterTurnStart.Called);

            yield return null;
        }

        [UnityTest]
        public IEnumerator RedFighterThenBlueFighterTest()
        {
            controller.commands = new AbstractCommand[] { fighterTurnCommands[1], fighterTurnCommands[0] };

            controller.InitCommands();
            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            Assert.AreEqual(1, RedFighterTurnStart.Called);
            Assert.AreEqual(1, BlueFighterTurnStart.Called);

            yield return null;
        }

        [UnityTest]
        public IEnumerator FullTurnTwiceWithRedStartTest()
        {
            controller.commands = new AbstractCommand[] { fighterTurnCommands[1], fighterTurnCommands[0] };

            controller.looping = true;

            controller.InitCommands();
            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            Assert.AreEqual(2, RedFighterTurnStart.Called);
            Assert.AreEqual(2, BlueFighterTurnStart.Called);

            yield return null;
        }

        [UnityTest]
        public IEnumerator FullTurnTwiceWithBlueStartTest()
        {
            controller.commands = new AbstractCommand[] { fighterTurnCommands[0], fighterTurnCommands[1] };

            controller.looping = true;

            controller.InitCommands();
            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            yield return new WaitForSeconds(1);

            controller.StartController();

            Assert.AreEqual(2, RedFighterTurnStart.Called);
            Assert.AreEqual(2, BlueFighterTurnStart.Called);

            yield return null;
        }

        public class CallChecker
        {
            public int Called = 0;
            
            public void Call()
            {
                Called++;
            }
        }
    }
}
