using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StageFifth : MonoBehaviour
{
    private bool isClockClear;

    public bool isNumberKeyPadClear;

    public NumberKeyPad numberKeyPad;
    public Material teacher_Mat;
    public Material robotArm_Mat;
    public GameObject robotArm;
    private void Start()
    {
        robotArm_Mat.SetFloat("_Dissolve", 1);
        teacher_Mat.SetFloat("_Dissolve", 1);
    }

    public void ClockClear()
    {
        isClockClear = true;

        StartCoroutine(DissolveCoroutine());
    }

    private IEnumerator DissolveCoroutine()
    {
        while (true)
        {
            float a = teacher_Mat.GetFloat("_Dissolve");
            float b = robotArm_Mat.GetFloat("_Dissolve");
            if (a < 0 && b < 0)
            {
                robotArm.GetComponent<XRGrabInteractable>().enabled = true;
                StopAllCoroutines();
            }
            if (a != 0)
                teacher_Mat.SetFloat("_Dissolve", a - 0.002f);
            if (b != 0)
                robotArm_Mat.SetFloat("_Dissolve", b - 0.002f);
            yield return null;
            Debug.Log($"a : {a}");
            Debug.Log($"b : {b}");
        }
    }

    public void NumberKeyPadClear()
    {
        if (numberKeyPad.isUnlocked)
        {
            isNumberKeyPadClear = true;
        }
    }
}
