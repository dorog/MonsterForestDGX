using System.Collections.Generic;
using UnityEngine;

public class SpellGuideDrawer : MonoBehaviour
{
    public GuideElement guideElement;
    public Transform Root;
    public Transform Helper;

    public GameObject guidePoint;

    private Transform startPoint;
    private List<Transform> guidePoints = new List<Transform>();

    public GuideDrawHelper guideDrawHelper;

    private GameObject guideHelper;

    private IPressed pressInput;

    public KeyBindingManager keyBindingManager;
    public BattleManager battleManager;

    private void Start()
    {
        pressInput = keyBindingManager.drawHelperInput;
        pressInput.SubscribeToPressed(Pressed);
    }

    public void DrawGuide(List<SpellPatternPoint> spellPatternPoints, float width = 10, float scale = 0.0025f)
    {
        ClearGuide();

        for (int i = 0; i < spellPatternPoints.Count - 1; i++)
        {
            GuideElement guideElementInstance = Instantiate(guideElement, Root);
            guideElementInstance.Set(spellPatternPoints[i].Point * scale, spellPatternPoints[i + 1].Point * scale, width * scale);
            if (i == 0)
            {
                startPoint = guideElementInstance.transform;
            }
            else
            {
                guidePoints.Add(guideElementInstance.transform);
            }
        }

        //Last point
        GameObject lastGuidePoint = Instantiate(guidePoint, Root);
        lastGuidePoint.transform.localPosition = new Vector3(spellPatternPoints[spellPatternPoints.Count - 1].Point.x * scale, spellPatternPoints[spellPatternPoints.Count - 1].Point.y * scale, 0);
        guidePoints.Add(lastGuidePoint.transform);

        GuideDrawHelper guideDrawHelperGO = Instantiate(guideDrawHelper, Helper);
        guideDrawHelperGO.Init(guidePoints, startPoint);

        guideHelper = guideDrawHelperGO.gameObject;

        pressInput.Activate();
    }

    public void ClearGuide()
    {
        foreach (Transform child in Root.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in Helper.transform)
        {
            Destroy(child.gameObject);
        }

        guideHelper = null;

        guidePoints.Clear();

        pressInput.Deactivate();
    }

    public void Pressed()
    {
        Debug.Log("gimme helpör");
        if (guideHelper != null)
        {
            Helper.gameObject.SetActive(!Helper.gameObject.activeSelf);
        }
    }
}
