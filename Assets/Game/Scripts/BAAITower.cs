using UnityEngine;

public class BAAITower : BAAIDamageable
{
    private void OnTriggerEnter(Collider other)
    {
        BAAIEnemy enemy = other.attachedRigidbody.gameObject.GetComponent<BAAIEnemy>();
        if (enemy != null)
        {
            enemy.StartAttacking(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BAAIEnemy enemy = other.attachedRigidbody.gameObject.GetComponent<BAAIEnemy>();
        if (enemy != null)
        {
            enemy.StopAttacking();
        }
    }
}