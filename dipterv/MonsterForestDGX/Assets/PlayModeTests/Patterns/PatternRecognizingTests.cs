using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PatternRecognizingTests
    {
        private MfxPattern[] patterns;
        private PatternRecognizerComponent patternRecognizer;

        private GameObject core;

        private IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/PatternRecognizing/PatternRecognizing.prefab");
            core = Object.Instantiate(coreGO);

            yield return new WaitForSeconds(2);

            patterns = core.GetComponentsInChildren<MfxPattern>();
            patternRecognizer = core.GetComponentInChildren<PatternRecognizerComponent>();

            yield return null;
        }

        private void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator GetUnavailablePatternTest()
        {
            yield return SetUp();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            SpellResult spellResult = patternRecognizer.GetSpell();

            Assert.AreEqual(null, spellResult);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetWithoutGuessTest()
        {
            yield return SetUp();

            SpellResult spellResult = patternRecognizer.GetSpell();

            Assert.AreEqual(null, spellResult);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetShowablePatternTest()
        {
            yield return SetUp();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            SpellResult spellResult = patternRecognizer.GetSpell();

            Assert.AreEqual(null, spellResult);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetAvailablePatternTest()
        {
            yield return SetUp();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(0, 0));

            SpellResult spellResult = patternRecognizer.GetSpell();

            Assert.AreEqual(2, spellResult.Index);
            Assert.AreEqual(1, spellResult.Coverage);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetAvailablePatternButCoverageNotEnoughTest()
        {
            yield return SetUp();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(100, 0));

            SpellResult spellResult = patternRecognizer.GetSpell();

            Assert.AreEqual(null, spellResult);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator BuyShowAblePatternAndGetItTest()
        {
            yield return SetUp();

            patterns[1].Increase();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            SpellResult spellResult = patternRecognizer.GetSpell();

            Assert.AreEqual(1, spellResult.Index);
            Assert.AreEqual(1, spellResult.Coverage);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockUnavailablePatternAndGetItTest()
        {
            yield return SetUp();

            patterns[0].Increase();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            SpellResult spellResult = patternRecognizer.GetSpell();

            Assert.AreEqual(null, spellResult);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator UnlockThanBuyUnavailablePatternAndGetItTest()
        {
            yield return SetUp();

            patterns[0].Increase();
            patterns[0].Increase();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, 0));

            SpellResult spellResult = patternRecognizer.GetSpell();

            Assert.AreEqual(0, spellResult.Index);
            Assert.AreEqual(1, spellResult.Coverage);

            TearDown();

            yield return null;
        }

        [UnityTest]
        public IEnumerator GetOneWithGuessesThanGetAgainWithoutGuess()
        {
            yield return SetUp();

            patternRecognizer.Guess(new Vector2(0, 0));
            patternRecognizer.Guess(new Vector2(100, 0));
            patternRecognizer.Guess(new Vector2(0, 100));
            patternRecognizer.Guess(new Vector2(-100, 0));
            patternRecognizer.Guess(new Vector2(0, -100));
            patternRecognizer.Guess(new Vector2(0, 0));

            patternRecognizer.GetSpell();
            patternRecognizer.ResetSpells();
            SpellResult spellResultWithoutGuess = patternRecognizer.GetSpell();

            Assert.AreEqual(null, spellResultWithoutGuess);

            TearDown();

            yield return null;
        }
    }
}
