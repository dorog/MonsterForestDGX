using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Mfx
{
    public class ShieldingTests
    {
        private MockButtonInput buttonInput;
        private GameEvents gameEvents;
        private BattleManager battleManager;
        private RoundHandler roundHandler;

        private MockFighter redFighter;
        private MockFighter blueFighter;

        private PlayerHealth playerHealth;
        private TimeDamageBlock[] timeDamageBlocks;

        private ShieldHandler shieldHandler;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Mfx/Shielding/Shielding.prefab");
            core = Object.Instantiate(coreGO);

            buttonInput = core.GetComponentInChildren<MockButtonInput>();
            gameEvents = core.GetComponentInChildren<GameEvents>();

            playerHealth = core.GetComponentInChildren<PlayerHealth>();
            timeDamageBlocks = core.GetComponentsInChildren<TimeDamageBlock>();
            shieldHandler = core.GetComponentInChildren<ShieldHandler>();

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
            Assert.AreEqual(null, playerHealth.damageBlock);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BattleStartedTest()
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(null, playerHealth.damageBlock);

            yield return null;
        }

        [UnityTest]
        public IEnumerator NoTurnTest()
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            yield return new WaitForSeconds(1);

            buttonInput.Pressed();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(null, playerHealth.damageBlock);

            yield return null;
        }


        [UnityTest]
        public IEnumerator PlayerTurnTest()
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            roundHandler.Fight();

            yield return new WaitForSeconds(1);

            buttonInput.Pressed();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(null, playerHealth.damageBlock);

            yield return null;
        }

        private static readonly ShieldTestParameter[] shieldTestParameters = new ShieldTestParameter[]
        {
            new ShieldTestParameter(){ rotation = new Vector3(0, 0, 0),  DamageBlockIndex = 0 },
            new ShieldTestParameter(){ rotation = new Vector3(0, 0, 30),  DamageBlockIndex = 0 },
            new ShieldTestParameter(){ rotation = new Vector3(0, 0, -30),  DamageBlockIndex = 0 },
            new ShieldTestParameter(){ rotation = new Vector3(0, 0, 90),  DamageBlockIndex = 0 },
            new ShieldTestParameter(){ rotation = new Vector3(0, 30, 0),  DamageBlockIndex = 0 },
            new ShieldTestParameter(){ rotation = new Vector3(0, -30, 0),  DamageBlockIndex = 0 },
            new ShieldTestParameter(){ rotation = new Vector3(0, 30, 30),  DamageBlockIndex = 0 },
            new ShieldTestParameter(){ rotation = new Vector3(-20, 0, 0),  DamageBlockIndex = 0 },
            new ShieldTestParameter(){ rotation = new Vector3(-30, 0, 0),  DamageBlockIndex = -1 },
            new ShieldTestParameter(){ rotation = new Vector3(-60, 0, 0),  DamageBlockIndex = -1 },
            new ShieldTestParameter(){ rotation = new Vector3(-70, 0, 0),  DamageBlockIndex = 1 },
            new ShieldTestParameter(){ rotation = new Vector3(-85, 0, 0),  DamageBlockIndex = 1 },
            new ShieldTestParameter(){ rotation = new Vector3(-95, 0, 0),  DamageBlockIndex = -1 },
        };

        [UnityTest]
        public IEnumerator MonsterTurnTest([ValueSource(nameof(shieldTestParameters))] ShieldTestParameter parameters)
        {
            gameEvents.Fight();
            battleManager.BattleStart();

            roundHandler.Def();

            shieldHandler.transform.rotation = Quaternion.Euler(parameters.rotation);

            yield return new WaitForSeconds(1);

            buttonInput.Pressed();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(GetDamageBlockByIndex(parameters.DamageBlockIndex), playerHealth.damageBlock);

            yield return null;
        }

        private DamageBlock GetDamageBlockByIndex(int index)
        {
            if (index == -1)
            {
                return null;
            }

            return timeDamageBlocks[index];
        }

        public class ShieldTestParameter
        {
            public Vector3 rotation;
            public int DamageBlockIndex;
        }
    }
}
