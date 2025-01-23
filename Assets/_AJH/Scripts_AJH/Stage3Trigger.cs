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
                Stage3StateManager.Instance.stage3Step++;
                Stage3StateManager.Instance.isTouchDoor = true;
            }
  
        }            
    }



}