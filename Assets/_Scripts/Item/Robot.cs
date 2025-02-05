using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Robot : MonoBehaviour
{
    public XRLockSocketInteractor xRLockSocketInteractor;
    public GameObject leftArm;
    public GameObject rearArm;
    public Transform rearArmAttach;
    public GameObject robotArm;

    public Key rearKey;

    private void Awake()
    {
        xRLockSocketInteractor.selectEntered.AddListener(OnLeftArmAdded);
        if (SceneManager.GetActiveScene().name == "Stage5_Complete")
        {
            xRLockSocketInteractor.selectEntered.RemoveListener(OnLeftArmAdded);
            xRLockSocketInteractor.selectEntered.AddListener(OnRearArmAdded);
        }
    }
    private void OnLeftArmAdded(SelectEnterEventArgs args)
    {
        robotArm.SetActive(false);
        leftArm.SetActive(true);
        xRLockSocketInteractor.attachTransform = rearArmAttach;
        xRLockSocketInteractor.selectEntered.RemoveListener(OnLeftArmAdded);
        xRLockSocketInteractor.selectEntered.AddListener(OnRearArmAdded);
        xRLockSocketInteractor.keychainLock.requiredKeys.Clear();
        xRLockSocketInteractor.keychainLock.requiredKeys.Add(rearKey);
        robotArm = null;
    }
    private void OnRearArmAdded(SelectEnterEventArgs args)
    {
        gameObject.tag = "Robot";
        robotArm.SetActive(false);
        rearArm.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RobotLeftArm") && leftArm.activeSelf == false)
        {
            robotArm = other.gameObject;
        }
        if (other.CompareTag("RobotRearArm") && rearArm.activeSelf == false)
        {
            robotArm = other.gameObject;
        }
    }

}
