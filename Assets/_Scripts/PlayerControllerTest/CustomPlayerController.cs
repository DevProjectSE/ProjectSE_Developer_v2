using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.Android;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using TMPro;

public class CustomPlayerController : MonoBehaviour
{
    // [SerializeField]
    // private InputActionReference activePrimaryX;
    [SerializeField]
    private InputActionReference rightControllerAction;
    private Camera cam;
    // private bool isSit;
    public Transform camOffset;
    public Transform camStartPos;
    public TextMeshProUGUI text;

    private bool isPressed;
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        // camOffset.position = camStartPos.position;
    }

    private void OnEnable()
    {
        rightControllerAction.action.performed += DownUpAction;
        rightControllerAction.action.started += DownUpAction;
        rightControllerAction.action.canceled += DownUpAction;

        // rightControllerAction.action.started += SitDown;
        // rightControllerAction.action.canceled += SitDown;
        // rightControllerAction.action.started += StandUp;
        // rightControllerAction.action.canceled += StandUp;
        // isSit = false;
        // activePrimaryX.action.performed += Primary;
    }

    private void OnDisable()
    {
        rightControllerAction.action.performed += DownUpAction;
        rightControllerAction.action.started += DownUpAction;
        rightControllerAction.action.canceled += DownUpAction;

        // rightControllerAction.action.started -= SitDown;
        // rightControllerAction.action.canceled -= SitDown;
        // rightControllerAction.action.started -= StandUp;
        // rightControllerAction.action.canceled -= StandUp;
        // activePrimaryX.action.performed -= Primary;
    }

    private void Update()
    {
        if (isPressed)
        {

        }
    }

    #region 콜백
    private void Primary(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log("하이");
    }
    private void SitDown(InputAction.CallbackContext context)
    {
        // if (isSit == true) return;
        float valuey = context.ReadValue<Vector2>().y;
        if (cam.transform.localPosition.y < -2) return;
        if (valuey <= -0.9)
        {
            camOffset.position =
            new Vector3(camOffset.position.x, camOffset.position.y - 2, camOffset.position.z);
            // isSit = true;
        }
    }

    private void StandUp(InputAction.CallbackContext context)
    {

        // if (isSit == false) return;
        float valuey = context.ReadValue<Vector2>().y;
        if (valuey >= 0.9)
        {
            camOffset.position =
            new Vector3(camOffset.position.x, camOffset.position.y + 2, camOffset.position.z);
            // isSit = false;

        }
    }

    private void DownUpAction(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        text.text = value.ToString();
    }
    #endregion
}
