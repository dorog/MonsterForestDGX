using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PetUiShowingTests
    {
        private PetManager petManager;
        private PetUiShowerComponent petUiShower;
        private PetAbilityDescriptionsUI petAbilityDescriptionsUI;

        private GameObject core;

        private IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/PetUiShowing/PetUiShowing.prefab");
            core = Object.Instantiate(coreGO);

            petManager = core.GetComponentInChildren<PetManager>();
            petUiShower = core.GetComponentInChildren<PetUiShowerComponent>();

            yield return null;
        }

        private void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator DefaultState()
        {
            yield return SetUp();

            Assert.AreEqual(false, petUiShower.root.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator CheckUnlockActivesTest()
        {
            yield return SetUp();

            petManager.UnlockPet(0);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual(true, petUiShower.root.activeSelf);
            Assert.AreEqual(true, petAbilityDescriptionsUI.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetWithAllAbilityTest()
        {
            yield return SetUp();

            petManager.UnlockPet(0);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Alpha", petUiShower.petNameText.text);
            Assert.AreEqual(3, petAbilityDescriptionsUI.transform.childCount);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetWithOneUpdateAndOneNormalAbilityTest()
        {
            yield return SetUp();

            petManager.UnlockPet(1);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Beta", petUiShower.petNameText.text);
            Assert.AreEqual(2, petAbilityDescriptionsUI.transform.childCount);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetWithOneUpdataAndZeroNormalAbilityTest()
        {
            yield return SetUp();

            petManager.UnlockPet(2);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Reddy", petUiShower.petNameText.text);
            Assert.AreEqual(1, petAbilityDescriptionsUI.transform.childCount);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetWithZeroUpdataAndOneNormalAbilityTest()
        {
            yield return SetUp();

            petManager.UnlockPet(3);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Tiger", petUiShower.petNameText.text);
            Assert.AreEqual(1, petAbilityDescriptionsUI.transform.childCount);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetWithTwoUpdataAndZeroNormalAbilityTest()
        {
            yield return SetUp();

            petManager.UnlockPet(4);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Zaz", petUiShower.petNameText.text);
            Assert.AreEqual(2, petAbilityDescriptionsUI.transform.childCount);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetWithZeroAbilityTest()
        {
            yield return SetUp();

            petManager.UnlockPet(5);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Zuk", petUiShower.petNameText.text);
            Assert.AreEqual(0, petAbilityDescriptionsUI.transform.childCount);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator HideUiTest()
        {
            yield return SetUp();

            petManager.UnlockPet(0);

            petUiShower.HideUI();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, petUiShower.root.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator TwoUnlockTest()
        {
            yield return SetUp();

            petManager.UnlockPet(0);

            petUiShower.HideUI();

            petManager.UnlockPet(1);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Beta", petUiShower.petNameText.text);
            Assert.AreEqual(2, petAbilityDescriptionsUI.transform.childCount);

            TearDown();

            yield return null;
        }
    }
}
