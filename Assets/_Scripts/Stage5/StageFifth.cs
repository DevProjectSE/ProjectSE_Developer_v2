using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFifth : MonoBehaviour
{
    private bool isClockClear;

    public bool isNumberKeyPadClear;

    public NumberKeyPad numberKeyPad;

    public void ClockClear()
    {
        isClockClear = true;
    }

    public void NumberKeyPadClear()
    {
        if (numberKeyPad.isUnlocked)
        {
            isNumberKeyPadClear = true;
        }
    }
}
