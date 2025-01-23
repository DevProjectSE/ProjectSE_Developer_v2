using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Trigger : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (Stage3StateManager.Instance.stage3Step == 0 && Stage3StateManager.Instance.isTouchDoor == false)
            {
                Destroy(gameObject);
                Stage3StateManager.Instance.stage3Step = 1;
                Stage3StateManager.Instance.isTouchDoor = true;
                
            }

            if (Stage3StateManager.Instance.stage3Step == 1 && Stage3StateManager.Instance.isTouchDoor == true)
            {
                Destroy(gameObject);
                Stage3StateManager.Instance.stage3Step = 2;
                Stage3StateManager.Instance.isCheckTeachersdesk = true;
                
            }

            if (Stage3StateManager.Instance.stage3Step == 1 && Stage3StateManager.Instance.isCheckTeachersdesk == true)
            {
                Stage3StateManager.Instance.stage3Step++;
                Stage3StateManager.Instance.isCheckHaYunsDesk = true;
                Destroy(gameObject);
            }



        }
    }
}

