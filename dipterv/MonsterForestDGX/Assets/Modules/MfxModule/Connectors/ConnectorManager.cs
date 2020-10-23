using UnityEngine;

public class ConnectorManager : MonoBehaviour
{
    public AbstractConnector[] patternConnectors;
    public AbstractConnector[] experienceConnectors;
    public AbstractConnector[] petConnectors;
    public AbstractConnector[] teleportConnectors;

    public void Start()
    {
        StartConnectors(patternConnectors);
        StartConnectors(experienceConnectors);
        StartConnectors(petConnectors);
        StartConnectors(teleportConnectors);
    }

    private void StartConnectors(AbstractConnector[] connectors)
    {
        foreach (var connector in connectors)
        {
            connector.Setup();
        }

        foreach (var connector in connectors)
        {
            connector.Load();
        }
    }
}
