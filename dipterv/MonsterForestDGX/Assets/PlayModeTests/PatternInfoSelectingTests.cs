using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PatternInfoSelectingTests
    {
        private MfxPatternInfoUI patternInfoUI = null;
        private MfxPattern[] patterns = null;

        private GameObject core;

        private IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/PatternInfoSelecting/PatternInfoSelecting.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            patterns = core.GetComponentsInChildren<MfxPattern>();

            yield return null;
        }

        private void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator NoSelectedPatternTest()
        {
            yield return SetUp();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();
            Assert.AreEqual(null, patternInfoUI);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternWithBuyOptionTest()
        {
            yield return SetUp();

            patterns[0].ShowInfo();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();
            Assert.AreNotEqual(null, patternInfoUI);
            Assert.AreEqual(true, patternInfoUI.gameObject.activeSelf);
            Assert.AreEqual(true, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(false, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternWithUpdateOptionTest()
        {
            yield return SetUp();

            patterns[1].ShowInfo();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();
            Assert.AreNotEqual(null, patternInfoUI);
            Assert.AreEqual(true, patternInfoUI.gameObject.activeSelf);
            Assert.AreEqual(false, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(true, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternTwiceTest()
        {
            yield return SetUp();

            patterns[0].ShowInfo();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();

            patterns[0].ShowInfo();

            Assert.AreNotEqual(null, patternInfoUI);
            Assert.AreEqual(false, patternInfoUI.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectAndUpdatePatternWithBuyOptionTest()
        {
            yield return SetUp();

            patterns[0].ShowInfo();
            patterns[0].Increase();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();

            Assert.AreEqual(false, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(true, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternWithBuyOptionButUpdateOtherPatternTest()
        {
            yield return SetUp();

            patterns[0].ShowInfo();
            patterns[1].Increase();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();


            Assert.AreEqual(true, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(false, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectPatternWithUpdateOptionButUpdateOtherPatternTest()
        {
            yield return SetUp();

            patterns[1].ShowInfo();
            patterns[0].Increase();

            patternInfoUI = core.GetComponentInChildren<MfxPatternInfoUI>();

            Assert.AreEqual(false, patternInfoUI.patternBuyUI.gameObject.activeSelf);
            Assert.AreEqual(true, patternInfoUI.patternUpdateUI.gameObject.activeSelf);

            TearDown();

            yield return null;
        }
    }
}
