using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySocketDestroyer : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
