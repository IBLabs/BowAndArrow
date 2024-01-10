using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseStateHandler : MonoBehaviour
{
    [SerializeField] private ScrollController scrollController;
    
    public void OnGameManagerStateChanged(GameManager.State newState)
    {
        if (newState == GameManager.State.Lose)
        {
            OnLose();
        }
    }

    private void OnLose()
    {
        StartCoroutine(LoseCoroutine());
    }
    
    private IEnumerator LoseCoroutine()
    {
        yield return new WaitForSeconds(2f);
        
        scrollController.SetText("We've lost the battle...\nBut not the war!");
        scrollController.Show(true);
    }
}
