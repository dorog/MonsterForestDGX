using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PetModule
{
    public class PetInitializingTests
    {
        private MockPetSelector petSelector;
        private GameEvents gameEvents;

        private Pet pet;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Pets/PetInitializing/PetInitializing.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            petSelector = core.GetComponentInChildren<MockPetSelector>();
            gameEvents = core.GetComponentInChildren<GameEvents>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
            if(pet != null)
            {
                Object.Destroy(pet.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator NoPetSelectedTest()
        {
            petSelector.Id = -1;

            gameEvents.petEnable = true;
            gameEvents.Fight();

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPetTest()
        {
            petSelector.Id = 0;
            gameEvents.petEnable = true;
            gameEvents.Fight();

            yield return new WaitForSeconds(2);

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual("Reddy", pet.petName);
        }

        [UnityTest]
        public IEnumerator SelectPetVanishedTest()
        {
            petSelector.Id = 0;

            gameEvents.petEnable = true;
            gameEvents.Fight();
            gameEvents.Explore();

            yield return new WaitForSeconds(1);

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);
        }

        [UnityTest]
        public IEnumerator SelectPetNexFightTest()
        {
            petSelector.Id = 0;

            gameEvents.petEnable = true;
            gameEvents.Fight();
            gameEvents.Explore();
            gameEvents.Fight();

            yield return new WaitForSeconds(2);

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual("Reddy", pet.petName);

            Object.Destroy(pet.gameObject);
        }

        [UnityTest]
        public IEnumerator SelectThanNotSelectPetNexFightTest()
        {
            petSelector.Id = 0;

            gameEvents.petEnable = true;
            gameEvents.Fight();
            gameEvents.Explore();

            yield return new WaitForSeconds(1);

            petSelector.Id = -1;

            gameEvents.Fight();

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);
        }

        [UnityTest]
        public IEnumerator NotValidIndexTest()
        {
            petSelector.Id = 5;

            gameEvents.petEnable = true;
            gameEvents.Fight();

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator NotAvailablePetSelectedTest()
        {
            gameEvents.petEnable = true;
            petSelector.Id = 2;

            gameEvents.Fight();

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ValidPetSelectedButDisabledPetsTest()
        {
            gameEvents.petEnable = false;
            petSelector.Id = 0;

            gameEvents.Fight();

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ValidPetSelectedButNextFightDisabledPetsTest()
        {
            gameEvents.petEnable = true;
            petSelector.Id = 0;

            gameEvents.Fight();
            gameEvents.Explore();

            yield return new WaitForSeconds(1);

            gameEvents.petEnable = false;
            gameEvents.Fight();

            pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);
        }
    }
}
