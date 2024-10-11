using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    BoxCollider triggerBox;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //var enemy = other.gameObject.GetComponent<Enemy>();
        //if (enemy != null)
        //{
        //    enemy.health.HP -= damage;
        //    if(enemy.health.HP <= 0)
        //    {
        //        Destroy(enemy.gameObject);
        //    }
        //}
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }
    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }
}
