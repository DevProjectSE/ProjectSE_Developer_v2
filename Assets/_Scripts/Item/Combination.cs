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
    protected XRLockSocketInteractor[] lockSocket_Inters;

    // 장착된 부착물의 정보를 보관할 컨테이너
    protected Dictionary<int, (XRGrabInteractable, InteractionLayerMask)> combinaionDict = new Dictionary<int, (XRGrabInteractable, InteractionLayerMask)>();

    protected virtual void Awake()
    {
        lockSocket_Inters = GetComponentsInChildren<XRLockSocketInteractor>();
        decompositAction.action.performed += ctx => DecompositActivate();
        decompositAction.action.canceled += ctx => DecompositDeactivate();
    }

    protected virtual void Start()
    {
        SocketLitenerAdd();
    }

    protected virtual void SocketLitenerAdd()
    {
        foreach (XRLockSocketInteractor interactor in lockSocket_Inters)
        {
            interactor.selectEntered.AddListener(OnCombination);
            interactor.selectExited.AddListener(OnDecomposition);
        }
    }

    protected virtual void OnCombination(SelectEnterEventArgs eventArgs)
    {

        XRGrabInteractable grabInter = eventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>();

        eventArgs.interactableObject.transform.gameObject.layer = LayerMask.NameToLayer("Mounted");

        eventArgs.interactableObject.transform.gameObject.GetComponent<Rigidbody>().excludeLayers += LayerMask.GetMask("Item");

        InteractionLayerMask mask = grabInter.interactionLayers;

        combinaionDict.Add(eventArgs.interactorObject.transform.gameObject.GetInstanceID(),
                           (eventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>(), mask));

        grabInter.interactionLayers -= mask;

        grabInter.interactionLayers += InteractionLayerMask.GetMask("Mounted");
    }

    protected virtual void OnDecomposition(SelectExitEventArgs eventArgs)
    {
        XRGrabInteractable grabInter = eventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>();

        eventArgs.interactableObject.transform.gameObject.layer = LayerMask.NameToLayer("Mounted");

        eventArgs.interactableObject.transform.gameObject.GetComponent<Rigidbody>().excludeLayers -= LayerMask.GetMask("Item");

        grabInter.interactionLayers -= InteractionLayerMask.GetMask("Decomposit");

        grabInter.interactionLayers += combinaionDict[eventArgs.interactorObject.transform.gameObject.GetInstanceID()].Item2;

        combinaionDict.Remove(eventArgs.interactorObject.transform.gameObject.GetInstanceID());
    }

    protected virtual void DecompositActivate()
    {
        foreach (var item in combinaionDict)
        {
            XRGrabInteractable grabInter = item.Value.Item1;
            grabInter.interactionLayers -= InteractionLayerMask.GetMask("Mounted");
            grabInter.interactionLayers += InteractionLayerMask.GetMask("Decomposit");
        }
    }
    protected virtual void DecompositDeactivate()
    {
        foreach (var item in combinaionDict)
        {
            XRGrabInteractable grabInter = item.Value.Item1;
            grabInter.interactionLayers -= InteractionLayerMask.GetMask("Decomposit");
            grabInter.interactionLayers += InteractionLayerMask.GetMask("Mounted");
        }
    }
}