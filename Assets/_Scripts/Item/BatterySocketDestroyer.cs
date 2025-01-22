using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySocketDestroyer : MonoBehaviour
{
    public BoxCollider soketColl;
    public void Destroy()
    {
        soketColl.enabled = true;
        Destroy(gameObject);
    }
}
