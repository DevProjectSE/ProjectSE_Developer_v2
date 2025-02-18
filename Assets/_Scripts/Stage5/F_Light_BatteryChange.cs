using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class F_Light_BatteryChange : MonoBehaviour
{
    XRLockSocketInteractor xRLockSocketInteractor;
    private int addEventCount = 0;
    private void Awake()
    {
        xRLockSocketInteractor = GetComponent<XRLockSocketInteractor>();
        if (GetComponent<FlashLight>().targetObject[0] == null)
            GetComponent<FlashLight>().targetObject[0] = FindAnyObjectByType<InvisibleText>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewBattery") && addEventCount == 0)
        {
            if (other.TryGetComponent<BatterySocketDestroyer>(out BatterySocketDestroyer newBattery))
            {
                xRLockSocketInteractor.selectEntered.AddListener(newBattery.Destroy);
                addEventCount++;
            }
        }
    }

    private void Start()
    {

    }
}
