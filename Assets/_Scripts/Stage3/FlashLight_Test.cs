using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class FlashLight_Test : MonoBehaviour
{
    public GameObject flashLight;
    [SerializeField]
    private InputActionReference leftActivateAction;
    [SerializeField]
    private InputActionReference rightActivateAction;

    public SpriteMask spriteMask;
    private Light lightComponent; //사용하는 빛

    public Color basicColor = Color.white;
    public Color UVColor = Color.blue;

    public float rayDistance = 50f; //거리
    public GameObject targetObject; //보이게할 대상

    private int lightState = 0;
    public bool canToggleLight = false;

    private void Awake()
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

            spriteMask.transform.position = hit.point;
            // 광선이 오브젝트에 닿았을 경우
            if (lightState == 2 && hit.collider.gameObject == targetObject)
            {
                if (targetObject != null)
                    // 오브젝트를 보이게 한다
                    targetObject.GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            if (targetObject != null)
                // 광선이 오브젝트에 닿지 않으면 오브젝트를 숨긴다
                targetObject.GetComponent<Renderer>().enabled = false;
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

    public void ToggleLight()
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

    public void OnSelectEntered()
    {
        canToggleLight = true;
    }

    public void OnSelectExited()
    {
        canToggleLight = false;
    }

}