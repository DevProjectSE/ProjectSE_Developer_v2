using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage3ClassDoor : OpenDoor
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneLoadManager.Instance.StageLoad(StageNumber.Stage4);
        }
    }
}


