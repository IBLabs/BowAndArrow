using System;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnButton : MonoBehaviour
{
    [SerializeField] private List<MeshDestroy> destroyObjects;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (MeshDestroy meshDestroy in destroyObjects)
            {
                meshDestroy.DestroyMesh();
            }
        }
    }
}