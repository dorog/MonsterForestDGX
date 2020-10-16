using UnityEngine;

public class ConnectorManager : MonoBehaviour
{
    public AbstractConnector[] patternConnectors;
    public AbstractConnector[] experienceConnectors;
    public AbstractConnector[] petConnectors;

    public void Start()
    {
        StartConnectors(patternConnectors);
        StartConnectors(experienceConnectors);
        StartConnectors(petConnectors);
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
