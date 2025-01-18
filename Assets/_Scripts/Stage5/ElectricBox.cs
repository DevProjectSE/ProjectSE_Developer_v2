using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class ElectricBox : MonoBehaviour
{
    public StageFifth stageFifth;
    public List<Material> lamps_Mat;
    public List<MeshRenderer> lamps_Mesh;
    public XRGripButton xRGripButton;
    public XRLever xRLever;
    public List<Knobs> knobs;

    private void Awake()
    {
        if (stageFifth == null)
        {
            stageFifth = GetComponentInParent<StageFifth>();
        }

    }

    public void OnLeverDeActive()
    {
        //초록on
        lamps_Mesh[0].material = lamps_Mat[0];
        //빨간off
        lamps_Mesh[1].material = lamps_Mat[1];
        foreach (Knobs knob in knobs)
        {
            knob.enabled = true;
        }
        xRGripButton.enabled = true;
    }
    public void OnLeverActive()
    {
        //초록off
        lamps_Mesh[0].material = lamps_Mat[2];
        //빨간On
        lamps_Mesh[1].material = lamps_Mat[3];
        foreach (Knobs knob in knobs)
        {
            knob.enabled = false;
        }
        xRGripButton.enabled = false;
        ResetButton();
    }

    public void KnobsActivateCheck()
    {
        foreach (Knobs knob in knobs)
        {
            if (knob.isOn == false)
            {
                return;
            }
        }
        xRGripButton.enabled = false;
        xRLever.enabled = false;
        foreach (Knobs knob in knobs)
        {
            knob.enabled = false;
        }
        stageFifth.numberKeyPad.KeyPadButtonsActivate();
    }

    public void ResetButton()
    {
        foreach (Knobs knob in knobs)
        {
            knob.ResetDetected();
        }
    }

}
