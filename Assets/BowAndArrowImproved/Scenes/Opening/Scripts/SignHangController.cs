using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SignHangController : MonoBehaviour
{
    private void Start()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform
            .DOLocalRotate(currentRotation + new Vector3(30, 0f, 0f), 1.5f)
            .From(currentRotation + new Vector3(-30f, 0f, 0f))
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
