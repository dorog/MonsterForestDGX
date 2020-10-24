using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Mfx
{
    public class BattlePreparationTests
    {
        private MockAxisInput[] axisInputs;
        private BattleLobby battleLobby;
        private BattlePlace battlePlace;
        private BattlePlaceTrigger battlePlaceTrigger;
        private MfxTeleportPoint[] teleports;
        private CharacterController player;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Mfx/MfxBattle.prefab");
            core = Object.Instantiate(coreGO);

            axisInputs = core.GetComponentsInChildren<MockAxisInput>();
            battleLobby = core.GetComponentInChildren<BattleLobby>();
            battlePlace = core.GetComponentInChildren<BattlePlace>();
            battlePlaceTrigger = core.GetComponentInChildren<BattlePlaceTrigger>();
            teleports = core.GetComponentsInChildren<MfxTeleportPoint>();
            player = core.GetComponentInChildren<CharacterController>();

            yield return null;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }



        [UnityTest]
        public IEnumerator EnterLobbyTest([Values(true, false)] bool petEnabled, [Values(true, false)] bool resistantEnabled)
        {
            battlePlace.petEnable = petEnabled;
            battlePlace.resistantEnable = resistantEnabled;

            yield return new WaitForSeconds(2);

            axisInputs[0].SetGetValue(new Vector3(0, 1), 3);

            yield return new WaitForSeconds(4);

            Assert.AreEqual(false, battlePlaceTrigger.gameObject.activeSelf);
            Assert.AreEqual(true, battleLobby.battleLobbyUI.activeSelf);
            Assert.AreEqual(resistantEnabled, battleLobby.resistantTab.activeSelf);
            Assert.AreEqual(petEnabled, battleLobby.petTab.activeSelf);
            Assert.IsTrue(0.1 > (player.transform.position - battlePlaceTrigger.transform.position).magnitude);
            Assert.IsTrue(0.1 > Quaternion.Angle(player.transform.rotation, battlePlaceTrigger.transform.rotation));

            yield return null;
        }

        [UnityTest]
        public IEnumerator EnterLobbyButWithdrawTest()
        {
            yield return new WaitForSeconds(2);

            axisInputs[0].SetGetValue(new Vector3(0, 1), 3);

            yield return new WaitForSeconds(4);

            GameObject startGO = GameObject.Find("Run");
            Button startBtn = startGO.GetComponent<Button>();
            startBtn.onClick.Invoke();

            yield return new WaitForSeconds(4);

            Assert.AreEqual(false, battleLobby.battleLobbyUI.activeSelf);
            Assert.AreEqual(true, battlePlaceTrigger.gameObject.activeSelf);
            Assert.IsTrue(0.1 > (player.transform.position - teleports[0].transform.position).magnitude);

            yield return null;
        }
    }
}
