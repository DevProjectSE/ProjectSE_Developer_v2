using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Yejins_Phone : MonoBehaviour
{

    private XRInputModalityManager modalityManager;

    public void OnKeySelected()
    {   //씬전환을 고려해서 인스펙터상에서 컴포넌트를 찾아옴.
        modalityManager = FindAnyObjectByType<XRInputModalityManager>();
        StartCoroutine(Phone());
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
}
