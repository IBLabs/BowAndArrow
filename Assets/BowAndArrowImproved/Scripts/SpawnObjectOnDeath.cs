using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;

    public void OnDeath(GameObject diedObject)
    {
        GameObject deathObject = Instantiate(spawnedObject, transform.position, Quaternion.LookRotation(transform.forward));

        Rigidbody deathObjectRb = deathObject.GetComponent<Rigidbody>();
        if (deathObjectRb != null)
        {
            deathObjectRb.AddForce((transform.up + (transform.forward * -1)) * Random.Range(1f, 3f), ForceMode.Impulse);
            deathObjectRb.AddTorque((deathObjectRb.transform.up).normalized * 10f * (Random.Range(0, 2) * 2 - 1));
            deathObjectRb.AddTorque((deathObjectRb.transform.forward).normalized * 10f * (Random.Range(0, 2) * 2 - 1));
        }
        
        Destroy(deathObject, 1f);
    }
}
