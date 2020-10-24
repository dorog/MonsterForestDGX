using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PetModule
{
    public class PetUnlockingTests
    {
        private MockDataIO mockDataIO;
        private MockPetUnlockSpot[] unlockSpots;
        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp2()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Pets/PetUnlocking/PetUnlocking.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            mockDataIO = core.GetComponentInChildren<MockDataIO>();
            unlockSpots = core.GetComponentsInChildren<MockPetUnlockSpot>();

            yield return null;
        }

        [TearDown]
        public void TearDown2()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator UnlockFirstOneTest()
        {
            unlockSpots[0].UnlockPet();

            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[3]);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockMiddleOneTest()
        {
            unlockSpots[1].UnlockPet();

            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[3]);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockLastOneTest()
        {
            unlockSpots[3].UnlockPet();

            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[3]);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockMoreTest()
        {
            unlockSpots[1].UnlockPet();
            unlockSpots[3].UnlockPet();

            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[3]);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockTheSameTwiceTest()
        {
            unlockSpots[1].UnlockPet();
            unlockSpots[1].UnlockPet();

            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[0]);
            Assert.AreEqual(true, mockDataIO.LocalGameData.availablePets[1]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[2]);
            Assert.AreEqual(false, mockDataIO.LocalGameData.availablePets[3]);

            yield return null;
        }
    }
}
