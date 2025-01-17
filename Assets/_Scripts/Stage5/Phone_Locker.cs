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
            if (l_Door.eulerAngles.z < 300)
                l_Door.Rotate(new Vector3(0, 0, 1) * Time.deltaTime);
            if (r_Door.eulerAngles.z < -30)
                r_Door.Rotate(new Vector3(0, 0, -1) * Time.deltaTime);

            Debug.Log("L" + l_Door.rotation.eulerAngles.z);
            Debug.Log("R" + r_Door.rotation.eulerAngles.z);
        }
    }
}
