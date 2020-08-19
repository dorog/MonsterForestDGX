using UnityEngine;

public class Teleport : MenuUI
{
    public GameObject lastLocation;
    private DataManager dataManager;

    public GameObject teleportsParent;

    private bool[] ports;

    private int lastTeleportUI = -1;

    public GameObject defaultLocation;

    public GameObject[] teleporPoints;

    public void Start()
    {
        dataManager = DataManager.GetInstance();

        int lastTeleportId = dataManager.GetLastLocation();

        GameObject last;
        if (lastTeleportId == -1)
        {
            last = defaultLocation;
        }
        else
        {
            last = teleporPoints[dataManager.GetLastLocation()];
        }

        ports = dataManager.GetTeleportsState();

        TeleportPlayer(last);

        SetupTeleportUI();
    }

    public void TeleportPlayer(GameObject port)
    {
        player.transform.position = port.transform.position;
        player.transform.rotation = port.transform.rotation;

        HideUI();
    }

    private void SetLastLocation(int id)
    {
        lastLocation = teleporPoints[id];
        dataManager.SavePortLocation(id);
    }

    public void ReachedTerritory(int id)
    {
        SetLocation(id);

        SetPortUI(id);
    }

    private void SetLocation(int id)
    {
        if (!ports[id])
        {
            ports[id] = true;
            dataManager.SaveTeleportUnlock(id);

            teleportsParent.transform.GetChild(id).gameObject.SetActive(true);
        }

        SetLastLocation(id);
    }

    private void SetPortUI(int id)
    {
        if (lastTeleportUI != -1 && ports[lastTeleportUI])
        {
            teleportsParent.transform.GetChild(lastTeleportUI).gameObject.SetActive(true);
        }
        teleportsParent.transform.GetChild(id).gameObject.SetActive(false);
        lastTeleportUI = id;
    }

    public void TeleportToLastPosition()
    {
        TeleportPlayer(lastLocation);
    }

    private void SetupTeleportUI()
    {
        for (int i = 0; i < ports.Length; i++)
        {
            teleportsParent.transform.GetChild(i).gameObject.SetActive(ports[i]);
        }
    }

    public void TeleportToDefaultLocation()
    {
        TeleportPlayer(defaultLocation);
    }
}
