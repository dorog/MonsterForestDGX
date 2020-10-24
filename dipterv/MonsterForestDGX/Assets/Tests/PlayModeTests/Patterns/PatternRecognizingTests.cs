using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PatternModule
{
    public class PatternRecognizingTests
    {
        private MfxPattern[] patterns;
        private PatternRecognizerComponent patternRecognizer;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Patterns/PatternRecognizing/PatternRecognizing.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            patterns = core.GetComponentsInChildren<MfxPattern>();
            patternRecognizer = core.GetComponentInChildren<PatternRecognizerComponent>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator GetUnavailablePatternTest()
        {
            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            RecognizingResult spellResult = patternRecognizer.GetResult();

            Assert.AreEqual(null, spellResult);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetWithoutGuessTest()
        {
            RecognizingResult spellResult = patternRecognizer.GetResult();

            Assert.AreEqual(null, spellResult);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetShowablePatternTest()
        {
            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            RecognizingResult spellResult = patternRecognizer.GetResult();

            Assert.AreEqual(null, spellResult);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetAvailablePatternTest()
        {
            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(0, 0));

            RecognizingResult spellResult = patternRecognizer.GetResult();

            Assert.AreEqual(2, spellResult.Index);
            Assert.AreEqual(1, spellResult.Coverage);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetAvailablePatternButCoverageNotEnoughTest()
        {
            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(100, 0));

            RecognizingResult spellResult = patternRecognizer.GetResult();

            Assert.AreEqual(null, spellResult);

            yield return null;
        }

        [UnityTest]
        public IEnumerator BuyShowAblePatternAndGetItTest()
        {
            patterns[1].Increase();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            RecognizingResult spellResult = patternRecognizer.GetResult();

            Assert.AreEqual(1, spellResult.Index);
            Assert.AreEqual(1, spellResult.Coverage);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockUnavailablePatternAndGetItTest()
        {
            patterns[0].Increase();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            RecognizingResult spellResult = patternRecognizer.GetResult();

            Assert.AreEqual(null, spellResult);

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockThanBuyUnavailablePatternAndGetItTest()
        {
            patterns[0].Increase();
            patterns[0].Increase();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            RecognizingResult spellResult = patternRecognizer.GetResult();

            Assert.AreEqual(0, spellResult.Index);
            Assert.AreEqual(1, spellResult.Coverage);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetOneWithGuessesThanGetAgainWithoutGuess()
        {
            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(0, 0));

            patternRecognizer.GetResult();
            patternRecognizer.ResetSpells();
            RecognizingResult spellResultWithoutGuess = patternRecognizer.GetResult();

            Assert.AreEqual(null, spellResultWithoutGuess);

            yield return null;
        }
    }
}
