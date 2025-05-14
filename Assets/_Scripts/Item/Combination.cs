using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Combination : MonoBehaviour
{
    [SerializeField]
    protected InputActionReference decompositAction;
    [SerializeField]
    protected LayerMask itemLayer;
    [SerializeField]
    protected LayerMask mountedLayer;
    [SerializeField]
    protected InteractionLayerMask xRSeparateLayer;
    [SerializeField]
    protected InteractionLayerMask xRMountedLayer;
    protected XRLockSocketInteractor[] lockSocket_Inters;

    // 장착된 부착물의 정보를 보관할 컨테이너
    protected Dictionary<XRGrabInteractable, InteractionLayerMask> combinaionDict = new Dictionary<XRGrabInteractable, InteractionLayerMask>();

    protected virtual void Awake()
    {
        lockSocket_Inters = GetComponentsInChildren<XRLockSocketInteractor>();
        decompositAction.action.performed += ctx => SeparationActivate();
        decompositAction.action.canceled += ctx => SeparationDeactivate();
    }
    protected virtual void Start()
    {
        SocketLitenerAdd();
    }
    // XRLockSocketInteractor에 이벤트 리스너 추가
    protected virtual void SocketLitenerAdd()
    {
        foreach (XRLockSocketInteractor interactor in lockSocket_Inters)
        {
            interactor.selectEntered.AddListener(OnCombination);
            interactor.selectExited.AddListener(OnSeparation);
        }
    }

    protected virtual void OnCombination(SelectEnterEventArgs eventArgs)
    {
        #region Get eventArgs Objects Component
        XRGrabInteractable interactableGrab = eventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>();
        GameObject interactableObj = eventArgs.interactorObject.transform.gameObject;
        Rigidbody interactableRb = eventArgs.interactableObject.transform.GetComponent<Rigidbody>();
        #endregion
        // 장착된 오브젝트의 현재 레이어 변경
        interactableObj.layer = LayerMask.NameToLayer("Mounted");
        // 물리적 상호작용 예외를 위한 레이어 추가
        interactableRb.excludeLayers += itemLayer;
        InteractionLayerMask mask = interactableGrab.interactionLayers;
        // 장착된 부착물의 기존 정보를 저장
        if (combinaionDict.TryAdd(interactableGrab, mask))
        {
            Debug.Log("Dictionary Added");
        }
        else
        {
            Debug.LogError("Key Already Exist");
        }
        // 장착된 부착물의 상호작용 가능 레이어 변경
        interactableGrab.interactionLayers = xRMountedLayer;
    }

    // 해체된 부착물의 정보를 삭제
    protected virtual void OnSeparation(SelectExitEventArgs eventArgs)
    {
        #region Get eventArgs Objects Component
        XRGrabInteractable interactableGrab = eventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>();
        Rigidbody interactableRb = eventArgs.interactableObject.transform.GetComponent<Rigidbody>();
        GameObject interactableObj = eventArgs.interactorObject.transform.gameObject;
        #endregion
        // 해체된 오브젝트의 현재 레이어 변경
        interactableObj.layer = LayerMask.NameToLayer("Item");
        // 물리적 상호작용 예외를 위한 레이어 제거
        interactableRb.excludeLayers -= itemLayer;
        // 해체된 부착물의 상호작용 가능 레이어를 원래 레이어로 변경
        interactableGrab.interactionLayers = combinaionDict[interactableGrab];
        // 해체된 부착물의 정보를 삭제
        combinaionDict.Remove(interactableGrab);
    }

    // 해체 활성화 시 해체 가능한 부착물을 레이어 변경으로 상호작용 활성화
    protected virtual void SeparationActivate()
    {
        foreach (XRGrabInteractable grabInter in combinaionDict.Keys)
        {
            grabInter.interactionLayers = xRSeparateLayer;
        }
    }
    // 해체 비활성화 시 해체 가능한 부착물을 레이어 변경으로 상호작용 비활성화
    protected virtual void SeparationDeactivate()
    {
        foreach (XRGrabInteractable grabInter in combinaionDict.Keys)
        {
            grabInter.interactionLayers = xRMountedLayer;
        }
    }
}