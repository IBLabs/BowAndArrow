using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollDemo : MonoBehaviour
{
    [SerializeField] private ScrollController scrollController;

    private int _times = 1;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (scrollController.isShown)
            {
                scrollController.Hide(true);
            }
            else
            {
                _times += 1;
                scrollController.SetText($"Show times: {_times}");
                scrollController.Show(true);
            }
        }
    }
}
