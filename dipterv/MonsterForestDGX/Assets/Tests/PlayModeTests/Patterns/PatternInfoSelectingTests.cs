using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PatternModule
{
    public class PatternInfoSelectingTests
    {
        private MfxPatternInfoUI patternInfoUI = null;
        private MfxPattern[] patterns = null;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp2()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Patterns/PatternInfoSelecting/PatternInfoSelecting.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            patterns = core.GetComponentsInChildren<MfxPattern>();
        }

        [TearDown]
        public void TearDown2()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator NoSelectedPatternTest()
        {
            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();
            Assert.AreEqual(null, patternInfoUI);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternWithBuyOptionTest()
        {
            patterns[0].ShowInfo();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();
            Assert.AreNotEqual(null, patternInfoUI);
            Assert.AreEqual(true, patternInfoUI.gameObject.activeSelf);
            Assert.AreEqual(true, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(false, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternWithUpdateOptionTest()
        {
            patterns[1].ShowInfo();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();
            Assert.AreNotEqual(null, patternInfoUI);
            Assert.AreEqual(true, patternInfoUI.gameObject.activeSelf);
            Assert.AreEqual(false, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(true, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternTwiceTest()
        {
            patterns[0].ShowInfo();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();

            patterns[0].ShowInfo();

            Assert.AreNotEqual(null, patternInfoUI);
            Assert.AreEqual(false, patternInfoUI.gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectAndUpdatePatternWithBuyOptionTest()
        {
            patterns[0].ShowInfo();
            patterns[0].Increase();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();

            Assert.AreEqual(false, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(true, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternWithBuyOptionButUpdateOtherPatternTest()
        {
            patterns[0].ShowInfo();
            patterns[1].Increase();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();


            Assert.AreEqual(true, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(false, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternWithUpdateOptionButUpdateOtherPatternTest()
        {
            patterns[1].ShowInfo();
            patterns[0].Increase();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();

            Assert.AreEqual(false, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(true, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            yield return null;
        }
    }
}
