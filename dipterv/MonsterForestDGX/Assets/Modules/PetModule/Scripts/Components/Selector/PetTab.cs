using System.Collections.Generic;
using UnityEngine;

public class PetTab : MonoBehaviour
{
    public GameObject root;
    public Transform parent;

    public PetUI petUI;
    private PetUI[] petUIs;

    public void SetUpUI(PetData[] petDatas, PetSelectorComponent petSelectorComponent, int defaultImage = -1)
    {
        if (petDatas == null || petDatas.Length == 0)
        {
            return;
        }
        else
        {
            petUIs = new PetUI[petDatas.Length];

            for (int i = 0; i < petDatas.Length; i++)
            {
                petUIs[i] = Instantiate(petUI, parent);
                petUIs[i].SetUI(i, petDatas[i], petSelectorComponent);
                if (i == defaultImage)
                {
                    petUIs[i].ChangePet();
                }
            }
        }
    }

    public void Refresh(List<PetDataDifference> petDataDifferences)
    {
        foreach(var petDataDifference in petDataDifferences)
        {
            petUIs[petDataDifference.Id].Refresh(petDataDifference.NewAvailability);
        }
    }

    public void ShowTab()
    {
        root.SetActive(true);
    }

    public void HideTab()
    {
        root.SetActive(false);
    }
}
