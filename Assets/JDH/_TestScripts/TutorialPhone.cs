using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialPhone : MonoBehaviour
{
    public void GrabPhone()
    {
        //�տ� ����� ��� =>�ν� �ȵ�.
        FindObjectOfType<TutorialManager>().isPhoneGrip = true;
        Debug.Log("�ڵ����� ��ҽ��ϴ�.");
    }
    public void CancelGrabPhone()
    {
        //�տ� ����� ��� =>�ν� �ȵ�.
        FindObjectOfType<TutorialManager>().isPhoneGrip = false;
        Debug.Log("�ڵ����� ���ҽ��ϴ�.");
    }
    
}
