using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PetModule
{
    public class PetAbilityUsingTests
    {
        private Pet[] pets;
        private MockFighter[] fighters;
        private Health[] healths;
        private MockResetable resetable;

        private GameObject handlers;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Pets/PetAbilityUsing/PetAbilityUsing.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            pets = core.GetComponentsInChildren<Pet>();
            fighters = core.GetComponentsInChildren<MockFighter>();
            healths = core.GetComponentsInChildren<Health>();
            resetable = core.GetComponentInChildren<MockResetable>();

            handlers = GameObject.Find("Handlers");

            healths[0].InitHealth();
            healths[1].InitHealth();

            yield return null;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator ResetAbilityTest()
        {
            pets[0].Init(handlers);

            resetable.ResetCall();

            Assert.AreEqual(true, resetable.Reseted);

            yield return null;
        }

        [UnityTest]
        public IEnumerator AttackAbilityTest()
        {
            pets[1].Init(handlers);
            fighters[0].Fight();

            yield return new WaitForSeconds(5);

            Assert.AreEqual(97, healths[1].currentHp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator AttackAbilitySwitchingTest()
        {
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
        }

        [UnityTest]
        public IEnumerator HealAbilityTest()
        {
            healths[0].currentHp -= 50;
            pets[2].Init(handlers);

            yield return new WaitForSeconds(5);

            Assert.AreEqual(53, healths[0].currentHp);
        }

        [UnityTest]
        public IEnumerator HealAbilitySwitchingTest()
        {
            healths[0].currentHp -= 50;
            pets[2].Init(handlers);

            yield return new WaitForSeconds(5);

            pets[2].updateAbilities[0].Deactivate();

            yield return new WaitForSeconds(3);

            Assert.AreEqual(53, healths[0].currentHp);

            pets[2].updateAbilities[0].Activate();

            yield return new WaitForSeconds(3);

            Assert.AreEqual(55, healths[0].currentHp);
        }

        [UnityTest]
        public IEnumerator HealAndAttackAbilityAtOnceTest()
        {
            healths[0].currentHp -= 50;
            pets[3].Init(handlers);
            fighters[0].Fight();

            yield return new WaitForSeconds(5);

            Assert.AreEqual(53, healths[0].currentHp);
            Assert.AreEqual(97, healths[1].currentHp);
        }

        [UnityTest]
        public IEnumerator AllAbilityAtOnceTest()
        {
            healths[0].currentHp -= 50;
            pets[4].Init(handlers);
            fighters[0].Fight();

            yield return new WaitForSeconds(5);

            Assert.AreEqual(53, healths[0].currentHp);
            Assert.AreEqual(97, healths[1].currentHp);

            resetable.ResetCall();

            Assert.AreEqual(true, resetable.Reseted);
        }
    }
}
