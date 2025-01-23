using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLeftArm : MonoBehaviour
{

    private void OnDisable()
    {
        FindAnyObjectByType<StageFourth>().openDoor.enabled = true;
    }
}
