using System;
using UnityEngine;

namespace BowAndArrowImproved.Scripts
{
    public class ArmorController : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Arrow"))
            {
                Destroy(gameObject);
            }
        }
    }
}