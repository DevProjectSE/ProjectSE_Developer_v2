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
using System.Runtime.InteropServices;
using System.Reflection;
using UnityEngine.SceneManagement;

public class CustomPlayerController : MonoBehaviour
{
    // [SerializeField]
    // private InputActionReference activePrimaryX;
    [SerializeField]
    private InputActionReference r_CtrlAction;
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
    public LayerMask targetLayer;

    public LocomotionSystem locomotionSystem;

    public GameObject tunnelingVignette;

    private void Awake()
    {
        if (l_Coll == null) l_Coll = l_Controller.GetComponentInChildren<SphereCollider>();
        if (r_Coll == null) r_Coll = r_Controller.GetComponentInChildren<SphereCollider>();
        if (locomotionSystem == null) locomotionSystem = GetComponentInChildren<LocomotionSystem>();
        l_Coll.radius = detectRange / 6;
        r_Coll.radius = detectRange / 6;
        if (cam == null) cam = GetComponentInChildren<Camera>();
        if (SceneManager.GetActiveScene().name == "Stage2_Complete")
        {
            transform.localPosition = new Vector3(-6.325f, 0.515f, 8.353f);
            Debug.Log("a");

        }
        if (SceneManager.GetActiveScene().name == "Stage3_Complete")
        {
            Debug.Log("B");
            transform.localPosition = new Vector3(-19.25f, 0.13f, -0.23f);
        }
        if (SceneManager.GetActiveScene().name == "Stage4_Complete")
        {
            Debug.Log("c");
            transform.localPosition = new Vector3(-16.095f, -0.133f, 6.315f);
        }
        if (SceneManager.GetActiveScene().name == "Stage5_Complete")
        {
            Debug.Log("D");
            transform.localPosition = new Vector3(11.368f, 0.5f, 1.306f);
        }
        if (SceneManager.GetActiveScene().name == "Stage6_Happy_Complete")
        {
            Debug.Log("E");
            transform.localPosition = new Vector3(0.577f, 1.713f, 2.572f);
        }
    }

    private void OnEnable()
    {
        r_CtrlAction.action.performed += DownUpAction;

        // activePrimaryX.action.performed += Primary;
    }

    private void OnDisable()
    {
        r_CtrlAction.action.performed -= DownUpAction;
        // activePrimaryX.action.performed -= Primary;
    }

    private void Update()
    {
        // FindClosest(l_Controller);
        // FindClosest(r_Controller);
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
    private void DownUpAction(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<Vector2>().y;
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

    public void CtrlActivation()
    {
        locomotionSystem.enabled = true;
        l_Controller.GetComponent<XRBaseController>().enabled = true;
        r_Controller.GetComponent<XRBaseController>().enabled = true;
        tunnelingVignette.SetActive(true);
        r_CtrlAction.action.performed += DownUpAction;
    }
    public void CtrlRelease()
    {
        locomotionSystem.enabled = false;
        l_Controller.GetComponent<XRBaseController>().enabled = false;
        r_Controller.GetComponent<XRBaseController>().enabled = false;
        tunnelingVignette.SetActive(false);
        r_CtrlAction.action.performed -= DownUpAction;
    }

    public void UIOpen()
    {
        locomotionSystem.enabled = false;
        tunnelingVignette.SetActive(false);
        Debug.Log($"구독해제전 {r_CtrlAction.action}");
        r_CtrlAction.action.performed -= DownUpAction;
        Debug.Log($"구독해제후 {r_CtrlAction.action}");
    }
    public void UIClose()
    {
        locomotionSystem.enabled = true;
        tunnelingVignette.SetActive(true);
        r_CtrlAction.action.performed += DownUpAction;
    }
}
