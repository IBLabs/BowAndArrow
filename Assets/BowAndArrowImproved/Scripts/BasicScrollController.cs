using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicScrollController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _isScrollOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_isScrollOpen)
            {
                animator.SetTrigger("closeScroll");
                _isScrollOpen = false;
            }
            else
            {
                animator.SetTrigger("openScroll");
                _isScrollOpen = true;
            }
        }
    }
}
