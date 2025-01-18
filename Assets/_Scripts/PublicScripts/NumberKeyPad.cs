using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class NumberKeyPad : MonoBehaviour
{
    [Tooltip("숫자만입력")]
    public string passWord;
    public TextMeshProUGUI text;
    public int num { get; set; }
    private bool isEnterPressed;
    public List<XRGripButton> keyPadButtons;
    public bool isUnlocked = false;

    public void KeyPadButtonsActivate()
    {
        foreach (XRGripButton xRGripButton in keyPadButtons)
        {
            xRGripButton.enabled = true;
        }
    }

    public void KeyPadInsert()
    {
        if (isEnterPressed)
        {
            text.text = "";
            text.color = new Color(255, 255, 255);
            isEnterPressed = false;
        }
        text.text += num.ToString();
    }

    public void OnEnterPress()
    {
        if (passWord == text.text)
        {
            text.color = new Color(0, 255, 0);
            text.text = "UNLOCKED";
            foreach (XRGripButton xRGripButton in keyPadButtons)
            {
                xRGripButton.enabled = false;
            }
            isUnlocked = true;
        }
        else
        {
            text.color = new Color(255, 0, 0);
            text.text = "ERROR";
        }
        isEnterPressed = true;
    }
}
