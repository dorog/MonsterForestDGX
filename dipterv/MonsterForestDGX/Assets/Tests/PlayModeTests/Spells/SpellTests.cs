using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SpellTests
    {
        private MockSpellTarget spellTarget;
        private GameObject downSpell;
        private GameObject middleSpell;
        private GameObject upSpell;

        private SpellMovement[] spellMovements;

        private GameObject core;

        private readonly string downSpellName = "ShadowMissileMegaDown";
        private readonly string middleSpellName = "ShadowMissileMegaMiddle";
        private readonly string upSpellName = "ShadowMissileMegaUp";

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Spells/Spelling.prefab");
            core = Object.Instantiate(coreGO);

            spellTarget = core.GetComponentInChildren<MockSpellTarget>();
            spellMovements = core.GetComponentsInChildren<SpellMovement>();

            downSpell = GameObject.Find(downSpellName);
            middleSpell = GameObject.Find(middleSpellName);
            upSpell = GameObject.Find(upSpellName);

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
            Assert.AreNotEqual(null, downSpell);
            Assert.AreNotEqual(null, middleSpell);
            Assert.AreNotEqual(null, upSpell);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TargetHitTest()
        {
            spellMovements[0].speed = 5;

            yield return new WaitForSeconds(4);

            Assert.AreEqual(10, spellTarget.damage);
            Assert.AreEqual(ElementType.TrueDamage.ToString(), spellTarget.elementTypeName);

            GameObject downSpellGO = GameObject.Find(downSpellName);
            Assert.AreEqual(null, downSpellGO);

            yield return null;
        }

        [UnityTest]
        public IEnumerator NoTargetHitTest()
        {
            spellMovements[1].speed = 5;

            yield return new WaitForSeconds(4);

            Assert.AreEqual(0, spellTarget.damage);
            Assert.AreEqual("", spellTarget.elementTypeName);

            GameObject middleSpellGO = GameObject.Find(middleSpellName);
            Assert.AreEqual(null, middleSpellGO);

            yield return null;
        }

        [UnityTest]
        public IEnumerator NoHitTest()
        {
            spellMovements[1].speed = 5;

            yield return new WaitForSeconds(4);

            Assert.AreEqual(0, spellTarget.damage);
            Assert.AreEqual("", spellTarget.elementTypeName);
            Assert.AreNotEqual(null, middleSpell);

            yield return null;
        }
    }
}
