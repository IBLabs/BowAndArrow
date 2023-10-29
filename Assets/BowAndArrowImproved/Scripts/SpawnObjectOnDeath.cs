using UnityEngine;


public class SpawnObjectOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;

    [SerializeField] private ParticleSystem.MinMaxCurve explosionPower;
    [SerializeField] private ParticleSystem.MinMaxCurve spinPower;

    public void OnDeath(GameObject diedObject)
    {
        GameObject deathObject = Instantiate(spawnedObject, transform.position, Quaternion.LookRotation(transform.forward));

        Rigidbody deathObjectRb = deathObject.GetComponent<Rigidbody>();
        if (deathObjectRb != null)
        {
            int randDirection = (Random.Range(0, 2) * 2 - 1);
            deathObjectRb.AddForce((transform.up + (transform.forward * -1)) * explosionPower.Evaluate(Random.value), ForceMode.Impulse);
            deathObjectRb.AddTorque((deathObjectRb.transform.up).normalized * spinPower.Evaluate(Random.value) * randDirection);
            deathObjectRb.AddTorque((deathObjectRb.transform.forward).normalized * spinPower.Evaluate(Random.value) * randDirection);
        }
        
        Destroy(deathObject, 1f);
    }
}
