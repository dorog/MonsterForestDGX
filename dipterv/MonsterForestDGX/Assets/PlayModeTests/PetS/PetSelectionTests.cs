using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PetSelectionTests
    {
        private GameEvents gameEvents;
        private PetTab petTab;
        private PetUI[] petUIs;
        private MockDataIO mockDataIO;
        private PetManager petManager;

        private GameObject core;

        private IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/PetSelecting/PetSelecting.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            gameEvents = core.GetComponentInChildren<GameEvents>();
            petTab = core.GetComponentInChildren<PetTab>();
            petManager = core.GetComponentInChildren<PetManager>();
            
            mockDataIO = core.GetComponentInChildren<MockDataIO>();

            yield return null;
        }

        private void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator DefaultStateTest()
        {
            yield return SetUp();

            Assert.AreEqual(false, petTab.root.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator NotShowPetTabTest()
        {
            yield return SetUp();

            gameEvents.EnteredLobby();

            Assert.AreEqual(false, petTab.root.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShowPetTabTest()
        {
            yield return SetUp();

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

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOnePetTest()
        {
            yield return SetUp();

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

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOneThanAnotherOneTest()
        {
            yield return SetUp();

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

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOneThanSelectTheSameOneTest()
        {
            yield return SetUp();

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

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectorFightTest()
        {
            yield return SetUp();

            gameEvents.petEnable = true;
            gameEvents.EnteredLobby();
            gameEvents.Fight();

            Assert.AreEqual(false, petTab.root.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectNothingThanNextFightTest()
        {
            yield return SetUp();

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

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOneThanNextFightTest()
        {
            yield return SetUp();

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

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetTest()
        {
            yield return SetUp();

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

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectOneThanUnlockAnotherOneTest()
        {
            yield return SetUp();

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

            TearDown();

            yield return null;
        }
    }
}
