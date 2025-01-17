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

    public SpriteMask spriteMask;
    private Light lightComponent; //����ϴ� ��

    public Color basicColor = Color.white;
    public Color UVColor = Color.blue;
    

    public float rayDistance = 50f; //�Ÿ�
    public GameObject targetObject; //���̰��� ���

    private int lightState = 0;
    public bool canToggleLight = false;

    public InputActionProperty leftButtonAction;
    public InputActionProperty rightButtonAction;

    private void Awake()
    {
        lightComponent = flashLight.GetComponent<Light>();
        flashLight.SetActive(false);
        
    }

    private void Start()
    {

        leftActivateAction.selectAction.reference.action.performed += ctx =>ToggleLight();
        rightActivateAction.selectAction.reference.action.performed += ctx => ToggleLight();

    }

    private void Update()
    {

        Ray ray = new Ray(flashLight.transform.position, flashLight.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            spriteMask.transform.position = hit.point;
            // ������ ������Ʈ�� ����� ���
            if (lightState == 2 && hit.collider.gameObject == targetObject)
            {
                // ������Ʈ�� ���̰� �Ѵ�
                targetObject.GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            // ������ ������Ʈ�� ���� ������ ������Ʈ�� �����
            targetObject.GetComponent<Renderer>().enabled = false;
        }


    }

    private void OnEnable()
    {
        if (leftActivateAction != null)
        {
            leftActivateAction.selectAction.reference.action.performed += ctx => ToggleLight();

        }

        if (rightActivateAction != null)
        {
            rightActivateAction.selectAction.reference.action.performed += ctx=>  ToggleLight();

        }
    }

    private void OnDisable()
    {
        if (leftActivateAction != null)
        {
            leftActivateAction.selectAction.reference.action.performed -= ctx => ToggleLight();
        }
        if (leftActivateAction != null)
        {
            rightActivateAction.selectAction.reference.action.performed -= ctx => ToggleLight();
        }
    }
    public void ToggleLight()
    {
        if(!canToggleLight)
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