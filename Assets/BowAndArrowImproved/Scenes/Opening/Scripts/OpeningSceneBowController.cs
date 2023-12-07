using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OpeningSceneBowController : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private Rigidbody _rigidbody;
    private XRGrabInteractable _interactable;
    private Sequence _animationSeq;
    private MeshRenderer _meshRenderer;

    private Vector3 _initialPos;
    private Quaternion _initialRot;

    private Coroutine _respawnCoroutine;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _rigidbody = GetComponent<Rigidbody>(); 
        _rigidbody.isKinematic = true;

        _interactable = GetComponent<XRGrabInteractable>();
        _interactable.selectExited.AddListener(DidRelease);
        _interactable.selectEntered.AddListener(DidGrab);

        _initialPos = transform.position;
        _initialRot = transform.rotation;

        StartFloatAnimation();
    }

    private void DidGrab(SelectEnterEventArgs args)
    {
        if (_respawnCoroutine != null)
        {
            StopCoroutine(_respawnCoroutine);    
        }
        
        _animationSeq.Kill();
        
        _meshRenderer.materials[1].SetColor(EmissionColor, Color.black);
    }

    private void DidRelease(SelectExitEventArgs args)
    {
        _rigidbody.isKinematic = false;

        _respawnCoroutine = StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(2f);

        _rigidbody.isKinematic = true;

        transform.position = _initialPos;
        transform.rotation = _initialRot;
        
        StartFloatAnimation();
    }

    private void StartFloatAnimation()
    {
        _animationSeq = DOTween.Sequence();

        _animationSeq.Append(
            transform
                .DOMoveY(transform.position.y + .1f, 1f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo)
        );
        
        _animationSeq.Join(
            transform
                .DOLocalRotate(new Vector3(0f, 360f, 0f), 8f, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear)
        );

        _animationSeq.Join(
            _meshRenderer.materials[1]
                .DOColor(Color.white / 2, "_EmissionColor", 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutCubic)
        );
    }
}
