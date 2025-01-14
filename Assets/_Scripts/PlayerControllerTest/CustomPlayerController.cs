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
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Unity.VisualScripting;
using EPOOutline;
using System;

public class CustomPlayerController : MonoBehaviour
{
    // [SerializeField]
    // private InputActionReference activePrimaryX;
    [SerializeField]
    private InputActionReference rightControllerAction;
    [SerializeField]
    private Transform l_Controller;
    [SerializeField]
    private Transform r_Controller;
    private SphereCollider l_Coll;
    private SphereCollider r_Coll;
    private Camera cam;
    private bool isSit = false;

    // private bool isSit;
    public float detectRange;
    public Transform camOffset;
    public Transform camStartPos;
    public TextMeshProUGUI text;
    public LayerMask targetLayer;
    private void Awake()
    {
        l_Coll = l_Controller.GetComponentInChildren<SphereCollider>();
        r_Coll = r_Controller.GetComponentInChildren<SphereCollider>();
        l_Coll.radius = detectRange / 6;
        r_Coll.radius = detectRange / 6;
        cam = GetComponentInChildren<Camera>();
        // XRinputModalityManager = GetComponent<XRInputModalityManager>();
        // r_interactionGroup = XRinputModalityManager.rightController.GetComponent<XRInteractionGroup>();
        // l_interactionGroup = XRinputModalityManager.leftController.GetComponent<XRInteractionGroup>();
        // r_xRDirectInteractor = r_interactionGroup.startingGroupMembers[1].GetComponent<XRDirectInteractor>();
        // l_xRDirectInteractor = l_interactionGroup.startingGroupMembers[1].GetComponent<XRDirectInteractor>();
        // l_xRDirectInteractor.hoverEntered.AddListener(L_FindClosest);
    }

    private void OnEnable()
    {
        rightControllerAction.action.performed += DownUpAction;

        // activePrimaryX.action.performed += Primary;
    }

    private void OnDisable()
    {
        rightControllerAction.action.performed -= DownUpAction;
        // activePrimaryX.action.performed -= Primary;
    }

    private void Update()
    {
        FindClosest(l_Controller);
        FindClosest(r_Controller);
    }

    private void FindClosest(Transform pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos.position, detectRange, targetLayer);
        if (colliders.Length == 0) return;
        GameObject target = null;
        float distance = 0;
        foreach (Collider c in colliders)
        {
            if (target == null)
            {
                target = c.gameObject;
                distance = Vector3.Distance(pos.position, target.transform.position);
            }
            else
            {
                GameObject tempTarget = c.gameObject;
                float tempDistance = Vector3.Distance(pos.position, tempTarget.transform.position);
                if (distance > tempDistance)
                {
                    target = tempTarget;
                }
            }
        }
        if (target != null)
        {
            foreach (Collider c in colliders)
            {
                if (target != c.gameObject)
                {
                    c.gameObject.GetComponent<Outlinable>().enabled = false;
                }
            }
            target.GetComponent<Outlinable>().enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(r_Controller.position, detectRange);
        Gizmos.DrawWireSphere(l_Controller.position, detectRange);
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
        if (valuey <= -0.35f)
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
        if (valuey >= 0.35f)
        {
            camOffset.position =
            new Vector3(camOffset.position.x, camOffset.position.y + 2, camOffset.position.z);
            // isSit = false;

        }
    }

    private void DownUpAction(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<Vector2>().y;
        text.text = value.ToString();
        if (value >= 0.35f && isSit == true)
        {
            camOffset.position =
            new Vector3(camOffset.position.x, camOffset.position.y + 2, camOffset.position.z);
            isSit = false;
        }
        if (value <= -0.35f && isSit == false)
        {
            camOffset.position =
            new Vector3(camOffset.position.x, camOffset.position.y - 2, camOffset.position.z);
            isSit = true;
        }
    }
    #endregion

}
