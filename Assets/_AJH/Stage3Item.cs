using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Stage3Item : MonoBehaviour
{
    public GameObject Knife;
    public GameObject Diary;
    public GameObject book;
    public GameObject backpack;
    public GameObject homework;

    public GameObject flashLight;

    public GameObject[] rope;

    public bool isKnife;
    public bool isDiary;
    public bool isBook;
    public bool isBackpack;
    public bool isHomework;

    public InputActionReference rightJoystick;
    public InputActionReference leftJoystick;
    public InputActionReference leftGripButton;
    public InputActionReference rightGripButton;
    public InputActionReference rightPrimaryButton;

    public XRBaseController leftXRController;
    public XRBaseController rightXRController;

}
