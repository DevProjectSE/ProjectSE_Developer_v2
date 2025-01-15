using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class FlashLight : MonoBehaviour
{
    public GameObject flashLight;
    private ActionBasedController leftActivateAction;
    private ActionBasedController rightActivateAction;

    private int lightState = 0;

    private Light lightComponent; //사용하는 빛
    public Color basicColor = Color.white;
    public Color UVColor = Color.blue;

    public GameObject targetObject; //보이게할 대상
    public float rayDistance = 50f; //거리

    private void Awake()
    {
        leftActivateAction = GetComponent<ActionBasedController>();
        rightActivateAction = GetComponent<ActionBasedController>();
    }

    private void Start()
    {
        lightComponent = flashLight.GetComponent<Light>();
        flashLight.SetActive(false);
    }

    private void Update()
    {

        Ray ray = new Ray(flashLight.transform.position, flashLight.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // 광선이 오브젝트에 닿았을 경우
            if (lightState == 2 && hit.collider.gameObject == targetObject)
            {
                // 오브젝트를 보이게 한다
                targetObject.GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            // 광선이 오브젝트에 닿지 않으면 오브젝트를 숨긴다
            targetObject.GetComponent<Renderer>().enabled = false;
        }

        if (lightState != 2)
        {
            targetObject.GetComponent<Renderer>().enabled = false;
        }

    }

    private void OnEnable()
    {
        if (leftActivateAction != null)
        {
            leftActivateAction.selectAction.reference.action.performed += GetOnLight;

        }

        if (rightActivateAction != null)
        {
            rightActivateAction.selectAction.reference.action.performed += GetOnLight;

        }
    }

    private void OnDisable()
    {
        if (leftActivateAction != null)
        {
            leftActivateAction.selectAction.reference.action.performed -= GetOnLight;
        }
        if (rightActivateAction != null)
        {
            rightActivateAction.selectAction.reference.action.performed -= GetOnLight;
        }
    }
    private void GetOnLight(InputAction.CallbackContext context)
    {
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
}