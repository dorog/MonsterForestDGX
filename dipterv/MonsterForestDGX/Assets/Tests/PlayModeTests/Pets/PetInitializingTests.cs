using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PetInitializingTests
    {
        private MockPetSelector petSelector;
        private GameEvents gameEvents;

        private GameObject core;

        private IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/PetInitializing/PetInitializing.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            petSelector = core.GetComponentInChildren<MockPetSelector>();
            gameEvents = core.GetComponentInChildren<GameEvents>();

            yield return null;
        }

        private void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator NoPetSelectedTest()
        {
            yield return SetUp();

            petSelector.Id = -1;

            gameEvents.petEnable = true;
            gameEvents.Fight();

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPetTest()
        {
            yield return SetUp();

            petSelector.Id = 0;
            gameEvents.petEnable = true;
            gameEvents.Fight();

            yield return new WaitForSeconds(2);

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual("Reddy", pet.petName);

            Object.Destroy(pet.gameObject);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPetVanishedTest()
        {
            yield return SetUp();

            petSelector.Id = 0;

            gameEvents.petEnable = true;
            gameEvents.Fight();
            gameEvents.Explore();

            yield return new WaitForSeconds(1);

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPetNexFightTest()
        {
            yield return SetUp();

            petSelector.Id = 0;

            gameEvents.petEnable = true;
            gameEvents.Fight();
            gameEvents.Explore();
            gameEvents.Fight();

            yield return new WaitForSeconds(2);

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual("Reddy", pet.petName);

            Object.Destroy(pet.gameObject);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectThanNotSelectPetNexFightTest()
        {
            yield return SetUp();

            petSelector.Id = 0;

            gameEvents.petEnable = true;
            gameEvents.Fight();
            gameEvents.Explore();

            yield return new WaitForSeconds(1);

            petSelector.Id = -1;

            gameEvents.Fight();

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator NotValidIndexTest()
        {
            yield return SetUp();

            petSelector.Id = 5;

            gameEvents.petEnable = true;
            gameEvents.Fight();

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator NotAvailablePetSelectedTest()
        {
            yield return SetUp();

            gameEvents.petEnable = true;
            petSelector.Id = 2;

            gameEvents.Fight();

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator ValidPetSelectedButDisabledPetsTest()
        {
            yield return SetUp();

            gameEvents.petEnable = false;
            petSelector.Id = 0;

            gameEvents.Fight();

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator ValidPetSelectedButNextFightDisabledPetsTest()
        {
            yield return SetUp();

            gameEvents.petEnable = true;
            petSelector.Id = 0;

            gameEvents.Fight();
            gameEvents.Explore();

            yield return new WaitForSeconds(1);

            gameEvents.petEnable = false;
            gameEvents.Fight();

            Pet pet = Object.FindObjectOfType<Pet>();

            Assert.AreEqual(null, pet);

            TearDown();

            yield return null;
        }
    }
}
