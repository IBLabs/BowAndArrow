using Game.Scenes.Common.ScriptableObjects;
using UnityEngine;

public class ArrowConfigurationManager : MonoBehaviour
{
    public ArrowConfiguration arrowConfig;
    [SerializeField] private SerializableDictionary<ArrowType, ArrowConfiguration> arrowsConfigurations;

    public void OnBalloonDeath(GameObject balloonGameObject, int score, bool killedByPlayer)
    {
        if (!killedByPlayer || !balloonGameObject.TryGetComponent<Ballon>(out var balloon)) return;
        SwitchArrow(balloon.arrowType);
    }

    public void SwitchArrow(ArrowType arrowType)
    {
        arrowConfig = arrowsConfigurations.GetByKey(arrowType);
    }
}