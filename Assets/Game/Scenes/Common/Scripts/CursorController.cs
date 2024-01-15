using System;
using UnityEngine;

namespace Game.Scenes.Common.Scripts
{
    public class CursorController : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}