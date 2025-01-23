using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using static UnityEngine.GraphicsBuffer;

public class FlashLight : MonoBehaviour
{
    public GameObject flashLight;

    [SerializeField]
    private InputActionReference leftActivateAction;
    [SerializeField]
    private InputActionReference rightActivateAction;

    //public SpriteMask spriteMask;
    [SerializeField]
    private Light lightComponent;

    public Color basicColor = Color.white;
    public Color UVColor = Color.blue;

    public float rayDistance = 50f;
    public GameObject[] targetObject;

    private int lightState = 0;
    public bool canToggleLight = false;

    //public InputActionReference leftSecondaryButton;
    //public InputActionReference rightSecondaryButton;

    //public InputActionProperty leftButtonAction;
    //public InputActionProperty rightButtonAction;

    private void Awake()
    {
        lightComponent = flashLight.GetComponent<Light>();
        flashLight.SetActive(false);
    }

    private void Start()
    {
        foreach (GameObject target in targetObject)
        {
            if (target != null)
                target.GetComponent<Renderer>().enabled = false;
        }

        leftActivateAction.action.performed += ctx => ToggleLight();
        rightActivateAction.action.performed += ctx => ToggleLight();

    }

    private void Update()
    {

        Ray ray = new Ray(flashLight.transform.position, flashLight.transform.forward);
        RaycastHit hit;
        foreach (GameObject target in targetObject)
        {
            if (target == null) continue;
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                //spriteMask.transform.position = hit.point;

                if (lightState == 2 && hit.collider.gameObject == target)
                {

                    target.GetComponent<Renderer>().enabled = true;
                }
            }
            else
            {

                target.GetComponent<Renderer>().enabled = false;
            }
        }

    }

    private void OnEnable()
    {

        if (leftActivateAction != null)
        {
            leftActivateAction.action.performed += ctx => ToggleLight();

        }

        if (rightActivateAction != null)
        {
            rightActivateAction.action.performed += ctx => ToggleLight();

        }
    }

    private void OnDisable()
    {
        if (leftActivateAction != null)
        {
            leftActivateAction.action.performed -= ctx => ToggleLight();
        }
        if (leftActivateAction != null)
        {
            rightActivateAction.action.performed -= ctx => ToggleLight();
        }
    }
    private void ToggleLight()
    {
        try
        {
            if (!canToggleLight)
            {
                return;
            }

            if (lightState == 0)
            {

                flashLight.SetActive(true);

                lightComponent.color = basicColor;
                lightState = 1;
            }
            else if (lightState == 1)
            {
                lightComponent.color = UVColor;
                lightState = 2;
            }
            else if (lightState == 2)
            {
                flashLight.SetActive(false);
                lightState = 0;
            }

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

    }
    public void OnSelectEntered()
    {
        canToggleLight = true;
    }

    public void OnSelectExited()
    {
        canToggleLight = false;
    }

}