using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Stage3Item
{
    protected override void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (Stage3StateManager.Instance.stage3Step == 12)
            {
                Destroy(gameObject);
                Stage3StateManager.Instance.stage3Step = 13;
                Stage3StateManager.Instance.isCheckRope = true;

            }
        }

    }
}
