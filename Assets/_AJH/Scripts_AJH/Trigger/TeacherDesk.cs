using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherDesk : Stage3Item
{
    protected override void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (Stage3StateManager.Instance.stage3Step == 1 && Stage3StateManager.Instance.isTouchDoor == true)
            {
                Destroy(gameObject);
                Stage3StateManager.Instance.stage3Step = 2;
                Stage3StateManager.Instance.isCheckTeachersdesk = true;

            }
        }

    }
}
