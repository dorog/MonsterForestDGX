using UnityEngine;

public class ConnectorManager : MonoBehaviour
{
    public AbstractConnector[] patternConnectors;

    void Start()
    {
        StartConnectors(patternConnectors);
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
