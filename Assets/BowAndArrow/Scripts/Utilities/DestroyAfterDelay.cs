using System;
using System.Collections;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 3f;

    private void OnEnable()
    {
        StartCoroutine(DestroyCoroutine(delay));
    }

    private IEnumerator DestroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Destroy(gameObject);
    }
}