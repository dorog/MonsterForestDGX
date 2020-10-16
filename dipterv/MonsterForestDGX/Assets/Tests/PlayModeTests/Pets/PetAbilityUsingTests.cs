using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PetAbilityUsingTests
    {
        private Pet[] pets;
        private MockFighter[] fighters;
        private Health[] healths;
        private MockResetable resetable;

        private GameObject handlers;

        private GameObject core;

        private IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Pets/PetAbilityUsing/PetAbilityUsing.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            pets = core.GetComponentsInChildren<Pet>();
            fighters = core.GetComponentsInChildren<MockFighter>();
            healths = core.GetComponentsInChildren<Health>();
            resetable = core.GetComponentInChildren<MockResetable>();

            handlers = GameObject.Find("Handlers");

            yield return null;
        }

        private void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator ResetAbilityTest()
        {
            yield return SetUp();

            pets[0].Init(handlers);

            resetable.ResetCall();

            Assert.AreEqual(true, resetable.Reseted);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator AttackAbilityTest()
        {
            yield return SetUp();

            pets[1].Init(handlers);
            fighters[0].Fight();

            yield return new WaitForSeconds(5);

            Assert.AreEqual(97, healths[1].currentHp);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator AttackAbilitySwitchingTest()
        {
            yield return SetUp();

            pets[1].Init(handlers);
            fighters[0].Fight();

            yield return new WaitForSeconds(5);

            pets[1].updateAbilities[0].Deactivate();

            Assert.AreEqual(97, healths[1].currentHp);

            yield return new WaitForSeconds(3);

            Assert.AreEqual(97, healths[1].currentHp);

            pets[1].updateAbilities[0].Activate();

            yield return new WaitForSeconds(3);

            Assert.AreEqual(95, healths[1].currentHp);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator HealAbilityTest()
        {
            yield return SetUp();

            healths[0].currentHp -= 50;
            pets[2].Init(handlers);

            yield return new WaitForSeconds(5);

            Assert.AreEqual(53, healths[0].currentHp);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator HealAbilitySwitchingTest()
        {
            yield return SetUp();

            healths[0].currentHp -= 50;
            pets[2].Init(handlers);

            yield return new WaitForSeconds(5);

            pets[2].updateAbilities[0].Deactivate();

            yield return new WaitForSeconds(3);

            Assert.AreEqual(53, healths[0].currentHp);

            pets[2].updateAbilities[0].Activate();

            yield return new WaitForSeconds(3);

            Assert.AreEqual(55, healths[0].currentHp);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator HealAndAttackAbilityAtOnceTest()
        {
            yield return SetUp();

            healths[0].currentHp -= 50;
            pets[3].Init(handlers);
            fighters[0].Fight();

            yield return new WaitForSeconds(5);

            Assert.AreEqual(53, healths[0].currentHp);
            Assert.AreEqual(97, healths[1].currentHp);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator AllAbilityAtOnceTest()
        {
            yield return SetUp();

            healths[0].currentHp -= 50;
            pets[4].Init(handlers);
            fighters[0].Fight();

            yield return new WaitForSeconds(5);

            Assert.AreEqual(53, healths[0].currentHp);
            Assert.AreEqual(97, healths[1].currentHp);

            resetable.ResetCall();

            Assert.AreEqual(true, resetable.Reseted);

            TearDown();

            yield return null;
        }
    }
}
