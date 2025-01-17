using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Knobs : MonoBehaviour
{
    public ElectricBox electricBox;
    public Transform offPos;
    public Transform onPos;
    public bool isOn;
    public bool isHovering { get; set; }
    public InputActionReference r_Trigger;
    public InputActionReference l_Trigger;

    private void OnEnable()
    {
        r_Trigger.action.performed += OnOff;
        l_Trigger.action.performed += OnOff;
    }
    private void OnDisable()
    {
        r_Trigger.action.performed -= OnOff;
        l_Trigger.action.performed -= OnOff;
    }

    public void OnOff(InputAction.CallbackContext context)
    {
        if (isOn && isHovering)
        {
            transform.position = new Vector3(offPos.position.x, transform.position.y, transform.position.z);
            isOn = false;
        }
        if (isOn == false && isHovering)
        {
            transform.position = new Vector3(onPos.position.x, transform.position.y, transform.position.z);
            isOn = true;
        }
        electricBox.KnobsActivateCheck();
    }

    public void ResetDetected()
    {
        transform.position = new Vector3(offPos.position.x, transform.position.y, transform.position.z);
        isOn = false;
    }

    //TODO : 게임전체 완성 후 위아래 Knob 움직이는 로직 작성
}
