using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugFPS : MonoBehaviour
{
    public int refreshRate = 10;
        
    [SerializeField] private TextMeshProUGUI fpsText;

    private int _frameCounter;
    private float _totalTime;
    
    void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        _frameCounter = 0;
        _totalTime = 0;
    }

    void Update()
    {
        if (_frameCounter == refreshRate)
        {
            float averageFps = (1.0f / (_totalTime / refreshRate));
            fpsText.text = averageFps.ToString("F1");
            _frameCounter = 0;
            _totalTime = 0;
        }
        else
        {
            _totalTime += Time.deltaTime;
            _frameCounter++;
        }
    }
}
