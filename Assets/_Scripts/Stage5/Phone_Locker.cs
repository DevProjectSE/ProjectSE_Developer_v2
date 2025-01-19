using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_Locker : MonoBehaviour
{
    public Transform l_Door;
    public Transform r_Door;
    private void OnTriggerStay(Collider other)
    {
        //쓰레기코드
        if (other.gameObject.CompareTag("Robot"))
        {
            if (l_Door.eulerAngles.y < 29 || l_Door.eulerAngles.y > 31)
                l_Door.Rotate(new Vector3(0, 0, 2f) * Time.deltaTime);
            if (r_Door.eulerAngles.y < 59 || r_Door.eulerAngles.y > 61)
                r_Door.Rotate(new Vector3(0, 0, -2f) * Time.deltaTime);
        }
    }
}
