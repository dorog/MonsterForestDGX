using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PetUnlockingTests
    {
        private MockDataIO mockDataIO;
        private MockPetUnlockSpot[] unlockSpots;
        private GameObject core;

        private IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/PetUnlocking/PetUnlocking.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            mockDataIO = core.GetComponentInChildren<MockDataIO>();
            unlockSpots = core.GetComponentsInChildren<MockPetUnlockSpot>();

            yield return null;
        }

        private void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator UnlockFirstOneTest()
        {
            yield return SetUp();

            unlockSpots[0].UnlockPet();

            Debug.Log("Loc: " + mockDataIO.LocalGameData.availablePets.Length);

            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[3]);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockMiddleOneTest()
        {
            yield return SetUp();

            unlockSpots[1].UnlockPet();

            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[3]);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockLastOneTest()
        {
            yield return SetUp();

            unlockSpots[3].UnlockPet();

            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[3]);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockMoreTest()
        {
            yield return SetUp();

            unlockSpots[1].UnlockPet();
            unlockSpots[3].UnlockPet();

            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[3]);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockTheSameTwiceTest()
        {
            yield return SetUp();

            unlockSpots[1].UnlockPet();
            unlockSpots[1].UnlockPet();

            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[3]);

            TearDown();

            yield return null;
        }
    }
}
