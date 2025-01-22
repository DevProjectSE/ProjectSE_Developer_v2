using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Trigger : MonoBehaviour
{
    public bool isTouchDoor = false; //1
    public bool isCheckTeachersdesk = false; //2
    public bool isCheckHaYunsDesk = false; //4

    public bool isDariyInInventory1 = false; //6
    public bool isDariyInInventory2 = false; //8
    public bool isDariyInInventory3 = false; //10
    public bool isDariyInInventory4 = false; //15

    public bool isSettingHaYunsObject = false; //12

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Stage3StateManager.Instance.stage3Step == 0 & isTouchDoor == false)
            {
                Stage3StateManager.Instance.stage3Step++;
                isTouchDoor = true;
            }
        }
        
    }

    public void TouchDoor()
    {
        print("Ãæµ¹2");
        if (Stage3StateManager.Instance.stage3Step == 0 & isTouchDoor ==false)
        {
            Stage3StateManager.Instance.stage3Step++;
        }
    }



}