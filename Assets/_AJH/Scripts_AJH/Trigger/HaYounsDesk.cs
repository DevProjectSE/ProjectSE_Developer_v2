using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaYounsDesk : Stage3Item
{
    protected override void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (Stage3StateManager.Instance.stage3Step == 3)
            {
                Destroy(gameObject);
                Stage3StateManager.Instance.stage3Step = 4;
                Stage3StateManager.Instance.isTouchDoor = true;

            }
        }

    }
}
