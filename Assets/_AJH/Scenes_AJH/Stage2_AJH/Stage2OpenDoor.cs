using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2OpenDoor : MonoBehaviour
{
    public GameObject OpenDoor;
    public Stage2LastDoor stage2LastDoor;

    private void Start()
    {
    stage2LastDoor = OpenDoor.GetComponent<Stage2LastDoor>();
    }


    private void OnTriggerExit(Collider ohter)
    {
        if (ohter.CompareTag("Player"))
        {
            print("ºÎµúÈû");

            stage2LastDoor.enabled = true;

        }
    }

}
