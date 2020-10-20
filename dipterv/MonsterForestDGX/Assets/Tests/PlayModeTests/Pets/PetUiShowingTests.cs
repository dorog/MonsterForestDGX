using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PetModule
{
    public class PetUiShowingTests
    {
        private MfxPetManager petManager;
        private PetUiShowerComponent petUiShower;
        private PetAbilityDescriptionsUI petAbilityDescriptionsUI;

        private GameObject core;

        [SetUp]
        public void SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Pets/PetUiShowing/PetUiShowing.prefab");
            core = Object.Instantiate(coreGO);

            petManager = core.GetComponentInChildren<MfxPetManager>();
            petUiShower = core.GetComponentInChildren<PetUiShowerComponent>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator DefaultState()
        {
            Assert.AreEqual(false, petUiShower.root.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator CheckUnlockActivesTest()
        {
            petManager.UnlockPet(0);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual(true, petUiShower.root.activeSelf);
            Assert.AreEqual(true, petAbilityDescriptionsUI.gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockPetWithAllAbilityTest()
        {
            petManager.UnlockPet(0);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Alpha", petUiShower.petNameText.text);
            Assert.AreEqual(3, petAbilityDescriptionsUI.transform.childCount);
        }

        [UnityTest]
        public IEnumerator UnlockPetWithOneUpdateAndOneNormalAbilityTest()
        {
            petManager.UnlockPet(1);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Beta", petUiShower.petNameText.text);
            Assert.AreEqual(2, petAbilityDescriptionsUI.transform.childCount);
        }

        [UnityTest]
        public IEnumerator UnlockPetWithOneUpdataAndZeroNormalAbilityTest()
        {
            petManager.UnlockPet(2);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Reddy", petUiShower.petNameText.text);
            Assert.AreEqual(1, petAbilityDescriptionsUI.transform.childCount);
        }

        [UnityTest]
        public IEnumerator UnlockPetWithZeroUpdataAndOneNormalAbilityTest()
        {
            petManager.UnlockPet(3);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Tiger", petUiShower.petNameText.text);
            Assert.AreEqual(1, petAbilityDescriptionsUI.transform.childCount);
        }

        [UnityTest]
        public IEnumerator UnlockPetWithTwoUpdataAndZeroNormalAbilityTest()
        {
            petManager.UnlockPet(4);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Zaz", petUiShower.petNameText.text);
            Assert.AreEqual(2, petAbilityDescriptionsUI.transform.childCount);
        }

        [UnityTest]
        public IEnumerator UnlockPetWithZeroAbilityTest()
        {
            petManager.UnlockPet(5);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Zuk", petUiShower.petNameText.text);
            Assert.AreEqual(0, petAbilityDescriptionsUI.transform.childCount);
        }

        [UnityTest]
        public IEnumerator HideUiTest()
        {
            petManager.UnlockPet(0);

            petUiShower.HideUI();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, petUiShower.root.activeSelf);
        }

        [UnityTest]
        public IEnumerator TwoUnlockTest()
        {
            petManager.UnlockPet(0);

            petUiShower.HideUI();

            petManager.UnlockPet(1);

            yield return new WaitForSeconds(1);

            petAbilityDescriptionsUI = core.GetComponentInChildren<PetAbilityDescriptionsUI>();

            Assert.AreEqual("Beta", petUiShower.petNameText.text);
            Assert.AreEqual(2, petAbilityDescriptionsUI.transform.childCount);
        }
    }
}
