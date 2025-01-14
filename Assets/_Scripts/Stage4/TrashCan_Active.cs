using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class TrashCan_Active : MonoBehaviour
{
    public StageFourth stageFourth;
    public Transform trashCan_model;
    private XRLever xRLever;
    public GameObject handle;
    public GameObject keyObject;
    private IEnumerator enumerator;
    private void Awake()
    {
        if (stageFourth == null)
        {
            stageFourth = GetComponentInParent<StageFourth>();
        }
        xRLever = GetComponentInChildren<XRLever>();
        keyObject.SetActive(false);
    }
    private void Start()
    {
        enumerator = CompleteFalled();
    }
    private IEnumerator CompleteFalled()
    {
        while (true)
        {
            if (trashCan_model.localEulerAngles.x >= xRLever.minAngle)
            {
                handle.GetComponent<Rigidbody>().useGravity = true;
                handle.GetComponent<Rigidbody>().isKinematic = false;
                handle.transform.parent = null;
                xRLever.enabled = false;
                keyObject.SetActive(true);
                keyObject.transform.parent = null;
                stageFourth.TrashCanClear();
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
        StopCoroutine(enumerator);
    }
    public void GrabActivate(bool value)
    {
        xRLever.enabled = value;
    }
}

