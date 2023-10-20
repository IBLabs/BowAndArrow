using UnityEngine;
using UnityEngine.Events;

public class WaveStartHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent startWave;
    [SerializeField] private PreWaveBallon shotToStartObject;

    private void Awake()
    {
        shotToStartObject.onPop.AddListener(OnShotToStartObjectPopped);
    }

    private void OnDisable()
    {
        shotToStartObject.onPop.RemoveListener(OnShotToStartObjectPopped);
    }

    public void ShowShotToStartObject()
    {
        shotToStartObject.gameObject.SetActive(true);
    }

    public void OnShotToStartObjectPopped()
    {
        shotToStartObject.gameObject.SetActive(false);
        startWave?.Invoke();
    }
}
