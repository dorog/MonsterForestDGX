using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Mfx
{
    public class PetCollectingTest
    {
        private MockButtonInput mockButtonInput;
        private MockDataIO mockDataIO;
        private Pet pet;

        private CharacterController player;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Mfx/PetCollecting/PetCollecting.prefab");
            core = Object.Instantiate(coreGO);

            mockButtonInput = core.GetComponentInChildren<MockButtonInput>();
            mockDataIO = core.GetComponentInChildren<MockDataIO>();
            player = core.GetComponentInChildren<CharacterController>();

            yield return new WaitForSeconds(1);

            pet = core.GetComponentInChildren<Pet>();

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
            SphereCollider availableSign = core.GetComponentInChildren<SphereCollider>();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(null, availableSign);
            Assert.AreNotEqual(null, pet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TriggerTest()
        {
            player.transform.position += new Vector3(0, 0, 8);

            yield return new WaitForSeconds(1);

            SphereCollider availableSign = core.GetComponentInChildren<SphereCollider>();

            Assert.AreNotEqual(null, availableSign);
            Assert.AreNotEqual(null, pet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TriggerOutTest()
        {
            player.transform.position += new Vector3(0, 0, 8);

            yield return new WaitForSeconds(1);

            player.transform.position += new Vector3(0, 0, -8);

            yield return new WaitForSeconds(1);

            SphereCollider availableSign = core.GetComponentInChildren<SphereCollider>();

            Assert.AreEqual(null, availableSign);
            Assert.AreNotEqual(null, pet);

            yield return null;
        }

        [UnityTest]
        public IEnumerator CollectInRangeTest()
        {
            player.transform.position += new Vector3(0, 0, 8);

            yield return new WaitForSeconds(1);

            mockButtonInput.Pressed();

            yield return new WaitForSeconds(1);

            SphereCollider availableSign = core.GetComponentInChildren<SphereCollider>();

            Assert.AreEqual(null, availableSign);
            Assert.AreEqual(false, pet.gameObject.activeSelf);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[0]);

            yield return null;
        }

        [UnityTest]
        public IEnumerator CollectOutOfRangeTest()
        {
            mockButtonInput.Pressed();

            yield return new WaitForSeconds(1);

            SphereCollider availableSign = core.GetComponentInChildren<SphereCollider>();

            Assert.AreEqual(null, availableSign);
            Assert.AreEqual(true, pet.gameObject.activeSelf);

            yield return null;
        }
    }
}
