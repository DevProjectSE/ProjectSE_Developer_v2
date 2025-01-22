using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Robot : MonoBehaviour
{
    public Stage3ClassDoor stage3ClassDoor;

    private void Start()
    {
        stage3ClassDoor = GetComponent<Stage3ClassDoor>();
    }
    public void EnabledDoor()
    {
        stage3ClassDoor.enabled = true;
    }
}
