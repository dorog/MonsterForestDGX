using UnityEngine;

public class ConnectorManager : MonoBehaviour
{
    public AbstractConnector[] connectors;

    void Start()
    {
        StartConnectors(connectors);
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
