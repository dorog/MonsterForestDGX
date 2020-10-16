using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PatternShoppingTests
    {
        private PatternShopComponent patternShopComponent;
        private ExperienceManager experienceManager;

        private GameObject core;

        private IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/ExpChanging/ExpChanging.prefab");
            core = Object.Instantiate(coreGO);

            patternShopComponent = core.GetComponentInChildren<PatternShopComponent>();
            experienceManager = core.GetComponentInChildren<ExperienceManager>();

            yield return null;
        }

        private void TearDown()
        {
            Object.Destroy(core);
        }


        [UnityTest]
        public IEnumerator NoBuyableSpellTest()
        {
            yield return SetUp();

            experienceManager.AddExp(50);
            experienceManager.Save();

            UpdateablePatternUI[] patternUIs = patternShopComponent.content.GetComponentsInChildren<UpdateablePatternUI>();

            Assert.AreEqual(3, patternUIs.Length);
            Assert.AreEqual("50 EXP", patternShopComponent.quantityText.text);
            Assert.AreEqual(false, patternUIs[0].button.interactable);
            Assert.AreEqual(false, patternUIs[1].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator OneBuyableSpellTest()
        {
            yield return SetUp();

            experienceManager.AddExp(110);
            experienceManager.Save();

            UpdateablePatternUI[] patternUIs = patternShopComponent.content.GetComponentsInChildren<UpdateablePatternUI>();

            Assert.AreEqual(3, patternUIs.Length);
            Assert.AreEqual("110 EXP", patternShopComponent.quantityText.text);
            Assert.AreEqual(true, patternUIs[0].button.interactable);
            Assert.AreEqual(false, patternUIs[1].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator MoreBuyableSpellTest()
        {
            yield return SetUp();

            experienceManager.AddExp(5000);
            experienceManager.Save();

            UpdateablePatternUI[] patternUIs = patternShopComponent.content.GetComponentsInChildren<UpdateablePatternUI>();

            Assert.AreEqual(3, patternUIs.Length);
            Assert.AreEqual("5000 EXP", patternShopComponent.quantityText.text);
            Assert.AreEqual(true, patternUIs[0].button.interactable);
            Assert.AreEqual(true, patternUIs[1].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator BuyableThanNotBuyableSpellTest()
        {
            yield return SetUp();

            experienceManager.AddExp(200);
            experienceManager.Save();

            UpdateablePatternUI[] patternUIs = patternShopComponent.content.GetComponentsInChildren<UpdateablePatternUI>();

            Assert.AreEqual(3, patternUIs.Length);
            Assert.AreEqual("200 EXP", patternShopComponent.quantityText.text);
            Assert.AreEqual(true, patternUIs[0].button.interactable);
            Assert.AreEqual(false, patternUIs[1].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.gameObject.activeSelf);

            experienceManager.RemoveExp(200);
            experienceManager.Save();

            Assert.AreEqual(3, patternUIs.Length);
            Assert.AreEqual("0 EXP", patternShopComponent.quantityText.text);
            Assert.AreEqual(false, patternUIs[0].button.interactable);
            Assert.AreEqual(false, patternUIs[1].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator BuyOneSpellTest()
        {
            yield return SetUp();

            experienceManager.AddExp(500);
            experienceManager.Save();

            UpdateablePatternUI[] patternUIs = patternShopComponent.content.GetComponentsInChildren<UpdateablePatternUI>();

            Assert.AreEqual(3, patternUIs.Length);
            Assert.AreEqual(true, patternUIs[0].button.interactable);
            Assert.AreEqual(true, patternUIs[1].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.gameObject.activeSelf);

            patternUIs[0].button.onClick.Invoke();

            Assert.AreEqual(3, patternUIs.Length);
            Assert.AreEqual(false, patternUIs[0].button.interactable);
            Assert.AreEqual(false, patternUIs[1].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.interactable);
            Assert.AreEqual(false, patternUIs[2].button.gameObject.activeSelf);

            TearDown();

            yield return null;
        }
    }
}
