using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PetModule
{
    public class PetSelectionTests
    {
        private GameEvents gameEvents;
        private PetTab petTab;
        private PetUI[] petUIs;
        private MockDataIO mockDataIO;
        private PetManager petManager;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Pets/PetSelecting/PetSelecting.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            gameEvents = core.GetComponentInChildren<GameEvents>();
            petTab = core.GetComponentInChildren<PetTab>();
            petManager = core.GetComponentInChildren<PetManager>();
            
            mockDataIO = core.GetComponentInChildren<MockDataIO>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator DefaultStateTest()
        {
            Assert.AreEqual(false, petTab.root.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator NotShowPetTabTest()
        {
            gameEvents.EnteredLobby();

            Assert.AreEqual(false, petTab.root.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShowPetTabTest()
        {
            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();

            Assert.AreEqual(true, petTab.root.activeSelf);

            petUIs = core.GetComponentsInChildren<PetUI>();

            Assert.AreEqual(3, petUIs.Length);

            Assert.AreEqual(false, petUIs[0].root.activeSelf);
            Assert.AreEqual(false, petUIs[0].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[1].root.activeSelf);
            Assert.AreEqual(false, petUIs[1].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[2].root.activeSelf);
            Assert.AreEqual(false, petUIs[2].selectedGo.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOnePetTest()
        {
            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();

            petUIs = core.GetComponentsInChildren<PetUI>();

            petUIs[1].ChangePet();

            Assert.AreEqual(false, petUIs[0].root.activeSelf);
            Assert.AreEqual(false, petUIs[0].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[1].root.activeSelf);
            Assert.AreEqual(true, petUIs[1].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[2].root.activeSelf);
            Assert.AreEqual(false, petUIs[2].selectedGo.activeSelf);

            Assert.AreEqual(1, mockDataIO.LocalGameData.lastSelectedPet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOneThanAnotherOneTest()
        {
            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();

            petUIs = core.GetComponentsInChildren<PetUI>();

            petUIs[1].ChangePet();
            petUIs[2].ChangePet();

            Assert.AreEqual(false, petUIs[0].root.activeSelf);
            Assert.AreEqual(false, petUIs[0].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[1].root.activeSelf);
            Assert.AreEqual(false, petUIs[1].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[2].root.activeSelf);
            Assert.AreEqual(true, petUIs[2].selectedGo.activeSelf);

            Assert.AreEqual(2, mockDataIO.LocalGameData.lastSelectedPet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOneThanSelectTheSameOneTest()
        {
            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();

            petUIs = core.GetComponentsInChildren<PetUI>();

            petUIs[1].ChangePet();
            petUIs[1].ChangePet();

            Assert.AreEqual(false, petUIs[0].root.activeSelf);
            Assert.AreEqual(false, petUIs[0].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[1].root.activeSelf);
            Assert.AreEqual(false, petUIs[1].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[2].root.activeSelf);
            Assert.AreEqual(false, petUIs[2].selectedGo.activeSelf);

            Assert.AreEqual(-1, mockDataIO.LocalGameData.lastSelectedPet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectorFightTest()
        {
            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();
            gameEvents.Fight();

            Assert.AreEqual(false, petTab.root.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectNothingThanNextFightTest()
        {
            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();

            petUIs = core.GetComponentsInChildren<PetUI>();

            gameEvents.Fight();
            gameEvents.EnteredLobby();

            Assert.AreEqual(false, petUIs[0].root.activeSelf);
            Assert.AreEqual(false, petUIs[0].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[1].root.activeSelf);
            Assert.AreEqual(false, petUIs[1].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[2].root.activeSelf);
            Assert.AreEqual(false, petUIs[2].selectedGo.activeSelf);

            Assert.AreEqual(-1, mockDataIO.LocalGameData.lastSelectedPet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOneThanNextFightTest()
        {
            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();

            petUIs = core.GetComponentsInChildren<PetUI>();

            petUIs[1].ChangePet();

            gameEvents.Fight();
            gameEvents.EnteredLobby();

            Assert.AreEqual(false, petUIs[0].root.activeSelf);
            Assert.AreEqual(false, petUIs[0].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[1].root.activeSelf);
            Assert.AreEqual(true, petUIs[1].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[2].root.activeSelf);
            Assert.AreEqual(false, petUIs[2].selectedGo.activeSelf);

            Assert.AreEqual(1, mockDataIO.LocalGameData.lastSelectedPet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetTest()
        {
            petManager.UnlockPet(0);

            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();

            petUIs = core.GetComponentsInChildren<PetUI>();

            Assert.AreEqual(true, petUIs[0].root.activeSelf);
            Assert.AreEqual(false, petUIs[0].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[1].root.activeSelf);
            Assert.AreEqual(false, petUIs[1].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[2].root.activeSelf);
            Assert.AreEqual(false, petUIs[2].selectedGo.activeSelf);

            Assert.AreEqual(-1, mockDataIO.LocalGameData.lastSelectedPet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOneThanUnlockAnotherOneTest()
        {
            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();

            petUIs = core.GetComponentsInChildren<PetUI>();

            petUIs[1].ChangePet();
            petManager.UnlockPet(0);

            Assert.AreEqual(true, petUIs[0].root.activeSelf);
            Assert.AreEqual(false, petUIs[0].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[1].root.activeSelf);
            Assert.AreEqual(true, petUIs[1].selectedGo.activeSelf);

            Assert.AreEqual(true, petUIs[2].root.activeSelf);
            Assert.AreEqual(false, petUIs[2].selectedGo.activeSelf);

            Assert.AreEqual(1, mockDataIO.LocalGameData.lastSelectedPet);

            yield return null;
        }
    }
}
