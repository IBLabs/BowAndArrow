using Game.Scenes.Common.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowConfigurationManager : MonoBehaviour
{
    public ArrowConfiguration arrowConfig;
    [SerializeField] private ArrowDataDictionary arrowsConfigurations;

    public void OnBalloonDeath(GameObject balloonGameObject, int score, bool killedByPlayer)
    {
        if (!killedByPlayer || !balloonGameObject.TryGetComponent<Ballon>(out var balloon)) return;
        SwitchArrow(balloon.arrowType);
    }

    public void SwitchArrow(ArrowType arrowType)
    {
        arrowConfig = arrowsConfigurations.GetConfigurationByArrowType(arrowType);
    }

    public void RandomSwitchArrow()
    {
        ArrowType[] arrowTypes = (ArrowType[])System.Enum.GetValues(typeof(ArrowType));
        ArrowType randomArrowType = arrowTypes[Random.Range(0, arrowTypes.Length)];

        SwitchArrow(randomArrowType);
    }
}