using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLeftArm : MonoBehaviour
{

    private void OnDisable()
    {
        GetComponentInParent<StageFourth>().openDoor.enabled = true;
    }
}
