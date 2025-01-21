using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Yejins_Phone : MonoBehaviour
{

    private XRInputModalityManager modalityManager;
    private bool isActivated = false;
    private CustomPlayerController CPC;

    //TODO 커스텀 입력 매니저를 만들어서 가져오도록 후처리
    [SerializeField]
    private InputActionReference l_Ref_Primary;
    [SerializeField]
    private InputActionReference r_Ref_Primary;

    [SerializeField]
    private InputActionReference l_Ref_Grip;
    [SerializeField]
    private InputActionReference r_Ref_Grip;

    public bool takeOnPhone = false;
    public Texture2D defaultTexture;
    public Texture2D changeTexture;
    public Material phoneScreen_Mat;

    private void Awake()
    {
        //TODO 게임매니저에서 가져오게 후처리 
        CPC = FindAnyObjectByType<CustomPlayerController>();
        phoneScreen_Mat.SetTexture("_BaseMap", defaultTexture);
    }

    private void OnUseAction(InputAction.CallbackContext context)
    {
        if (takeOnPhone == false)
        {
            takeOnPhone = true;
            StartCoroutine(TakeOnPhone());
        }
    }
    private void OnGrabAction(InputAction.CallbackContext context)
    {
        CPC.CtrlActivation();
    }

    private IEnumerator TakeOnPhone()
    {
        Debug.Log("Call With Yejin_Start");
        GripActionOff();
        phoneScreen_Mat.SetTexture("_BaseMap", defaultTexture);
        yield return new WaitForSeconds(5f);
        GripActionOn();
        Debug.Log("Call With Yejin_End");
    }

    public void OnKeySelected()
    {   //씬전환을 고려해서 인스펙터상에서 컴포넌트를 찾아옴.
        if (isActivated == false)
        {
            //TODO 게임매니저에서 가져오게 후처리
            modalityManager = FindAnyObjectByType<XRInputModalityManager>();
            StartCoroutine(Phone());
        }
        isActivated = true;
    }

    public void OnPhoneSelected()
    {
        //TODO 게임매니저에서 가져오게 후처리
        if (CPC == null)
            CPC = FindAnyObjectByType<CustomPlayerController>();
        CPC.CtrlRelease();
        PrimaryKeyOn();
        GripActionOn();
        if (takeOnPhone == false)
            phoneScreen_Mat.SetTexture("_BaseMap", changeTexture);
    }

    public void OnPhoneSelectExited()
    {
        CPC.CtrlActivation();
        PrimaryKeyOff();
        GripActionOff();
        phoneScreen_Mat.SetTexture("_BaseMap", defaultTexture);
        //TODO : @겜매에서 가져오도록 후처리@
        FindAnyObjectByType<StageFifth>().openDoor_Stage5.enabled = true;
    }

    private IEnumerator Phone()
    {
        while (true)
        {
            modalityManager.leftController.
            GetComponentInChildren<XRBaseController>().SendHapticImpulse(0.7f, 2f);
            modalityManager.rightController.
            GetComponentInChildren<XRBaseController>().SendHapticImpulse(0.7f, 2f);
            yield return new WaitForSeconds(5f);
        }
    }

    private void GripActionOn()
    {
        l_Ref_Grip.action.canceled += OnGrabAction;
        r_Ref_Grip.action.canceled += OnGrabAction;
    }
    private void GripActionOff()
    {
        l_Ref_Grip.action.canceled -= OnGrabAction;
        r_Ref_Grip.action.canceled -= OnGrabAction;
    }
    private void PrimaryKeyOn()
    {
        l_Ref_Primary.action.performed += OnUseAction;
        r_Ref_Primary.action.performed += OnUseAction;
    }
    private void PrimaryKeyOff()
    {
        l_Ref_Primary.action.performed -= OnUseAction;
        r_Ref_Primary.action.performed -= OnUseAction;
    }
}
