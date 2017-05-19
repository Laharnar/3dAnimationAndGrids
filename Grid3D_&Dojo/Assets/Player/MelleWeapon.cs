using UnityEngine;
using System.Collections;

public class MelleWeapon : MonoBehaviour {

    public string targetTag;

    bool hitting  = false;

    int targets = 0;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.tag == targetTag)
        {
            targets++;
        }
    }

    void OnTriggerStay(Collider trigger)
    {
        if (hitting && trigger.tag == targetTag)
            Destroy(trigger.gameObject);
    }

    void OnTriggerExit(Collider trigger)
    {
        if (trigger.tag == targetTag)
            targets--;
    }

    public void Attack()
    {
        hitting = true;
    }

    public void EndAttack()
    {
        hitting = false;
    }
}
