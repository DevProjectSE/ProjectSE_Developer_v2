using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class TrashCan_Active : MonoBehaviour
{

    public Transform trashCan_model;
    private XRLever xRLever;

    public GameObject handle;

    private IEnumerator enumerator;
    private void Awake()
    {
        xRLever = GetComponentInChildren<XRLever>();
    }
    private void Start()
    {
        enumerator = CompleteFalled();
    }
    private IEnumerator CompleteFalled()
    {
        while (true)
        {
            Debug.Log("In Corou");

            if (trashCan_model.localEulerAngles.x >= xRLever.minAngle)
            {
                handle.GetComponent<Rigidbody>().useGravity = true;
                handle.GetComponent<Rigidbody>().isKinematic = false;
                handle.transform.parent = null;
                xRLever.enabled = false;
                StopCoroutine(enumerator);
            }
            yield return null;
        }

    }
    public void LeverDeActive()
    {
        StartCoroutine(enumerator);
    }

    public void LeverActive()
    {
        Debug.Log("Stop");
        StopCoroutine(enumerator);
    }

}

