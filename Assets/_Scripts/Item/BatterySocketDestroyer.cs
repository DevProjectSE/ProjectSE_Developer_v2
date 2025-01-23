using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BatterySocketDestroyer : MonoBehaviour
{
    public BoxCollider soketColl;
    public void Destroy(SelectEnterEventArgs args)
    {
        Destroy(gameObject);
    }
    public void DestroyMySelf()
    {
        soketColl.enabled = true;
        Destroy(gameObject);
    }
}
