using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Mfx
{
    public class TraningTests
    {
        private MockButtonInput traningButtonInput;

        private CharacterController player;

        private GameObject core;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            GameObject coreGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tests/Mfx/Traning/Traning.prefab");
            core = Object.Instantiate(coreGO);

            traningButtonInput = core.GetComponentInChildren<MockButtonInput>();
            player = core.GetComponentInChildren<CharacterController>();

            yield return null;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(core);
        }

        [UnityTest]
        public IEnumerator StartTraningTest()
        {
            TrainingCampUI trainingCampUI = core.GetComponentInChildren<TrainingCampUI>();
            Assert.AreNotEqual(null, trainingCampUI);

            player.transform.position += new Vector3(0, 0, 5);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, trainingCampUI.root.activeSelf);

            Button startButton = core.GetComponentInChildren<Button>();
            startButton.onClick.Invoke();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(true, trainingCampUI.root.activeSelf);

            Toggle[] toggles = core.GetComponentsInChildren<Toggle>();
            Assert.AreEqual(2, toggles.Length);

            Dropdown dropdown = core.GetComponentInChildren<Dropdown>();
            Assert.AreNotEqual(null, dropdown);
            Assert.AreEqual("No", dropdown.options[0].text);
            Assert.AreEqual("Air", dropdown.options[1].text);
            Assert.AreEqual("Earth", dropdown.options[2].text);
            Assert.AreEqual("Water", dropdown.options[3].text);

            Slider cooldownSlider = core.GetComponentInChildren<Slider>();
            Assert.AreNotEqual(null, cooldownSlider);

            yield return null;
        }

        [UnityTest]
        public IEnumerator GuideTest()
        {
            TrainingCampUI trainingCampUI = core.GetComponentInChildren<TrainingCampUI>();
            Assert.AreNotEqual(null, trainingCampUI);

            player.transform.position += new Vector3(0, 0, 5);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(false, trainingCampUI.root.activeSelf);

            Button startButton = core.GetComponentInChildren<Button>();
            startButton.onClick.Invoke();

            yield return new WaitForSeconds(1);

            Dropdown dropdown = core.GetComponentInChildren<Dropdown>();
            dropdown.value = 1;

            yield return new WaitForSeconds(1);

            SpellGuideDrawer spellGuideDrawer = core.GetComponentInChildren<SpellGuideDrawer>();
            Assert.AreEqual(6, spellGuideDrawer.Root.transform.childCount);
            Assert.AreEqual(1, spellGuideDrawer.Helper.transform.childCount);

            yield return new WaitForSeconds(1);

            traningButtonInput.Pressed();

            yield return new WaitForSeconds(1);

            Assert.AreEqual(true, spellGuideDrawer.Helper.gameObject.activeSelf);
        }
    }
}
