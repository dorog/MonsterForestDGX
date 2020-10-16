using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextCooldownShower : MonoBehaviour, ICooldownShower
{
    public Text coolDown;
    private bool resetedCd = false;

    public void ResetCoolDown()
    {
        resetedCd = true;
    }

    public void SetUpCoolDown(float cd)
    {
        if (!resetedCd)
        {
            coolDown.text = cd.ToString();
            StartCoroutine(Countdown(cd));
        }
        else
        {
            resetedCd = false;
        }
    }

    private IEnumerator Countdown(float cd)
    {
        float duration = cd;
        while (duration > 0 && !resetedCd)
        {
            coolDown.text = duration.ToString();
            duration -= Time.deltaTime;

            yield return null;
        }

        coolDown.text = "Ready";

        resetedCd = false;
    }
}
