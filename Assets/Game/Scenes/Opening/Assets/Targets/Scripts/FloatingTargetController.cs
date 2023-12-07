using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FloatingTargetController : MonoBehaviour
{
    [SerializeField] private AudioClip hitSoundEffect;

    [SerializeField] private bool shouldAnimate = true;

    private Rigidbody _rigidbody;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private Sequence _sequence;

    void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        _rigidbody = GetComponent<Rigidbody>();

        StartAnimationIfNeeded();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (LayerMask.NameToLayer("Arrow") == other.gameObject.layer)
        {
            if (_sequence != null) _sequence.Kill();

            _rigidbody.isKinematic = false;

            _rigidbody.AddForceAtPosition(
                other.transform.forward.normalized * 5f,
                other.transform.position, ForceMode.Impulse
            );

            AudioSource.PlayClipAtPoint(hitSoundEffect, Camera.main.transform.position);

            StartCoroutine(RespawnCoroutine());
        }
    }

    private void StartAnimationIfNeeded()
    {
        if (!shouldAnimate) return;

        _sequence = DOTween.Sequence();

        float randomDuration = UnityEngine.Random.Range(1f, 2f);
        float randomOffset = UnityEngine.Random.Range(0f, 0.2f);

        _sequence.Append(
            transform.DOMoveY(transform.position.y - randomOffset, randomDuration).SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
        );
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(5f);

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _rigidbody.isKinematic = true;

        transform.position = _initialPosition;
        transform.rotation = _initialRotation;

        StartAnimationIfNeeded();
    }
}