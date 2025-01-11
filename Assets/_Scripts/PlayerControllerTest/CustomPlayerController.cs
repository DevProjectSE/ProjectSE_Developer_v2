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

public class CustomPlayerController : MonoBehaviour
{
    // [SerializeField]
    // private InputActionReference activePrimaryX;
    [SerializeField]
    private InputActionReference rightControllerAction;
    private Camera cam;

    private bool isSit = false;

    public Transform camOffset;
    public Transform standPos;
    public Transform sitPos;
    // public Transform mainCamPos;

    [Tooltip("플레이어 중심부터 카메라가 나아갈 수 있는 최대 거리")]
    public float maxDistance;
    [Tooltip("Down Up Value == 최대 높이 - 최소 높이")]
    public float downUpValue;
    [Tooltip("카메라가 지정 범위를 벗어났을 시 컨트롤러 제어가능")]
    public bool ctrlerControl;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();

    }

    private void OnEnable()
    {
        rightControllerAction.action.performed += SitDown;
        rightControllerAction.action.performed += StandUp;
        // activePrimaryX.action.performed += Primary;
    }

    private void OnDisable()
    {
        rightControllerAction.action.performed -= SitDown;
        rightControllerAction.action.performed -= StandUp;
        // activePrimaryX.action.performed -= Primary;
    }

    #region 콜백
    private void Primary(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log("하이");
    }
    private void SitDown(InputAction.CallbackContext context)
    {
        float valuey = context.ReadValue<Vector2>().y;
        if (cam.transform.localPosition.y < -2) return;
        if (valuey < 0 && isSit == false)
        {
            camOffset.position =
            new Vector3(camOffset.position.x, camOffset.position.y - 2, camOffset.position.z);
            isSit = true;
        }
    }

    private void StandUp(InputAction.CallbackContext context)
    {
        float valuey = context.ReadValue<Vector2>().y;
        if (valuey > 0 && isSit)
        {
            camOffset.position =
            new Vector3(camOffset.position.x, camOffset.position.y + 2, camOffset.position.z);
            isSit = false;
        }
    }
    #endregion

    #region 미사용
    private void Update()
    {
        // CtrlerControl();
    }

    //충동체 중심으로 카메라가 가면 안되도록.
    // private void CtrlerControl()
    // {   //다른 스크립트에서 범위제한 해제 == ctrlerControl = true
    //     if (ctrlerControl == false) return;

    //     //충돌체 중심부터 카메라의 거리까지 계산
    //     Vector3 ccTrans = char_Ctrler.transform.position + new Vector3(0, 3, 0);
    //     Vector3 camTrans = mainCamPos.position;
    //     float distance = Vector3.Distance(camTrans, ccTrans);

    //     if (distance > maxDistance)
    //     {
    //         locoManager.gameObject.SetActive(false);
    //         xRInModalManager.leftController.SetActive(false);
    //         xRInModalManager.rightController.SetActive(false);
    //     }
    //     if (distance < maxDistance)
    //     {
    //         locoManager.gameObject.SetActive(true);
    //         xRInModalManager.leftController.SetActive(true);
    //         xRInModalManager.rightController.SetActive(true);
    //     }
    // }
    #endregion
}
