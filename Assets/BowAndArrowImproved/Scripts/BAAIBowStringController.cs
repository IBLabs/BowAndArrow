using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class BAAIBowStringController : MonoBehaviour
{
    public UnityEvent OnBowPulled;
    public UnityEvent<float> OnBowReleased;

    [SerializeField] private Transform midPointGrabObject;
    [SerializeField] private Transform midPointVisualObject;
    [SerializeField] private Transform midPointParent;
    [SerializeField] private BAAIBowString bowString;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float stringLimit = .3f;
    [SerializeField] private float stringSoundThreshold = 0.001f;

    private XRGrabInteractable _grabInteractable;
    private Transform _interactorTransform;

    private float _strength, _previousStrength;

    private void Awake()
    {
        _grabInteractable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        _grabInteractable.selectEntered.AddListener(PrepareBowString);
        _grabInteractable.selectExited.AddListener(ResetBowString);
    }

    private void Update()
    {
        if (_interactorTransform != null)
        {
            Vector3 midPointLocalPosition = midPointParent.InverseTransformPoint(midPointGrabObject.position);
            float midPointLocalPositionZAbs = Mathf.Abs(midPointLocalPosition.z);

            _previousStrength = _strength;

            if (midPointLocalPosition.z < 0 && midPointLocalPositionZAbs < stringLimit)
            {
                if (!audioSource.isPlaying && _strength <= 0.01f)
                {
                    audioSource.Play();
                }
                
                _strength = RemapValue(midPointLocalPositionZAbs, 0f, stringLimit, 0f, 1f);
                midPointVisualObject.localPosition = new Vector3(0f, 0f, midPointLocalPosition.z);
            }
            else if (midPointLocalPosition.z < 0 && midPointLocalPositionZAbs >= stringLimit)
            {
                _strength = 1f;
                midPointVisualObject.localPosition = new Vector3(0f, 0f, -stringLimit);
                
                // we reaced limit, stop playing
                audioSource.Pause();
            }
            else if (midPointLocalPosition.z >= 0)
            {
                _strength = 0f;
                midPointVisualObject.localPosition = Vector3.zero;

                // reset audio source
                audioSource.pitch = 1;
                audioSource.Stop();
            }
            
            bowString.UpdateString(midPointGrabObject.transform.position);
            
            PlayStringSound();
        }
    }

    private void PlayStringSound()
    {
        if (Mathf.Abs(_strength - _previousStrength) > stringSoundThreshold)
        {
            if (_strength < _previousStrength)
            {
                audioSource.pitch = -1;
            }
            else
            {
                audioSource.pitch = 1;
            }
            
            audioSource.UnPause();
        }
        else
        {
            audioSource.Pause();
        }
    }

    private float RemapValue(float value, float sourceMin, float sourceMax, float targetMin, float targetMax)
    {
        return (value - sourceMin) / (sourceMax - sourceMin) * (targetMax - targetMin) + targetMin;
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        _interactorTransform = arg0.interactorObject.transform;
        
        OnBowPulled?.Invoke();
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        OnBowReleased?.Invoke(_strength);
        
        _strength = 0;
        _previousStrength = 0;
        audioSource.pitch = 1;
        audioSource.Stop();
        
        _interactorTransform = null;
        
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;

        bowString.UpdateString(null);
    }
}
