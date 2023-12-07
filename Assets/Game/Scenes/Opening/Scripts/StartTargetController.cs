using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTargetController : MonoBehaviour
{
    private const float Delay = 3f;

    public void OnTargetCollided(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            StartCoroutine(StartGameAfterDelay());
        }
    }

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(Delay);
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
