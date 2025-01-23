using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2LastDoor : OpenDoor
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoadManager.Instance.StageLoad(StageNumber.Stage3);
        }
    }

}
