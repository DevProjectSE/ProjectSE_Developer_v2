using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Robot : Combination
{

    // public GameObject leftArm;
    // public GameObject rearArm;
    // public Transform rearArmAttach;
    // public GameObject robotArm;

    // public Key rearKey;

    // protected override void CominationItemAdd()
    // {
    //     base.CominationItemAdd();
    // }

    protected override void Awake()
    {
        base.Awake();

        #region Before
        // lockSock_Inter.selectEntered.AddListener(OnLeftArmAdded);
        // if (SceneManager.GetActiveScene().name == "Stage5_Complete")
        // {
        //     lockSock_Inter.selectEntered.RemoveListener(OnLeftArmAdded);
        //     lockSock_Inter.selectEntered.AddListener(OnRearArmAdded);
        // }
        #endregion

    }

    #region Before
    // private void OnLeftArmAdded(SelectEnterEventArgs args)
    // {
    //     robotArm.SetActive(false);
    //     leftArm.SetActive(true);
    //     lockSock_Inter.attachTransform = rearArmAttach;
    //     lockSock_Inter.selectEntered.RemoveListener(OnLeftArmAdded);
    //     lockSock_Inter.selectEntered.AddListener(OnRearArmAdded);
    //     lockSock_Inter.keychainLock.requiredKeys.Clear();
    //     lockSock_Inter.keychainLock.requiredKeys.Add(rearKey);
    //     robotArm = null;
    // }
    // private void OnRearArmAdded(SelectEnterEventArgs args)
    // {
    //     gameObject.tag = "Robot";
    //     robotArm.SetActive(false);
    //     rearArm.SetActive(true);
    // }
    #endregion

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        #region Before
        // if (other.CompareTag("RobotLeftArm") && leftArm.activeSelf == false)
        // {
        //     robotArm = other.gameObject;
        // }
        // if (other.CompareTag("RobotRearArm") && rearArm.activeSelf == false)
        // {
        //     robotArm = other.gameObject;
        // }
        #endregion

    }

}
